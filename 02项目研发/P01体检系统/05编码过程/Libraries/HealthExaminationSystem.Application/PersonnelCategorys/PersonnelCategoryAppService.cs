using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys
{
    public class PersonnelCategoryAppService : MyProjectAppServiceBase, IPersonnelCategoryAppService
    {
        public readonly IRepository<PersonnelCategory, Guid> _categoryRepository; //人员类别

        public PersonnelCategoryAppService(IRepository<PersonnelCategory, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 查询人员类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<PersonnelCategoryViewDto> QueryCategoryList(PersonnelCategoryViewDto dto)
        {
            var CategoryList = _categoryRepository.GetAll();
            return CategoryList.MapTo<List<PersonnelCategoryViewDto>>();
        }

        /// <summary>
        /// 新增人员类别
        /// </summary>
        /// <param name="dto"></param>
        public PersonnelCategoryViewDto SaveCategory(PersonnelCategoryViewDto dto)
        {
            if (dto != null)
            {
                if (_categoryRepository.GetAll().Any(o => o.Name == dto.Name))
                    throw new FieldVerifyException("类别名称重复！", "类别名称重复！");
                var cate = _categoryRepository.Insert(dto.MapTo<PersonnelCategory>());
                return cate.MapTo<PersonnelCategoryViewDto>();
            }

            return null;
        }

        /// <summary>
        /// 编辑人员类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool EditeCategory(PersonnelCategoryViewDto dto)
        {
            if (dto != null)
            {
                var cate = _categoryRepository.Get(dto.Id);
                _categoryRepository.Update(dto.MapTo(cate));
                return true;
            }

            return false;
        }
        /// <summary>
        /// 删除啊人员类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool DeleteCategory(PersonnelCategoryViewDto dto)
        {
            if (dto != null)
            {
                var cate = _categoryRepository.Get(dto.Id);
                _categoryRepository.Delete(dto.MapTo(cate));
                return true;
            }

            return false;
        }
    }
}