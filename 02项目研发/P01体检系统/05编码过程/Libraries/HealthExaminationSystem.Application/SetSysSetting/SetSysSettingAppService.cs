using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;

namespace Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting
{
    [AbpAuthorize]
    public class SetSysSettingAppService : ISetSysSettingAppService
    {
        #region 声明变量

        private readonly IRepository<TdbSetSysSetting, Guid> SetSysSetting;

        #endregion

        #region 构造函数

        public SetSysSettingAppService(IRepository<TdbSetSysSetting, Guid> _SetSysSetting)
        {
            SetSysSetting = _SetSysSetting;
        }

        #endregion
        /// <summary>
        /// 获取参数值
        /// </summary>  
        public string GetSysSetting(QuerySetSysSettingDto QuerySetSysSetting)
        {
            var IdNumber = SetSysSetting.FirstOrDefault(p => p.KeyID == QuerySetSysSetting.KeyID||p.KeyName==QuerySetSysSetting.KeyName);
            return IdNumber.KeyValue;

        }

        /// <summary>
        /// 获取所有参数值
        /// </summary>  
        public List<SetSysSettingDto> GetAllSysSetting()
        {
            var query=   SetSysSetting.GetAll().ToList();
            return query.MapTo<List<SetSysSettingDto>>();

        }
    }
}
