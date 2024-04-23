using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
    public class SearchItemInfoDto 
    {
        public virtual Guid? DepartmentId { get; set; }

        public virtual string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int? IsActive { get; set; }
    }
}