using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy 
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class QueryAllForNumberDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分组id
        /// </summary>
        public virtual Guid ClientTeamInfo_Id { get; set; }
        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ClientRegDto ClientReg { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonTypeDto PersonnelCategory { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 体检类别 字典
        /// </summary>
        public virtual string ClientType { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 当前数据
        /// </summary>
        public virtual int? CurrentData { get; set; }
        /// <summary>
        /// 当前数据
        /// </summary>
        public virtual int? CurrentDatas { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        public int? RenCount { get; set; }
    
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }



#if Application
        [IgnoreMap]
#endif
        public virtual string GroupOrPersonal
        {
            get
            {
                if (ClientReg != null)
                {
                    return "单位";
                }
                else
                {
                    return "个人";
                }
            }

        }

    }
}
