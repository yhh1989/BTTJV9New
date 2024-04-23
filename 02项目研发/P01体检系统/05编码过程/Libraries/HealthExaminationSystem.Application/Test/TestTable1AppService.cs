using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Test.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Test;

namespace Sw.Hospital.HealthExaminationSystem.Application.Test
{
    [AbpAuthorize]
    public class TestTable1AppService : MyProjectAppServiceBase, ITestTable1AppService
    {
        private readonly IRepository<TestTable1, Guid> _testTable1Repository;

        public TestTable1AppService(IRepository<TestTable1, Guid> testTable1Repository)
        {
            _testTable1Repository = testTable1Repository;
        }
        
        public ICollection<TestTable1Dto> GetAll()
        {
            //var ddd = _testTable1Repository.Load(new Guid("075BA85B-BE49-4AFD-99BB-00093403CEE5"));
            var result = _testTable1Repository.GetAll();
            var dddd = new TestTable1();
            //ObjectMapper.Map(ddd, dddd);
            dddd.Id = Guid.NewGuid();
            dddd.Column1 = "1";
            dddd.Column2 = "2";
            dddd.Column3 = "3";
            dddd.Column4 = "4";
            dddd.Column5 = "5";
            var tf = dddd.IsTransient();
            var id = _testTable1Repository.InsertAndGetId(dddd);
             var ddddd = _testTable1Repository.Load(dddd.Id);
            var ddddds = _testTable1Repository.GetAllList(r => r.Column1 == "1");
            //CurrentUnitOfWork.SaveChanges();
            return result.MapTo<ICollection<TestTable1Dto>>();
        }

        public ICollection<TestTable11Dto> GetAll1()
        {
            var result = _testTable1Repository.GetAll();
            return result.MapTo<ICollection<TestTable11Dto>>();
        }

        public ICollection<TestTable1Dto> Query(TestTable1Dto input)
        {
            var result = _testTable1Repository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.Column1))
            {
                result = result.Where(r => r.Column1 == input.Column1);
            }
            if (!string.IsNullOrWhiteSpace(input.Column2))
            {
                result = result.Where(r => r.Column2 == input.Column2);
            }
            if (!string.IsNullOrWhiteSpace(input.Column3))
            {
                result = result.Where(r => r.Column3 == input.Column3);
            }
            if (!string.IsNullOrWhiteSpace(input.Column4))
            {
                result = result.Where(r => r.Column4 == input.Column4);
            }
            if (!string.IsNullOrWhiteSpace(input.Column5))
            {
                result = result.Where(r => r.Column5 == input.Column5);
            }
            if (!string.IsNullOrWhiteSpace(input.Column6))
            {
                result = result.Where(r => r.Column6 == input.Column6);
            }
            return result.MapTo<ICollection<TestTable1Dto>>();
        }
    }
}