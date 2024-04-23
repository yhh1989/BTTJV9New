using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport
{
    public interface ICustomerReportAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 交接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerReportFullDto Handover(CustomerReportHandoverInput input);
        /// <summary>
        /// 取消交接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerReportFullDto CancelHandover(CustomerReportHandoverInput input);
        /// <summary>
        /// 批量交接
        /// </summary>
        /// <param name="input"></param>
        void BatchHandover(List<CustomerReportHandoverInput> input);
        /// <summary>
        /// 发单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerReportFullDto Send(CustomerReportHandoverInput input);

        CustomerReportFullDto Cancel(CustomerReportHandoverInput input);
        /// <summary>
        /// 批量发单
        /// </summary>
        /// <param name="input"></param>
        void BatchSend(List<CustomerReportHandoverInput> input);
        /// <summary>
        /// 获取一个报告
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CustomerReportFullDto Get(CustomerReportByNumber input);
        /// <summary>
        /// 获取添加报告列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<CustomerReportFullDto> Query(CustomerReportQuery input);
        /// <summary>
        /// 柜子设置
        /// </summary>
        /// <returns></returns>
        TbmCabinetDto getTbmCabinet();
        /// <summary>
        /// 保存柜子设置
        /// </summary>
        /// <returns></returns>
         TbmCabinetDto SaveTbmCabinet(TbmCabinetDto input);

        /// <summary>
        /// 查询柜子存放记录
        /// </summary>
        /// <returns></returns>
        List<TjlCusCabitDto> getTjlCabinet();
        /// <summary>
        /// 保存柜子存放设置
        /// </summary>
        /// <returns></returns>
        TjlCusCabitDto SaveTjlCabinet(TjlCusCabitDto input);
        /// <summary>
        /// 删除柜子存放设置
        /// </summary>
        /// <returns></returns>
        void DelTjlCabinet(TjlCusCabitDto input);
        CustomerUpCatDto UpCustomerUpCat(CustomerUpCatDto input);
        /// <summary>
        /// 个人报告领取查询
        /// </summary>
        /// <returns></returns>
        List<TjlCusCabitCRDto> getTjlCabinetlist(SeachCusCabDto input);

        /// <summary>
        /// 保存领取信息
        /// </summary>
        /// <returns></returns>
        CusCabitLQDto SaveTjlCabinetLQ(CusCabitLQDto input);
        /// <summary>
        /// 判断审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ChargeBM IsSH(EntityDto<Guid> input);
        /// <summary>
        /// 修改单位柜子存方设置
        /// </summary>
        /// <returns></returns>
        ClientRegUpCatDto UpClientRegUpCat(ClientRegUpCatDto input);
        /// <summary>
        /// 团报报告领取查询
        /// </summary>
        /// <returns></returns>
        List<ClientCabitCRDto> getClientCabinetlist(SearchClientRegLQDto input);
    }
}
