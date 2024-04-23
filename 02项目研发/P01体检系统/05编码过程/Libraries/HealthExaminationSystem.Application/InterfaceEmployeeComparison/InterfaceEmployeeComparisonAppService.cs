using System;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceEmployeeComparison
{
    [AbpAuthorize]
    public class InterfaceEmployeeComparisonAppService : IInterfaceEmployeeComparison
    {
        private readonly IRepository<TdbInterfaceEmployeeComparison, Guid> _InterfaceEmployeeComparison;
        public InterfaceEmployeeComparisonAppService(IRepository<TdbInterfaceEmployeeComparison, Guid> InterfaceEmployeeComparison)
        {
            _InterfaceEmployeeComparison = InterfaceEmployeeComparison;
           
        }
        /// <summary>
        /// 根据对应的id获取检查医生
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        public InterfaceEmployeeComparisonDto GetJC(TInterfaceresult Interfaceresult)
        {
            var query = _InterfaceEmployeeComparison.FirstOrDefault(p => p.ObverseEmpId== Interfaceresult.indoctorname);
            var dto = query.MapTo<InterfaceEmployeeComparisonDto>();
            return dto;
        }

        /// <summary>
        /// 根据对应的项目id获取审核
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        public InterfaceEmployeeComparisonDto GetSH(TInterfaceresult Interfaceresult)
        {
            var query = _InterfaceEmployeeComparison.FirstOrDefault(p =>  p.ObverseEmpId == Interfaceresult.inSHdoctorname);
            var dto = query.MapTo<InterfaceEmployeeComparisonDto>();
            return dto;
        }


    }
}
