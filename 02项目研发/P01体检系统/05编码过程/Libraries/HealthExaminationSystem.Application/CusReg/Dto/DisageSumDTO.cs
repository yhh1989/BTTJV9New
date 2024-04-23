using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    public class DisageSumDTO
    {
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string SummarizeName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int Cout { get; set; }

    }
}
