using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew
{
  public interface IOccDisProposalNewAppService
#if !Proxy
        : Abp.Application.Services.IApplicationService
#endif
    {

        TbmOccDictionaryDto Add(TbmOccDictionaryDto input);

        TbmOccDictionaryDto Edit(TbmOccDictionaryDto input);

        void SaveDictionary(List<TbmOccDictionaryDto> inputlist);
        /// <summary>
        /// 根据类别获取相应字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OutOccDictionaryDto> getOutOccDictionaryDto(ChargeBM input);


        List<ShowOccDictionary> GetAll(ShowOccDictionary input);

        void Del(EntityDto<Guid> input);

        TbmOccDictionaryDto GetOccDictionarys(EntityDto<Guid> input);

    }
}
