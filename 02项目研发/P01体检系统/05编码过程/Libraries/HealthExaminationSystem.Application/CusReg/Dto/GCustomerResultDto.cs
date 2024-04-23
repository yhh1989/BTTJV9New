using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GCustomerResultDto
    {
        /// <summary>
        /// 社区机构编码
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// 社区机构名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 报告单编号
        /// </summary>
        public string RecordNo { get; set; }
        /// <summary>
        /// 报告单日期
        /// </summary>
        public string MeasureTime { get; set; }
        /// <summary>
        /// 人员
        /// </summary>
        public GMemberDto Member { get; set; }
        /// <summary>
        /// 身高体重
        /// </summary>
        public GHeightDto Height { get; set; }
        /// <summary>
        /// 脂肪
        /// </summary>
        public GMinFatDto MinFat { get; set; }
        /// <summary>
        /// 血压
        /// </summary>
        public GBloodPressureDto BloodPressure { get; set; }
        /// <summary>
        /// 血氧
        /// </summary>
        public GBoDto Bo { get; set; }
        /// <summary>
        /// 12导心电
        /// </summary>
        public GPEEcgDto PEEcg { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        public GTemperatureDto Temperature { get; set; }
        /// <summary>
        /// 腰臀比
        /// </summary>
        public GWhrDto Whr { get; set; }
        /// <summary>
        /// 血糖
        /// </summary>
        public GBloodSugarDto BloodSugar { get; set; }
        /// <summary>
        /// 尿酸
        /// </summary>
        public GUaDto Ua { get; set; }
        /// <summary>
        /// 总胆固醇
        /// </summary>
        public GCholDto Chol { get; set; }
        /// <summary>
        /// 血脂
        /// </summary>
        public GBloodFatDto BloodFat { get; set; }
        /// <summary>
        /// 血红蛋白
        /// </summary>
        public GHbDto Hb { get; set; }
        /// <summary>
        /// 尿液分析
        /// </summary>
        public GUrinalysisDto Urinalysis { get; set; }
        /// <summary>
        /// 体检查体信息
        /// </summary>
        public GMedicalInfoDto MedicalInfo { get; set; }
        


    }
}
