using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
   public class FullTargetDiseaseDto
    {


        /// <summary>
        ///  目标疾病
        /// </summary>
        public CreateOrUpdateTargetDiseaseDto OneTargetDisease { get; set; }
        /// <summary>
       /// 禁忌证
       /// </summary>
        public List<TbmOccTargetDiseaseContraindicationDto> ManyTargetDiseaseContraindication { get; set; }


        /// <summary>
        ///  职业健康
        /// </summary>
        public List<Guid> OneDisease { get; set; }       
       
        /// <summary>
        /// 多条
        /// </summary>
        public List<TbmOccTargetDiseaseSymptomsDto> ManyTargetDiseaseSymptoms { get; set; }

        /// <summary>
        ///  必选项目
        /// </summary>
        public List<Guid> MustGroups { get; set; }

        /// <summary>
        ///  可选项目
        /// </summary>
        public List<Guid> mayGroups { get; set; }
        /// <summary>
        /// 依据项目
        /// </summary>

        public List<Guid> ItemInfo { get; set; }

    }
}
