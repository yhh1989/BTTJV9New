using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using static Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto.OccDiseaseDto;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain
{
    [AbpAuthorize]

    public class OccDiseaseAppService : MyProjectAppServiceBase,IOccDiseaseAppService

       
    {
        private readonly IRepository<Core.Occupational.TbmOccDisease, Guid> _tbmoccdiseaseRepository;
        private readonly IRepository<TbmOccStandard, Guid> _tbmoccstandardRepository;

        public OccDiseaseAppService(IRepository<Core.Occupational.TbmOccDisease, Guid> tbmoccdiseaseRepository,
             IRepository<TbmOccStandard, Guid> tbmoccstandardRepository)
        {
            _tbmoccdiseaseRepository = tbmoccdiseaseRepository;
            _tbmoccstandardRepository = tbmoccstandardRepository;
        }
      

        /// <summary>
        /// 获取全部职业健康
        /// </summary>
        /// <returns></returns>
        public List<OutOccDiseaseDto> GetAllOccDisease(Occdieaserucan input)
        {
            var Occdiease = _tbmoccdiseaseRepository.GetAll();
            if (input.IsActive != null)
            {
                Occdiease = Occdiease.Where(i => i.IsActive == input.IsActive);
            }
            if (input.ParentId != null)
                Occdiease = Occdiease.Where(i => i.ParentId == input.ParentId);
            if (input.Text != null)
            {
                Occdiease = Occdiease.Where(i => i.Text == input.Text);
            }
            //var result = Occdiease.Select(o => new OutOccDiseaseDto
            //{
            //    Text=o.Text,
            //    IsActive=o.IsActive,
            //    OrderNum=o.OrderNum,
            //    HelpChar = o.HelpChar,
            //    Id = o.Id,
            //    ParentName = o.Parent.Text,
               
            //});
            return Occdiease.MapTo<List<OutOccDiseaseDto>>();
        }

        public List<OutOccDiseaseDto> Get()
        {
            var Occdiease = _tbmoccdiseaseRepository.GetAll();
            return Occdiease.MapTo<List<OutOccDiseaseDto>>();
        }

        /// <summary>
        /// 职业健康添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccDiseaseDto Add(OccDieaseAndStandardDto input)
        {
            if (string.IsNullOrEmpty(input.occDisease.Text))
            {
                throw new FieldVerifyException("分类名称不可为空！", "分类名称不可为空！");
            }
            if (_tbmoccdiseaseRepository.GetAll().Any(r => r.Text == input.occDisease.Text))
            {
                throw new FieldVerifyException("分类名称重复！", "分类名称重复！");
            }           
           //保存职业健康
            var entity = input.occDisease.MapTo<TbmOccDisease>();
            entity.Id = Guid.NewGuid();
            entity = _tbmoccdiseaseRepository.Insert(entity);
            entity.TbmOccStandards = null;
            entity.TbmOccStandards = new List<TbmOccStandard>();
            //保存标准
            if (input.listStandard != null && input.listStandard.Count > 0)
            {
                foreach (var stand in input.listStandard)
                {
                    var tbmstand = stand.MapTo<TbmOccStandard>();
                    tbmstand.Id= Guid.NewGuid();
                    tbmstand.OccDiseasesId = entity.Id;
                    tbmstand= _tbmoccstandardRepository.Insert(tbmstand);
                    entity.TbmOccStandards.Add(tbmstand);
                }

            }
            var dto = entity.MapTo<OutOccDiseaseDto>();
                
            return dto;
          
        }

        /// <summary>
        /// 删除职业健康
        /// </summary>
        /// <param name="input"></param>
        public void OccDel(EntityDto<Guid> input)
        {
            var entity = _tbmoccdiseaseRepository.FirstOrDefault(input.Id);
            if (entity != null)
                _tbmoccdiseaseRepository.Delete(entity);
        }

        //反显
        public OutOccDiseaseDto GetById(EntityDto<Guid> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var data = _tbmoccdiseaseRepository.Get(input.Id);
                return data.MapTo<OutOccDiseaseDto>();
            }

        }

        /// <summary>
        /// 职业健康修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccDiseaseDto Edit(OccDieaseAndStandardDto input)
        {
                 
            //yhh修改标准，先删除，再添加 
            if (input.listStandard != null && input.listStandard.Count > 0)
            {
                var stanemels = input.listStandard.Select(o=>o.StandardName).ToList();
                //删除没有的标准
                var delet = _tbmoccstandardRepository.GetAll().Where(o => o.OccDiseasesId == input.occDisease.Id && !stanemels.Contains(o.StandardName)).Delete();
                foreach (var stand in input.listStandard)
                {
                    var ishs = _tbmoccstandardRepository.FirstOrDefault(o => o.OccDiseasesId == input.occDisease.Id && o.StandardName == stand.StandardName);
                    //已有修改
                    if (ishs != null)
                    {

                        // 更新标准
                        stand.MapTo(ishs);
                        ishs= _tbmoccstandardRepository.Update(ishs);
                       

                    }
                    else
                    {
                        //新增标准
                        stand.Id = Guid.NewGuid();
                        var itemGroupEntity = stand.MapTo<TbmOccStandard>();
                        itemGroupEntity.OccDiseasesId = input.occDisease.Id;
                        ishs = _tbmoccstandardRepository.Insert(itemGroupEntity);
                       
                    }
                }
            }
            //没有组合都删掉
            else
            {
                var hsStanders = _tbmoccstandardRepository.GetAll().Where(o => o.OccDiseasesId == input.occDisease.Id).Delete();
            }
            var entity = _tbmoccdiseaseRepository.Get(input.occDisease.Id);
            input.occDisease.MapTo(entity);
            entity = _tbmoccdiseaseRepository.Update(entity);
            var dto = entity.MapTo<OutOccDiseaseDto>();
                return dto;
           

        }

        /// <summary>
        /// 获取职业健康标准
        /// </summary>
        /// <returns></returns>
        public List<OccDiseaseStandardDto> GetStandard()
        {
            //var result = _departmentRepository.GetAllList().OrderBy(o => o.OrderNum);
            //return result.MapTo<List<TbmDepartmentDto>>();
            var result = _tbmoccstandardRepository.GetAllList();
            return result.MapTo<List<OccDiseaseStandardDto>>();
        }

     
    }
}
