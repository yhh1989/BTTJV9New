using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 查询合同分类条件输入数据传输对象
    /// </summary>
    public class QueryContractCategoryConditionInput
    {
        /// <summary>
        /// 启用的
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 合同列表名称
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string Name { get; set; }
    }
}