using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy 
    [AutoMapFrom(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegsViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public ClientInfoRegDto ClientInfo { get; set; }

        ///// <summary>
        ///// 单位组合信息
        ///// </summary>
        public ClientTeamInfosDto ClientTeamInfo { get; set; }

        /// <summary>
        /// 体检人信息
        /// </summary>
        public CustomersViewDto Customer { get; set; }
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string FormatSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Customer.Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public int CheckSate { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 交表确认 1未交表2已交表
        /// </summary>
        public virtual int? SendToConfirm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatCheckSate
        {
            get
            {
                var Check = EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(ExaminationState), CheckSate.ToString())));
                return Check;
            }

        }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatRegisterState
        {
            get
            {
                if (RegisterState != null)
                {
                    var reg = EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(RegisterState), RegisterState.ToString())));
                    return reg;
                }
                else
                    return "";
                
            }
        }


#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatSummSate
        {
            get
            {
                if (SummSate != null)
                {
                    var reg = EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(SummSate), SummSate.ToString())));
                    return reg;
                }
                else
                    return "未总检";

            }
        }
    }
}
