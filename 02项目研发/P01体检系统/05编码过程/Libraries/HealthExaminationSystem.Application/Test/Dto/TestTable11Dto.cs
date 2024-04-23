using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Test;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Test.Dto
{
#if !Proxy
    [AutoMap(typeof(TestTable1))]
#endif
    public class TestTable11Dto : EntityDto<Guid>
    {
        public virtual string Column1 { get; set; }
        public virtual string Column2 { get; set; }
        public virtual string Column3 { get; set; }
        public virtual string Column4 { get; set; }
        public virtual string Column5 { get; set; }
        public virtual string Column6 { get; set; }
    }
}