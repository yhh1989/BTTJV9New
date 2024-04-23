using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{/// <summary>
 /// 5.职业健康档案-检查项目
 /// </summary>
    public class OutOccCusItemDto
    {
        
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string 体检编号
        { get; set; }


        /// <summary>
        /// 检查科室
        /// </summary>
        public virtual string 检查科室
        { get; set; }

        /// <summary>
        /// 体检项目编号
        /// </summary>
        [StringLength(3072)]
        public virtual string 体检项目编号 { get; set; }

        /// <summary>
        /// 其他项目名称
        /// </summary>
        [StringLength(3072)]
        public virtual string 其他项目名称
 { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(3072)]
        public virtual string 项目组合名称
        { get; set; }


        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(16)]
        public virtual string 计量单位
 { get; set; }

        /// <summary>
        /// 参考值最大值
        /// </summary>
        [StringLength(256)]
        public virtual string 参考范围最大值
 { get; set; }
        /// <summary>
        /// 参考值最小值
        /// </summary>
        [StringLength(256)]
        public virtual string 参考范围最小值 { get; set; }

        

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string 检查项目结果
        { get; set; }

        /// <summary>
        /// （合格、不合格）
        /// </summary>
        [StringLength(16)]
        public virtual string 合格标记
 { get; set; }
        /// <summary>
        /// 检查时间（yyyy-MM-dd）
        /// </summary>
        public virtual DateTime? 检查日期
 { get; set; }
        /// <summary>
        /// 检查医生
        /// </summary>
        public virtual string 检查医生
        { get; set; }

        /// <summary>
        /// 检查结果类别，默认2
        /// </summary>
        public virtual string 检查结果类别编码
{ get; set; }

    }
}
