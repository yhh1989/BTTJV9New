using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    public class GMinFatDto
    {
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 脂肪率
        /// </summary>
        public string FatMass { get; set; }
        /// <summary>
        /// 水分含量
        /// </summary>
        public string Water { get; set; }
        /// <summary>
        /// 水分率
        /// </summary>
        public string WaterRate { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 体重指数
        /// </summary>
        public string Bmi { get; set; }
        /// <summary>
        /// 基础代谢
        /// </summary>
        public string BasicMetabolism { get; set; }
        /// <summary>
        /// 体质指数判断结果：1偏低 2标准 3 偏高 4 高
        /// </summary>
        public string TiZhiResult { get; set; }
        /// <summary>
        /// 体型判断结果 1消瘦 2标准 3 隐藏性肥胖 4 肌肉性肥胖/健壮 5肥胖
        /// </summary>
        public string TiXingResult { get; set; }
    }
}
