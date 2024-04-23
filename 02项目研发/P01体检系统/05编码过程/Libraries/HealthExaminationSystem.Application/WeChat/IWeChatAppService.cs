using Abp.Application.Services;
#if !Proxy
using Abp.Authorization;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat
{
   
    public interface IWeChatAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 预约接口提交订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutResult SubmitOrder(InCusInfoDto input);
        /// <summary>
        /// 上传报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<reportjson> RegportList(InCusRegBMDto input);
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>
        List<ClientRegBMDto> CustomerBM(ReceiveCos cosPrice);
        /// <summary>
        /// 体检人信息接口
        /// </summary>
        /// <param name="cosPrice"></param>
        /// <returns></returns>
        List<CustomerJson> Customerlist(InCusRegBMDto cosPrice);

#region 基本信息接口

        /// <summary>
        /// 科室信息
        /// </summary>
        /// <returns></returns>
        List<ReportDepartDto> getDeparts(SearchBiseDto dto);


        /// <summary>
        /// 组合信息
        /// </summary>
        /// <returns></returns>
        List<ReportItemGroupDto> getItemGroups(SearchBiseDto dto);

        /// <summary>
        /// 项目信息
        /// </summary>
        /// <returns></returns>
        List<ReportItemInfoDto> getItemInfos(SearchBiseDto dto);

        /// <summary>
        /// 套餐信息
        /// </summary>
        /// <returns></returns>
        List<ReportItemSuitDto> getItemSuits(SearchBiseDto dto);

        /// <summary>
        /// 获取建议编码
        /// </summary>
        /// <returns></returns>
        List<ClientRegBMDto> getISummarizeBM(SearchBiseDto dto);

        /// <summary>
        /// 获取建议库
        /// </summary>
        /// <returns></returns>
        List<ReportSummarizeAdviceDto> getISummarizeAdvices(InCusRegBMDto dto);



#endregion
#region 团体预约
        /// <summary>
        /// 获取更新的预约编码
        /// </summary>
        /// <returns></returns>
        List<ClientRegBMDto> getClientRegBM(SearchBiseDto dto);
        /// <summary>
        /// 根据编码获取单位预约信息
        /// </summary>
        /// <returns></returns>
        List<WXClientRegDto> getClientRegInfo(SearchClientCusDto dto);

        /// <summary>
        /// 根据编码更新时间节点获取更新的人员编码
        /// </summary>
        /// <returns></returns>       
        List<ClientRegBMDto> getClientRegCusBM(SearchClientCusDto dto);
        /// <summary>
        /// 根据编码更新时间节点获取更新的人员信息
        /// </summary>
        /// <returns></returns>
        List<WXClientCusInfoDto> getClientRegCus(InCusRegBMDto dto);
        /// <summary>
        /// 团体预约（只修改日期）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultDto RegClientCusDate(SearchRegDateDto dto);

#endregion
        /// <summary>
        /// 体检人检查结果信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WCusRegDto> getCusResult(InCusRegBMDto input);

        /// <summary>
        /// 微信预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutResult RegCus(NInCusInfoDto input);

        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        OutResult SaveCusQuestion(TjlCusQuestionDto input);

        /// <summary>
        /// 保存优康云问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutResult SaveYKYQuestion(WJMainDto input);

        ResultDto DelCusReg(SearchRegDateDto input);
        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        OutResult Payment(InCusPayDto dto);
        /// <summary>
        /// 微信取消支付
        /// </summary>
        /// <param name="ReceiptId"></param>
        /// <returns></returns>
        OutResult CancelPayment(ReceiptIdDto ReceiptId);
        OutResult SaveHis(fee_chargeDto feeinput);
        OutResult CancelHis(Refee_chargeDto feeinput);
        /// <summary>
        /// 获取更新体检号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ClientRegBMDto> getCusReBM(SeachRepot input);
        /// <summary>
        /// 体检状态接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusStateDto> GetCusState(InCusBMDto input);
        /// <summary>
        /// 复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WxCusReview> getWxCusReview(SeachRepot input);
        /// <summary>
        /// 复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WxCusReviewMessDto> getWxCusReviewMess(SeachRepot input);
        /// <summary>
        /// 补检通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WxCusReviewMessDto> getCusGiveUp(SeachRepot input);
        /// <summary>
        /// 危急值通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        List<WxCusMessDto> getCusCrisis(SeachRepot input);
        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WXClientInfoDto> getWXClientInfo(SearchBiseDto input);
        /// <summary>
        /// 获取有人员的单位预约编码
        /// </summary>
        /// <returns></returns>        
        List<ClientRegBMDto> getCheckClientRegBM(SearchBiseDto dto);
        /// <summary>
        /// 单位预约及人员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        WXClientCusListDto wXClientCusListDto(SearchClientCusDto dto);

        /// <summary>
        /// 体检预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<WXCusReglistDto> wXCusReglistDtos(SeachRepot input);

        /// <summary>
        /// 体检人诊断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusSumBMLisDto> cusSumBMLisDtos(SeachRepot input);
        /// <summary>
        /// 体检人慢病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CusMBSumBMDtocs> cusMBSumDtos(SeachRepot input);
        /// <summary>
        /// 组合关联医嘱项目
        /// </summary>
        /// <returns></returns>
        List<WXGroupPriceSynchronizesDto> GetWXGroupPriceSynchronizes(SearchBiseDto dto);
        /// <summary>
        /// 医嘱项目
        /// </summary>
        /// <returns></returns>

#if Application
        Task<List<WXPriceSynchronizeDto>> GetWXPriceSynchronize(SearchBiseDto dto);
#elif Proxy
         List<WXPriceSynchronizeDto> GetWXPriceSynchronize();
#endif
    }
}
