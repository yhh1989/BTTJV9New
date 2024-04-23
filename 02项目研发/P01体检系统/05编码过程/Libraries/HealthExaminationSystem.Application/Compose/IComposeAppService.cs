using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose
{
    /// <summary>
    /// 组单
    /// </summary>
    public interface IComposeAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取全部组单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<FullComposeDto> QueryFulls(SearchComposeInput input);
        /// <summary>
        /// 分页获取组单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<ComposeDto> PageComposes(PageInputDto<SearchComposeInput> input);
        /// <summary>
        /// 分页获取组单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<FullComposeDto> PageFullComposes(PageInputDto<SearchComposeInput> input);
        /// <summary>
        /// 按组单id获取组单分组
        /// </summary>
        /// <returns></returns>
        List<FullComposeGroupDto> GetComposeGroupByComposeId(EntityDto<Guid> input);
        /// <summary>
        /// 添加新的组单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullComposeDto CreateCompose(CreateOrUpdateComposeInput input);
        /// <summary>
        /// 更新组单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullComposeDto UpdateCompose(CreateOrUpdateComposeInput input);
    }
}
