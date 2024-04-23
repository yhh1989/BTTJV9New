using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 科室就诊人员列表
    /// </summary>
    public class DepartMentCustomerRegOutPut
    {
        /// <summary>
        ///科室序号
        /// </summary>
        public int? DepartmentOrder { get; set; }
        /// <summary>
        ///科室id
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartMentName { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string strSex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        public string Introducer { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public  string Department { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public int? CheckState { get; set; }
        /// <summary>
        /// 体检状态
        /// </summary>
        public string  strCheckState { get; set; }

        /// <summary>
        /// 总检状态
        /// </summary>
        public int? SummSate { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime? LoginDate { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime? BookingDate { get; set; }
        /// <summary>
        /// 科室组合
        /// </summary>
        public List<JLItemGroupDto> ItemGroups { get; set; }
#if Application
            [IgnoreMap]
#endif
        public virtual string AllGroups
        {
            get
            {
                if (ItemGroups != null)
                {
                    return string.Join(",", ItemGroups.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Select(o => o.ItemGroupName).ToList()).TrimEnd(',');
                }
                else
                {
                    return "";
                }
            }

        }

    }
    /// <summary>
    /// 
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomerItemGroup))]
#endif
    public class JLItemGroupDto
    {

        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }
        /// <summary>
        /// 组合小结 bxy
        /// </summary>
        public virtual string ItemGroupSum { get; set; }
        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }
        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }
    }
}
