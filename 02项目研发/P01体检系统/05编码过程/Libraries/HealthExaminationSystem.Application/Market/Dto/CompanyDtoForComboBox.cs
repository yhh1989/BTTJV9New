using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 单位数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Company.TjlClientInfo))]
#endif
    public class CompanyDtoForComboBox : CompanyDtoForComplaint
    {
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(128)]
        public virtual string HelpCode { get; set; }
    }
}