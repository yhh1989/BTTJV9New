using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease
{
    /// <summary>
    /// 目标疾病
    /// </summary>
    public class TargetDiseaseAppService : MyProjectAppServiceBase, ITargetDiseaseAppService
    {
        private readonly IRepository<Core.Illness.OccupationalDiseaseIncludeItemGroup, Guid> _occupationalDiseaseIncludeItemGroup; //目标疾病 
        private readonly IRepository<Symptom, Guid> _symptom; //症状表
        private readonly IRepository<HumanBodySystem, Guid> _humanBodySystem; //体格检查
        private readonly IRepository<DiseaseContraindicationExplain, Guid> _diseaseContraindicationExplain; //职业健康和禁忌证
        private readonly IRepository<RiskFactor, Guid> _riskFactor; //危害因素
        private readonly IRepository<JobCategory, Guid> _jobCategory; //岗位
        private readonly IRepository<TbmItemGroup, Guid> _tbmItemGroup; //项目组合编码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="occupationalDiseaseIncludeItemGroup"></param>
        /// <param name="symptom"></param>
        /// <param name="humanBodySystem"></param>
        /// <param name="diseaseContraindicationExplain"></param>
        public TargetDiseaseAppService(
                      IRepository<Core.Illness.OccupationalDiseaseIncludeItemGroup, Guid> occupationalDiseaseIncludeItemGroup,
                      IRepository<Symptom, Guid> symptom,
                      IRepository<HumanBodySystem, Guid> humanBodySystem,
                      IRepository<DiseaseContraindicationExplain, Guid> diseaseContraindicationExplain,
                                          IRepository<RiskFactor, Guid> riskFactor,
                                                              IRepository<JobCategory, Guid> jobCategory,
                                                                             IRepository<TbmItemGroup, Guid> tbmItemGroup
            )
        {
            _occupationalDiseaseIncludeItemGroup = occupationalDiseaseIncludeItemGroup;
            _symptom = symptom;
            _humanBodySystem = humanBodySystem;
            _diseaseContraindicationExplain = diseaseContraindicationExplain;
            _riskFactor = riskFactor;
            _jobCategory = jobCategory;
            _tbmItemGroup = tbmItemGroup;
        }
        /// <summary>
        /// 根据危害因素和岗位类别查询
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public SearchOccupationalDiseaseIncludeItemGroupDto GetOccupationalDiseaseIncludeItemGroup(QueryTargetDisease TargetDisease)
        {
            var condition = _occupationalDiseaseIncludeItemGroup.FirstOrDefault(o => o.JobCategoryId == TargetDisease.JobCategoryId && o.RiskFactorId == TargetDisease.RiskFactorId);
            return condition.MapTo<SearchOccupationalDiseaseIncludeItemGroupDto>();
        }
        /// <summary>
        /// 查询症状
        /// </summary>
        /// <returns></returns>
        public List<UpdateSymptomDto> GetSymptom(UpdateSymptomDto dto)
        {
            return _symptom.GetAll().MapTo<List<UpdateSymptomDto>>();
        }
        /// <summary>
        /// 新增/修改症状
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public List<UpdateSymptomDto> UpdateSymptom(UpdateSymptomDto UpdateSymptomDto)
        {
            var sympton = _symptom.FirstOrDefault(o => o.Id == UpdateSymptomDto.Id);
            if (sympton == null)
            {
                sympton = new Symptom();
                sympton.Id = Guid.NewGuid();
                sympton.Name = UpdateSymptomDto.Name;
                sympton.MnemonicCode = UpdateSymptomDto.MnemonicCode;
                _symptom.Insert(sympton);
            }
            else
            {
                sympton.Name = UpdateSymptomDto.Name;
                sympton.MnemonicCode = UpdateSymptomDto.MnemonicCode;
                _symptom.Update(sympton);
            }
            return _symptom.GetAll().MapTo<List<UpdateSymptomDto>>();
        }
        /// <summary>
        /// 保存目标疾病信息
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        public SearchOccupationalDiseaseIncludeItemGroupDto UpdateItemGroup(SearchOccupationalDiseaseIncludeItemGroupDto Dto)
        {
            var occupational = _occupationalDiseaseIncludeItemGroup.FirstOrDefault(o => o.Id == Dto.Id);
            //必选项目
            foreach (var item in Dto.MustHaveItemGroups)
            {
                var itemGroup = _tbmItemGroup.FirstOrDefault(o => o.Id == item.Id);
                if (!occupational.MustHaveItemGroups.Any(o => o.Id == itemGroup.Id))
                {
                    occupational.MustHaveItemGroups.Add(itemGroup);
                }
            }
            List<TbmItemGroup> MustHaveItemGroup = new List<TbmItemGroup>();
            foreach (var item in occupational.MustHaveItemGroups)
            {
                if (!Dto.MustHaveItemGroups.Any(o => o.Id == item.Id))
                {
                    MustHaveItemGroup.Add(item);
                }
            }
            foreach (var item in MustHaveItemGroup)
            {
                occupational.MustHaveItemGroups.Remove(item);
            }
            //可选项目
            foreach (var item in Dto.MayHaveItemGroups)
            {
                var itemGroup = _tbmItemGroup.FirstOrDefault(o => o.Id == item.Id);
                if (!occupational.MayHaveItemGroups.Any(o => o.Id == itemGroup.Id))
                {
                    occupational.MayHaveItemGroups.Add(itemGroup);
                }
            }
            List<TbmItemGroup> MayHaveItemGroup = new List<TbmItemGroup>();
            foreach (var item in occupational.MayHaveItemGroups)
            {
                if (!Dto.MayHaveItemGroups.Any(o => o.Id == item.Id))
                {
                    MayHaveItemGroup.Add(item);
                }
            }
            foreach (var item in MayHaveItemGroup)
            {
                occupational.MayHaveItemGroups.Remove(item);
            }
            //症状
            foreach (var item in Dto.Symptoms)
            {
                var symptom = _symptom.FirstOrDefault(o => o.Id == item.Id);
                if (!occupational.Symptoms.Any(o => o.Id == symptom.Id))
                {
                    occupational.Symptoms.Add(symptom);
                }
            }
            List<Symptom> Symptom = new List<Symptom>();
            foreach (var item in occupational.Symptoms)
            {
                if (!Dto.Symptoms.Any(o => o.Id == item.Id))
                {
                    Symptom.Add(item);
                }
            }
            foreach (var item in Symptom)
            {
                occupational.Symptoms.Remove(item);
            }
            //人体检查
            foreach (var item in Dto.HumanBodySystems)
            {
                var humanBodySystem = _humanBodySystem.FirstOrDefault(o => o.Id == item.Id);
                if (!occupational.HumanBodySystems.Any(o => o.Id == humanBodySystem.Id))
                {
                    occupational.HumanBodySystems.Add(humanBodySystem);
                }
            }
            List<HumanBodySystem> HumanBodySystem = new List<HumanBodySystem>();
            foreach (var item in occupational.HumanBodySystems)
            {
                if (!Dto.HumanBodySystems.Any(o => o.Id == item.Id))
                {
                    HumanBodySystem.Add(item);
                }
            }
            foreach (var item in HumanBodySystem)
            {
                occupational.HumanBodySystems.Remove(item);
            }
            _occupationalDiseaseIncludeItemGroup.Update(occupational);
            return new SearchOccupationalDiseaseIncludeItemGroupDto();
        }
        /// <summary>
        /// 查询所有体格检查信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<UpdateHumanBodySystemDto> SelectHumanBodySystemAll(UpdateHumanBodySystemDto dto)
        {
            return _humanBodySystem.GetAll().MapTo<List<UpdateHumanBodySystemDto>>();
        }
        /// <summary>
        /// 体格检查
        /// </summary>
        /// <returns></returns>
        public List<UpdateHumanBodySystemDto> UpdateHumanBodySystem(UpdateHumanBodySystemDto dto)
        {
            HumanBodySystem system = new HumanBodySystem();
            system.Id = Guid.NewGuid();
            system.Name = dto.Name;
            system.MnemonicCode = dto.MnemonicCode;
            _humanBodySystem.Insert(system);
            return new List<UpdateHumanBodySystemDto>();
        }
        /// <summary>
        /// 根据危害因素和岗位类别查询符合条件的数据集合
        /// </summary>
        /// <param name="TargetDisease"></param>
        /// <returns></returns>
        public List<SearchOccupationalDiseaseIncludeItemGroupDto> GetOccupationalDiseaseIncludeItemGroupDto(QueryTargetDiseaseDto TargetDisease)
        {
            var condition = _occupationalDiseaseIncludeItemGroup.GetAll();
            //岗位类别
            if (TargetDisease.JobCategoryId != null && TargetDisease.JobCategoryId != Guid.Empty)
            {
                condition = condition.Where(o => o.JobCategoryId == TargetDisease.JobCategoryId);
            }
            //危害因素
            if (TargetDisease.RiskFactorId != null && TargetDisease.RiskFactorId.Count > 0)
            {
                condition = condition.Where(o => TargetDisease.RiskFactorId.Contains(o.RiskFactorId.ToString()));
            }
            return condition.MapTo<List<SearchOccupationalDiseaseIncludeItemGroupDto>>();
        }
        /// <summary>
        /// 修改和增加职业健康或禁忌证
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<SearchOccupationalDiseaseIncludeItemGroupDto> UpdateUpdateDiseaseOrContraindication(UpdateDiseaseContraindicationExplainDto dto)
        {
            DiseaseContraindicationExplain inserts = _diseaseContraindicationExplain.FirstOrDefault(o => o.Id == dto.Id);
            if (dto.Id != Guid.Empty)
            {
                inserts.Name = dto.Name;
                inserts.Explain = dto.Explain;
                inserts.Type = dto.Type;
                _diseaseContraindicationExplain.Update(inserts);
                return new List<SearchOccupationalDiseaseIncludeItemGroupDto>();
            }




            Core.Illness.OccupationalDiseaseIncludeItemGroup ItemGroupDtos = _occupationalDiseaseIncludeItemGroup.FirstOrDefault(o => o.JobCategoryId == dto.JobCategoryId && o.RiskFactorId == dto.RiskFactorId);
            inserts = new DiseaseContraindicationExplain();
            inserts.Id = Guid.NewGuid();
            inserts.Name = dto.Name;
            inserts.Explain = dto.Explain;
            inserts.Type = dto.Type;
            var Disease = _diseaseContraindicationExplain.Insert(inserts);
            if (ItemGroupDtos == null || ItemGroupDtos.Id == Guid.Empty)
            {
                ItemGroupDtos = new Core.Illness.OccupationalDiseaseIncludeItemGroup();
                ItemGroupDtos.Id = Guid.NewGuid();
                ItemGroupDtos.JobCategoryId = dto.JobCategoryId;
                ItemGroupDtos.JobCategory = _jobCategory.FirstOrDefault(o => o.Id == dto.JobCategoryId);
                ItemGroupDtos.RiskFactorId = dto.RiskFactorId;
                ItemGroupDtos.RiskFactor = _riskFactor.FirstOrDefault(o => o.Id == dto.RiskFactorId);
                List<DiseaseContraindicationExplain> DiseaseList = new List<DiseaseContraindicationExplain>();
                DiseaseList.Add(Disease);
                ItemGroupDtos.DiseaseContraindicationExplains = DiseaseList;
                _occupationalDiseaseIncludeItemGroup.Insert(ItemGroupDtos);
            }
            else
            {
                ItemGroupDtos.DiseaseContraindicationExplains.Add(inserts);
                _occupationalDiseaseIncludeItemGroup.Update(ItemGroupDtos);
            }
            return new List<SearchOccupationalDiseaseIncludeItemGroupDto>();
        }


        /// <summary>
        /// 根据id查询疾病禁忌证及解释
        /// </summary>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public DiseaseContraindicationExplainDto GetDiseaseContraindicationExplainDtoForId(EntityDto<Guid> Id)
        {
            var Result = _diseaseContraindicationExplain.GetAll();
            DiseaseContraindicationExplain info = new DiseaseContraindicationExplain();
            if (Id != null && Id.Id != Guid.Empty)
            {
                info = Result.FirstOrDefault(n => n.Id == Id.Id);
            }
            return info.MapTo<DiseaseContraindicationExplainDto>();
        }
        /// <summary>
        /// 查询必选项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SimpleItemGroupDto> GetHaveItemGroup(QueryTargetDisease query)
        {
            var List = _occupationalDiseaseIncludeItemGroup.GetAll().Where(o => o.JobCategoryId == query.JobCategoryId && query.RiskFactorIdList.Contains(o.RiskFactorId));
            List<SimpleItemGroupDto> dto = new List<SimpleItemGroupDto>();
            foreach (var item in List)
            {
                dto.AddRange(item.MustHaveItemGroups.MapTo<List<SimpleItemGroupDto>>());
            }
            return dto;
        }
    }
}
