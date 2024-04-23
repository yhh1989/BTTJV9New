using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sw.Hospital.HealthExamination.Drivers.Models.LisInterface
{
    [Table("TInterfaceresult")]
    public class TInterfaceresult
    {
        /// <summary>
        /// 对应的项目id
        /// </summary>
        public string initemid { get; set; }

        /// <summary>
        /// 对应的项目名称
        /// </summary>
        public string initemname { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        public string inactivenum { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public string invalue { get; set; }

        /// <summary>
        /// 诊断（针对pacs，所见诊断在一条记录上）
        /// </summary>
        public string initemchar { get; set; }

        /// <summary>
        /// 检查医生id
        /// </summary>
        public string indoctorid { get; set; }

        /// <summary>
        /// 检查医生姓名
        /// </summary>
        public string indoctorname { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public string inSHdoctorid { get; set; }

        /// <summary>
        /// 审核医生名称
        /// </summary>
        public string inSHdoctorname { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? checkdate { get; set; }

        /// <summary>
        /// 仪器id
        /// </summary>
        public string inYQid { get; set; }

        /// <summary>
        /// 图片路径（可空）
        /// </summary>
        public string inPicDirs { get; set; }

        /// <summary>
        /// 参考值（lis不可空，其他可空）
        /// </summary>
        public string xmckz { get; set; }

        /// <summary>
        /// 项目标示（lis不可空，其他可空）
        /// </summary>
        public string xmbs { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        public string xmdw { get; set; }

        /// <summary>
        /// 条码号（lis不可空，其他可空）
        /// </summary>
        public string barnum { get; set; }

        /// <summary>
        /// 体检人预约id（可空）
        /// </summary>
        public string customerregid { get; set; }

        /// <summary>
        /// 转换状态（默认为1，转换完写2）
        /// </summary>
        public int? resultstate { get; set; }

        /// <summary>
        /// 转换时间
        /// </summary>
        public DateTime? resultdate { get; set; }

        [Key]
        public int idnum { get; set; }

        /// <summary>
        /// 危急值
        /// </summary>
        public string wjz { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string inLRdoctorid { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string inLRdoctorname { get; set; }
    }
}