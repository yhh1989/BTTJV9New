using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys
{
    public class PersonnelCategoryAppService : AppServiceApiProxyBase, IPersonnelCategoryAppService
    {
        public List<PersonnelCategoryViewDto> QueryCategoryList(PersonnelCategoryViewDto dto)
        {
            return GetResult<PersonnelCategoryViewDto, List<PersonnelCategoryViewDto>>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new System.NotImplementedException();
        }

        public PersonnelCategoryViewDto SaveCategory(PersonnelCategoryViewDto dto)
        {
            return GetResult<PersonnelCategoryViewDto, PersonnelCategoryViewDto>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new System.NotImplementedException();
        }

        public bool EditeCategory(PersonnelCategoryViewDto dto)
        {
            return GetResult<PersonnelCategoryViewDto, bool>(dto, DynamicUriBuilder.GetAppSettingValue());
            //throw new System.NotImplementedException();
        }
        public bool DeleteCategory(PersonnelCategoryViewDto dto)
        {
            return GetResult<PersonnelCategoryViewDto, bool>(dto, DynamicUriBuilder.GetAppSettingValue());

        }
    }
}