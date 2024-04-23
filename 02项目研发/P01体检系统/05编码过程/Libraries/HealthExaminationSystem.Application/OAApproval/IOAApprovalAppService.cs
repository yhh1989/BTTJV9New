using Abp.Application.Services;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.Domain.Entities;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval
{
  public   interface IOAApprovalAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加修改审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CreatOAApproValcsDto creatOAApproValcs(CreatOAApproValcsDto input);
        void DelOAApproValcs(EntityDto<Guid> input);
        List<CreatOAApproValcsDto> SearchOAApproValcs(SearchOAApproValcsDto input);

        CreateClientXKSetDto creatCreateClientXKSet(CreateClientXKSetDto input);

        CreateClientXKSetDto getCreateClientXKSet();
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CreatOAApproValcsDto upOAApproValcsDto(UpOAApproValcsDto input);


    }
}
