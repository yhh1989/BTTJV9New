using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Models
{
    /// <summary>
    /// 报表数据对象
    /// </summary>
    public class ReportDataObject
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        public List<MasterDataObject> Master { get; set; }

        /// <summary>
        /// 记录集
        /// </summary>
        public List<DetailDataObject> Detail { get; set; }
    }

    /// <summary>
    /// 报表数据对象
    /// </summary>
    /// <typeparam name="TMaster"></typeparam>
    /// <typeparam name="TDetail"></typeparam>
    public class ReportDataObject<TMaster, TDetail>
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        public List<TMaster> Master { get; set; }

        /// <summary>
        /// 记录集
        /// </summary>
        public List<TDetail> Detail { get; set; }
    }

    /// <summary>
    /// 报表数据对象
    /// </summary>
    /// <typeparam name="TMaster"></typeparam>
    public class ReportDataObjectOnlyMaster<TMaster>
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        public List<TMaster> Master { get; set; }
    }

    /// <summary>
    /// 报表数据对象
    /// </summary>
    /// <typeparam name="TDetail"></typeparam>
    public class ReportDataObjectOnlyDetail<TDetail>
    {
        /// <summary>
        /// 记录集
        /// </summary>
        public List<TDetail> Detail { get; set; }
    }
}