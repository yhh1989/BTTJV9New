using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
#endif
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
  public   class ClientTeamCusListDto : EntityDto<Guid>
    {

        /// <summary>
        /// 预约短信 0未发送 1已发送
        /// </summary>
        public int? RegMessageState { get; set; }
        public virtual Guid CustomerId { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>    
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual Guid TeamId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>

        public virtual string TeamName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
  
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 预约备注
        /// </summary>
        public string RegRemarks { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public int CheckSate { get; set; }


        /// <summary>
        /// 单位序号
        /// </summary>
        public virtual int? ClientRegNum { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }


        /// <summary>
        /// 是否已打导引单 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? GuidanceSate { get; set; }

        /// <summary>
        /// 是否已打条码 1未打印2已打印3无须打印
        /// </summary>
        public virtual int? BarState { get; set; }

        /// <summary>
        /// 体检来源 字典ClientSource
        /// </summary>
        public virtual int? InfoSource { get; set; }


        /// <summary>
        /// 就诊卡
        /// </summary>
       
        public virtual string VisitCard { get; set; }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatRegisterState
        {
            get
            {
                var Register = EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(RegisterState), RegisterState.ToString())));
                return Register;
            }

        }

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
    }
}
