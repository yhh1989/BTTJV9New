using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement
{
    public class InvoiceManagementAppService : AppServiceApiProxyBase, IInvoiceManagementAppService
    {
        public List<UserViewDto> GetUser()
        {
            return GetResult<List<UserViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptManagerDto> AddReceiptManage(AddReceiptManageDto input)
        {
            return GetResult<AddReceiptManageDto, List<MReceiptManagerDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public MReceiptManagerDto EditReceiptManage(AddReceiptManageDto input)
        {
            return GetResult<AddReceiptManageDto, MReceiptManagerDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteReceiptManage(MReceiptManagerDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptManagerDto> QueryReceiptManage(MReceiptManagerDto input)
        {
            return GetResult<MReceiptManagerDto, List<MReceiptManagerDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<MReceiptManagerDto> GetUserInvoice()
        {
            return GetResult<List<MReceiptManagerDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}