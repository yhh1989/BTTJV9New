using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.IDNumber
{
    public class IDNumberAppService : AppServiceApiProxyBase, IIDNumberAppService
    {
        public string CreateDepartmentBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateClientBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateItemGroupBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateItemBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateEmployeeBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateItemSuitBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateClientRegBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateCustomerBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateCustomerRegBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateArchivesNumBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
        public string CreateBarBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<IDNumberDto> GetAllList()
        {
            return GetResult<List<IDNumberDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public void Create(CreateIdNumberDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public IDNumberDto GetByName(NameDto input)
        {
            return GetResult<NameDto, IDNumberDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public string CreateTeamBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
        public string CreateAdviceBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
        public string CreateApplicationBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
       
        public string CreateJKZBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
        public string CreatHGZBM()
        {
            return GetResult<string>(DynamicUriBuilder.GetAppSettingValue());
        }
        public int CreateRegNum()
        {
            return GetResult<int>(DynamicUriBuilder.GetAppSettingValue());

        }
        public string CreateClientZKBM()
        { return GetResult<string>(DynamicUriBuilder.GetAppSettingValue()); }

    }
}