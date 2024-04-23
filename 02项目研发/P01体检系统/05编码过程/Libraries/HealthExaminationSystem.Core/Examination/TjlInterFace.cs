using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
  public  class TjlInterFace : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 对应的项目id
        /// </summary>   
        [MaxLength(64)]
        public string initemid { get; set; }

        /// <summary>
        /// 对应的项目名称
        /// </summary>
        [MaxLength(64)]
        public string initemname { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(64)]
        public string name { get; set; }
        

        /// <summary>
        /// 档案号
        /// </summary>   
        [MaxLength(64)]
        public string inactivenum { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        [MaxLength(64)]
        public string invalue { get; set; }

        /// <summary>
        /// 诊断（针对pacs，所见诊断在一条记录上）
        /// </summary>
        [MaxLength(3072)]
        public string initemchar { get; set; }

        /// <summary>
        /// 检查医生id
        /// </summary>
        [MaxLength(64)]
        public string indoctorid { get; set; }

        /// <summary>
        /// 检查医生姓名
        /// </summary>
        [MaxLength(64)]
        public string indoctorname { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        [MaxLength(64)]
        public string inSHdoctorid { get; set; }

        /// <summary>
        /// 审核医生名称
        /// </summary>
        [MaxLength(64)]
        public string inSHdoctorname { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
       
        public DateTime? checkdate { get; set; }

        /// <summary>
        /// 仪器id
        /// </summary>
        [MaxLength(64)]
        public string inYQid { get; set; }

        /// <summary>
        /// 图片路径（可空）
        /// </summary>
        [MaxLength(640)]
        public string inPicDirs { get; set; }

        /// <summary>
        /// 参考值（lis不可空，其他可空）
        /// </summary>
        [MaxLength(64)]
        public string xmckz { get; set; }

        /// <summary>
        /// 项目标示（lis不可空，其他可空）
        /// </summary>
        [MaxLength(64)]
        public string xmbs { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        [MaxLength(64)]
        public string xmdw { get; set; }

        /// <summary>
        /// 条码号（lis不可空，其他可空）
        /// </summary>
        [MaxLength(64)]
        public string barnum { get; set; }

        /// <summary>
        /// 转换状态（默认为1，转换完写2）
        /// </summary>
        public int? resultstate { get; set; }

        /// <summary>
        /// 转换时间
        /// </summary>       
        public DateTime? resultdate { get; set; }


        public int? idnum { get; set; }

        /// <summary>
        /// 危急值
        /// </summary>
        [MaxLength(64)]
        public string wjz { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        [MaxLength(64)]
        public string Code { get; set; }
        [MaxLength(640)]
        public string msg { get; set; }
    }
}
