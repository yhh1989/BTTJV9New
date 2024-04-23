using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.Department
{
    [AbpAuthorize]
    public class DepartmentAppService : MyProjectAppServiceBase, IDepartmentAppService
    {
        private readonly IRepository<TbmDepartment, Guid> _departmentRepository; //科室设置
        private readonly IRepository<TbmItemGroup, Guid> _ItemGroupRepository; //组合设置
        private readonly IRepository<TbmItemInfo, Guid> _ItemInfoRepository; //项目设置

        public DepartmentAppService(
            IRepository<TbmDepartment, Guid> departmentRepository,
            IRepository<TbmItemGroup, Guid> ItemGroupRepository,
             IRepository<TbmItemInfo, Guid> ItemInfoRepository)
        {
            _departmentRepository = departmentRepository;
            _ItemGroupRepository = ItemGroupRepository;
            _ItemInfoRepository = ItemInfoRepository;

        }

        /// <summary>
        /// 获取所有科室信息/部门名称
        /// </summary>
        public PageResultDto<TbmDepartmentDto> QueryDepartment(PageInputDto<TbmDepartmentDto> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            { 
                var query = _departmentRepository.GetAll();
                if (!string.IsNullOrWhiteSpace(input.Input.Name)) //部门名称
                    query = query.Where(o =>
                        o.Name.Contains(input.Input.Name) || o.HelpChar.Contains(input.Input.Name));
                query = query.Where(o => o.IsDeleted == input.Input.IsDeleted);
              
                    query = query.Where(o => o.IsActive == input.Input.IsActive);

                if (query.Count() != 0)
                {
                    query = query.OrderBy(o => o.OrderNum).ThenBy(r => r.Name);
                    var result = new PageResultDto<TbmDepartmentDto> { CurrentPage = input.CurentPage };
                    result.Calculate(query.Count());
                    query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
                    result.Result = query.MapTo<List<TbmDepartmentDto>>();
                    return result;
                }


                return null;
            }
        }

        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        public TbmDepartmentDto GetById(EntityDto<Guid> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var department = _departmentRepository.Get(input.Id);
                return department.MapTo<TbmDepartmentDto>();
            }
        }

        /// <summary>
        /// 删除科室信息
        /// </summary>
        /// <param name="input">主键</param>
        public void DeleteDepartment(EntityDto<Guid> input)
        {
            var pa = _departmentRepository.FirstOrDefault(o => o.Id == input.Id);
            _departmentRepository.Delete(pa);
            //禁用该科室下所有组合及项目
            _ItemGroupRepository.GetAll().Where(o => o.DepartmentId == pa.Id).Update(o => new TbmItemGroup { IsDeleted = true });
            _ItemInfoRepository.GetAll().Where(o => o.DepartmentId == pa.Id).Update(o => new TbmItemInfo { IsDeleted = true });
        }

        /// <summary>
        /// 添加编辑科室信息
        /// </summary>
        /// <param name="input">类</param>
        public void InsertDepartmentn(CreateDepartmentDto input)
        {
            if (_departmentRepository.GetAll().Any(r => r.DepartmentBM == input.DepartmentBM))
                throw new FieldVerifyException("科室编码重复！", "科室编码重复！");
            if (_departmentRepository.GetAll().Any(r => r.Name == input.Name))
                throw new FieldVerifyException("科室名称重复！", "科室名称重复！");
            var dept = input.MapTo<TbmDepartment>();
            dept.Id = Guid.NewGuid();
            _departmentRepository.Insert(dept);
        }

        public async Task<List<TbmDepartmentDto>> GetAll()
        {
            
            var result = (await _departmentRepository.GetAllListAsync(p=>p.IsActive!=true)).OrderBy(o => o.OrderNum);
            return result.MapTo<List<TbmDepartmentDto>>();
        }

        public List<TbmDepartmentDto> GetByName(SearchDepartmentNameDto input)
        {
            var result = _departmentRepository.GetAllList(d => d.Name.Contains(input.Name));
            return result.MapTo<List<TbmDepartmentDto>>();
        }

        public void Update(UpdateDepartmentDto input)
        {
            if (_departmentRepository.GetAll().Any(r => r.DepartmentBM == input.DepartmentBM && r.Id != input.Id))
                throw new FieldVerifyException("科室编码重复！", "科室编码重复！");
            if (_departmentRepository.GetAll().Any(r => r.Name == input.Name && r.Id != input.Id))
                throw new FieldVerifyException("科室名称重复！", "科室名称重复！");
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {

                var dept = _departmentRepository.Get(input.Id);
                bool isdel = dept.IsDeleted;
                input.MapTo(dept);
                _departmentRepository.Update(dept);
                //禁用所有组合及项目将会禁用
                if (isdel != dept.IsDeleted)
                {

                    if (dept.IsDeleted == true)
                    {            //禁用该科室下所有组合及项目
                        _ItemGroupRepository.GetAll().Where(o => o.DepartmentId == dept.Id).Update(o => new TbmItemGroup { IsDeleted = true });
                        _ItemInfoRepository.GetAll().Where(o => o.DepartmentId == dept.Id).Update(o => new TbmItemInfo { IsDeleted = true });
                    }
                    else
                    {
                        _ItemGroupRepository.GetAll().Where(o => o.DepartmentId == dept.Id).Update(o => new TbmItemGroup { IsDeleted = false });
                        _ItemInfoRepository.GetAll().Where(o => o.DepartmentId == dept.Id).Update(o => new TbmItemInfo { IsDeleted = false });
                    }
                }
            }
        }


        public List<DepartmentSimpleDto> QuerySimples()
        {
            return _departmentRepository.GetAll().OrderBy(m => m.OrderNum).MapTo<List<DepartmentSimpleDto>>();
        }
        /// <summary>
        /// 科室排序
        /// </summary>
        /// <param name="input"></param>
        public void UpdateOrder(ChargeBM input)
        {
            TbmDepartment tbmDepartment = _departmentRepository.Get(input.Id);
            tbmDepartment.OrderNum =int.Parse(input.Name);
            _departmentRepository.Update(tbmDepartment);
        }
        public int? GetMaxOrderNum()
        {
            var result = _departmentRepository.Count();
            if (result != 0)
            {
                result =(int)_departmentRepository.GetAll().Max(o => o.OrderNum);
            }
            return result;
        }
    }
}