using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto
{
    /// <summary>
    /// 1.职业健康档案信息
    /// </summary>
    public class OutOccCusInfoDto 
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string 体检编号 { get; set; }
        /// <summary>
        /// 健康档案类别默认101职业卫生
        /// </summary>
        [StringLength(32)]
        public virtual string 职业健康档案类别 { get; set; }
        /// <summary>
        /// 总工龄年
        /// </summary>
        [StringLength(16)]
        public virtual string 总工龄年 { get; set; }

        /// <summary>
        /// 总工龄月
        /// </summary>
        [StringLength(16)]
        public virtual string 总工龄月 { get; set; }

        /// <summary>
        /// 接害工龄年
        /// </summary>
        [StringLength(16)]
        public virtual string 接触工龄年 { get; set; }
        /// <summary>
        /// 接害工龄月
        /// </summary>
        [StringLength(16)]
        public virtual string 接触工龄月{ get; set; }
        /// <summary>
        /// 体检类型（检查类型（11初检，21复查）
        /// </summary>
        [StringLength(16)]
        public virtual string 检查类型 { get; set; }

        /// <summary>
        /// 体检类型编码（1上岗，2在岗，3离岗）
        /// </summary>
        [StringLength(16)]
        public virtual string 体检类型编码 { get; set; }

        /// <summary>
        /// 复查原体检号
        /// </summary>

        public virtual string 复检对应上次的职业健康档案编号 { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual string 体检时间 { get; set; }

        /// <summary>
        /// 填表人姓名
        /// </summary>

        public virtual string 填表人名称
        { get; set; }

        /// <summary>
        /// 填表人电话
        /// </summary>

        public virtual string 填表人电话
        { get; set; }

        /// <summary>
        ///填表日期（格式yyyy-MM-dd）
        /// </summary>
        public virtual string 填表日期 { get; set; }

        /// <summary>
        ///填表单位
        /// </summary>
        public virtual string 填表单位名称
        { get; set; }


        /// <summary>
        /// 体检报告时间 （格式yyyy-MM-dd） 
        /// </summary>
        public virtual string 体检报告时间
        { get; set; }


        /// <summary>
        ///主检结论
        /// </summary>
        public virtual string 主检结论
        { get; set; }

        /// <summary>
        ///监测类型代码（01-常规监测，02-主动监测，03-其他）
        /// </summary>
        public virtual string 监测类型代码 { get; set; }


        /// <summary>
        ///报告单位名称
        /// </summary>
        public virtual string 报告单位名称
        { get; set; }

        /// <summary>
        ///报告人姓名
        /// </summary>
        public virtual string 报告人姓名
        { get; set; }
        /// <summary>
        ///报告人电话
        /// </summary>
        public virtual string 报告人联系电话
        { get; set; }

        // <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string 备注
        { get; set; }

        /// <summary>
        ///主检建议
        /// </summary>
        public virtual string 主检建议
        { get; set; }
        /// <summary>
        ///主检医生
        /// </summary>
        public virtual string 主检医生
        { get; set; }

        /// <summary>
        ///体检机构名称
        /// </summary>
        public virtual string 体检机构编码
        { get; set; }
        /// <summary>
        ///体检机构代码
        /// </summary>
        public virtual string 体检机构名称
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
        ///用人单位电话
        /// </summary>
        public virtual string 用人单位联系电话
        { get; set; }


        /// <summary>
        ///工号
        /// </summary>
        public virtual string 劳动者工号
        { get; set; }

        /// <summary>
        ///姓名
        /// </summary>
        public virtual string 姓名
        { get; set; }


        /// <summary>
        /// 用户证件 IDCardType字典
        /// </summary>
        public virtual int? 证件类型 { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string 证件号码
        { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? 性别 { get; set; }

        /// <summary>
        /// 出生日期（格式yyyy-MM-dd）
        /// </summary>
        public virtual string 出生日期 { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? 婚姻状况 { get; set; }

        
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string 联系电话
        { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(32)]
        public virtual string 工种编码
        { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(16)]
        public virtual string 车间
        { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        [StringLength(32)]
        public virtual string 紧急联系电话
        { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [StringLength(32)]
        public virtual string 紧急联系人
        { get; set; }


    }
}
