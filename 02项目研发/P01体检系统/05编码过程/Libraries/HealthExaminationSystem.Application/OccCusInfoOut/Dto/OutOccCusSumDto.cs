using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
    /// <summary>
    /// 6.职业健康档案-体检结论
    /// </summary>
    public class OutOccCusSumDto
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string 体检编号
        { get; set; }
        /// <summary>
        /// （1未见异常，2复查，3疑似，4禁忌，5其他）
        /// </summary>
        [StringLength(5000)]
        public virtual string 体检结论编码
 { get; set; }

        /// <summary>
        /// 复查项目编码（体检结论复查时填写）
        /// </summary>
        [StringLength(5000)]
        public virtual string 需复查的检查项目编码
 { get; set; }
        /// <summary>
        /// （体检结论复查时填写）
        /// </summary>
        [StringLength(5000)]
        public virtual string 需复查的其他检查项目名称
 { get; set; }

        /// <summary>
        /// （体检结论职业禁忌证、疑似职业健康时填写）
        /// </summary>       
        public virtual string 危害因素编码
 { get; set; }

        /// <summary>
        /// （危害因素为其他时填写）
        /// </summary>       
        public virtual string 其他危害因素具体名称
 { get; set; }


        /// <summary>
        /// （体检结论职业禁忌证时填写）
        /// </summary>
        public virtual string 职业禁忌证编码
 { get; set; }

        /// <summary>
        /// （体检结论疑似职业健康时填写）
        /// </summary>
        public virtual string 疑似职业健康编码
 { get; set; }

        /// <summary>
        /// （体检结论其他疾病时填写）
        /// </summary>
        public virtual string 其他疾病名称

        { get; set; }
    }
}
