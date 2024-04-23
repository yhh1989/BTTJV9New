using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Test.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Test
{
    public class TestTable1AppService : AppServiceApiProxyBase, ITestTable1AppService
    {
        public ICollection<TestTable1Dto> GetAll()
        {
            return GetResult<ICollection<TestTable1Dto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public ICollection<TestTable11Dto> GetAll1()
        {
            return GetResult<ICollection<TestTable11Dto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public ICollection<TestTable1Dto> Query(TestTable1Dto input)
        {
            return GetResult<TestTable1Dto, ICollection<TestTable1Dto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}