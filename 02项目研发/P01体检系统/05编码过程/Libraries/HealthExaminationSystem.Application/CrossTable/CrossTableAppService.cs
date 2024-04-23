using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable
{
    [AbpAuthorize]
    public class CrossTableAppService : MyProjectAppServiceBase, ICrossTableAppService
    {
        private readonly IRepository<TjlCustomer, Guid> _jlCustomer; //用户表
        private readonly IRepository<TjlCustomerReg, Guid> _jlCustomerReg; //预约表
        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository;
        private readonly IRepository<TjlCustomerRegItem, Guid> _customerRegItemRepository;
        private readonly IRepository<TjlCusGiveUp, Guid> _cusGiveUpRepository;
        private readonly IRepository<TjlCustomerBloodNum, Guid> _customerBloodNumRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TjlMcusPayMoney, Guid> _mcusPayMoneyRepository;
        private readonly IRepository<TbmBasicDictionary, Guid> _basicDictionaryRepository;//字典表
        public CrossTableAppService(IRepository<TjlCustomer, Guid> jlCustomer,
            IRepository<TjlCustomerReg, Guid> jlCustomerReg, IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
            IRepository<TjlCustomerRegItem, Guid> customerRegItemRepository,
            IRepository<TjlCusGiveUp, Guid> cusGiveUpRepository,
            IRepository<TjlCustomerBloodNum, Guid> customerBloodNumRepository,
            IRepository<User, long> userRepository,
            IRepository<TjlMcusPayMoney, Guid> mcusPayMoneyRepository,
            IRepository<TbmBasicDictionary, Guid> basicDictionaryRepository)
        {
            _jlCustomer = jlCustomer;
            _jlCustomerReg = jlCustomerReg;
            _customerItemGroupRepository = customerItemGroupRepository;
            _customerRegItemRepository = customerRegItemRepository;
            _cusGiveUpRepository = cusGiveUpRepository;
            _customerBloodNumRepository = customerBloodNumRepository;
            _userRepository = userRepository;
            _mcusPayMoneyRepository = mcusPayMoneyRepository;
            _basicDictionaryRepository = basicDictionaryRepository;
        }
        /// <summary>
        /// 取消抽血
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerBloodNumDto CancelDrawBlood(CustomerRegForCrossTableDto input)
        {
            var customerRegEntity = _jlCustomerReg.Get(input.Id);
            customerRegEntity.DrawCard = null;
            customerRegEntity.DrawTime = null;
            _jlCustomerReg.Update(customerRegEntity);
            var bloodEntity = _customerBloodNumRepository.FirstOrDefault(b => b.CustomerRegId == input.Id);
            if (bloodEntity==null)
                throw new FieldVerifyException("未找到抽血记录！", "未找到抽血记录！");
            bloodEntity.BloodSate = input.BloodState;
            var result= _customerBloodNumRepository.Update(bloodEntity);
            var drawBloodDepartmentId = _basicDictionaryRepository.GetAll().Where(d => d.Type == BasicDictionaryType.DrawBloodDepartmentID.ToString());
            if (drawBloodDepartmentId != null)
            {
                var drawBlood = drawBloodDepartmentId.FirstOrDefault();
                if (drawBlood != null)
                {
                    var itemGroup = _customerItemGroupRepository.GetAll().Where(i => i.CustomerRegBMId == input.Id && i.DepartmentCodeBM == drawBlood.Value.ToString());
                    foreach (var group in itemGroup)
                    {
                        group.CheckState = (int)ProjectIState.Not;
                        _customerItemGroupRepository.Update(group);
                        var items = _customerRegItemRepository.GetAll().Where(i => i.CustomerItemGroupBMid == group.Id);
                        foreach (var item in items)
                        {
                            item.ProcessState = (int)ProjectIState.Not;
                            _customerRegItemRepository.Update(item);
                        }
                    }
                }
            }
            return result.MapTo<CustomerBloodNumDto>();
        }
        /// <summary>
        /// 编辑交表状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerRegForCrossTableDto EditCrossTableState(CustomerRegForCrossTableDto input)
        {
            var entity = _jlCustomerReg.Get(input.Id);
            entity.SendToConfirm = input.SendToConfirm;
            entity.SendToConfirmDate = DateTime.Now;
            entity.SendUserId = input.SendUserId;
            if (input.GuidancePhotoId.HasValue)
            {
                entity.GuidancePhotoId = input.GuidancePhotoId;
            }
            var result = _jlCustomerReg.Update(entity);
            return result.MapTo<CustomerRegForCrossTableDto>();
        }
        /// <summary>
        /// 编辑修改时间状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerRegForCrossTableDto EditCheckDateState(CustomerRegForCrossTableDto input)
        {
            var entity = _jlCustomerReg.Get(input.Id);
            if (entity.SummSate != (int)SummSate.Audited
                && entity.SummSate != (int)SummSate.HasInitialReview)
            {
                if (entity.ArriveCheck == 1)
                {
                    entity.ArriveCheck = 1;
                    var result = _jlCustomerReg.Update(entity);
                    return result.MapTo<CustomerRegForCrossTableDto>();
                }
                else
                {
                    entity.LoginDate = DateTime.Now;
                    entity.ArriveCheck = 1;
                    var result = _jlCustomerReg.Update(entity);
                    return result.MapTo<CustomerRegForCrossTableDto>();

                }
            }
            
            return null;
           
        }
        /// <summary>
        /// 抽血
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerBloodNumDto DrawBlood(CustomerBloodNumDto input)
        {
            var result = new CustomerBloodNumDto();
            var cusRegEntity = _jlCustomerReg.Get(input.CustomerReg.Id);
            var bloodNumList = _customerBloodNumRepository.GetAllList();
            var exist = bloodNumList.FirstOrDefault(b=>b.CustomerRegId==cusRegEntity.Id);
            //判断是否存在抽血记录
            if (exist == null)
            {
                var bloodNum = bloodNumList.Where(b => b.CreationTime.Date == DateTime.Now.Date).Max(b => b.BloodNum);
                int dayNum;//日序号
                if (string.IsNullOrWhiteSpace(bloodNum))
                    dayNum = 1;
                else
                    dayNum = Convert.ToInt16(bloodNum) + 1;
                cusRegEntity.DrawCard = dayNum;
                cusRegEntity.DrawTime = DateTime.Now;
                _jlCustomerReg.Update(cusRegEntity);

                input.Id = Guid.NewGuid();
                input.BloodNum = dayNum.ToString();
                var bloodNumEntity = input.MapTo<TjlCustomerBloodNum>();
                bloodNumEntity.CustomerReg = cusRegEntity;
                bloodNumEntity.EmployeeBM = _userRepository.Get(input.EmployeeBM.Id);
                var insertResult = _customerBloodNumRepository.Insert(bloodNumEntity);
                result=insertResult.MapTo<CustomerBloodNumDto>();
            }
            //存在修改记录状态
            else
            {
                cusRegEntity.DrawCard = Convert.ToInt16(exist.BloodNum);
                cusRegEntity.DrawTime = exist.BloodDate;
                _jlCustomerReg.Update(cusRegEntity);
                exist.BloodSate = input.BloodSate;
                var updateResult = _customerBloodNumRepository.Update(exist);
                result=updateResult.MapTo<CustomerBloodNumDto>();
            }
            var drawBloodDepartmentId=_basicDictionaryRepository.GetAll().Where(d=>d.Type==BasicDictionaryType.DrawBloodDepartmentID.ToString());
            if (drawBloodDepartmentId != null)
            {
                var drawBlood = drawBloodDepartmentId.FirstOrDefault();
                if (drawBlood!=null)
                {
                    var itemGroup = _customerItemGroupRepository.GetAll().Where(i => i.CustomerRegBMId == input.CustomerReg.Id && i.DepartmentCodeBM==drawBlood.Value.ToString());
                    foreach (var group in itemGroup)
                    {
                        group.CheckState = (int)ProjectIState.Complete;
                        _customerItemGroupRepository.Update(group);
                        var items = _customerRegItemRepository.GetAll().Where(i => i.CustomerItemGroupBMid == group.Id);
                        foreach (var item in items)
                        {
                            item.ProcessState = (int)ProjectIState.Complete;
                            _customerRegItemRepository.Update(item);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 编辑项目组状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CustomerItemGroupDto> EditItemGroupState(EditItemGroupStateInput input)
        {
            List<CustomerItemGroupDto> resultDto = new List<CustomerItemGroupDto>();
            List<Guid> cusidlis = new List<Guid>();
            foreach (var Id in input.Ids)
            {
                var entity = _customerItemGroupRepository.Get(Id);
                if (entity.CustomerRegBMId.HasValue && !cusidlis.Contains(entity.CustomerRegBMId.Value))
                {
                    cusidlis.Add(entity.CustomerRegBMId.Value);
                }
                entity.CheckState = input.CheckState;
                foreach (var item in entity.CustomerRegItem)
                {
                    var itemEntity= _customerRegItemRepository.Get(item.Id);
                    itemEntity.ProcessState=input.CheckState;
                    _customerRegItemRepository.Update(itemEntity);
                };
                var dto= _customerItemGroupRepository.Update(entity).MapTo<CustomerItemGroupDto>();
                if (input.Record != null)
                {
                    var exist= _cusGiveUpRepository.GetAll().Where(g=>g.CustomerItemGroupId==dto.Id).FirstOrDefault();
                    if (exist!=null)
                    {
                        var cusGiveUpEntity = exist;
                        cusGiveUpEntity.CustomerItemGroup = entity;
                        cusGiveUpEntity.remart = input.Record.remart;
                        cusGiveUpEntity.Customer = entity.CustomerRegBM.Customer;
                        cusGiveUpEntity.CustomerReg = entity.CustomerRegBM;
                        cusGiveUpEntity.stayDate = input.Record.stayDate;
                        cusGiveUpEntity.stayType = input.CheckState;
                        _cusGiveUpRepository.Update(cusGiveUpEntity);
                        ////设置体检人待查状态
                        //var cusreg = _jlCustomerReg.Get(entity.CustomerRegBMId.Value);
                        //cusreg.stayDate = input.Record.stayDate;
                        //cusreg.stayType = 1;
                        //_jlCustomerReg.Update(cusreg);
                    }
                    else
                    {
                        var cusGiveUpEntity = new TjlCusGiveUp();
                        cusGiveUpEntity.CustomerItemGroup = entity;
                        cusGiveUpEntity.remart = input.Record.remart;
                        cusGiveUpEntity.Customer = entity.CustomerRegBM.Customer;
                        cusGiveUpEntity.CustomerReg = entity.CustomerRegBM;
                        cusGiveUpEntity.Id = Guid.NewGuid();
                        cusGiveUpEntity.stayDate = input.Record.stayDate;
                        cusGiveUpEntity.stayType = input.CheckState;
                        _cusGiveUpRepository.Insert(cusGiveUpEntity);
                    }
                }
                resultDto.Add(dto);
            }
            //设置体检状态 杜智菁修改
            foreach (Guid cusid in cusidlis)
            {
                var reg = _jlCustomerReg.Get(cusid);         
                
                if (!_customerItemGroupRepository.GetAll().Any(o =>
                       o.CustomerRegBMId == cusid && o.CheckState != (int)ProjectIState.Complete
                        && o.CheckState != (int)ProjectIState.Part &&
                       o.CheckState != (int)ProjectIState.GiveUp &&
                       o.PayerCat != (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus))
                {
                    reg.CheckSate = (int)PhysicalEState.Process;       
                }
                else
                {
                    reg.CheckSate = (int)PhysicalEState.Complete;
                }
                _jlCustomerReg.Update(reg);
            }
            return resultDto;
        }
        public List<CusGiveUpDto> getGiveUps(EntityDto<Guid> input)
        {
          var cusGive=  _cusGiveUpRepository.GetAll().Where(o => o.CustomerRegId == input.Id);
          return cusGive.MapTo<List<CusGiveUpDto>>();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public CrossTableViewDto QueryCustomerInfo(PageInputDto<QueryInfoDto> input)
        {
            var allData = _jlCustomerReg.GetAll().AsNoTracking();
            
            allData = allData.Include(r => r.CustomerItemGroup);
            allData = allData.Include(r => r.Customer);
            allData = allData.Where(a=>a.RegisterState==2);//只要已登记的
            var result = new CrossTableViewDto();
            var resultData = new PageResultDto<CustomerRegForCrossTableViewDto>();
            if (!string.IsNullOrWhiteSpace(input.Input.CustomerRegNum))
            {
                allData = allData.Where(a => a.CustomerBM == input.Input.CustomerRegNum).OrderBy(c=>c.CustomerRegNum);
                result.SumRegister = allData.Count(a => a.RegisterState==2);
                result.SumSendToConfirm = allData.Count(a => a.SendToConfirm==2);
                resultData.CurrentPage = input.CurentPage;
                resultData.Calculate(allData.Count());
                allData = allData.Skip((resultData.CurrentPage - 1) * resultData.PageSize).Take(resultData.PageSize);
                resultData.Result = allData.MapTo<List<CustomerRegForCrossTableViewDto>>();
                result.CustomerReg = resultData;
                return result;
            }
            if (!string.IsNullOrWhiteSpace(input.Input.Name))
            {
                allData = allData.Where(a => a.Customer.Name == input.Input.Name);
            }
            if (input.Input.StartDate != null)
                allData = allData.Where(a => a.LoginDate >= input.Input.StartDate);
            if (input.Input.EndDate != null)
                allData = allData.Where(a => a.LoginDate <= input.Input.EndDate);
            if (input.Input.SendToConfrim != null)
            {
                if (input.Input.SendToConfrim == (int)SendToConfirm.No)
                {
                    allData = allData.Where(a => a.SendToConfirm == input.Input.SendToConfrim||!a.SendToConfirm.HasValue);
                } else
                    allData = allData.Where(a => a.SendToConfirm == input.Input.SendToConfrim);
            }
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                allData = allData.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            allData = allData.OrderByDescending(o=>o.SendToConfirmDate).ThenBy(o => o.LoginDate).ThenBy(o => o.CustomerRegNum);
            result.SumRegister = allData.Count(a => a.RegisterState == 2);
            result.SumSendToConfirm = allData.Count(a => a.SendToConfirm == 2);
            resultData.CurrentPage = input.CurentPage;
            resultData.Calculate(allData.Count());
            allData = allData.Skip((resultData.CurrentPage - 1) * resultData.PageSize).Take(resultData.PageSize);
            resultData.Result = allData.MapTo<List<CustomerRegForCrossTableViewDto>>();
            result.CustomerReg = resultData;
            return result;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<CustomerRegForCrossTableViewDto> QueryCustomer(QueryInfoDto input)
        {
            var allData = _jlCustomerReg.GetAll().AsNoTracking();

            allData = allData.Include(r => r.CustomerItemGroup);
            allData = allData.Include(r => r.Customer);
            allData = allData.Where(a => a.RegisterState == 2);//只要已登记的
            var result = new CrossTableViewDto();          
            if (!string.IsNullOrWhiteSpace(input.CustomerRegNum))
            {
                allData = allData.Where(a => a.CustomerBM == input.CustomerRegNum).OrderBy(c => c.CustomerRegNum);
                result.SumRegister = allData.Count(a => a.RegisterState == 2);
                result.SumSendToConfirm = allData.Count(a => a.SendToConfirm == 2);

                return allData.MapTo<List<CustomerRegForCrossTableViewDto>>();
            }
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                allData = allData.Where(a => a.Customer.Name == input.Name);
            }
            if (input.StartDate != null)
                allData = allData.Where(a => a.LoginDate >= input.StartDate);
            if (input.EndDate != null)
                allData = allData.Where(a => a.LoginDate <= input.EndDate);
            if (input.SendToConfrim != null)
            {
                if (input.SendToConfrim == (int)SendToConfirm.No)
                {
                    allData = allData.Where(a => a.SendToConfirm == input.SendToConfrim || !a.SendToConfirm.HasValue);
                }
                else
                    allData = allData.Where(a => a.SendToConfirm == input.SendToConfrim);
            }
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                allData = allData.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            allData = allData.OrderByDescending(o => o.SendToConfirmDate).ThenBy(o => o.LoginDate).ThenBy(o => o.CustomerRegNum);
            result.SumRegister = allData.Count(a => a.RegisterState == 2);
            result.SumSendToConfirm = allData.Count(a => a.SendToConfirm == 2);        
           
            return allData.MapTo<List<CustomerRegForCrossTableViewDto>>();
        }

        public CusGiveUpDto QueryGiveUpInfo(QueryGiveUpDto input)
        {
            var allData = _cusGiveUpRepository.GetAllIncluding(c=>c.CustomerItemGroup,c=>c.CustomerReg);
            var data = allData.FirstOrDefault(a=>a.CustomerRegId==input.CustomerRegId&&a.CustomerItemGroupId==input.CustomerItemGroupId);
            return data.MapTo<CusGiveUpDto>();
        }

        public List<CustomerRegItemDto> QueryCustomerItems(QuerCustomerItemsDto input)
        {
            var result = _customerRegItemRepository.GetAll().Where(c=>c.CustomerRegId==input.CustomerRegId);
            return result.MapTo<List<CustomerRegItemDto>>();
        }
        /// <summary>
        /// 设置项目加减项
        /// </summary>
        public SetItemGroupAddMinusDto SetItemGroupAddMinusState(SetItemGroupAddMinusDto input)
        {
            var result = new SetItemGroupAddMinusDto() { ItemGroupIds = new List<Guid>() ,DelItemGroupIds=new List<Guid>()};
            var customerReg = _jlCustomerReg.FirstOrDefault(o => o.Id == input.RegID);
            if (customerReg != null)
            {
                foreach (var id in input.ItemGroupIds)
                {
                    var itemgroup = _customerItemGroupRepository.FirstOrDefault(o => o.Id == id);
                    if (itemgroup != null)
                    {//减项的情况
                        if (input.SetAddMinusState == (int)AddMinusType.Minus)
                        {
                            if (customerReg.ClientTeamInfoId.HasValue)
                            {//单位情况
                                if (customerReg.ClientTeamInfo.ClientTeamRegitem.Any(o => o.TbmItemGroupId == itemgroup.ItemGroupBM_Id))
                                {//该项目为分组内项目。直接设为减项目
                                    itemgroup.IsAddMinus = input.SetAddMinusState;
                                    _customerItemGroupRepository.Update(itemgroup);
                                }
                                else
                                {//分组外项目已收费设为减项目待退费，未收费项目则为删除
                                    
                                    if(itemgroup.PayerCat==(int)PayerCatType.PersonalCharge)
                                    {
                                        itemgroup.IsAddMinus = input.SetAddMinusState;
                                        itemgroup.RefundState = (int)PayerCatType.StayRefund;
                                        _customerItemGroupRepository.Update(itemgroup);
                                    }
                                    else
                                    {
                                        foreach (var i in itemgroup.CustomerRegItem?.ToList())
                                            _customerRegItemRepository.Delete(i);
                                        _customerItemGroupRepository.Delete(itemgroup);
                                        result.DelItemGroupIds.Add(id);
                                    }
                                }
                            }
                            else
                            {//个人情况 收费则设置减项目 未收费则删除
                                if (itemgroup.PayerCat == (int)PayerCatType.PersonalCharge)
                                {
                                    itemgroup.IsAddMinus = input.SetAddMinusState;
                                    itemgroup.RefundState = (int)PayerCatType.StayRefund;
                                }
                                else
                                {
                                    foreach (var item in itemgroup.CustomerRegItem?.ToList())
                                        _customerRegItemRepository.Delete(item);
                                    _customerItemGroupRepository.Delete(itemgroup);
                                    result.DelItemGroupIds.Add(id);
                                }
                            }
                        }
                        else
                        {//恢复正常项目的情况
                            if (customerReg.ClientTeamInfoId.HasValue)
                            {//单位的在分组内是正常项目分组外是加项需要自己付费
                                itemgroup.RefundState = (int)PayerCatType.NotRefund;
                                if (customerReg.ClientTeamInfo.ClientTeamRegitem.Any(o => o.TbmItemGroupId == itemgroup.ItemGroupBM_Id))
                                {
                                    itemgroup.IsAddMinus = (int)AddMinusType.Normal;
                                    itemgroup.PayerCat = (int)PayerCatType.ClientCharge;
                                }
                                else
                                {
                                    itemgroup.IsAddMinus = (int)AddMinusType.Add;
                                }
                            }
                            else
                            {//个人的恢复为正常项目
                                itemgroup.RefundState = (int)PayerCatType.NotRefund;
                                itemgroup.IsAddMinus = (int)AddMinusType.Normal;
                            }
                            _customerItemGroupRepository.Update(itemgroup);
                        }
                        result.ItemGroupIds.Add(id);
                        //itemgroup.IsAddMinus = input.SetAddMinusState;
                        //_customerItemGroupRepository.Update(itemgroup);
                        //result.ItemGroupIds.Add(id);
                    }
                }
            }

            var itemgroups = _customerItemGroupRepository.GetAll().Where(o => o.CustomerRegBMId == input.RegID);
            var payinfo = _mcusPayMoneyRepository.FirstOrDefault(o => o.CustomerReg.Id == input.RegID);
            if (payinfo != null)
            {
                decimal personalPay = 0; //个人应收
                decimal clientMoney = 0; //团体应收
                decimal personalAdd = 0; //个人加项
                decimal personalMinusMoney = 0; //个人减项
                decimal clientAdd = 0;
                decimal clientMinusMoney = 0;
                foreach (var g in itemgroups)
                {
                    if (g.IsAddMinus == (int)AddMinusType.Normal || g.IsAddMinus == (int)AddMinusType.Add)
                    {
                        personalPay += g.GRmoney;
                        clientMoney += g.TTmoney;
                    }

                    if (g.IsAddMinus == (int)AddMinusType.Add)
                    {
                        //加项
                        personalAdd += g.GRmoney;
                        clientAdd += g.TTmoney;
                    }
                    else if (g.IsAddMinus == (int)AddMinusType.Minus)
                    {
                        //减项
                        clientMinusMoney += g.TTmoney;
                        personalMinusMoney += g.GRmoney;
                    }
                }
                if (payinfo.PersonalMoney != personalPay || payinfo.ClientMoney != clientMoney ||
                           payinfo.PersonalAddMoney != personalAdd || payinfo.ClientAddMoney != personalAdd ||
                           payinfo.ClientMinusMoney != clientMinusMoney)
                {
                    payinfo.PersonalMoney = personalPay;
                    payinfo.ClientMoney = clientMoney;
                    payinfo.PersonalAddMoney = personalAdd;
                    payinfo.PersonalMinusMoney = personalMinusMoney;
                    payinfo.ClientAddMoney = clientAdd;
                    payinfo.ClientMinusMoney = clientMinusMoney;
                    _mcusPayMoneyRepository.Update(payinfo);
                }
            }
            return result;

        }
    }
}