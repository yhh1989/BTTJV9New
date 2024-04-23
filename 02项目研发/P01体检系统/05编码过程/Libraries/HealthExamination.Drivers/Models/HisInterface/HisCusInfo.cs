using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{
   public  class HisCusInfo
    {
        /// <summary>
        /// 患者ID
        /// </summary>       
        public virtual string BLH { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>       
        public virtual string Name { get; set; }

        /// <summary>
        ///  年龄1男2女
        /// </summary>
        public virtual int? Sex { get; set; }      

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }
       

        /// <summary>
        /// 身份证号
        /// </summary>      
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>      
        public virtual string Mobile { get; set; }       

        /// <summary>
        /// 通讯地址
        /// </summary>       
        public virtual string Address { get; set; }

        /// <summary>
        /// 就诊卡
        /// </summary>      
        public virtual string VisitCard { get; set; }

        /// <summary>
        /// 医保卡
        /// </summary>      
        public virtual string MedicalCard { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>       
        public virtual string SectionNum { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>      
        public virtual string HospitalNum { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>      
        public virtual string ut_id { get; set; }

        


    }
}
