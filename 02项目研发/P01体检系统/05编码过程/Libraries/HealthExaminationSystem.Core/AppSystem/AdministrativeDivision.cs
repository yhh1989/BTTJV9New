using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class AdministrativeDivision : Entity<Guid>
    {
        /// <summary>
        /// 代码
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (!string.IsNullOrWhiteSpace(Code))
            {
                return Code.GetHashCode();
            }
            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is AdministrativeDivision division)
            {
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    return Code == division.Code;
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>相等则为 True，否则为 False。</returns>
        public static bool operator ==(AdministrativeDivision a1, AdministrativeDivision a2)
        {
            return a1?.Code == a2?.Code;
        }

        /// <summary>
        /// 比较两个对象是否不相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>不相等则为 True，否则为 False。</returns>
        public static bool operator !=(AdministrativeDivision a1, AdministrativeDivision a2)
        {
            return !(a1 == a2);
        }
    }
}