#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DepDto
    {
        /// <summary>
        /// 编码 
        /// </summary>
        [StringLength(64)]
        public virtual string DepartmentBM { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他
        /// </summary>
        [StringLength(64)]
        public virtual string Category { get; set; }

        /// <summary>
        /// 部门职责
        /// </summary>
        [StringLength(64)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 最大日检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }

        /// <summary>
        /// 贵宾科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 女科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string WomenAddress { get; set; }

        /// <summary>
        /// 男科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string MenAddress { get; set; }

        /// <summary>
        /// 小结格式
        /// </summary>
        [StringLength(512)]
        public virtual string SumFormat { get; set; }

    }
}
