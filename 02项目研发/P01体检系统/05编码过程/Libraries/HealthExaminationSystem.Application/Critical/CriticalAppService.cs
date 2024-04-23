using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical
{
    /// <summary>
    /// 新版危急值
    /// </summary>
    [AbpAuthorize]
    public class CriticalAppService : MyProjectAppServiceBase, ICriticalAppService
    {
        private readonly IRepository<TbmCriticalSet, Guid> _CriticalSetRepository;
        private readonly IRepository<TbmCriticalDetail, Guid> _CriticalDetail;

        private readonly IRepository<TjlCustomerRegItem, Guid> _TjlCustomerRegItem;
        public CriticalAppService(
           IRepository<TbmCriticalSet, Guid> CriticalSetRepository,
           IRepository<TjlCustomerRegItem, Guid> TjlCustomerRegItem,
           IRepository<TbmCriticalDetail, Guid> CriticalDetail

           )
        {
            _CriticalSetRepository = CriticalSetRepository;
            _TjlCustomerRegItem = TjlCustomerRegItem;
            _CriticalDetail = CriticalDetail;
        }
        /// <summary>
        /// 查询危急值列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CriticalDto> getSearchCriticalDto(SearchCriticalDto input)
        {
            var que = _CriticalSetRepository.GetAll();
            if(input.Id !=null && input.Id!=Guid.Empty)
            {
                que = que.Where(p=>p.Id== input.Id);
            }
            if (input.DepartmentId.HasValue)
            {
                que = que.Where(p => p.DepartmentId == input.DepartmentId);
            }
            if (input.ItemInfoId.HasValue)
            {
                que = que.Where(p => p.ItemInfoId == input.ItemInfoId);
            }
            que = que.OrderBy(p=>p.Department.OrderNum);
            //var Critque = que.Select(p => new 
            //{
            //    CalculationType = p.CalculationType,
            //    CriticalType = p.CriticalType,
            //    DepartmentId = p.DepartmentId,
            //    ItemInfoId = p.ItemInfoId,
            //    Id = p.Id,
            //    Operator = p.Operator,
            //    ValueChar = p.ValueChar,
            //    ValueNum = p.ValueNum,
            //    p.Department.OrderNum
            //}).OrderBy(p=>p.OrderNum).ToList();
            return ObjectMapper.Map<List<CriticalDto>>(que); //que.MapTo < List < CriticalDto >>();
        }
        /// <summary>
        /// 保存危机值
        /// </summary>
        public void SaveCritical(CriticalDto input)
        {
             
            if (input.Id != null && input.Id != Guid.Empty)
            {
                List<TbmCriticalDetail> CriticalDetaillist = new List<TbmCriticalDetail>();
                if (input.CriticalDetails != null && input.CriticalDetails.Count > 0)
                {
                    var deldetails = _CriticalDetail.GetAll().Where(p=>p.CriticalSetId== input.Id);
                    foreach (var deltail in deldetails)
                    {
                        _CriticalDetail.Delete(deltail);
                        CurrentUnitOfWork.SaveChanges();
                    }
                    foreach (var inDetail in input.CriticalDetails)
                    {

                        var InCriticalDetail = inDetail.MapTo<TbmCriticalDetail>();
                        InCriticalDetail.Id = Guid.NewGuid();
                        InCriticalDetail.CriticalSetId = input.Id;
                        _CriticalDetail.Insert(InCriticalDetail);
                        CurrentUnitOfWork.SaveChanges();
                        CriticalDetaillist.Add(InCriticalDetail);
                    }
                }
                var CriticalSet = _CriticalSetRepository.Get(input.Id);
                //var detail = CriticalSet.CriticalDetails;
                CriticalSet.CalculationType = input.CalculationType;
                CriticalSet.CriticalType = input.CriticalType;
                CriticalSet.Department = null;
                CriticalSet.DepartmentId = input.DepartmentId;
                CriticalSet.ItemInfo = null;
                CriticalSet.ItemInfoId = input.ItemInfoId;
                CriticalSet.Old = input.Old;
                CriticalSet.Operator = input.Operator;
                CriticalSet.Sex = input.Sex;
                CriticalSet.ValueChar = input.ValueChar;
                CriticalSet.ValueNum = input.ValueNum;


                _CriticalSetRepository.Update(CriticalSet);
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                var CriticalSet = input.MapTo<TbmCriticalSet>();
                CriticalSet.Id = Guid.NewGuid();
                CriticalSet.CriticalDetails = null;
                _CriticalSetRepository.Insert(CriticalSet);
                if (input.CriticalDetails != null)
                {
                    foreach (var inDetail in input.CriticalDetails)
                    {
                        var InCriticalDetail = inDetail.MapTo<TbmCriticalDetail>();
                        InCriticalDetail.Id = Guid.NewGuid();
                        InCriticalDetail.CriticalSetId = CriticalSet.Id;
                        _CriticalDetail.Insert(InCriticalDetail);
                        CurrentUnitOfWork.SaveChanges();
                    }
                }

            }
        }
        /// <summary>
        /// 删除危害因素
        /// </summary>
        /// <param name="input"></param>
        public void DelCritical(SearchCriticalDto input)
        {
            var CriticalSet = _CriticalSetRepository.Get(input.Id);
            _CriticalSetRepository.Delete(CriticalSet);
        }
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CrisisMessageDto> getCrisisMessageDto(SearchtCrisisMessageDto input)
        {
          var qure=  _TjlCustomerRegItem.GetAll().Where(p=>p.CrisisSate== (int)CrisisSate.Abnormal) ;

            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                qure = qure.Where(p=>p.CustomerRegBM.CustomerBM== input.CustomerBM);
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                qure = qure.Where(p => p.CustomerRegBM.Customer.Name == input.Name);
            }
            if (input.CheckDateStart.HasValue)
            {
                qure = qure.Where(p => p.CustomerItemGroupBM.FirstDateTime >= input.CheckDateStart);
            }
            if (input.CheckDateEnd.HasValue)
            {
                qure = qure.Where(p => p.CustomerItemGroupBM.FirstDateTime < input.CheckDateEnd);
            }
            if (input.ClientRegId.HasValue)
            {
                qure = qure.Where(p => p.CustomerRegBM.ClientRegId == input.ClientRegId);
            }
            if(input.CrisisLever.HasValue)
            {
                qure = qure.Where(p => p.CrisisLever == input.CrisisLever);
            }
            if (input.CrisisVisitSate.HasValue)
            {
                if (input.CrisisVisitSate != 2 && input.CrisisVisitSate != 3)
                {
                    qure = qure.Where(p => p.CrisisVisitSate !=2 && p.CrisisVisitSate !=3);
                }
                else
                { qure = qure.Where(p => p.CrisisVisitSate == input.CrisisVisitSate); }
                
            }
            if (input.DepartmentId.HasValue)
            {
                qure = qure.Where(p => p.DepartmentId == input.DepartmentId);
            }
            if (input.MessageState.HasValue && input.MessageState!=0)
            {
                qure = qure.Where(p => p.MessageState == input.MessageState);
            }
            if (input.MessageTimeStar.HasValue)
            {
                qure = qure.Where(p => p.MessageTime >= input.MessageTimeStar);
            }
            if (input.MessageTimeEnd.HasValue)
            {
                qure = qure.Where(p => p.MessageTime < input.MessageTimeEnd);
            }
            var nowtime = System.DateTime.Now.AddDays(-1);
             
            var cusMesss = qure.Select(p => new CrisisMessageDto
            {
                 ClientName=p.CustomerRegBM.ClientInfo==null?"": p.CustomerRegBM.ClientInfo.ClientName,
                  department=p.CustomerRegBM.Customer.Department,
                   IDCard=p.CustomerRegBM.Customer.IDCardNo,
                Age = p.CustomerRegBM.Customer.Age,
                BookingDate = p.CustomerRegBM.BookingDate,
                CheckEmployeeBMId = p.CustomerItemGroupBM.CheckEmployeeBMId,
                ClientInfoId = p.CustomerRegBM.ClientRegId,
                CrisiChar = p.CrisiChar,
                CrisisLever = p.CrisisLever,
                CrisisVisitSate = p.CrisisVisitSate,
                CustomerBM = p.CustomerRegBM.CustomerBM,
                DepartmentId = p.DepartmentId,
                ExamineTime = p.ExamineTime,
                ExamineUserId = p.ExamineUserId,
                FirstDateTime = p.CustomerItemGroupBM.FirstDateTime,
                Id = p.Id,
                ItemName = p.ItemName,
                ItemResultChar =p.Symbol=="P"?p.ItemDiagnosis: p.ItemResultChar,
                LoginDate = p.CustomerRegBM.LoginDate,
                MessageState = p.MessageState,
                MessageTime = p.MessageTime,
                MessageUserId = p.MessageUserId,
                Mobile = p.CustomerRegBM.Customer.Mobile,
                Name = p.CustomerRegBM.Customer.Name,
                Sex = p.CustomerRegBM.Customer.Sex,
                 cusRegId=p.CustomerRegId,
                  Unit=p.Unit,
                   CrisiContent=p.CrisiContent,
                  CrissMessageState=p.CustomerRegBM.CrissMessageState,
                  Isovertime=p.CrisisVisitSate==3?0: p.CrisisVisitSate == 2 ?0: nowtime > p.CustomerItemGroupBM.FirstDateTime?1:0
            }).OrderBy(p=> p.FirstDateTime).ToList();
            //收缩压舒张压都显示
            var ssyregid = cusMesss.Where(p => p.ItemName == "收缩压" || p.ItemName == "舒张压").ToList();
            foreach (var regid in ssyregid)
            {
                var p = _TjlCustomerRegItem.GetAll().FirstOrDefault(o => o.CustomerRegId == regid.cusRegId
                 && (o.ItemName == "收缩压" || o.ItemName == "舒张压") && o.ItemName != regid.ItemName);
                if (!cusMesss.Any(o => o.cusRegId == p.CustomerRegId && o.ItemName == p.ItemName))
                {
                    CrisisMessageDto noereg = new CrisisMessageDto();
                    if (p.CustomerRegBM == null)
                    {
                        continue;
                    }
                    noereg.ClientName = p.CustomerRegBM.ClientInfo == null ? "" : p.CustomerRegBM.ClientInfo.ClientName;
                    noereg.department = p.CustomerRegBM.Customer.Department;
                     noereg.IDCard = p.CustomerRegBM.Customer.IDCardNo;
                    noereg.Age = p.CustomerRegBM.Customer.Age;
                    noereg.BookingDate = p.CustomerRegBM.BookingDate;
                    noereg.CheckEmployeeBMId = p.CustomerItemGroupBM.CheckEmployeeBMId;
                    noereg.ClientInfoId = p.CustomerRegBM.ClientRegId;
                    noereg.CrisiChar = p.CrisiChar;
                    noereg.CrisisLever = p.CrisisLever;
                    noereg.CrisisVisitSate = p.CrisisVisitSate;
                    noereg.CustomerBM = p.CustomerRegBM.CustomerBM;
                    noereg.DepartmentId = p.DepartmentId;
                    noereg.ExamineTime = p.ExamineTime;
                    noereg.ExamineUserId = p.ExamineUserId;
                    noereg.FirstDateTime = p.CustomerItemGroupBM.FirstDateTime;
                    noereg.Id = p.Id;
                    noereg.ItemName = p.ItemName;
                    noereg.ItemResultChar = p.Symbol == "P" ? p.ItemDiagnosis : p.ItemResultChar;
                    noereg.LoginDate = p.CustomerRegBM.LoginDate;
                    noereg.MessageState = p.MessageState;
                    noereg.MessageTime = p.MessageTime;
                    noereg.MessageUserId = p.MessageUserId;
                    noereg.Mobile = p.CustomerRegBM.Customer.Mobile;
                    noereg.Name = p.CustomerRegBM.Customer.Name;
                    noereg.Sex = p.CustomerRegBM.Customer.Sex;
                    noereg.cusRegId = p.CustomerRegId;
                    noereg.CrissMessageState = p.CustomerRegBM.CrissMessageState;
                    noereg.Isovertime = p.CrisisVisitSate == 3 ? 0 : p.CrisisVisitSate == 2 ? 0 : nowtime > p.CustomerItemGroupBM.FirstDateTime ? 1 : 0;
                    noereg.Unit = p.Unit;
                    noereg.CrisiContent = p.CrisiContent;
                    cusMesss.Add(noereg);
                }
            }
            #region  

            #endregion

            return cusMesss.OrderBy(p => p.FirstDateTime).ToList(); 
        }
        /// <summary>
        /// 修改危急值状态
        /// </summary>
        /// <param name="entityDto"></param>
        public void UpCrisisSate(UpCricalStateDto entityDto)
        {
            var qure = _TjlCustomerRegItem.Get(entityDto.Id);
            qure.CrisisVisitSate = entityDto.Sate;
            qure.ExamineTime = System.DateTime.Now;
            qure.ExamineUserId = AbpSession.UserId;
            _TjlCustomerRegItem.Update(qure);

        }
        /// <summary>
        /// 修改危急值通知状态
        /// </summary>
        /// <param name="entityDto"></param>
        public void UpMessSate(UpCricalStateDto entityDto)
        {
            var qure = _TjlCustomerRegItem.Get(entityDto.Id);
            qure.MessageState = entityDto.Sate;
            qure.MessageTime= System.DateTime.Now;
            qure.MessageUserId= AbpSession.UserId;
            _TjlCustomerRegItem.Update(qure);

        }
    }
}
