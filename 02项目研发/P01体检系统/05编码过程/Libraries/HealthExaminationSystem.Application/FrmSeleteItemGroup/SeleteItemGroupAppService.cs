using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup
{
    [AbpAuthorize]
    public class SeleteItemGroupAppService : MyProjectAppServiceBase, ISeleteItemGroupAppService
    {
        #region 接口和引用
        private readonly IRepository<TbmItemGroup, Guid> _tbmItemGroup; //科室组合项目查询

        public SeleteItemGroupAppService(
            IRepository<TbmItemGroup, Guid> tbmItemGroup
        )
        {
            _tbmItemGroup = tbmItemGroup;
        }
        #endregion

        #region 查询科室组合
        /// <summary>
        /// 科室组合查询
        /// </summary>
        [UnitOfWork(false)]
        public List<SeleteItemGroupDto> QueryInfoGroup(SeleteItemGroupDto input)
        {
            var paDtoList = _tbmItemGroup.GetAll();
            //体检号
            if (input.ItemGroupName != null && input.ItemGroupName != "")
                paDtoList = paDtoList.Where(o => o.ItemGroupName.Contains(input.ItemGroupName));
            if (input.CurSex ==1)
            {
                paDtoList = paDtoList.Where(o => o.Sex !=2);
            }
            if(input.CurSex == 2)
            {
                paDtoList = paDtoList.Where(o => o.Sex != 1);
            }
            var rows = paDtoList.MapTo<List<SeleteItemGroupDto>>();
            return rows;
        }
        #endregion

        
    }
}