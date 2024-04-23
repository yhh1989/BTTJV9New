using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice
{
    public interface ISummarizeAdviceAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullSummarizeAdviceDto Add(SummarizeAdviceInput input);
        /// <summary>
        /// 删除建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Del(EntityDto<Guid> input);
        /// <summary>
        /// 修改建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullSummarizeAdviceDto Edit(SummarizeAdviceInput input);
        /// <summary>
        /// 获取一个建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FullSummarizeAdviceDto Get(SearchSummarizeAdvice input);

        /// <summary>
        /// 获取简单的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SimpleSummarizeAdviceDto> QuerySimples(SearchSummarizeAdvice input);
        /// <summary>
        /// 获取完整的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SummarizeAdviceDto> QueryNatives(SearchSummarizeAdvice input);
        /// <summary>
        /// 获取含关联的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<FullSummarizeAdviceDto> QueryFulls(SearchSummarizeAdvice input);

        /// <summary>
        /// 获取含关联的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SearchSummarizeAdviceDto> QueryAll ();
        /// <summary>
        /// 分页获取完整的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<SummarizeAdviceDto> PageNatives(PageInputDto<SearchSummarizeAdvice> input);

        /// <summary>
        /// 分页获取含关联的建议列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PageResultDto<FullSummarizeAdviceDto> PageFulls(PageInputDto<SearchSummarizeAdvice> input);

        /// <summary>
        /// 建议字典数据，缓存使用
        /// </summary>
        /// <returns></returns>
        List<SummarizeAdviceDto> GetSummAll(InputSearchSumm Input);

        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        List<SummarizeAdviceDto> GetSummForGuidList(List<Guid> GuidList);
        /// <summary>
        /// 建议字典数据，缓存使用
        /// </summary>
        /// <returns></returns>
       

#if Application
        Task<List<SummarizeAdviceDto>> GetAllSummAll();
#elif Proxy
         List<SummarizeAdviceDto> GetAllSummAll();
#endif
        SumConflictDto SaveSumConflict(SumConflictDto input);
        List<SumConflictDto> SearchSumConflict(ChargeBM input);
        void DelSumConflict(ChargeBM input);
        TbmSummHBDto SaveSumHB(TbmSummHBDto input);
        List<TbmSummHBDto> SearchSumHB();
        void DelSumHB(EntityDto<Guid> input);
    }
}