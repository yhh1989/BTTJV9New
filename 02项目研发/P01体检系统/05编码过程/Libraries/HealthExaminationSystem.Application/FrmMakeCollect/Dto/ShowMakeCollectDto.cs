using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>

    public class ShowMakeCollectDto
    {

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual string BookingDate { get; set; }
        /// <summary>
        /// 预约人数
        /// </summary>
        public int? allcout { get; set; }
        /// <summary>
        /// 线上预约
        /// </summary>
        public int? xs { get; set; }
        /// <summary>
        /// 线下预约
        /// </summary>
        public int? xx { get; set; }
        /// <summary>
        /// 个人预约总人数
        /// </summary>
        public int? gr { get; set; }
        /// <summary>
        /// 团体预约总人数
        /// </summary>
        public int? dw { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        //public string departlist { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDataTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDataTime { get; set; }

        public  List<ClientInfoMakeCollectDto> departlist { get;set; }

        
    }
}
