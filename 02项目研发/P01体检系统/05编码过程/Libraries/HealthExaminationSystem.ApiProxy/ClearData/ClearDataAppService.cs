using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.ClearData
{
    public class ClearDataAppService : AppServiceApiProxyBase, IClearDataAppService
    {
        public DataBaseDto TimeDeleteData(ClearDataDto input)
        {
            return GetResult<ClearDataDto, DataBaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public DataBaseDto AllDeleteData(InputClearData input)
        {
            return GetResult<InputClearData,DataBaseDto>(input,DynamicUriBuilder.GetAppSettingValue());
         }
        public DataBaseDto delTableByTiem(ClearDataDto input)
        {
            return GetResult<ClearDataDto, DataBaseDto> (input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
