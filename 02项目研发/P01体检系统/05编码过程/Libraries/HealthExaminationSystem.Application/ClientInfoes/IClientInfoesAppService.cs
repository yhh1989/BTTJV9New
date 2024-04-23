using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes
{
    /// <summary>
    /// 单位列表接口
    /// </summary>
    public interface IClientInfoesAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ClientInfosViewDto Add(ClientInfoesDto input);

        /// <summary>
        /// 删除单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(ClientInfoesDto input);

        /// <summary>
        /// 修改单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ClientInfoesDto Edit(ClientInfoesDto input);

        /// <summary>
        /// 获取一个单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ClientInfosViewDto Get(ClientInfoesListInput input);

        /// <summary>
        /// 获取添加单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ClientInfosViewDto> Query(ClientInfoesListInput input);
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        ICollection<ClientInfosViewDto> GetAll();
        /// <summary>
        /// 分页获取添加单位列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<ClientInfosViewDto> PageFulls(PageInputDto<ClientInfoesListInput> input);
        /// <summary>
        /// 单位名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

#if Application
        Task<List<ClientInfosNameDto>> QueryClientName(ChargeBM input);
#elif Proxy
       List<ClientInfosNameDto> QueryClientName(ChargeBM input);
#endif

        /// <summary>
        /// 为缓存获取所有单位信息缓存
        /// </summary>
        /// <returns></returns>
        Task<List<ClientInfoCacheDto>> GetAllForCache();
    }
}