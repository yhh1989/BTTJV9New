using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif
 
namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif  
    public class QueryAllForPersonDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual QueryAllForPayMoneyDto McusPayMoney { get; set; }

        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual List<CustomerItemGroupDto> CustomerItemGroup { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual QueryCustomerDto Customer { get; set; }

        /// <summary>
        /// 单位分组
        /// </summary>
        public virtual ClientTeamInfoDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ClientRegDto ClientReg { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonTypeDto PersonnelCategory { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 介绍人名字
        /// </summary>
        public virtual string Introducer { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [StringLength(128)]
        public virtual string ItemSuitName { get; set; }

        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }
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
        /// 原体检人
        /// </summary>
        [StringLength(64)]
        public virtual string PrimaryName { get; set; }


        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 加项
        /// </summary>
        public string AddItem
        {
            get
            {
                string result =string.Empty;
                CustomerItemGroup.Where(c=>c.IsAddMinus== (int)AddMinusType.Add).ToList().ForEach(c=> {
                    result += c.ItemGroupName+",";
                });
                return result.Trim(',');
            }
        }
        /// <summary>
        /// 减项
        /// </summary>
        public string MinusItem
        {
            get
            {
                string result = string.Empty;
                CustomerItemGroup.Where(c => c.IsAddMinus == (int)AddMinusType.Minus).ToList().ForEach(c => {
                    result += c.ItemGroupName + ",";
                });
                return result.Trim(',');
            }
        }
        /// <summary>
        /// 未检查项
        /// </summary>
        public string NotCheck
        {
            get
            {
                string result = string.Empty;
                CustomerItemGroup.Where(c => c.IsAddMinus != (int)AddMinusType.Minus && c.CheckState== (int)ProjectIState.Not).ToList().ForEach(c => {
                    result += c.ItemGroupName + ",";
                });
                return result.Trim(',');
            }
        }
        public string Jieshaoren
        {
            get
            {
                if (ClientRegId == null || ClientRegId == Guid.Empty)
                    return Introducer;
                else
                    return ClientReg.linkMan;
            }
        }

        public string PayType
        {
            get
            {
                if (CustomerItemGroup.Where(c => c.PayerCat == (int)PayerCatType.PersonalCharge).Count() > 0)
                    return "个人支付";
                else if (CustomerItemGroup.Where(c => c.PayerCat == (int)PayerCatType.ClientCharge).Count() > 0)
                    return "单位支付";
                else if (CustomerItemGroup.Where(c => c.PayerCat == (int)PayerCatType.NoCharge).Count() > 0)
                    return "未收费";
                else
                    return "";
            }
        }
    }
}
