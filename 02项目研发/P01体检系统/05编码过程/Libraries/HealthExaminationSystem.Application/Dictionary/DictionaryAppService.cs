using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Application.Dictionary
{
    public class DictionaryAppService : MyProjectAppServiceBase, IDictionaryAppService
    {


        #region 接口和引用
        private readonly IRepository<TbmDepartment, Guid> _tbmDepartment;//科室查询
        private readonly IRepository<TbmItemInfo, Guid> _tbmItemInfo;//项目查询
        private readonly IRepository<TbmItemDictionary, Guid> _tbmItemDictionary;//项目字典

        public DictionaryAppService(
               IRepository<TbmDepartment, Guid> tbmDepartment,
               IRepository<TbmItemInfo, Guid> tbmItemInfo,
               IRepository<TbmItemDictionary, Guid> tbmItemDictionary
            )
        {
            _tbmDepartment = tbmDepartment;
            _tbmItemInfo = tbmItemInfo;
            _tbmItemDictionary = tbmItemDictionary;
        }
        #endregion

        #region 查询科室项目
        /// <summary>
        /// 科室项目查询
        /// </summary>
        public List<DictionaryItemInfoDto> QueryInfoDepart(SearchItemInfoDto input)
        {


            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                var itemInfo = _tbmItemInfo.GetAllIncluding(r => r.Department);
                if (input.DepartmentId.HasValue && input.DepartmentId != Guid.Empty)
                    itemInfo = itemInfo.Where(i => i.DepartmentId == input.DepartmentId);
                if (!string.IsNullOrWhiteSpace(input.Name))
                    itemInfo = itemInfo.Where(i => i.Name.Contains(input.Name) || i.HelpChar.Contains(input.Name) || i.WBCode.Contains(input.Name) || i.NameEngAbr.Contains(input.Name));
                var itemdic = itemInfo.OrderBy(o=>o.Department.OrderNum).MapTo<List<DictionaryItemInfoDto>>();
                return itemdic;
            //}

        }
        #endregion

        /// <summary>
        /// 用科室项目主键查找
        /// </summary>
        /// <param name="input">主键</param>
        public List<DictionaryItemDictionaryDto> GetById( DictionaryItemDictionaryDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var department = _tbmItemDictionary.GetAllIncluding(r => r.DepartmentBM , r => r.iteminfoBM);

                department = department.Where(i => i.iteminfoBMId == input.iteminfoBMId && i.DepartmentBMId == input.DepartmentBMId && i.IsDeleted == false);
                department = department.OrderByDescending(o => o.CreationTime);
                var row = department.MapTo<List<DictionaryItemDictionaryDto>>();
                return row;
            }

        }

        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        public DictionaryItemDictionaryDto GetByDictionaryId(EntityDto<Guid> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var department = _tbmItemDictionary.Get(input.Id);
                return department.MapTo<DictionaryItemDictionaryDto>();

                //var query = _tbmItemDictionary.GetAll();
                //query = query.Where(o => o.Id == input.Id);
                //var sew = query.MapTo<DictionaryItemDictionaryDto>();
                //return sew;
            }

        }

        /// <summary>
        /// 删除项目字典
        /// </summary>
        /// <param name="input">主键</param>
        public void DeleteItemDictionary(EntityDto<Guid> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var department = _tbmItemDictionary.Get(input.Id);
                _tbmItemDictionary.Delete(department);
            }
            
        }

        /// <summary>
        /// 添加项目字典
        /// </summary>
        /// <param name="input">类</param>
        public void InsertItemDictionary(DictionaryItemDictionaryDto input)
        {
            var dept = input.MapTo<TbmItemDictionary>();
            dept.Id = Guid.NewGuid();
            _tbmItemDictionary.Insert(dept);
        }

        /// <summary>
        /// 修改项目字典
        /// </summary>
        /// <param name="input">类</param>
        public void UpdateItemDictionary(DictionaryItemDictionaryDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var dept = _tbmItemDictionary.Get(input.Id);
                input.MapTo(dept);
                _tbmItemDictionary.Update(dept);
            }

        }


    }

}