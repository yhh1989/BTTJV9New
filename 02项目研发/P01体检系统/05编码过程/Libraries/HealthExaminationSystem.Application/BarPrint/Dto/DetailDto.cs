using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto
{
    /// <summary>
    /// 明细网格
    /// </summary>
    public class DetailDto
    {
        /// <summary>
        /// 打勾
        /// </summary>
        public string Tick { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeparmentName { get; set; }
        /// <summary>
        /// 检验类型
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// 检验类型
        /// </summary>
        public string TestType { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string ItemGroupName { get; set; }

        /// <summary>
        /// 科室地址（提示信息）
        /// </summary>
        public string DepartmentAddress { get; set; }

        /// <summary>
        /// 医生签名
        /// </summary>
        public string DoctorSign { get; set; }

        /// <summary>
        /// 延期签字
        /// </summary>
        public string PostponeSign { get; set; }

        /// <summary>
        /// 拒检签字
        /// </summary>
        public string RejectSign { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerExaminationNumber { get; set; }

        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 登记序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientInfoName { get; set; }

        /// <summary>
        /// 原体检人
        /// </summary>
        public virtual string PrimaryName { get; set; }


        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }



    }
}
