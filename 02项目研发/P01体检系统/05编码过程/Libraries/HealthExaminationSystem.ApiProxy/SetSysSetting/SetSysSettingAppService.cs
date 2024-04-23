using Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting
{
    public class SetSysSettingAppService : ISetSysSettingAppService
    {

        /// <summary>
        /// 获取参数值
        /// </summary>  
        public string GetSysSetting(QuerySetSysSettingDto QuerySetSysSetting)
        {
            //var IdNumber = SetSysSetting.FirstOrDefault(p => p.KeyID == QuerySetSysSetting.KeyID||p.KeyName==QuerySetSysSetting.KeyName);
            //return IdNumber.KeyValue;
            return "";
        }

        /// <summary>
        /// 获取所有参数值
        /// </summary>  
        public List<SetSysSettingDto> GetAllSysSetting()
        {
            // var query=   SetSysSetting.GetAll().ToList();
            //return query.MapTo<List<SetSysSettingDto>>();
            return new List<SetSysSettingDto>();
        }
    }
}
