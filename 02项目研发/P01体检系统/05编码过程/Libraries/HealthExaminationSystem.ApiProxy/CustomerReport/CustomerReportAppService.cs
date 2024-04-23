using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport
{
    public class CustomerReportAppService : AppServiceApiProxyBase, ICustomerReportAppService
    {
        public CustomerReportFullDto Handover(CustomerReportHandoverInput input)
        {
            return GetResult<CustomerReportHandoverInput, CustomerReportFullDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerReportFullDto CancelHandover(CustomerReportHandoverInput input)
        {
            return GetResult<CustomerReportHandoverInput, CustomerReportFullDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void BatchHandover(List<CustomerReportHandoverInput> input)
        {
            GetResult<List<CustomerReportHandoverInput>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CustomerReportFullDto Send(CustomerReportHandoverInput input)
        {
            return GetResult<CustomerReportHandoverInput, CustomerReportFullDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerReportFullDto Cancel(CustomerReportHandoverInput input)
        {
            return GetResult<CustomerReportHandoverInput, CustomerReportFullDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void BatchSend(List<CustomerReportHandoverInput> input)
        {
            GetResult<List<CustomerReportHandoverInput>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public CustomerReportFullDto Get(CustomerReportByNumber input)
        {
            return GetResult<CustomerReportByNumber, CustomerReportFullDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<CustomerReportFullDto> Query(CustomerReportQuery input)
        {
            return GetResult<CustomerReportQuery, List<CustomerReportFullDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 柜子设置
        /// </summary>
        /// <returns></returns>
        public TbmCabinetDto getTbmCabinet()
        {
            return GetResult<TbmCabinetDto>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存柜子设置
        /// </summary>
        /// <returns></returns>
        public TbmCabinetDto SaveTbmCabinet(TbmCabinetDto input)
        {
            return GetResult<TbmCabinetDto, TbmCabinetDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 查询柜子存放记录
        /// </summary>
        /// <returns></returns>
        public List<TjlCusCabitDto> getTjlCabinet()
        {
            return GetResult<List<TjlCusCabitDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 保存柜子存放设置
        /// </summary>
        /// <returns></returns>
        public TjlCusCabitDto SaveTjlCabinet(TjlCusCabitDto input)
        {
            return GetResult<TjlCusCabitDto, TjlCusCabitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 删除柜子存放设置
        /// </summary>
        /// <returns></returns>
        public void DelTjlCabinet(TjlCusCabitDto input)
        {            
            GetResult<TjlCusCabitDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CustomerUpCatDto UpCustomerUpCat(CustomerUpCatDto input)
        {
            return GetResult<CustomerUpCatDto, CustomerUpCatDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TjlCusCabitCRDto> getTjlCabinetlist(SeachCusCabDto input)
        {
            return GetResult<SeachCusCabDto, List<TjlCusCabitCRDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public CusCabitLQDto SaveTjlCabinetLQ(CusCabitLQDto input)
        {
            return GetResult<CusCabitLQDto, CusCabitLQDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public ChargeBM IsSH(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, ChargeBM>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 修改单位柜子存方设置
        /// </summary>
        /// <returns></returns>
        public   ClientRegUpCatDto UpClientRegUpCat(ClientRegUpCatDto input)
        {
            return GetResult<ClientRegUpCatDto, ClientRegUpCatDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 团报报告领取查询
        /// </summary>
        /// <returns></returns>
        public List<ClientCabitCRDto> getClientCabinetlist(SearchClientRegLQDto input)
        {
            return GetResult<SearchClientRegLQDto, List<ClientCabitCRDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
