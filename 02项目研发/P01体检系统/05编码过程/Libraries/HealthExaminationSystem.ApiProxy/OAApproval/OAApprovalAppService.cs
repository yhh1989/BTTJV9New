using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval
{
    public class OAApprovalAppService : AppServiceApiProxyBase, IOAApprovalAppService
    {
        /// <summary>
        /// 添加修改审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreatOAApproValcsDto creatOAApproValcs(CreatOAApproValcsDto input)
        {
            return GetResult<CreatOAApproValcsDto, CreatOAApproValcsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 删除审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelOAApproValcs(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        ///查询审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CreatOAApproValcsDto> SearchOAApproValcs(SearchOAApproValcsDto input)
        {
            return GetResult<SearchOAApproValcsDto, List<CreatOAApproValcsDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 添加修改审批设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateClientXKSetDto creatCreateClientXKSet(CreateClientXKSetDto input)
        {
            return GetResult<CreateClientXKSetDto, CreateClientXKSetDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        ///查询审批设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateClientXKSetDto getCreateClientXKSet()
        {
            return GetResult<CreateClientXKSetDto>( DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreatOAApproValcsDto upOAApproValcsDto(UpOAApproValcsDto input)
        {
           // return GetResult<CreateClientXKSetDto>(DynamicUriBuilder.GetAppSettingValue());

            return GetResult<UpOAApproValcsDto, CreatOAApproValcsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
     
    }
}
