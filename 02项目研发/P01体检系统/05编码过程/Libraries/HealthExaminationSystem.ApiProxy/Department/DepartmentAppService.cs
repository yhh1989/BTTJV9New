using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Department
{
    public class DepartmentAppService : AppServiceApiProxyBase, IDepartmentAppService
    {
        public PageResultDto<TbmDepartmentDto> QueryDepartment(PageInputDto<TbmDepartmentDto> input)
        {
            return GetResult<PageInputDto<TbmDepartmentDto>, PageResultDto<TbmDepartmentDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }


        public TbmDepartmentDto GetById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, TbmDepartmentDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteDepartment(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void InsertDepartmentn(CreateDepartmentDto input)
        {
            GetResult<CreateDepartmentDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TbmDepartmentDto> GetAll()
        {
            return GetResult<List<TbmDepartmentDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<TbmDepartmentDto> GetByName(SearchDepartmentNameDto input)
        {
            return GetResult<SearchDepartmentNameDto, List<TbmDepartmentDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Update(UpdateDepartmentDto input)
        {
            GetResult<UpdateDepartmentDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ItemInfoDto> QueryDepartmentItemInfo(ItemInfoDto PaDto)
        {
            return GetResult<ItemInfoDto, List<ItemInfoDto>>(PaDto, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<DepartmentSimpleDto> QuerySimples()
        {
            return GetResult<List<DepartmentSimpleDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public void UpdateOrder(ChargeBM input)
        {
             GetResult<ChargeBM>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public int? GetMaxOrderNum()
        {
            return GetResult<int?>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}