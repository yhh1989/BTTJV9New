using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto
{
    public class MasterDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int ParameterSerialNumber { get; set; }

        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string ParameterCustomerName { get; set; }

        /// <summary>
        /// 体检人性别
        /// </summary>
        public string ParameterCustomerSex { get; set; }

        /// <summary>
        /// 体检人年龄
        /// </summary>
        public int ParameterCustomerAge { get; set; }

        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime ParameterExaminationDate { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string ParameterCustomerExaminationNumber { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ParameterCompanyName { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string AppliyNum { get; set; }

        /// <summary>
        /// 开单医师
        /// </summary>
        public string ParameterDoctor { get; set; }

        /// <summary>
        /// 体检类别
        /// </summary>
        public string ParameterExaminationType { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        public string ParameterIntroducer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ParameterRemark { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string ParameterSuitName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ParameterMobile { get; set; }

        /// <summary>
        /// 婚否
        /// </summary>
        public string ParameterMarriage { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string ParameterDepartment { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string ParameterNation { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string ParameterTypeWork { get; set; }



    }
}
