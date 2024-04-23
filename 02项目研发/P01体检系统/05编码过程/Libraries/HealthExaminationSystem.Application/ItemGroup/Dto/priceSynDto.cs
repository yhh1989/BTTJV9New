using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmPriceSynchronize))]
#endif
    public class priceSynDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary>  
        [StringLength(64)]
        public string chkit_id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(64)]
        public string chkit_name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [StringLength(128)]
        public string chkit_fmt { get; set; }

        /// <summary>
        /// 助记符
        /// </summary>
        [StringLength(64)]
        public string chkit_id2 { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(64)]
        public string aut_name { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(64)]
        public string ut_id { get; set; }
     
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? chkit_costn { get; set; }
        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime? upd_date { get; set; }
        /// <summary>
        /// 项目类别
        /// </summary>    
        public string chkit_Type { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string chkit_TypeName
        {
            get
            {//C  Q   M   S  分别代表  项目  材料   药品   申请单组合
                if (chkit_Type != null && chkit_Type != "")
                {
                    switch (chkit_Type)
                    {
                        case "C":
                            return "项目";
                        case "Q":
                            return "材料";
                        case "M":
                            return "药品";
                        case "S":
                            return "申请单组合";
                        default:
                            return chkit_Type;
                            break;
                    }
                }
                else
                {
                    return "";
                }
            }

        }
       
    }
}
