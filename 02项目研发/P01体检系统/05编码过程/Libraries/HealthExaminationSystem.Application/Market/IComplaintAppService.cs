using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market
{
    /// <summary>
    /// 投诉应用服务接口
    /// </summary>
    public interface IComplaintAppService : IApplicationService
    {
        /// <summary>
        /// 查询投诉信息集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ComplaintInformationDto>> QueryComplaintInformationCollection(QueryComplaintConditionInput input);

        /// <summary>
        /// 插入或更新投诉信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ComplaintInformationDto> InsertOrUpdateComplaintInformation(UpdateComplaintInformationDto input);

        /// <summary>
        /// 查询一个空的投诉信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ComplaintInformationDto> QueryEmptyComplaintInformation(QueryComplaintConditionInput input);
    }
}