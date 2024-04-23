using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
    public class SaveCusDto: Abp.Application.Services.Dto.EntityDto<Guid>
    {
         
            /// <summary>
            /// 体检号
            /// </summary>

            public virtual string CustomerBM { get; set; }


            

            /// <summary>
            /// 婚姻状况 1未婚2结婚
            /// </summary>
            public virtual int? MarriageStatus { get; set; }

            /// <summary>
            /// 文化程度 字典
            /// </summary>
            public virtual int? Degree { get; set; }

            /// <summary>
            /// 职务
            /// </summary>
            [StringLength(16)]
            public virtual string Duty { get; set; }

            /// <summary>
            /// 移动电话
            /// </summary>
            [StringLength(16)]
            public virtual string Mobile { get; set; }

            /// <summary>
            /// 照射种类
            /// </summary>         
            public virtual string RadiationName { get; set; }         


       
    }
}
