using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Test.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Test
{
    public interface ITestTable1AppService
#if !Proxy
        : IApplicationService
#endif
    {
        ICollection<TestTable1Dto> GetAll();

        ICollection<TestTable11Dto> GetAll1();

        ICollection<TestTable1Dto> Query(TestTable1Dto input);
    }
}