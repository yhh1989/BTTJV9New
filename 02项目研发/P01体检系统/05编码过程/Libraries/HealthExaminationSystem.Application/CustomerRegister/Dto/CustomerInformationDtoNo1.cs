using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto
{
    /// <summary>
    /// 体检人信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Sw.Hospital.HealthExaminationSystem.Core.Examination.TjlCustomer))]
#endif
    public class CustomerInformationDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        /// <remarks>
        /// 人员信息是一个基础表，患者的每一次体检都使用这条信息，年龄没有任何用处
        /// <para>患者的每一次体检时年龄时固定的，所以年龄应该存在于体检人预约登记表</para>
        /// <para>标明患者当次体检时的年龄</para>
        /// </remarks>
        [Obsolete("停用准备删除字段。")]
        public virtual int? Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 行业 字典
        /// </summary>
        [StringLength(64)]
        public virtual string CustomerTrade { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(64)]
        public string Department { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(16)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 个人照片
        /// </summary>
        public virtual Guid? CusPhotoBmId { get; set; }

#if Proxy
        /// <summary>
        /// 格式化性别
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public virtual string SexFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Sex.ToString()))
                {
                    if (int.TryParse(Sex.ToString(), out var result))
                    {
                        return HealthExaminationSystem.Common.Helpers.EnumHelper.GetEnumDesc((Sex)result);
                    }
                }

                return string.Empty;
            }
        }
#endif
    }
}