#if !Proxy
using AutoMapper;
using HealthExaminationSystem.Enumerations.Helpers;
#endif
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
   public class CheckCusRegGroupDto
    {
        /// <summary>
        /// 姓名
        /// </summary>   
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>       
        public virtual string CustomerBM { get; set; }

        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }


        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核5审核不通过
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>     
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>       
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? GroupCheckState { get; set; }

        /// <summary>
        /// 第一次检查时间 bxy
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }

        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }


        /// <summary>
        /// 科室名称
        /// </summary>    
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>    
        public virtual string Department  { get; set; }
        /// <summary>
        /// 数量
        /// </summary>    
        public virtual int? Count { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 个人结账ID
        /// </summary>     
        public virtual Guid? MReceiptInfoPersonalId { get; set; }  

        /// <summary>
        /// 单位结账ID
        /// </summary>    
        public virtual Guid? MReceiptInfoClientlId { get; set; }
        /// <summary>
        /// 第一次检查时间 bxy
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual DateTime? FormatFirstDateTime
        {
            get
            {
                if (GroupCheckState == 1)
                {
                    return null;
                }
                else
                { return FirstDateTime; }
            }

        }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormatPayerCat
        {
            get
            {
                if (PayerCat == 3)
                {
                    return "单位支付";
                }
                else
                { return "个人支付"; }
            }

        }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatChage
        {
            get
            {
                if (MReceiptInfoPersonalId.HasValue || MReceiptInfoClientlId.HasValue)
                {
                    return "已收费";
                }
                else
                { return "未收费"; }
            }

        }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormatSummSate
        {
            get
            {

                // var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                var Physical = SummSateHelper.SummSateFormatter(SummSate);
                return Physical;
            }

        }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatRegisterState
        {
            get
            {
                //var Physical = RegisterStateHelper.GetSelectList(RegisterState);
                //return Physical;
                if (RegisterState == 1)
                {
                    return "未登记";
                }
                if (RegisterState == 2)
                {
                    return "已登记";
                }
                return null;
            }

        }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormatPhysical
        {
            get
            {
                var Physical = CheckSateHelper.PhysicalEStateFormatter(CheckSate);
                return Physical;
            }

        }
#if Application
        [IgnoreMap]
#endif
        public virtual string GroupFormatPhysical
        {
            get
            {
                var Physical = CheckSateHelper.ProjectIStateFormatter(GroupCheckState);
                return Physical;
            }

        }

        /// <summary>
        /// 格式化性别
        /// </summary>
#if Application
        [IgnoreMap]
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
