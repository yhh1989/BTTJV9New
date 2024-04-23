using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
    /// <summary>
    /// 体检人检查结果
    /// </summary>
  public   class ReSultCusInfoDto
    {
        public  Guid Id { get; set; }
        /// <summary>
        /// 项目结果
        /// </summary>      
        public virtual ICollection<ReSultCusItemDto> ReSultCusItems { get; set; }
        /// <summary>
        /// 组合小结
        /// </summary>      
        public virtual ICollection<ReSultCusGroupDto> ReSultCusGrous { get; set; }
        /// <summary>
        /// 科室小结
        /// </summary>      
        public virtual ICollection<ReSultCusDepartDto> ReSultCusDeparts { get; set; }
        /// <summary>
        /// 总检建议
        /// </summary>      
        public virtual  ReSultCusSumDto ReSultCusSum { get; set; }
        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual string ReSultCusSums { get; set; }
        /// <summary>
        /// 职业健康建议
        /// </summary>      
        //public virtual ReSultCusOccSumDto ReSultCusOccSum { get; set; }
        public virtual string ReSultCusOccSum { get; set; }
        /// <summary>
        /// 建议名称
        /// </summary>
        public virtual string AdViceNames { get; set; }
        /// <summary>
        /// 职业建议名称
        /// </summary>
        public virtual string OCCAdViceNames { get; set; }
        /// <summary>
        /// 职业总检建议
        /// </summary>
        public virtual string OCCReSultCusSums { get; set; }
        /// <summary>
        /// 参考处理意见
        /// </summary>       
        public virtual string Opinions { get; set; }
        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 预约Id
        /// </summary>
        public Guid CustomerRegId { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 单位 
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }
        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }


        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual string Department { get; set; }
        /// <summary>
        /// 备注
        /// </summary>    
        public virtual string Remarks { get; set; }
#if Proxy
        /// <summary>
        /// 格式化登记状态
        /// </summary>
        [JsonIgnore]
        public virtual string RegisterStateFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RegisterState.ToString()))
                {
                    if (int.TryParse(RegisterState.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((RegisterState)result);
                    }
                }

                return string.Empty;
            }
        }
#endif
#if Proxy
        /// <summary>
        /// 格式化体检状态
        /// </summary>
        [JsonIgnore]
        public virtual string CheckSateFormat
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CheckSate.ToString()))
                {
                    if (int.TryParse(CheckSate.ToString(), out var result))
                    {
                        return EnumHelper.GetEnumDesc((PhysicalEState)result);
                    }
                }

                return string.Empty;
            }
        }
#endif

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }
        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }


        /// <summary>
        /// 危害因素 逗号隔开
        /// </summary>
        [StringLength(800)]
        public virtual string RiskS { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [StringLength(16)]
        public virtual string PostState { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(16)]
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(16)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 总工龄
        /// </summary>
        [StringLength(16)]
        public virtual string TotalWorkAge { get; set; }
        

        /// <summary>
        /// 接害工龄
        /// </summary>
        [StringLength(16)]
        public virtual string InjuryAge { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        public string PhysicalTypeName { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

    }
}
