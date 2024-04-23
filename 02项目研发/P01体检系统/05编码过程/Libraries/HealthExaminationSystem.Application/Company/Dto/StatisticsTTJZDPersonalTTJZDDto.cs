
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 团体结账单按个人统计
    /// </summary>
    public class StatisticsTTJZDPersonalTTJZDDto
    {
       /// <summary>
       /// 付费人员信息
       /// </summary>
       public List<Pay> PayPersons { get; set; }
       /// <summary>
       /// 免费人员信息
       /// </summary>
       public List<IsFree> FreePersons { get; set; }
    }
    /// <summary>
    /// 付费人员
    /// </summary>
    public class Pay : Person
    {

        /// <summary>
        /// 总折后价格
        /// </summary>
        public decimal PriceAfterDis { get; set; }
        /// <summary>
        /// 加项团付项目
        /// </summary>
        public string AddItemGroupTeam { get; set; }
        /// <summary>
        /// 加项团付价
        /// </summary>
        public decimal AddItemGroupTeamPrice { get; set; }
        /// <summary>
        /// 加项自费项目
        /// </summary>
        public string AddItemGroupPersonl { get; set; }
        /// <summary>
        /// 加项自费价
        /// </summary>
        public decimal AddItemGroupPersonlPrice { get; set; }
        /// <summary>
        /// 增减项目
        /// </summary>
        public string ZengjianItemGroup { get; set; }
        /// <summary>
        /// 赠检金额
        /// </summary>
        public decimal ZengjianItemGroupPrice { get; set; }
        /// <summary>
        /// 放弃项目
        /// </summary>
        public string FangQiItemGroup { get; set; }
        /// <summary>
        /// 放弃金额
        /// </summary>
        public decimal FangQiItemGroupPrice { get; set; }
        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal PersonalPayPrice { get; set; }
        /// <summary>
        /// 实检金额
        /// </summary>
        public decimal CheckPrice { get; set; }
    }
    /// <summary>
    /// 免费人员
    /// </summary>
    public class IsFree: Person
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemGroups { get; set; }
    }
    /// <summary>
    /// 人员信息
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomRegNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal SumPrice { get; set; }
#if Application
        [AutoMapper.IgnoreMap]
#endif
        
        public virtual string FormatSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }
    }
}
