﻿#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccQuestionnaire))]
#endif
    //问卷
    public class OccQuestionnaireDto:EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
  
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 职业史
        /// </summary>
        public virtual List<OccupationHistoryDto> OccCareerHistory { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        public virtual List<OccQuesPastHistoryDto> OccPastHistory { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        public virtual List<OccQuesFamilyHistoryDto> OccFamilyHistory { get; set; }

        /// <summary>
        /// 症状
        /// </summary>
        public virtual List<OccQuesSymptomDto> OccQuesSymptom { get; set; }

        /// <summary>
        /// 婚姻史
        /// </summary>
        public virtual List<OccQuesMerriyHistoryDto> OccQuesMerriyHistory { get; set; }
        /// <summary>
        /// 放射职业史
        /// </summary>
        public virtual List<OccQuesRadioactiveCareerHistoryDto> RadioactiveCareerHistory { get; set; }

        /// <summary>
        /// 家族遗传史
        /// </summary>     
        public virtual string GeneticHistory { get; set; }

        /// <summary>
        /// 服药史
        /// </summary>      
        public virtual string MedicationHistory { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary>   
        public virtual string AllergicHistory { get; set; }
        /// <summary>
        /// 药物禁忌
        /// </summary>     
        public virtual string DrugTaboo { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>      
        public virtual string PastHistory { get; set; }
        /// <summary>
        /// 月经初潮年龄（岁）
        /// </summary>

        public virtual int? StarAge { get; set; }
        /// <summary>
        /// 月经周期
        /// </summary>

        public virtual int? Cycle { get; set; }
        /// <summary>
        /// 经期
        /// </summary>

        public virtual int? period { get; set; }
        

        /// <summary>
        /// 停经年龄
        /// </summary>

        public virtual int? EndAge { get; set; }

        /// <summary>
        /// 子女数量
        /// </summary>

        public virtual int? ChildrenNum { get; set; }

        /// <summary>
        /// 有无流产1有2无
        /// </summary>

        public virtual string IsAbortion { get; set; }
        /// <summary>
        /// 流产次数
        /// </summary>

        public virtual int? AbortionCount { get; set; }

        /// <summary>
        /// 早产与否有、无
        /// </summary>
     
        public virtual string IsPrematureDelivery { get; set; }
        /// <summary>
        /// 死产与否1有2无
        /// </summary>
 
        public virtual string IsStillbirth { get; set; }

        /// <summary>
        /// 早产次数
        /// </summary>
        public virtual string PrematureDeliveryCount { get; set; }

        /// <summary>
        /// 死产次数
        /// </summary>      
        public virtual string StillbirthCount { get; set; }
        /// <summary>
        /// 异常胎次数
        /// </summary>     
        public virtual string AbnormityCount { get; set; }

        /// <summary>
        /// 吸烟习惯
        /// </summary>

        public virtual string IsSmoke { get; set; }

        /// <summary>
        /// 吸烟次数/天
        /// </summary>       
        public virtual int? SmokeCount { get; set; }

        /// <summary>
        /// 吸烟年限
        /// </summary>
        public virtual int? SmokeYears { get; set; }
        /// <summary>
        /// 饮酒习惯
        /// </summary>
   
        public virtual string IsDrink { get; set; }

        /// <summary>
        /// 饮酒次数/天
        /// </summary>
        public virtual int? DrinkCount { get; set; }

        /// <summary>
        /// 饮酒年限
        /// </summary>
        public virtual int? DrinkYears { get; set; }

        /// <summary>
        /// 询问建议
        /// </summary>
    
        public virtual string AskAdvice { get; set; }

       
        /// <summary>
        /// 先天畸形与否有、无
        /// </summary>  
        public virtual string malFormation { get; set; }

        /// <summary>
        /// 异常胎否有、无
        /// </summary>
      
        public virtual string Abnormal { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public virtual Guid? SignaTureImage { get; set; }




        /// <summary>
        /// 孕次数
        /// </summary>

        public virtual int? PregnancyCount { get; set; }
        /// <summary>
        /// 活产
        /// </summary>

        public virtual int? LiveBirth { get; set; }
        /// <summary>
        /// 畸胎
        /// </summary>
        public virtual int? Teratogenesis { get; set; }
        /// <summary>
        /// 多胎
        /// </summary>
        public virtual int? MultipleBirths { get; set; }
        /// <summary>
        /// 异位妊娠
        /// </summary>
        public virtual int? EctopicPregnancy { get; set; }
        /// <summary>
        /// 不孕不育原因
        /// </summary>
        [StringLength(320)]
        public virtual string Infertility { get; set; }
        /// <summary>
        /// 男孩数量
        /// </summary>
        public virtual int? BoyChildrenNum { get; set; }
        /// <summary>
        /// 男孩出生日期
        /// </summary>
        public virtual DateTime? BoyBrith { get; set; }
        /// <summary>
        /// 女孩数量
        /// </summary>
        public virtual int? grilChildrenNum { get; set; }
        /// <summary>
        /// 女孩出生日期
        /// </summary>
        public virtual DateTime? grilBrith { get; set; }
        /// <summary>
        /// 女健康情况
        /// </summary>
        [StringLength(320)]
        public virtual string ChildHealthy { get; set; }

        /// <summary>
        /// 个人生活史
        /// </summary>
        [StringLength(500)]
        public virtual string LifeHistory { get; set; }

        /// <summary>
        /// 戒烟年限
        /// </summary>
        public virtual int? QuitYears { get; set; }


    }
}
