using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;


namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseSet.OccdiseaseMain
{
   
   public class OccDiseaseAppService : AppServiceApiProxyBase, IOccDiseaseAppService
    {
      
        /// <summary>
        /// 获取全部职业健康
        /// </summary>
        /// <returns></returns>
        public List<OutOccDiseaseDto> GetAllOccDisease(Occdieaserucan input)
        {
            return GetResult<Occdieaserucan, List<OutOccDiseaseDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

       
        public List<OutOccDiseaseDto>  Get()
        {
            return GetResult<List<OutOccDiseaseDto>>( DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 职业健康添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccDiseaseDto Add(OccDieaseAndStandardDto input)
        {

            return GetResult<OccDieaseAndStandardDto, OutOccDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 删除职业健康
        /// </summary>
        /// <param name="input"></param>
        public void OccDel(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        //返显(根据id查职业健康)
        public OutOccDiseaseDto GetById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, OutOccDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 职业健康修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccDiseaseDto Edit(OccDieaseAndStandardDto input)
        {
            return GetResult<OccDieaseAndStandardDto, OutOccDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <summary>
        /// 获取职业健康标准
        /// </summary>
        /// <returns></returns>
        public List<OccDiseaseStandardDto> GetStandard()
        {
            return GetResult<List<OccDiseaseStandardDto>>(DynamicUriBuilder.GetAppSettingValue());
        }




    }
}
