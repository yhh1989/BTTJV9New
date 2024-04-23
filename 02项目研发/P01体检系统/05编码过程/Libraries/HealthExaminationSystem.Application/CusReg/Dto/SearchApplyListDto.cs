using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class SearchApplyListDto
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        public string APPLY_NO { get; set; }
        /// <summary>
        /// 档案ID
        /// </summary>
        public string ARCHIVE_ID { get; set; }
        /// <summary>
        /// 申请日期 格式2014-05-31
        /// </summary>
        public DateTime? APPLY_TIME_START { get; set; }
        /// <summary>
        /// 申请日期 格式2014-05-31
        /// </summary>
        public DateTime? APPLY_TIME_END { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string SICK_NAME { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string ARCHIVE_CODE { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDENTITY_CARD_NO { get; set; }
        /// <summary>
        /// 起始年龄
        /// </summary>
        public string SICK_AGE_START { get; set; }
        /// <summary>
        /// 结束年龄
        /// </summary>
        public string SICK_AGE_END { get; set; }
        /// <summary>
        /// 性别（男或则女）
        /// </summary>
        public string SICK_SEX { get; set; }
    

    }
}
