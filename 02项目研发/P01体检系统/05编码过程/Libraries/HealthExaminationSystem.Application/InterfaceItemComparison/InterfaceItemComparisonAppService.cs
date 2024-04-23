using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison
{
    [AbpAuthorize]
    public class InterfaceItemComparisonAppService : IInterfaceItemComparisonAppService
    {
        private readonly IRepository<TdbInterfaceItemComparison, Guid> _InterfaceItemComparison;
        public InterfaceItemComparisonAppService(IRepository<TdbInterfaceItemComparison, Guid> InterfaceItemComparison)
        {
            _InterfaceItemComparison = InterfaceItemComparison;
           
        }
        /// <summary>
        /// 根据对应的项目id获取项目id
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        public List<InterfaceItemComparisonDto> Getlst(TInterfaceresult Interfaceresult)
        {
            var query = _InterfaceItemComparison.GetAllList(p => p.ObverseItemId==Interfaceresult.initemid &&
           p.InstrumentModelNumber.Contains(Interfaceresult.inYQid));

            var dto = query.MapTo<List<InterfaceItemComparisonDto>>();
            return dto;
        }     


    }
}
