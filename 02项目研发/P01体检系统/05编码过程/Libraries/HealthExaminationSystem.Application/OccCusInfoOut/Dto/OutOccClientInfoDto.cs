using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
    /// <summary>
    /// 2.职业健康档案-用人单位信息
    /// </summary>
    public class OutOccClientInfoDto
    {

        /// <summary>
        /// 体检号
        /// </summary>       
        public virtual string 体检编号
        { get; set; }

        /// <summary>
        ///用人单位统一社会信用代码
        /// </summary>
        public virtual string 用人单位统一社会信用代码
        { get; set; }


        /// <summary>
        ///用人单位名称
        /// </summary>
        public virtual string 用人单位名称
        { get; set; }

        /// <summary>
        /// 经济类型
        /// </summary>
        public int? 经济类型编码
 { get; set; }
        /// <summary>
        /// 企业规模
        /// </summary>
        public int? 企业规模
 { get; set; }


        /// <summary>
        /// 行业
        /// </summary>      
        public virtual string 行业编码 { get; set; }
        /// <summary>
        /// 所属区
        /// </summary>       
        public virtual string 所属地区编码
        { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>       
        public virtual string 通讯地址
        { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>       
        public virtual string 邮政编码
        { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>      
        public virtual string 用人单位联系人
        { get; set; }


        /// <summary>
        /// 企业负责人
        /// </summary>       
        public virtual string 用人单位联系电话
        { get; set; }


        /// <summary>
        /// 用人单位所在区名称
        /// </summary>       
        public virtual string 用人单位所在区名称
        { get; set; }
    }
}
