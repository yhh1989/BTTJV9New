
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using AutoMapper;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人预约登记信息表  
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CustomerGroupSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目组合名称
        /// </summary>    
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual int? FormatGiveUp
        {
            get
            {
                if (CheckState.HasValue &&  CheckState == 4)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }

        }
        /// <summary>
        /// 组合诊断 bxy
        /// </summary>     
        public virtual string ItemGroupDiagnosis { get; set; }

        /// <summary>
        /// 审核备注 
        /// </summary>
        [StringLength(256)]
        public virtual string SumRemark { get; set; }


        /// <summary>
        /// 体检人项目
        /// </summary>
        public virtual List<CustomerRegisterItemDto> CustomerRegItem { get; set; }
    }
}
