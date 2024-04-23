using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Crisis;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Linq.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis
{
    /// <summary>
    /// 危急值服务接口
    /// </summary>
    [AbpAuthorize]
    public class CrisisAppService : MyProjectAppServiceBase, ICrisisAppService
    {
        private readonly IRepository<TjlCustomerServiceCallBack, Guid> _customerServiceCallBackRepository;
        private readonly IRepository<TjlCrisisSet, Guid> _crisisSetRepository;
        private readonly IRepository<TjlCustomerRegItem, Guid> _regItemRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TjlCustomerReg, Guid> _tjlCustomerRegrepository;

        private readonly IRepository<TbmReviewItemSet, Guid> _ReviewItemSet;
        private readonly IRepository<TbmItemGroup, Guid> _ItemGroup;
        private readonly IRepository<TbmSummarizeAdvice, Guid> _SummarizeAdvice;
        private readonly IRepository<TjlCrisVisit, Guid> _TjlCrisVisit;
        private readonly IRepository<TjlCustomer, Guid> _TjlCustomer;
        private readonly IRepository<TjlCustomerSummarize, Guid> _RegSummer;
        private readonly IRepository<TjlCustomerItemGroup, Guid> _RegGroup;

        public CrisisAppService(IRepository<TjlCustomerServiceCallBack, Guid> customerServiceCallBackRepository,
            IRepository<TjlCrisisSet, Guid> crisisSetRepository,
            IRepository<TjlCustomerRegItem, Guid> regItemRepository,
            IRepository<User, long> userRepository,
            IRepository<TjlCustomerReg, Guid> tjlCustomerRegrepository,
             IRepository<TbmReviewItemSet, Guid> ReviewItemSet,
             IRepository<TbmItemGroup, Guid> ItemGroup,
             IRepository<TbmSummarizeAdvice, Guid> SummarizeAdvice,
            IRepository<TjlCrisVisit, Guid> TjlCrisVisit,
            IRepository<TjlCustomer, Guid> TjlCustomer,
            IRepository<TjlCustomerSummarize, Guid> RegSummer,
           IRepository<TjlCustomerItemGroup, Guid> RegGroup

            )
        {
            _crisisSetRepository = crisisSetRepository;
            _customerServiceCallBackRepository = customerServiceCallBackRepository;
            _regItemRepository = regItemRepository;
            _userRepository = userRepository;
            _tjlCustomerRegrepository = tjlCustomerRegrepository;
            _ReviewItemSet = ReviewItemSet;
            _ItemGroup = ItemGroup;
            _SummarizeAdvice = SummarizeAdvice;
            _TjlCrisVisit = TjlCrisVisit;
            _TjlCustomer = TjlCustomer;
            _RegSummer = RegSummer;
            _RegGroup = RegGroup;
        }
        /// <summary>
        /// 查询危急值
        /// </summary>
        public CrisisSetViewDto QueryCrisisInfos(SearchCrisisInfosDto input)
        {
            var result = new CrisisSetViewDto() { RegItemList = new List<CustomerRegItemDto>(), CrisisList = new List<CrisisInfo>() };
            var crisisRows = _crisisSetRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                var reg = _tjlCustomerRegrepository.FirstOrDefault(o => o.CustomerBM == input.CustomerBM);
                result.Age = reg.Customer.Age.Value;
                if (reg.ClientRegId.HasValue)
                {
                    result.ClinentId = reg.ClientRegId;
                    result.ClientName = reg.ClientInfo.ClientName;
                }
                result.IDCardNo = reg.Customer.IDCardNo;
                result.Mobile = reg.Customer.Mobile;
                result.Name = reg.Customer.Name;
                result.Sex = reg.Customer.Sex.Value;
                result.CustomerRegId = reg.Id;

                var crisisList = crisisRows.Where(o => o.TjlCustomerRegItem.CustomerRegBM.CustomerBM == input.CustomerBM)?.ToList();
                if (crisisList != null)
                {
                    foreach (var row in crisisList)
                    {
                        var name = _userRepository.Get(row.CreatorUserId.Value).Name;
                        if (row.TjlCustomerServiceCallBacks.Count > 0)
                        {
                            foreach (var r in row.TjlCustomerServiceCallBacks)
                            {
                                var crisis = new CrisisInfo()
                                {
                                    CheckTime = row.TjlCustomerRegItem.CustomerItemGroupBM.FirstDateTime,
                                    DepartmentName = row.TjlCustomerRegItem.DepartmentBM.Name,
                                    Id = row.Id,
                                    ItemName = row.TjlCustomerRegItem.ItemName,
                                    ItemResultChar = row.TjlCustomerRegItem.ItemResultChar,
                                    SetNotice = row.SetNotice,
                                    RegItemId = row.TjlCustomerRegItemId,
                                    ItemGroupName = row.TjlCustomerRegItem.CustomerItemGroupBM.ItemGroupName
                                };
                                crisis.SetName = name;
                                crisis.CallBacKContent = r.CallBacKContent;
                                crisis.CallBackDate = r.CallBacKDate;
                                crisis.CallBackState = r.CallBackState;
                                crisis.CallBackType = r.CallBackType;
                                crisis.CallBackName = _userRepository.Get(r.CreatorUserId.Value).Name;
                                result.CrisisList.Add(crisis);
                            }
                        }
                        else
                        {
                            var crisis = new CrisisInfo()
                            {
                                CheckTime = row.TjlCustomerRegItem.CustomerItemGroupBM.FirstDateTime,
                                DepartmentName = row.TjlCustomerRegItem.DepartmentBM.Name,
                                Id = row.Id,
                                ItemName = row.TjlCustomerRegItem.ItemName,
                                ItemResultChar = row.TjlCustomerRegItem.ItemResultChar,
                                SetNotice = row.SetNotice,
                                RegItemId = row.TjlCustomerRegItemId,
                                ItemGroupName = row.TjlCustomerRegItem.CustomerItemGroupBM.ItemGroupName
                            };
                            crisis.SetName = name;
                            result.CrisisList.Add(crisis);
                        }
                    }
                }

                var user = _userRepository.FirstOrDefault(o => o.Id == AbpSession.UserId);
                var depts = user.TbmDepartments?.ToList();
                if (depts != null)
                {
                    var regitems = _regItemRepository.GetAll().
                        Where(o => o.CustomerRegBM.CustomerBM == input.CustomerBM
                                 && o.ProcessState == (int)ProjectIState.Complete
                                 ).OrderBy(o => o.DepartmentId).ThenBy(o => o.ItemGroupBMId);
                    var regitemlist = regitems.ToList();
                    regitemlist = regitemlist.Where(o => depts.FindIndex(c => c.Id == o.DepartmentId) != -1 && !crisisList.Any(k => k.TjlCustomerRegItemId == o.Id)).ToList();
                    result.RegItemList = regitemlist.Select(o =>
                        new CustomerRegItemDto
                        {
                            DepartmentName = o.DepartmentBM.Name,
                            ItemGroupName = o.ItemGroupBM.ItemGroupName,
                            Id = o.Id,
                            ItemName = o.ItemName,
                            ItemResultChar = o.ItemResultChar,
                            CheckTime = o.CustomerItemGroupBM.FirstDateTime,
                            Symbol = o.Symbol
                        }).ToList();
                }
            }
            return result;
        }
        /// <summary>
        /// 设置危急值
        /// </summary>
        public void SetCrisisList(SetCrisisDto input)
        {
            var old = _crisisSetRepository.GetAll().Where(o => o.TjlCustomerRegItem.CustomerRegId == input.CustomerRegId)?.ToList();
            if (old != null)
            {
                foreach (var odata in old)
                {
                    if (input.CrisisInfos.Any(o => o.TjlCustomerRegItemId == odata.TjlCustomerRegItemId))
                        continue;
                    else
                    {
                        _customerServiceCallBackRepository.Delete(o => o.TjlCrisisSetId == odata.Id);
                        _crisisSetRepository.Delete(odata);
                        var regitem = _regItemRepository.FirstOrDefault(o => o.Id == odata.TjlCustomerRegItemId);
                        if (regitem != null)
                        {
                            regitem.CrisisSate = 1;
                            _regItemRepository.Update(regitem);
                        }
                    }
                }
            }

            foreach (var info in input.CrisisInfos)
            {
                var row = _crisisSetRepository.FirstOrDefault(o => o.TjlCustomerRegItemId == info.TjlCustomerRegItemId);
                if (row == null)
                {
                    info.CrisisType = (int)CrisisType.ManualSetting;
                    _crisisSetRepository.Insert(info.MapTo<TjlCrisisSet>());
                    var regitem = _regItemRepository.FirstOrDefault(o => o.Id == info.TjlCustomerRegItemId);
                    if (regitem != null)
                    {
                        regitem.CrisisSate = 2;
                        _regItemRepository.Update(regitem);
                    }
                }
                else
                {
                    row.SetNotice = info.SetNotice;
                    _crisisSetRepository.Update(row);
                }

            }
        }
        /// <summary>
        /// 查询危急值列表
        /// </summary>
        public List<CustomerServiceCrisisViewDto> QueryCrisisList(SearchCrisisInfosDto input)
        {
            var rows = _crisisSetRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
                rows = rows.Where(o => o.TjlCustomerRegItem.CustomerRegBM.CustomerBM == input.CustomerBM);
            if (!string.IsNullOrWhiteSpace(input.Name))
                rows = rows.Where(o => o.TjlCustomerRegItem.CustomerRegBM.Customer.Name == input.Name);
            if (input.CallBackState.HasValue)
                rows = rows.Where(o => o.CallBackState == input.CallBackState);
            if (input.MsgState.HasValue)
                rows = rows.Where(o => o.MsgState == input.MsgState);
            if (input.StartCheckTime.HasValue)
                rows = rows.Where(o => o.TjlCustomerRegItem.CustomerItemGroupBM.FirstDateTime > input.StartCheckTime);
            if (input.EndCheckTime.HasValue)
                rows = rows.Where(o => o.TjlCustomerRegItem.CustomerItemGroupBM.FirstDateTime < input.EndCheckTime);
            if (input.ClientRegId != null)
            {
                rows = rows.Where(o => input.ClientRegId.Any(j => j == o.TjlCustomerRegItem.CustomerRegBM.ClientRegId.Value));
            }
            var result = new List<CustomerServiceCrisisViewDto>();
            foreach (var row in rows)
            {
                var data = new CustomerServiceCrisisViewDto();
                data.Id = row.Id;
                data.CustomerBM = row.TjlCustomerRegItem.CustomerRegBM.CustomerBM;
                data.Name = row.TjlCustomerRegItem.CustomerRegBM.Customer.Name;
                data.Age = row.TjlCustomerRegItem.CustomerRegBM.Customer.Age;
                data.Sex = row.TjlCustomerRegItem.CustomerRegBM.Customer.Sex;
                data.Moblie = row.TjlCustomerRegItem.CustomerRegBM.Customer.Mobile;
                data.ClientName = row.TjlCustomerRegItem.CustomerRegBM.ClientInfo?.ClientName;
                data.ItemName = row.TjlCustomerRegItem.ItemName;
                data.ItemResultChar = row.TjlCustomerRegItem.ItemResultChar;
                var user = _userRepository.FirstOrDefault(o => o.Id == row.CreatorUserId);
                if (user != null)
                    data.SetName = user.Name;
                data.SetNotice = row.SetNotice;
                data.MsgState = row.MsgState;
                data.CallbackState = row.CallBackState;
                data.SetTime = row.CreationTime;
                result.Add(data);
            }
            return result;

        }
        /// <summary>
        /// 查询回访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CallBackDto> QueryCallBackList(EntityDto<Guid> input)
        {
            var rows = _customerServiceCallBackRepository.GetAllIncluding(m => m.TjlCrisisSet).Where(o => o.TjlCrisisSetId == input.Id);
            var result = new List<CallBackDto>();
            foreach (var row in rows)
            {
                var data = row.MapTo<CallBackDto>();
                data.ItemName = row.TjlCrisisSet.TjlCustomerRegItem.ItemName;
                data.ItemResultChar = row.TjlCrisisSet.TjlCustomerRegItem.ItemResultChar;
                var user = _userRepository.FirstOrDefault(o => o.Id == row.CreatorUserId);
                if (user != null)
                    data.CallBackName = user.Name;
                result.Add(data);
            }
            return result;
        }
        /// <summary>
        /// 保存回访信息
        /// </summary>
        /// <param name="input"></param>
        public void SaveCallBack(CallBackDto input)
        {
            if (input.Id == Guid.Empty)
            {
                _customerServiceCallBackRepository.Insert(input.MapTo<TjlCustomerServiceCallBack>());
                var crisis = _crisisSetRepository.FirstOrDefault(o => o.Id == input.TjlCrisisSetId);
                if (crisis != null)
                {
                    crisis.CallBackState = 1;
                    _crisisSetRepository.Update(crisis);
                }
            }
            else
            {
                var row = _customerServiceCallBackRepository.FirstOrDefault(o => o.Id == input.Id);
                if (row != null)
                {
                    row.CallBackType = input.CallBackType;
                    row.CallBacKDate = input.CallBacKDate;
                    row.CallBackState = input.CallBackState;
                    row.CallBacKContent = input.CallBacKContent;
                    _customerServiceCallBackRepository.Update(row);
                }
            }
        }
        /// <summary>
        /// 保存复查设置
        /// </summary>
        /// <param name="input"></param>
        public void SaveReviewSet(ReviewSetDto input)
        {
            if (input.Id != Guid.Empty)
            {
                var review = _ReviewItemSet.Get(input.Id);

                input.MapTo(review);
                if (input.SummarizeAdviceId.HasValue)
                {
                    var advice = _SummarizeAdvice.Get(input.SummarizeAdviceId.Value);
                    review.SummarizeAdvice = advice;
                }
                review.ItemGroupBM = new List<TbmItemGroup>();
                foreach (var group in input.ItemGroupBM)
                {
                    var tbmgroup = _ItemGroup.Get(group.Id);
                    review.ItemGroupBM.Add(tbmgroup);
                }
                _ReviewItemSet.Update(review);

            }
            else
            {
                var review = input.MapTo<TbmReviewItemSet>();
                review.Id = Guid.NewGuid();
                if (input.SummarizeAdviceId.HasValue)
                {
                    var advice = _SummarizeAdvice.Get(input.SummarizeAdviceId.Value);
                    review.SummarizeAdvice = advice;
                }
                review.ItemGroupBM = new List<TbmItemGroup>();
                foreach (var group in input.ItemGroupBM)
                {
                    var tbmgroup = _ItemGroup.Get(group.Id);
                    review.ItemGroupBM.Add(tbmgroup);
                }
                _ReviewItemSet.Insert(review);

                //CurrentUnitOfWork.SaveChanges();

            }
        }

        /// <summary>
        /// 保存复查设置
        /// </summary>
        /// <param name="input"></param>
        public void DelReviewSet(ReviewSetDto input)
        {
            if (input.Id != Guid.Empty)
            {
                var review = _ReviewItemSet.Get(input.Id);
                _ReviewItemSet.Delete(review);

            }
          
        }
        /// <summary>
        /// 复查设置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ReviewSetDto> getAllReview()
        {
            var Reviewlist = _ReviewItemSet.GetAll().MapTo<List<ReviewSetDto>>();
            return Reviewlist;
        }

        /// <summary>
        /// lx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public List<CrisVisitDto> PageFulls(CrisVisitSelectDto input)
        {
            var rows = _regItemRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
                rows = rows.Where(o => o.CustomerRegBM.CustomerBM == input.CustomerBM);
            if (input.DepartmentBM.HasValue)
                rows = rows.Where(o => o.DepartmentId == input.DepartmentBM);
            if (input.ItemId.HasValue)
                rows = rows.Where(o => o.ItemId == input.ItemId);
            if (input.GroupId.HasValue)
                rows = rows.Where(o => o.ItemGroupBMId == input.GroupId);

            if (input.StartTime.HasValue)
                rows = rows.Where(o => o.CreationTime == input.StartTime);
            if (input.EndTime.HasValue)
                rows = rows.Where(o => o.DeletionTime==input.EndTime);
           

            var result =rows.Where(o => o.CrisisSate == 1).Select(o => new CrisVisitDto
            {
                CreateTime = o.LastModificationTime == null ? o.CreationTime : o.LastModificationTime,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                Name = o.CustomerRegBM.Customer.Name,
                Age = o.CustomerRegBM.Customer.Age,
                Sexs = o.CustomerRegBM.Customer.Sex==1?"男":"女",
                Moblie = o.CustomerRegBM.Customer.Mobile,
                ItemName = o.ItemName,
                ItemResultChar = o.ItemResultChar,
                Stand = o.Stand,
                DepartmentName = o.DepartmentBM.Name
            }).ToList();
            return result;
   

        }

        public string acc()
        {
            return "aaaaaaa";
        }

        public List<CrisisCustomerDto> GetCrisisCustome()
        {
            //查询危急值
            var result = (from tjlCustomers in _tjlCustomerRegrepository.GetAll()                
                join tjlCustomer in _TjlCustomer.GetAll() on tjlCustomers.CustomerId equals tjlCustomer.Id
                join TjlCustomeritem in _regItemRepository.GetAll().Where(o => o.CrisisSate == 1)
                        .Where(o => o.CrisisVisitSate != 2 && o.CrisisVisitSate != 3) on tjlCustomers.Id equals
                    TjlCustomeritem.CustomerRegId

                select new CrisisCustomerDto
                {
                    CustomerBM = tjlCustomers.CustomerBM,
                    Name = tjlCustomer.Name,
                    Sexs = tjlCustomer.Sex == 1 ? "男" : "女",
                    Age = tjlCustomer.Age,
                    ItemName = TjlCustomeritem.ItemName,
                    CrisisSate = TjlCustomeritem.CrisisSate,
                    ItemResultChar = TjlCustomeritem.ItemResultChar,
                    Stand = TjlCustomeritem.Stand,
                    CustomerRegId = tjlCustomers.Id,
                    CustomerItemGroupBMid = TjlCustomeritem.CustomerItemGroupBMid,
                    Id = TjlCustomeritem.Id

                }).Distinct().Take(1);

            return result.ToList();
        }

        //修改危急值
        public CrisisCustomerDto UpdateCrisis(CrisisCustomerDto CrisisCustomerDto)
        {
            // var TjlCrisVisit = _TjlCrisVisit.GetAll().Where(o => o.CustomerReg.Id == CrisisCustomerDto.CustomerRegId).FirstOrDefault();
            //if (null == TjlCrisVisit)
            //{
            var AddResult = _TjlCrisVisit.Insert(new TjlCrisVisit
            {           
                CustomerRegId = CrisisCustomerDto.CustomerRegId,
                CallBacKContent = CrisisCustomerDto.CallBacKContent,
                //CallBacKDate = DateTime.Now,
                CallBacKUserId = CrisisCustomerDto.CallBacKUserId,
                CallBackState = 1
            });
            var users = _regItemRepository.GetAll().Where(o => o.Id == CrisisCustomerDto.Id).FirstOrDefault();
            users.CrisisSate = 2;
            users.CrisisVisitSate = 2;
            
            var ab = _regItemRepository.Update(users);
            CurrentUnitOfWork.SaveChanges();
            return CrisisCustomerDto;
            //}
            //TjlCrisVisit.CallBacKDate = CrisisCustomerDto.CallBacKDate;
            //TjlCrisVisit.CallBacKUserId = CrisisCustomerDto.CallBacKUserId;
            //TjlCrisVisit.CallBacKContent = CrisisCustomerDto.CallBacKContent;
            //TjlCrisVisit.CustomerRegId = CrisisCustomerDto.CustomerRegId;
            //var UpdateResult = _TjlCrisVisit.Update(TjlCrisVisit);
            //CrisisCustomerDto.CustomerRegId = UpdateResult.CustomerRegId == null ? Guid.NewGuid() : UpdateResult.CustomerRegId;

           

            return CrisisCustomerDto;
        }
        public int UpdateTjlCrisVisit(CrisisCustomerDto CrisisCustomerDto)
        {
            var result = _TjlCrisVisit.GetAll().Where(o => o.CustomerReg.Id == CrisisCustomerDto.CustomerRegId).FirstOrDefault();
            result.CallBacKContent = CrisisCustomerDto.CallBacKContent;
            result.SHContent = CrisisCustomerDto.SHContent;
            try
            {
                if (null == _TjlCrisVisit.Update(result))
                    return 0;
                return 1;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}