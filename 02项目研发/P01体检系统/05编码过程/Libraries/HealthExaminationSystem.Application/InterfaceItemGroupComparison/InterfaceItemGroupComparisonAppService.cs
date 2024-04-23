using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison
{
    [AbpAuthorize]
    public class InterfaceItemGroupComparison : IInterfaceItemGroupComparison
    {
        private readonly IRepository<TdbInterfaceItemGroupComparison, Guid> _InterfaceItemGroupComparison;
        public InterfaceItemGroupComparison(IRepository<TdbInterfaceItemGroupComparison, Guid> InterfaceItemComparison)
        {
            _InterfaceItemGroupComparison = InterfaceItemComparison;
           
        }
        /// <summary>
        /// 根据对应的项目id获取项目id
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        public InterfaceItemGroupComparisonDto Get(TInterfaceresult Interfaceresult)
        {
            var query = _InterfaceItemGroupComparison.FirstOrDefault(p => p.ObverseItemId == Interfaceresult.initemid &&
            p.InstrumentModelNumber == Interfaceresult.inYQid);

            var dto = query.MapTo<InterfaceItemGroupComparisonDto>();
            return dto;
        }

       
    }
}
