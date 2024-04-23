using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose
{
    public class ComposeAppService : AppServiceApiProxyBase, IComposeAppService
    {
        public List<FullComposeDto> QueryFulls(SearchComposeInput input)
        {
            return GetResult<SearchComposeInput, List<FullComposeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public PageResultDto<ComposeDto> PageComposes(PageInputDto<SearchComposeInput> input)
        {
            return GetResult<PageInputDto<SearchComposeInput>, PageResultDto<ComposeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<FullComposeDto> PageFullComposes(PageInputDto<SearchComposeInput> input)
        {
            return GetResult<PageInputDto<SearchComposeInput>, PageResultDto<FullComposeDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FullComposeGroupDto> GetComposeGroupByComposeId(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<FullComposeGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullComposeDto CreateCompose(CreateOrUpdateComposeInput input)
        {
            return GetResult<CreateOrUpdateComposeInput, FullComposeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullComposeDto UpdateCompose(CreateOrUpdateComposeInput input)
        {
            return GetResult<CreateOrUpdateComposeInput, FullComposeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
