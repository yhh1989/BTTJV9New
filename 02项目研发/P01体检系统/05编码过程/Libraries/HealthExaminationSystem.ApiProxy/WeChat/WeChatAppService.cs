using Sw.Hospital.HealthExaminationSystem.Application.WeChat;
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.WeChat
{
    public class WeChatAppService : AppServiceApiProxyBase, IWeChatAppService
    {

        /// <summary>
        /// 预约接口提交订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutResult SubmitOrder(InCusInfoDto input)
        {
            return GetResult<InCusInfoDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 上传报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <returns></returns>
        public List<reportjson> RegportList(InCusRegBMDto input)
        {
            return GetResult<InCusRegBMDto, List<reportjson>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>
        public List<ClientRegBMDto> CustomerBM(ReceiveCos input)
        {
            return GetResult<ReceiveCos, List<ClientRegBMDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 体检人信息接口
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>
        public List<CustomerJson> Customerlist(InCusRegBMDto cosPrice)
        {
            return GetResult<InCusRegBMDto, List<CustomerJson>>(cosPrice, DynamicUriBuilder.GetAppSettingValue());

        }

        #region 基本信息接口

        /// <summary>
        /// 科室信息
        /// </summary>
        /// <returns></returns>
        public List<ReportDepartDto> getDeparts(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ReportDepartDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }


        /// <summary>
        /// 组合信息
        /// </summary>
        /// <returns></returns>
        public List<ReportItemGroupDto> getItemGroups(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ReportItemGroupDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 项目信息
        /// </summary>
        /// <returns></returns>
        public List<ReportItemInfoDto> getItemInfos(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ReportItemInfoDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 套餐信息
        /// </summary>
        /// <returns></returns>
        public List<ReportItemSuitDto> getItemSuits(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ReportItemSuitDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 获取建议编码
        /// </summary>
        /// <returns></returns>
        public List<ClientRegBMDto> getISummarizeBM(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ClientRegBMDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 获取建议库
        /// </summary>
        /// <returns></returns>
        public List<ReportSummarizeAdviceDto> getISummarizeAdvices(InCusRegBMDto dto)
        {
            return GetResult<InCusRegBMDto, List<ReportSummarizeAdviceDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }



        #endregion
        #region 团体预约
        /// <summary>
        /// 获取更新的预约编码
        /// </summary>
        /// <returns></returns>
        public List<ClientRegBMDto> getClientRegBM(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ClientRegBMDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 根据编码获取单位预约信息
        /// </summary>
        /// <returns></returns>
        public List<WXClientRegDto> getClientRegInfo(SearchClientCusDto dto)
        {
            return GetResult<SearchClientCusDto, List<WXClientRegDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 根据编码更新时间节点获取更新的人员编码
        /// </summary>
        /// <returns></returns>       
        public List<ClientRegBMDto> getClientRegCusBM(SearchClientCusDto dto)
        {
            return GetResult<SearchClientCusDto, List<ClientRegBMDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 根据编码更新时间节点获取更新的人员信息
        /// </summary>
        /// <returns></returns>
        public List<WXClientCusInfoDto> getClientRegCus(InCusRegBMDto dto)
        {
            return GetResult<InCusRegBMDto, List<WXClientCusInfoDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 团体预约（只修改日期）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultDto RegClientCusDate(SearchRegDateDto dto)
        {
            return GetResult<SearchRegDateDto, ResultDto>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        #endregion
        /// <summary>
        /// 体检人检查结果信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WCusRegDto> getCusResult(InCusRegBMDto input)
        {
            return GetResult<InCusRegBMDto, List<WCusRegDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 微信预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutResult RegCus(NInCusInfoDto input)
        {
            return GetResult<NInCusInfoDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        public OutResult SaveCusQuestion(TjlCusQuestionDto input)
        {
            return GetResult<TjlCusQuestionDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public OutResult SaveYKYQuestion(WJMainDto input)
        {
            return GetResult<WJMainDto, OutResult>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ResultDto DelCusReg(SearchRegDateDto input)
        {
            return GetResult<SearchRegDateDto, ResultDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public OutResult Payment(InCusPayDto dto)
        {
            return GetResult<InCusPayDto, OutResult>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 微信取消支付
        /// </summary>
        /// <param name="ReceiptId"></param>
        /// <returns></returns>
        public OutResult CancelPayment(ReceiptIdDto ReceiptId)
        {
            return GetResult<ReceiptIdDto, OutResult>(ReceiptId, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutResult SaveHis(fee_chargeDto feeinput)
        {
            return GetResult<fee_chargeDto, OutResult>(feeinput, DynamicUriBuilder.GetAppSettingValue());

        }
        public OutResult CancelHis(Refee_chargeDto feeinput)
        {
            return GetResult<Refee_chargeDto, OutResult>(feeinput, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientRegBMDto> getCusReBM(SeachRepot input)
        {
            return GetResult<SeachRepot, List<ClientRegBMDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusStateDto> GetCusState(InCusBMDto input)
        {
            return GetResult<InCusBMDto, List<CusStateDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  List<WxCusReview> getWxCusReview(SeachRepot input)
        {
            return GetResult<SeachRepot, List<WxCusReview>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusReviewMessDto> getWxCusReviewMess(SeachRepot input)
        {
            return GetResult<SeachRepot, List<WxCusReviewMessDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 补检通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WxCusReviewMessDto> getCusGiveUp(SeachRepot input)
        {
            return GetResult<SeachRepot, List<WxCusReviewMessDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 危急值通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public List<WxCusMessDto> getCusCrisis(SeachRepot input)
        {
            return GetResult<SeachRepot, List<WxCusMessDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WXClientInfoDto> getWXClientInfo(SearchBiseDto input)
        {
            return GetResult<SearchBiseDto, List<WXClientInfoDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取有人员的单位预约编码
        /// </summary>
        /// <returns></returns>        
        public List<ClientRegBMDto> getCheckClientRegBM(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<ClientRegBMDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 单位预约及人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public WXClientCusListDto wXClientCusListDto(SearchClientCusDto dto)
        {
            return GetResult<SearchClientCusDto, WXClientCusListDto>(dto, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 体检预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WXCusReglistDto> wXCusReglistDtos(SeachRepot input)
        {
            return GetResult<SeachRepot, List<WXCusReglistDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 体检人诊断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusSumBMLisDto> cusSumBMLisDtos(SeachRepot input)
        {
            return GetResult<SeachRepot, List<CusSumBMLisDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 体检人慢病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusMBSumBMDtocs> cusMBSumDtos(SeachRepot input)
        {
            return GetResult<SeachRepot, List<CusMBSumBMDtocs>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 组合关联医嘱项目
        /// </summary>
        /// <returns></returns>
        public List<WXGroupPriceSynchronizesDto> GetWXGroupPriceSynchronizes(SearchBiseDto dto)
        {
            return GetResult<SearchBiseDto, List<WXGroupPriceSynchronizesDto>>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 医嘱项目
        /// </summary>
        /// <returns></returns>

#if Application
        Task<List<WXPriceSynchronizeDto>> GetWXPriceSynchronize(SearchBiseDto dto);
#elif Proxy
        public List<WXPriceSynchronizeDto> GetWXPriceSynchronize()
        {
            return GetResult<List<WXPriceSynchronizeDto>>( DynamicUriBuilder.GetAppSettingValue());

        }
#endif

    }
}
