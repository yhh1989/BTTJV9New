using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using Sw.Hospital.HealthExaminationSystem.Core.Company;

namespace Sw.Hospital.HealthExaminationSystem.Application.OutInspects
{
    [AbpAuthorize]
    public class OutInspectsQueryAppService : MyProjectAppServiceBase, IOutInspectsQueryAppService
    {
        private readonly IRepository<TjlCustomer, Guid> _jlCustomer; //用户表
        private readonly IRepository<TjlCustomerReg, Guid> _jlCustomerReg; //预约表
        private readonly IRepository<TjlCustomerRegItem, Guid> _jlCustomerRegItem;//登记项目表
        private readonly IRepository<TjlClientTeamRegitem, Guid> _jlClientTeamRegitem;//分组项目信息表
        public OutInspectsQueryAppService(
            IRepository<TjlCustomer, Guid> jlCustomer,
            IRepository<TjlCustomerReg, Guid> jlCustomerReg,
            IRepository<TjlCustomerRegItem, Guid> jlCustomerRegItem,
            IRepository<TjlClientTeamRegitem, Guid> jlClientTeamRegitem)

        {
            _jlCustomer = jlCustomer;
            _jlCustomerReg = jlCustomerReg;
            _jlCustomerRegItem = jlCustomerRegItem;
            _jlClientTeamRegitem = jlClientTeamRegitem;
        }
        /// <summary>
        /// 外出体检查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutinspectsQueryDto> OutinspectsQuery(OutCusInfoDto input)
        {
            var que = _jlCustomerReg.GetAll();
            //登记号
            if (!string.IsNullOrWhiteSpace(input.CustomerRegNum.ToString()))
            {
                que = que.Where(p => p.CustomerRegNum == input.CustomerRegNum);
            }
            //体检号
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                que = que.Where(p => p.CustomerBM == input.CustomerBM);
            }
            //开始结束时间
            if (input.enddate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.enddate);
            }
            if (input.startdate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.startdate);
            }
            //条码状态
            if (input.BarState.HasValue)
            {
                que = que.Where(p => p.BarState >= input.BarState);
            }
            //体检状态
            if (input.CheckSate.HasValue)
            {
                que = que.Where(p => p.CheckSate == input.CheckSate);
            }
            //总检状态
            if (input.SummSate.HasValue)
            {
                que = que.Where(p => p.SummSate == input.SummSate);
            }
            //姓名性别年龄
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                que = que.Where(p => p.Customer.Name == input.Name);
            }
            if (!string.IsNullOrWhiteSpace(input.Age.ToString()))
            {
                que = que.Where(p => p.Customer.Age == input.Age);
            }
            if (!string.IsNullOrWhiteSpace(input.Sex.ToString()))
            {
                que = que.Where(p => p.Customer.Sex  == input.Sex);
            }
            //体检日期
            if (input .BookingDate.HasValue)
            {
                que = que.Where(p => p.BookingDate == input.BookingDate);
            }
            //挂号科室
            if (input.NucleicAcidType.HasValue)
            {
                que = que.Where(p => p.NucleicAcidType == input.NucleicAcidType);
            }
            var result = que.Select(o => new OutinspectsQueryDto
            {
                NucleicAcidType = o.NucleicAcidType,
                CustomerRegNum = o.CustomerRegNum,
                CustomerBM = o.CustomerBM,
                Name = o.Customer.Name,
                Sex = o.Customer.Sex == 1 ? "男" : "女",
                Age = o.Customer.Age,
                IDCardNo = o.Customer.IDCardNo,
                LoginDate = o.LoginDate,
                
               CheckSate = o.CheckSate,
                SummSate=o.SummSate,
                BarState=o.BarState,
                BookingDate=o.BookingDate,
                ItemSuitName=o.ItemSuitName,
                 Id=o.Id
                
            }).OrderBy(o=>o.LoginDate).ThenBy(o=>o.CustomerRegNum).ToList();          
            return result.MapTo<List<OutinspectsQueryDto>>();
        }

    }
}
