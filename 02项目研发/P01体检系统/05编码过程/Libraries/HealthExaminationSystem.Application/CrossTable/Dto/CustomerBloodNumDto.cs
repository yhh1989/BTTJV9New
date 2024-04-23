using System;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerBloodNum))]
#endif
    public class CustomerBloodNumDto: EntityDto<Guid>
    {
        /// <summary>
        /// 抽血时间
        /// </summary>
        public virtual DateTime? BloodDate { get; set; }

        /// <summary>
        /// 抽血号
        /// </summary>
        public virtual string BloodNum { get; set; }

        /// <summary>
        /// 预约记录外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 预约id
        /// </summary>
        public virtual CustomerRegForCrossTableDto CustomerReg { get; set; }


        /// <summary>
        /// 抽血人
        /// </summary>
        public virtual UserViewDto EmployeeBM { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public virtual string Ip { get; set; }

        /// <summary>
        /// 抽血状态 1已抽血2已取消
        /// </summary>
        public virtual int? BloodSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }
    }
}