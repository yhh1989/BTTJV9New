using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect
{
    /// <summary>
    /// 预约汇总
    /// </summary>
    [AbpAuthorize]
    public class FrmMakeCollectAppService : MyProjectAppServiceBase,IFrmMakeCollectAppService
    {
        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository; //体检人预约登记信息表

        private readonly IRepository<TjlCustomerReg, Guid> _CustomerRepository; //体检人预约登记信息表bo
      
        public FrmMakeCollectAppService
        (
            IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
            IRepository<TjlCustomerReg, Guid> CustomerRepository
        )
        {
            _customerItemGroupRepository = customerItemGroupRepository;
            _CustomerRepository = CustomerRepository;
        }

        public List<ShowMakeCollectDto> GetShowMakeCollects(ShowMakeCollectDto input)
        {
            var query = _CustomerRepository.GetAll();
            if (input.StartDataTime != null)
            {
                query = query.Where(o => o.BookingDate >= input.StartDataTime.Value);
            }
            if (input.EndDataTime != null)
            {
                query = query.Where(o => o.BookingDate <= input.EndDataTime.Value);
            }
            var reglist = query.Where(o => o.BookingStatus == 1 && o.RegisterState == 1).
                GroupBy(o =>new  { BookingDate= o.BookingDate.Value.Year + "-" + o.BookingDate.Value.Month + "-" + o.BookingDate.Value.Day }).Select(o => new
                ShowMakeCollectDto
                {
                    BookingDate=o.Key.BookingDate,
                    allcout = o.Count(),
                    xs = o.Where(n => n.InfoSource == 1).Count(),
                    xx = o.Where(n => n.InfoSource == 2).Count(),
                    gr = o.Where(n => n.ClientInfoId == null).Count(),
                    dw = o.Where(n => n.ClientInfoId != null).Count(),
                    departlist = o.SelectMany(n => n.CustomerItemGroup).Where(r => r.IsAddMinus != 3).
                    Select(t=>new { t.DepartmentName,t.CustomerRegBMId }).Distinct().
              GroupBy(p => p.DepartmentName).Select(w => new ClientInfoMakeCollectDto { departname = w.Key, count = w.Count() }).ToList()
                });

            // var result = reglist.ToList();
             return reglist.ToList();         
        }
        /// <summary>
        /// 科室数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutDepartCusDto> getDepartCount(InIdDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var que = _customerItemGroupRepository.GetAll().Where(o =>( o.CustomerRegBM.BookingStatus == 1 || o.CustomerRegBM.BespeakState==-1) && o.IsAddMinus != (int)AddMinusType.Minus && o.IsDeleted==false);
                if (input.BookingDate.HasValue)
                {
                    var strdate =DateTime.Parse( input.BookingDate.Value.ToShortDateString());
                    var enddate = DateTime.Parse(input.BookingDate.Value.AddDays(1).ToShortDateString());
                    que = que.Where(o => o.CustomerRegBM.BookingDate >= strdate && o.CustomerRegBM.BookingDate < enddate);
                }
                if (input.ClienRegId.HasValue)
                {
                    que = que.Where(o => o.CustomerRegBM.ClientRegId == input.ClienRegId);
                }
                if (input.RegState.HasValue)
                {
                    switch (input.RegState)
                    {
                        case 1:
                            que = que.Where(o => o.CustomerRegBM.BookingStatus == 1 && o.CustomerRegBM.RegisterState == 1  && o.CustomerRegBM.IsDeleted==false);
                            break;
                        case 2:
                            que = que.Where(o => o.CustomerRegBM.BookingStatus == 1 && o.CustomerRegBM.RegisterState == 2 && o.CustomerRegBM.IsDeleted == false);
                            break;
                        case 3:
                            que = que.Where(o => o.CustomerRegBM.BookingStatus == -1  );
                            break;
                    }
                   
                }
                if (input.RegType.HasValue)
                {
                    que = que.Where(o => o.CustomerRegBM.InfoSource == input.RegType);
                }
                if (input.TimeSoft.HasValue)
                {
                    switch (input.TimeSoft)
                    {
                        case 1:
                            que = que.Where(o => o.CustomerRegBM.BookingDate.Value.Hour>=7 && o.CustomerRegBM.BookingDate.Value.Hour<=9);
                            break;
                        case 2:
                            que = que.Where(o => o.CustomerRegBM.BookingDate.Value.Hour > 9 && o.CustomerRegBM.BookingDate.Value.Hour <= 10);
                            break;
                        case 3:
                            que = que.Where(o => o.CustomerRegBM.BookingDate.Value.Hour > 10 && o.CustomerRegBM.BookingDate.Value.Hour <= 11);
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(input.SearchName))
                {
                    que = que.Where(o =>o.CustomerRegBM.CustomerBM== input.SearchName || o.CustomerRegBM.Customer.Name== input.SearchName
                    || o.CustomerRegBM.Customer.Mobile == input.SearchName);
                }
                var depCount = que.GroupBy(o => new { o.DepartmentName, o.ItemGroupName ,o.DepartmentId,o.ItemGroupBM_Id}).Select(o => new OutDepartCusDto
                {
                    deparTname = o.Key.DepartmentName,
                    departCount = o.Where(n => n.DepartmentName == o.Key.DepartmentName).Count(),
                    groupName = o.Key.ItemGroupName,
                    groupCoutn = o.Count(),
                    deparTnameCount=o.Key.DepartmentName + "("+ o.Where(n => n.DepartmentName == o.Key.DepartmentName ).Count() + "人)",
                     DepartId=o.Key.DepartmentId,
                      GroupId=o.Key.ItemGroupBM_Id
                }).ToList();
                return depCount;
            }
        }
        /// <summary>
        /// 获取预约人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutRegCusListDto> getRegCusLis(InIdDto input)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                var que = _CustomerRepository.GetAll().Where(o => o.CustomerItemGroup.Any(n => n.IsAddMinus != (int)AddMinusType.Minus));
                if (input.BookingDate.HasValue)
                {
                    var strdate = DateTime.Parse(input.BookingDate.Value.ToShortDateString());
                    var enddate = DateTime.Parse(input.BookingDate.Value.AddDays(1).ToShortDateString());
                    que = que.Where(o => o.AppointmentTime >= strdate && o.AppointmentTime < enddate);
                }
                if(input.BookingStar.HasValue)
                {
                    que = que.Where(o => o.AppointmentTime >= input.BookingStar);
                }
                if (input.BookingEnd.HasValue)
                {
                    que = que.Where(o => o.AppointmentTime < input.BookingEnd);
                }
                if (input.ClienRegId.HasValue)
                {
                    que = que.Where(o => o.ClientRegId == input.ClienRegId);
                }
                if (input.RegState.HasValue)
                {
                    switch (input.RegState)
                    {
                        case 1:
                            que = que.Where(o => o.BookingStatus == 1 && o.RegisterState == 1 && o.IsDeleted == false);
                            break;
                        case 2:
                            que = que.Where(o => o.BookingStatus == 1 && o.RegisterState == 2 && o.IsDeleted == false);
                            break;
                        case 3:
                            que = que.Where(o => o.BookingStatus == -1);
                            break;
                    }

                }
                if (input.RegType.HasValue)
                {
                    que = que.Where(o => o.InfoSource == input.RegType);
                }
              
                if (input.TimeSoft.HasValue)
                {
                    switch (input.TimeSoft)
                    {
                        case 1:
                            que = que.Where(o => o.AppointmentTime.Value.Hour >= 7 && o.AppointmentTime.Value.Hour <= 9);
                            break;
                        case 2:
                            que = que.Where(o => o.AppointmentTime.Value.Hour > 9 && o.AppointmentTime.Value.Hour <= 10);
                            break;
                        case 3:
                            que = que.Where(o => o.AppointmentTime.Value.Hour > 10 && o.AppointmentTime.Value.Hour <= 11);
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(input.SearchName))
                {
                    que = que.Where(o=> o.Customer.Name == input.SearchName);
                }
                if (!string.IsNullOrEmpty(input.customerBM))
                {
                    que = que.Where(o => o.CustomerBM == input.customerBM);
                }
                if (!string.IsNullOrEmpty(input.IdCard))
                {
                    que = que.Where(o => o.Customer.IDCardNo == input.IdCard);
                }
                if (input.InfoSource.HasValue)
                {
                    que = que.Where(o => o.InfoSource == input.InfoSource);
                }
                if (!string.IsNullOrEmpty(input.OrderNum))
                {
                    que = que.Where(o => o.OrderNum == input.OrderNum);
                }
                if (!string.IsNullOrEmpty(input.Moblie))
                {
                    que = que.Where(o => o.Customer.Mobile == input.Moblie);
                }
                if (!string.IsNullOrEmpty(input.UserName))
                {
                    que = que.Where(o => o.Introducer == input.UserName);
                }
                if (input.DepartId.HasValue)
                {
                    que = que.Where(o => o.CustomerItemGroup.Any(n => n.DepartmentId == input.DepartId) );
                }
                if (input.GroupId.HasValue)
                { que = que.Where(o => o.CustomerItemGroup.Any(n => n.ItemGroupBM_Id == input.GroupId)); }
                var cuslist = que.Select(o => new OutRegCusListDto {
                 Age=o.Customer.Age,
                 Birthday=o.Customer.Birthday,
                 ClientName=o.ClientInfo.ClientName,
                 BookingDate=o.BookingDate,
                  AppointmentTime=o.AppointmentTime,
                 CustomerBM=o.CustomerBM,
                 IDCardNo=o.Customer.IDCardNo,
                 InfoSource=o.InfoSource,
                 Mobile=o.Customer.Mobile,
                 Name=o.Customer.Name,
                 PersonnelCategoryId=o.PersonnelCategoryId,
                 Sex=o.Customer.Sex,
                 State= o.RegisterState==2?"已登记":"未登记",
                 Introducer=o.Introducer,
                 OrderNum=o.OrderNum,
                 SuitName=o.ItemSuitName}).ToList();
                return cuslist;
            //}

        }

    }
}
