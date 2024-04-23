using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor
{
    [AbpAuthorize]
    public class OccHazardFactorAppService : MyProjectAppServiceBase, IOccHazardFactorAppService
    {
        private readonly IRepository<TbmOccHazardFactor, Guid> _TbmOccHazardFactor;
        private readonly IRepository<TbmOccHazardFactorsProtective, Guid> _TbmOccHazardFactorsProtective;
        private readonly IRepository<TbmRadiation, Guid> _TbmRadiation;

        public OccHazardFactorAppService(IRepository<TbmOccHazardFactor, Guid> TbmOccHazardFactor, 
            IRepository<TbmOccHazardFactorsProtective, Guid> TbmOccHazardFactorsProtective,
             IRepository<TbmRadiation, Guid> TbmRadiation)

        
        {
            _TbmOccHazardFactor = TbmOccHazardFactor;
            _TbmOccHazardFactorsProtective = TbmOccHazardFactorsProtective;
            _TbmRadiation = TbmRadiation;
        }
        /// <summary>
        /// 添加危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto Add(FullHarardFactor input)
        {
            if (input.HazardFactorDto.Id== Guid.Empty) input.HazardFactorDto.Id = Guid.NewGuid();
            var entity = input.HazardFactorDto.MapTo<TbmOccHazardFactor>();
            entity = _TbmOccHazardFactor.Insert(entity);
            entity.Protectivis = null;
            entity.Protectivis = new List<TbmOccHazardFactorsProtective>();
            if (input.HazardFactorsProtectiveDto != null && input.HazardFactorsProtectiveDto.Count > 0)
            {
                foreach(var factors in input.HazardFactorsProtectiveDto)
                {
                    var tbmfactors = factors.MapTo<TbmOccHazardFactorsProtective>();
                    tbmfactors.Id = Guid.NewGuid();
                    tbmfactors.OccHazardFactorsId = entity.Id;
                    tbmfactors = _TbmOccHazardFactorsProtective.Insert(tbmfactors);
                    entity.Protectivis.Add(tbmfactors);
                }
            }
            var dto = entity.MapTo<OutOccHazardFactorDto>();
            return dto;           
        }
        /// <summary>
        /// 添加照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public RadiationDto AddRadiation(RadiationDto input)
        {
            if (input.Id == Guid.Empty) input.Id = Guid.NewGuid();
            var entity = input.MapTo<TbmRadiation>();
            entity.ParentId = input.ParentId;
            entity = _TbmRadiation.Insert(entity);       
         
            var dto = entity.MapTo<RadiationDto>();
            return dto;
        }
        /// <summary>
        /// 显示照射种类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<RadiationDto> ShowRadiation(RadiationDto input)
        {
            var OccDictionary = _TbmRadiation.GetAll();
            if (!string.IsNullOrWhiteSpace(input.Text))
            {
                OccDictionary = OccDictionary.Where(i => i.Text == input.Text);
            }           
            //if (input.IsActive == 0)
            //{
            //    OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            //}
            //else if (input.IsActive == 1)
            //{
            //    OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            //}
            if (input.ParentId != null)
            {
                OccDictionary = OccDictionary.Where(i => i.ParentId == input.ParentId);
            }
            OccDictionary = OccDictionary.OrderBy(m => m.OrderNum);

            return OccDictionary.MapTo<List<RadiationDto>>();

        }

        /// <summary>
        /// 删除照射种类
        /// </summary>
        /// <param name="input"></param>
        public void DelRadiation(EntityDto<Guid> input)
        {
            _TbmRadiation.Delete(input.Id);
        }
        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public RadiationDto EditRadiation(RadiationDto input)
        {
            var entity = _TbmRadiation.Get(input.Id);
            input.MapTo(entity);
            entity.ParentId = input.ParentId;
            entity = _TbmRadiation.Update(entity);
            var dto = entity.MapTo<RadiationDto>();
            return dto;
        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public RadiationDto GetRadiationDto(EntityDto<Guid> input)
        {
            var data = _TbmRadiation.Get(input.Id);
            return data.MapTo<RadiationDto>();
        }
        /// <summary>
        /// 获取危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutOccHazardFactorDto> getOccHazardFactors()
        {
            var que = _TbmOccHazardFactor.GetAll().Where(o => o.IsActive == 1);
            var outlist = que.Select(o => new OutOccHazardFactorDto
            {
                Id = o.Id,
                CASBM = o.CASBM,
                Category = o.Parent == null ? "" : o.Parent.Text,
                HelpChar = o.HelpChar,
                OrderNum = o.OrderNum,
                Text = o.Text
            }).OrderBy(o => o.OrderNum).ToList();
            return outlist;
        }
        /// <summary>
        /// 获取危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ShowOccHazardFactorDto> getSimpOccHazardFactors()
        {
            List<ShowOccHazardFactorDto> outlist = new List<ShowOccHazardFactorDto>();
            try
            {
                var que = _TbmOccHazardFactor.GetAll().Where(o => o.IsActive == 1);
                 outlist = que.Select(o => new ShowOccHazardFactorDto
                {
                    Id = o.Id,
                    CASBM = o.CASBM,
                    Category = o.Parent == null ? "" : o.Parent.Text,
                    HelpChar = o.HelpChar,
                    Text = o.Text,
                    OrderNum=o.OrderNum
                }).OrderBy(o => o.OrderNum).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return outlist;
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutOccHazardFactorDto> ShowOccHazardFactor(OutOccHazardFactorDto input)
        {           
            var OccDictionary = _TbmOccHazardFactor.GetAll();
            if (!string.IsNullOrWhiteSpace(input.Text))
            { 
                OccDictionary = OccDictionary.Where(i => i.Text == input.Text);
            }
            if (input.IsActive == 3)
            {
                OccDictionary = OccDictionary;
            }
            else if (input.IsActive == 0)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            }
            else if (input.IsActive == 1)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsActive);
            } 
            if (input.ParentId != null)
            {
                OccDictionary = OccDictionary.Where(i => i.ParentId == input.ParentId);
            }
            OccDictionary = OccDictionary.OrderBy(m => m.OrderNum);
           
            return OccDictionary.MapTo<List<OutOccHazardFactorDto>>();
            
        }

        /// <summary>
        /// 删除危害因素
        /// </summary>
        /// <param name="input"></param>
        public void Del(EntityDto<Guid> input)
        {
            _TbmOccHazardFactor.Delete(input.Id);
        }
        /// <summary>
        /// 修改危害因素
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto Edit(FullHarardFactor input)
        {
            //var entity = _TbmOccHazardFactor.Get(input.HazardFactorDto.Id);
            //input.MapTo(entity); // 赋值
            //entity = _TbmOccHazardFactor.Update(entity);
            //var dto = entity.MapTo<CreateOrUpdateHazardFactorDto>();
            //return dto;

            //yhh修改标准，先删除，再添加 
            if (input.HazardFactorsProtectiveDto != null && input.HazardFactorsProtectiveDto.Count > 0)
            {
                var stanemels = input.HazardFactorsProtectiveDto.Select(o => o.Text).ToList();
                //删除没有的标准
                var delet = _TbmOccHazardFactorsProtective.GetAll().Where(o => o.OccHazardFactorsId == input.HazardFactorDto.Id && !stanemels.Contains(o.Text)).Delete();
                foreach (var stand in input.HazardFactorsProtectiveDto)
                {
                    var ishs = _TbmOccHazardFactorsProtective.FirstOrDefault(o => o.OccHazardFactorsId == input.HazardFactorDto.Id && o.Text == stand.Text);
                    //已有修改
                    if (ishs != null)
                    {

                        // 更新标准
                        stand.MapTo(ishs);
                        ishs = _TbmOccHazardFactorsProtective.Update(ishs);


                    }
                    else
                    {
                        //新增标准
                        stand.Id = Guid.NewGuid();
                        var itemGroupEntity = stand.MapTo<TbmOccHazardFactorsProtective>();
                        itemGroupEntity.OccHazardFactorsId = input.HazardFactorDto.Id;
                        ishs = _TbmOccHazardFactorsProtective.Insert(itemGroupEntity);

                    }
                }
            }
            //没有组合都删掉
            else
            {
                var hsStanders = _TbmOccHazardFactorsProtective.GetAll().Where(o => o.OccHazardFactorsId == input.HazardFactorDto.Id).Delete();
            }
            var entity = _TbmOccHazardFactor.Get(input.HazardFactorDto.Id);
            input.HazardFactorDto.MapTo(entity);
            entity = _TbmOccHazardFactor.Update(entity);
            var dto = entity.MapTo<OutOccHazardFactorDto>();
            return dto;
        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public OutOccHazardFactorDto GetOccHazardFactor(EntityDto<Guid> input)
        {
            var data = _TbmOccHazardFactor.Get(input.Id);
            return data.MapTo<OutOccHazardFactorDto>();
        }
        
        /// <summary>
        /// 获取防护措施
        /// </summary>
        /// <returns></returns>
        public List<HazardFactorsProtective> GetHazardFactorsProtective()
        {
            var data = _TbmOccHazardFactorsProtective.GetAll();
            return data.MapTo<List<HazardFactorsProtective>>();
        }


    }
}
