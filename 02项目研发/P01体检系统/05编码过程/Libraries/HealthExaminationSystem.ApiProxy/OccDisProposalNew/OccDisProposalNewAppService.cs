using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew
{
    public  class OccDisProposalNewAppService : AppServiceApiProxyBase, IOccDisProposalNewAppService
    {      
        public TbmOccDictionaryDto Add(TbmOccDictionaryDto input)
        {
            return GetResult<TbmOccDictionaryDto, TbmOccDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public TbmOccDictionaryDto Edit(TbmOccDictionaryDto input)
        {
            return GetResult<TbmOccDictionaryDto, TbmOccDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void SaveDictionary(List<TbmOccDictionaryDto> input)
        {
              GetResult<List<TbmOccDictionaryDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public List<ShowOccDictionary> GetAll(ShowOccDictionary input)
        {
            return GetResult<ShowOccDictionary,List<ShowOccDictionary>>(input,DynamicUriBuilder.GetAppSettingValue());
        }        
        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public TbmOccDictionaryDto GetOccDictionarys(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, TbmOccDictionaryDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        
        /// <summary>
        /// 根据类别获取相应字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutOccDictionaryDto> getOutOccDictionaryDto(ChargeBM input)
        {
            return GetResult<ChargeBM, List<OutOccDictionaryDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccTargetCountDto> getTargetCount(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<OccTargetCountDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
