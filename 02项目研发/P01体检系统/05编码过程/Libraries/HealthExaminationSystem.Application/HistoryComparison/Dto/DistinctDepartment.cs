using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    /// <summary>
    /// 去重用的组合
    /// </summary>
    public class DistinctDepartment
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int? OrderNum { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (Id != Guid.Empty)
            {
                return Id.GetHashCode();
            }
            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is DistinctDepartment division)
            {
                if (Id != Guid.Empty)
                {
                    return Id == division.Id;
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
        public static bool operator ==(DistinctDepartment a1, DistinctDepartment a2)
        {
            return a1?.Id == a2?.Id;
        }

        /// <summary>
        /// 比较两个对象是否不相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>不相等则为 True，否则为 False。</returns>
        public static bool operator !=(DistinctDepartment a1, DistinctDepartment a2)
        {
            return !(a1 == a2);
        }
    }
}
