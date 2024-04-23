using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Abp.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport
{
    [AbpAuthorize]
    public class CustomerReportAppService : MyProjectAppServiceBase, ICustomerReportAppService
    {
        private readonly IRepository<TjlCustomerReportCollection, Guid> _customerReportRepository;
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;
        private readonly IRepository<TbmCabinet, Guid> _TbmCabinet;
        private readonly IRepository<TjlCusCabit, Guid> _TjlCusCabit;
        private readonly IRepository<TjlClientReg, Guid> _TjlClientReg;
        public CustomerReportAppService(IRepository<TjlCustomerReportCollection, Guid> customerReportRepository,
            IRepository<TjlCustomerReg, Guid> customerRegRepository, IRepository<TbmCabinet, Guid> TbmCabinet,
            IRepository<TjlCusCabit, Guid> TjlCusCabit,
            IRepository<TjlClientReg, Guid> TjlClientReg)
        {
            _customerReportRepository = customerReportRepository;
            _customerRegRepository = customerRegRepository;
            _TbmCabinet = TbmCabinet;
            _TjlCusCabit = TjlCusCabit;
            _TjlClientReg = TjlClientReg;
        }

        public CustomerReportFullDto Handover(CustomerReportHandoverInput input)
        {
            var entity = _customerReportRepository.FirstOrDefault(m => m.CustomerReg.CustomerBM == input.Number);
            if (entity == null)
            {
                var sr = _customerRegRepository.FirstOrDefault(m => m.CustomerBM == input.Number);
                if (sr == null)
                {
                    throw new UserException.Verification.FieldVerifyException("未找到报告！", "未找到报告！");
                }
                entity = new TjlCustomerReportCollection();
                entity.Id = Guid.NewGuid();
                entity.CustomerRegId = sr.Id;
                entity.State = input.Complete == true ? 1 : 0;
                entity.HandoverTime = DateTime.Now;
                entity.Handover = input.Handover;
                entity.Receiptor = input.Receiptor;
                _customerReportRepository.Insert(entity);
            }
            else
            {
                if (entity.State == 1)
                {
                    throw new UserException.Verification.FieldVerifyException("报告已交接！", "报告已交接！");
                }
                if (entity.State == 2)
                {
                    throw new UserException.Verification.FieldVerifyException("报告已发放！", "报告已发放！");
                }
                entity.State = input.Complete == true ? 1 : entity.State;
                entity.HandoverTime = DateTime.Now;
                entity.Handover = input.Handover;
                entity.Receiptor = input.Receiptor;
                _customerReportRepository.Update(entity);
            }
            return entity.MapTo<CustomerReportFullDto>();
        }
        public CustomerReportFullDto CancelHandover(CustomerReportHandoverInput input)
        {
            var entity = _customerReportRepository.FirstOrDefault(m => m.CustomerReg.CustomerBM == input.Number);
            if (entity == null)
            {
                var sr = _customerRegRepository.FirstOrDefault(m => m.CustomerBM == input.Number);
                if (sr == null)
                {
                    throw new UserException.Verification.FieldVerifyException("未找到报告！", "未找到报告！");
                }
                entity = new TjlCustomerReportCollection();
                entity.Id = Guid.NewGuid();
                entity.CustomerRegId = sr.Id;
                entity.State = 0;
                entity.HandoverTime = null;
                entity.Handover = "";
                entity.Receiptor = "";
                _customerReportRepository.Insert(entity);
            }
            else
            {
                //if (entity.State == 1)
                //{
                //    throw new UserException.Verification.FieldVerifyException("报告已交接！", "报告已交接！");
                //}
                //if (entity.State == 2)
                //{
                //    throw new UserException.Verification.FieldVerifyException("报告已发放！", "报告已发放！");
                //}
                entity.State = 0;
                entity.HandoverTime = DateTime.Now;
                entity.Handover = "";
                entity.Receiptor = "";
                _customerReportRepository.Update(entity);
            }
            return entity.MapTo<CustomerReportFullDto>();
        }
        public void BatchHandover(List<CustomerReportHandoverInput> input)
        {
            DateTime now = DateTime.Now;
            foreach (var item in input)
            {
                var entity = _customerReportRepository.FirstOrDefault(m => m.State == 0 && m.CustomerReg.CustomerBM == item.Number);
                if (entity == null) continue;
                entity.State = 1;
                entity.HandoverTime = now;
                entity.Handover = item.Handover;
                entity.Receiptor = item.Receiptor;
                _customerReportRepository.Update(entity);
            }
        }

        public CustomerReportFullDto Send(CustomerReportHandoverInput input)
        {
            var entity = _customerReportRepository.FirstOrDefault(m => m.CustomerReg.CustomerBM == input.Number ||
             m.CustomerReg.Customer.IDCardNo== input.Number);
            if (entity == null)
            {
                
                var sr = _customerRegRepository.FirstOrDefault(m => m.CustomerBM == input.Number
                || m.Customer.IDCardNo == input.Number);
                if (sr == null)
                {
                    throw new UserException.Verification.FieldVerifyException("未找到报告！", "未找到报告！");
                }
                entity = new TjlCustomerReportCollection();
                entity.Id = Guid.NewGuid();
                entity.CustomerRegId = sr.Id;
                entity.State = input.Complete == true ? 2 : 0;
                entity.SendTime = DateTime.Now;
                entity.Sender = input.Handover;
                entity.Receiver = input.Receiptor;
                _customerReportRepository.Insert(entity);
                //更新报告领取状态
                sr.ReceiveSate = 2;
                _customerRegRepository.Update(sr);
            }
            else
            {
                if (entity.State == 2 && input.Complete == true)
                {
                    throw new UserException.Verification.FieldVerifyException("报告已发放！", "报告已发放！");
                }
                else if(entity.State == 2)
                {
                    return entity.MapTo<CustomerReportFullDto>();

                }
                entity.State = input.Complete == true ? 2 : entity.State;
                entity.SendTime = DateTime.Now;
                entity.Sender = input.Handover;
                entity.Receiver = input.Receiptor;
                _customerReportRepository.Update(entity);
                var sr = _customerRegRepository.Get(entity.CustomerRegId);
                sr.ReceiveSate = 2;
                _customerRegRepository.Update(sr);

            }
            return entity.MapTo<CustomerReportFullDto>();
        }
        public CustomerReportFullDto Cancel(CustomerReportHandoverInput input)
        {
            var entity = _customerReportRepository.FirstOrDefault(m => m.CustomerReg.CustomerBM == input.Number 
            || m.CustomerReg.Customer.IDCardNo == input.Number);
            if (entity == null)
            {
                var sr = _customerRegRepository.FirstOrDefault(m => m.CustomerBM == input.Number 
                || m.Customer.IDCardNo== input.Number);
                //if (sr == null)
                //{
                //    throw new UserException.Verification.FieldVerifyException("未找到报告！", "未找到报告！");
                //}
                entity = new TjlCustomerReportCollection();
                entity.Id = Guid.NewGuid();
                entity.CustomerRegId = sr.Id;
                entity.State = 0;
                entity.SendTime = DateTime.Now;
                entity.Sender = input.Handover;
                entity.Receiver = input.Receiptor;
                _customerReportRepository.Insert(entity);
                //更新报告领取状态
                sr.ReceiveSate = 1;
                _customerRegRepository.Update(sr);
            }
            else
            {
                //if (entity.State == 2)
                //{
                //    throw new UserException.Verification.FieldVerifyException("报告已发放！", "报告已发放！");
                //}
                entity.State = 0;
                entity.SendTime = DateTime.Now;
                entity.Sender = input.Handover;
                entity.Receiver = input.Receiptor;
                _customerReportRepository.Update(entity);
                var sr = _customerRegRepository.Get(entity.CustomerRegId);
                sr.ReceiveSate = 1;
                _customerRegRepository.Update(sr);

            }
            return entity.MapTo<CustomerReportFullDto>();
        }
        public void BatchSend(List<CustomerReportHandoverInput> input)
        {
            DateTime now = DateTime.Now;
            foreach (var item in input)
            {
                var entity = _customerReportRepository.FirstOrDefault(m => m.State < 2 && m.CustomerReg.CustomerBM == item.Number);
                if (entity == null)
                {
                    var sr = _customerRegRepository.FirstOrDefault(m => m.CustomerBM == item.Number);
                    if (sr == null)
                    {
                        throw new UserException.Verification.FieldVerifyException("未找到报告！", "未找到报告！");
                    }
                    entity = new TjlCustomerReportCollection();
                    entity.Id = Guid.NewGuid();
                    entity.CustomerRegId = sr.Id;
                    entity.State = item.Complete == true ? 2 : 0;
                    entity.SendTime = DateTime.Now;
                    entity.Sender = item.Handover;
                    entity.Receiver = item.Receiptor;
                    _customerReportRepository.Insert(entity);
                    //更新报告领取状态
                    sr.ReceiveSate = 2;
                    _customerRegRepository.Update(sr);
                }
                else
                {
                    entity.State = 2;
                    entity.SendTime = now;
                    entity.Sender = item.Handover;
                    entity.Receiver = item.Receiptor;
                    _customerReportRepository.Update(entity);
                    
                    var sr = _customerRegRepository.Get(entity.CustomerRegId);
                    sr.ReceiveSate = 2;
                    _customerRegRepository.Update(sr);
                }
            }
        }

        public CustomerReportFullDto Get(CustomerReportByNumber input)
        {
            var entity = _customerReportRepository.GetAll().FirstOrDefault(m => m.CustomerReg.CustomerBM == input.CustomerBM);
            var dto = entity.MapTo<CustomerReportFullDto>();
            return dto;
        }

        public List<CustomerReportFullDto> Query(CustomerReportQuery input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<CustomerReportFullDto>>();
        }

        private IQueryable<TjlCustomerReportCollection> BuildQuery(CustomerReportQuery input)
        {
            var query = _customerReportRepository.GetAll();
            if (input != null)
            {
                if (!string.IsNullOrEmpty(input.QueryText))
                    query = query.Where(m => m.CustomerReg.CustomerBM.Contains(input.QueryText)
                            || m.CustomerReg.Customer.Name.Contains(input.QueryText)
                            || m.CustomerReg.Customer.IDCardNo.Contains(input.QueryText));

                if (input.CustomerReportState != null)
                    query = query.Where(m => m.State == input.CustomerReportState);

                if (input.StartRegisterDate != null)
                {
                    var s = input.StartRegisterDate.Value.Date;
                    query = query.Where(m => m.CustomerReg.LoginDate >= s);
                }
                if (input.EndRegisterDate != null)
                {
                    var e = input.EndRegisterDate.Value.Date.AddDays(1);
                    query = query.Where(m => m.CustomerReg.LoginDate < e);
                }
                if (input.StartHandoverDate != null)
                {
                    var s = input.StartHandoverDate.Value.Date;
                    query = query.Where(m => m.HandoverTime >= s);
                }
                if (input.EndHandoverDate != null)
                {
                    var e = input.EndHandoverDate.Value.Date.AddDays(1);
                    query = query.Where(m => m.HandoverTime < e);
                }
                if (input.StartSendDate != null)
                {
                    var s = input.StartSendDate.Value.Date;
                    query = query.Where(m => m.SendTime >= s);
                }
                if (input.EndSendDate != null)
                {
                    var e = input.EndSendDate.Value.Date.AddDays(1);
                    query = query.Where(m => m.SendTime < e);
                }
                if (input.ClientRegId.HasValue)
                {
                    query = query.Where(p=> p.CustomerReg!=null && p.CustomerReg.ClientRegId== input.ClientRegId);
                }
                if (!string.IsNullOrEmpty(input.Handover))
                    query = query.Where(m => m.Handover.Contains(input.Handover));
                if (!string.IsNullOrEmpty(input.Receiptor))
                    query = query.Where(m => m.Receiptor.Contains(input.Receiptor));
                if (!string.IsNullOrEmpty(input.Sender))
                    query = query.Where(m => m.Sender.Contains(input.Sender));
                if (!string.IsNullOrEmpty(input.Receiver))
                    query = query.Where(m => m.Receiver.Contains(input.Receiver));
            }
            query = query.OrderByDescending(m => m.CreationTime);
            return query;
        }
        /// <summary>
        /// 柜子设置
        /// </summary>
        /// <returns></returns>
        public TbmCabinetDto getTbmCabinet()
        {
            var result = _TbmCabinet.GetAll().ToList();
            if (result != null && result.Count > 0)
            {
                return result?.First()?.MapTo<TbmCabinetDto>();
            }
            else
            {
                return null;
            }


        }
        /// <summary>
        /// 保存柜子设置
        /// </summary>
        /// <returns></returns>
        public TbmCabinetDto SaveTbmCabinet(TbmCabinetDto input)
        {
            var result = _TbmCabinet.GetAll().ToList();
            var dto = new TbmCabinet();


            if (result.Count == 0)
            {
                var ret = input.MapTo<TbmCabinet>();
                ret.Id = Guid.NewGuid();
                dto = _TbmCabinet.Insert(ret);
            }
            else
            {
                dto = result.First();
                input.MapTo(dto);
                dto = _TbmCabinet.Update(dto);
            }
            return dto.MapTo<TbmCabinetDto>();
        }
        /// <summary>
        /// 查询柜子存放记录
        /// </summary>
        /// <returns></returns>
        public List<TjlCusCabitDto> getTjlCabinet()
        {
            var result = _TjlCusCabit.GetAll().Where(o => o.GetState == 1).ToList();
            return result.MapTo<List<TjlCusCabitDto>>();
        }
        /// <summary>
        /// 保存柜子存放设置
        /// </summary>
        /// <returns></returns>
        public TjlCusCabitDto SaveTjlCabinet(TjlCusCabitDto input)
        {

            var dto = new TjlCusCabit();
            if (input.ClientRegId.HasValue)
            {
                dto = _TjlCusCabit.FirstOrDefault(o => o.ClientRegId == input.ClientRegId);
            }
            if (input.CustomerRegId.HasValue)
            {
                dto = _TjlCusCabit.FirstOrDefault(o => o.CustomerRegId == input.CustomerRegId);
            }
            if (dto == null)
            {
                var ret = input.MapTo<TjlCusCabit>();
                ret.Id = Guid.NewGuid();
                ret.HandoverId = AbpSession.UserId;
                ret.HandoverTime = System.DateTime.Now;
                dto = _TjlCusCabit.Insert(ret);
            }
            else
            {
                var id = dto.Id;
                input.MapTo(dto);
                dto.Id = id;
                dto.HandoverId = AbpSession.UserId;
                dto.HandoverTime = System.DateTime.Now;
                dto = _TjlCusCabit.Update(dto);            }
            return dto.MapTo<TjlCusCabitDto>();
        }
        /// <summary>
        /// 删除柜子存放设置
        /// </summary>
        /// <returns></returns>
        public void DelTjlCabinet(TjlCusCabitDto input)
        {
            var dto = new TjlCusCabit();
            if (input.Id != null && input.Id !=Guid.Empty)
            {
                dto = _TjlCusCabit.Get(input.Id);
            }
            else if (input.CustomerRegId.HasValue)
            {
                dto = _TjlCusCabit.FirstOrDefault(o => o.CustomerRegId == input.CustomerRegId);
            }
            else if (input.ClientRegId.HasValue)
            {
                dto = _TjlCusCabit.FirstOrDefault(o => o.ClientRegId == input.ClientRegId);
            }
            if (dto != null && dto.Id != Guid.Empty)
            {
                _TjlCusCabit.Delete(dto);
            }
        }
        /// <summary>
        /// 删除柜子存放设置
        /// </summary>
        /// <returns></returns>
        public CustomerUpCatDto UpCustomerUpCat(CustomerUpCatDto input)
        {
           var cusreg= _customerRegRepository.Get(input.Id);
            input.MapTo(cusreg);
            cusreg = _customerRegRepository.Update(cusreg);
            return cusreg.MapTo<CustomerUpCatDto>();
        }
        /// <summary>
        /// 修改单位柜子存方设置
        /// </summary>
        /// <returns></returns>
        public ClientRegUpCatDto UpClientRegUpCat(ClientRegUpCatDto input)
        {
            var cusreg = _TjlClientReg.Get(input.Id);
            input.MapTo(cusreg);
            cusreg = _TjlClientReg.Update(cusreg);
            return cusreg.MapTo<ClientRegUpCatDto>();
        }
        /// <summary>
        /// 个人报告领取查询
        /// </summary>
        /// <returns></returns>
        public List<TjlCusCabitCRDto> getTjlCabinetlist(SeachCusCabDto input)
        {
            var result = _TjlCusCabit.GetAll().Where(o=>o.ReportState==1);
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                result = result.Where(o=>o.CustomerRegBM.CustomerBM== input.CustomerBM);
            }
            if (!string.IsNullOrEmpty(input.CustomerName))
            {
                result = result.Where(o => o.CustomerRegBM.Customer.Name == input.CustomerName);
            }
            if (!string.IsNullOrEmpty(input.Mobile))
            {
                result = result.Where(o => o.CustomerRegBM.Customer.Mobile == input.Mobile);
            }
            if (input.ClientRegId.HasValue)
            {
                result = result.Where(o => o.CustomerRegBM.ClientRegId == input.ClientRegId);
            }
            if (!string.IsNullOrEmpty(input.CabitName))
            {
                result = result.Where(o => o.CabitName== input.CabitName);
            }
            if (input.EndLoginTime.HasValue  && input.StarLoginTime.HasValue)
            {
                var star = input.StarLoginTime;
                var end = input.EndLoginTime.Value.AddDays(1);
                result = result.Where(o => o.CustomerRegBM.LoginDate >= star && o.CustomerRegBM.LoginDate< end);

            }
            if (input.EndSendTime.HasValue && input.StarSendTime.HasValue)
            {
                var star = input.StarSendTime;
                var end = input.EndSendTime.Value.AddDays(1);
                result = result.Where(o => o.SendTime >= star && o.SendTime < end);

            }
            if (input.GetState.HasValue)
            {
                if (input.GetState.Value != 3)
                {
                    result = result.Where(o=>o.GetState== input.GetState.Value);
                }
            }
            //var ss = result.ToList();
            var retlist = result.Select(o => new TjlCusCabitCRDto
            {
                CabitName = o.CabitName,
                ClientName = o.CustomerRegBM.ClientInfo.ClientName,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                CustomerName = o.CustomerRegBM.Customer.Name,
                GetState = o.GetState,
                HandoverTime = o.HandoverTime,
                Id = o.Id,
                Mobile = o.CustomerRegBM.Customer.Mobile,
                Receiver = o.Receiver,
                Remarks = o.Remarks,
                SendTime = o.SendTime
            }).ToList();
            return retlist;
        }
        /// <summary>
        /// 保存领取信息
        /// </summary>
        /// <returns></returns>
        public CusCabitLQDto SaveTjlCabinetLQ(CusCabitLQDto input)
        {

            var dto = new TjlCusCabit();
            if (input.Id !=Guid.Empty)
            {
                dto = _TjlCusCabit.Get(input.Id);
            }   
            if(dto!=null)
            {
                
                
                input.MapTo(dto);
                dto.SenderId = AbpSession.UserId;
                dto.SendTime = System.DateTime.Now;
                 dto = _TjlCusCabit.Update(dto);
                var cus = _customerRegRepository.Get(dto.CustomerRegId.Value);
                cus.ReceiveSate = 2;
                _customerRegRepository.Update(cus);
            }
            return dto.MapTo<CusCabitLQDto>();
        }
        /// <summary>
        /// 判断审核状态
        /// </summary>
        /// <returns></returns>
        public ChargeBM IsSH(EntityDto<Guid> input)
        {
            ChargeBM chargeBM = new ChargeBM();
            var dto = new TjlCusCabit();
            if (input.Id != Guid.Empty)
            {
                dto = _TjlCusCabit.Get(input.Id);
                var cus = _customerRegRepository.Get(dto.CustomerRegId.Value);
                if (cus.SummSate != (int)SummSate.Audited)
                { chargeBM.Name = cus.CustomerBM + "-" + cus.Customer.Name +  ",未审核"; }
            }
           
            return chargeBM;
        }
        /// <summary>
        /// 团报报告领取查询
        /// </summary>
        /// <returns></returns>
        public List<ClientCabitCRDto> getClientCabinetlist(SearchClientRegLQDto input)
        {
            var result = _TjlCusCabit.GetAll().Where(o => o.ReportState == 2);
            if (input.Id!=null && input.Id !=Guid.Empty)
            {
                result = result.Where(o => o.ClientRegId == input.Id);
            }            
            if (!string.IsNullOrEmpty(input.CusCabitBM))
            {
                result = result.Where(o => o.CabitName == input.CusCabitBM);
            }            
            //var ss = result.ToList();
            var retlist = result.Select(o => new ClientCabitCRDto
            {
                CabitName = o.CabitName,
                ClientName = o.ClientReg.ClientInfo.ClientName,             
                GetState = o.GetState,
                HandoverTime = o.HandoverTime,
                Id = o.Id,             
                Receiver = o.Receiver,
                Remarks = o.Remarks,
                SendTime = o.SendTime,
                StartCheckDate=o.ClientReg.StartCheckDate
                 
            }).ToList();
            return retlist;
        }

    }
}
