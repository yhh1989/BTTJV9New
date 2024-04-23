using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition;
using Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.SchedulingSecondEdition
{
    /// <summary>
    /// 人工行程安排应用服务
    /// </summary>
    public class ManualSchedulingAppService : AppServiceApiProxyBase, IManualSchedulingAppService
    {
        /// <inheritdoc />
        public Task<List<ManualSchedulingDtoNo1>> QueryManualSchedulingList()
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ManualSchedulingDtoNo1>>.Factory.StartNew(() =>
                GetResult<List<ManualSchedulingDtoNo1>>(url));
        }

        /// <inheritdoc />
        public Task<ManualSchedulingDtoNo1> InsertManualScheduling(ManualSchedulingDtoNo2 input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ManualSchedulingDtoNo1>.Factory.StartNew(() =>
                GetResult<ManualSchedulingDtoNo2, ManualSchedulingDtoNo1>(input, url));
        }

        /// <inheritdoc />
        public Task<ManualSchedulingDtoNo1> UpdateManualScheduling(ManualSchedulingDtoNo2 input)
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<ManualSchedulingDtoNo1>.Factory.StartNew(() =>
                GetResult<ManualSchedulingDtoNo2, ManualSchedulingDtoNo1>(input, url));
        }

        /// <inheritdoc />
        public Task<List<ManualSchedulingDtoNo1>> QueryPersonSchedulingList()
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ManualSchedulingDtoNo1>>.Factory.StartNew(() =>
                GetResult<List<ManualSchedulingDtoNo1>>(url));
        }

        /// <inheritdoc />
        public Task<List<ManualSchedulingDtoNo1>> QueryCompanySchedulingList()
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ManualSchedulingDtoNo1>>.Factory.StartNew(() =>
                GetResult<List<ManualSchedulingDtoNo1>>(url));
        }
    }
}
