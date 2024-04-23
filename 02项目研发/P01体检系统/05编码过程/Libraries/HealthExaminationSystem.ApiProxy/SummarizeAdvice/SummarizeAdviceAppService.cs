using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice
{
    public class SummarizeAdviceAppService : AppServiceApiProxyBase, ISummarizeAdviceAppService
    {
        public FullSummarizeAdviceDto Add(SummarizeAdviceInput input)
        {
            return GetResult<SummarizeAdviceInput, FullSummarizeAdviceDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullSummarizeAdviceDto Edit(SummarizeAdviceInput input)
        {
            return GetResult<SummarizeAdviceInput, FullSummarizeAdviceDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FullSummarizeAdviceDto Get(SearchSummarizeAdvice input)
        {
            return GetResult<SearchSummarizeAdvice, FullSummarizeAdviceDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SimpleSummarizeAdviceDto> QuerySimples(SearchSummarizeAdvice input)
        {
            return GetResult<SearchSummarizeAdvice, List<SimpleSummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SummarizeAdviceDto> QueryNatives(SearchSummarizeAdvice input)
        {
            return GetResult<SearchSummarizeAdvice, List<SummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FullSummarizeAdviceDto> QueryFulls(SearchSummarizeAdvice input)
        {
            return GetResult<SearchSummarizeAdvice, List<FullSummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<SummarizeAdviceDto> PageNatives(PageInputDto<SearchSummarizeAdvice> input)
        {
            return GetResult<PageInputDto<SearchSummarizeAdvice>, PageResultDto<SummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<FullSummarizeAdviceDto> PageFulls(PageInputDto<SearchSummarizeAdvice> input)
        {
            return GetResult<PageInputDto<SearchSummarizeAdvice>, PageResultDto<FullSummarizeAdviceDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SummarizeAdviceDto> GetSummAll(InputSearchSumm Input)
        {
            return GetResult<InputSearchSumm, List<SummarizeAdviceDto>>(Input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SummarizeAdviceDto> GetSummForGuidList(List<Guid> Input)
        {
            return GetResult<List<Guid>, List<SummarizeAdviceDto>>(Input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<SearchSummarizeAdviceDto> QueryAll()
        {
            return GetResult<List<SearchSummarizeAdviceDto>>( DynamicUriBuilder.GetAppSettingValue());
        }
        public List<SummarizeAdviceDto> GetAllSummAll()
        {
            return GetResult<List<SummarizeAdviceDto>>(DynamicUriBuilder.GetAppSettingValue());

        }
        public SumConflictDto SaveSumConflict(SumConflictDto input)
        { 
            return  GetResult<SumConflictDto, SumConflictDto>(input,DynamicUriBuilder.GetAppSettingValue());

        }
        public List<SumConflictDto> SearchSumConflict(ChargeBM input)
        {
            return GetResult<ChargeBM,List<SumConflictDto>>(input,DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 删除冲突关键词
        /// </summary>
        public void DelSumConflict(ChargeBM input)
        {
             GetResult<ChargeBM>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public TbmSummHBDto SaveSumHB(TbmSummHBDto input)
        {
            return GetResult<TbmSummHBDto, TbmSummHBDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TbmSummHBDto> SearchSumHB()
        {
            return GetResult< List<TbmSummHBDto>>( DynamicUriBuilder.GetAppSettingValue());
        }
        public void DelSumHB(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}
