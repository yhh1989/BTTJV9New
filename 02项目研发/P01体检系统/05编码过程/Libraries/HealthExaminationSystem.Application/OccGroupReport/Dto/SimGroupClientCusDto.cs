using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;

#endif

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{

    public class SimGroupClientCusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid TjlClientReg_Id { get; set; }

        public virtual Guid? ClientTeamInfoId { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }


        /// <summary>
        /// 分组名称
        /// </summary>

        public virtual string TeamName { get; set; }



        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 岁
        /// </summary>
        [StringLength(2)]
        public virtual string AgeUnit { get; set; }      

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }

    }
}
