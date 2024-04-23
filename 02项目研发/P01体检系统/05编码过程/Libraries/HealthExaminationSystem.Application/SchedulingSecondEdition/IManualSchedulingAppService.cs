using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition
{
    /// <summary>
    /// 人工行程安排应用服务接口
    /// </summary>
    public interface IManualSchedulingAppService : IApplicationService
    {
        /// <summary>
        /// 查询“人工行程安排”列表
        /// </summary>
        /// <returns></returns>
        Task<List<ManualSchedulingDtoNo1>> QueryManualSchedulingList();

        /// <summary>
        /// 插入“人工行程安排”
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ManualSchedulingDtoNo1> InsertManualScheduling(ManualSchedulingDtoNo2 input);

        /// <summary>
        /// 更新“人工行程安排”
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ManualSchedulingDtoNo1> UpdateManualScheduling(ManualSchedulingDtoNo2 input);

        /// <summary>
        /// 查询“个人行程安排”列表
        /// </summary>
        /// <returns></returns>
        Task<List<ManualSchedulingDtoNo1>> QueryPersonSchedulingList();

        /// <summary>
        /// 查询“公司行程安排”列表
        /// </summary>
        /// <returns></returns>
        Task<List<ManualSchedulingDtoNo1>> QueryCompanySchedulingList();
    }
}