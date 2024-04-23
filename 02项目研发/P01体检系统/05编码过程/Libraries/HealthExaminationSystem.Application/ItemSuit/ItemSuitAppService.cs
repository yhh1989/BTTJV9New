using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using AutoMapper;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemSuit
{
    [AbpAuthorize]
    public class ItemSuitAppService : MyProjectAppServiceBase, IItemSuitAppService
    {
        private readonly IRepository<TbmItemSuit, Guid> _itemSuitRepository;

        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;

        private readonly IRepository<TbmItemSuitItemGroupContrast, Guid> _itemSuitItemGroupRepository;

        private readonly IRepository<TjlClientInfo, Guid> _clientInfoesRepository;

        private readonly IIDNumberAppService _idService;

        public ItemSuitAppService(IRepository<TbmItemSuit, Guid> itemSuitRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository,
            IRepository<TbmItemSuitItemGroupContrast, Guid> itemSuitItemGroupRepository,
            IRepository<TjlClientInfo, Guid> clientInfoesRepository,
            IDNumberAppService idService)
        {
            _itemSuitRepository = itemSuitRepository;
            _itemGroupRepository = itemGroupRepository;
            _itemSuitItemGroupRepository = itemSuitItemGroupRepository;
            _clientInfoesRepository = clientInfoesRepository;
            _idService = idService;
        }

        public FullItemSuitDto Add(ItemSuitInput input)
        {
            if (string.IsNullOrEmpty(input.ItemSuit.ItemSuitName))
            {
                throw new FieldVerifyException("套餐名称不可为空！", "套餐名称不可为空！");
            }
            if (_itemSuitRepository.GetAll().Any(r => r.ItemSuitName == input.ItemSuit.ItemSuitName))
            {
                throw new FieldVerifyException("套餐名称重复！", "套餐名称重复！");
            }
            if (input.ItemSuitItemGroups == null || input.ItemSuitItemGroups.Count == 0)
            {
                throw new FieldVerifyException("套餐必需有组合！", "套餐必需有组合！");
            }
            if (input.ItemSuit.CostPrice != input.ItemSuitItemGroups.Sum(m => m.ItemPrice))
            {
                throw new FieldVerifyException("套餐成本价与组合单价不一致！", "成本价与组合单价不一致！");
            }
            if (input.ItemSuit.Price != input.ItemSuitItemGroups.Sum(m => m.PriceAfterDis))
            {
                var allmoney = input.ItemSuit.Price - input.ItemSuitItemGroups.Sum(m => m.PriceAfterDis);
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == guid);
                if (bmGrop != null)
                {
                    ItemSuitItemGroupContrastDto suitgroup = new ItemSuitItemGroupContrastDto();
                    suitgroup.Id = Guid.NewGuid();
                    suitgroup.ItemGroupId = guid;
                    suitgroup.ItemPrice = allmoney;
                    suitgroup.PriceAfterDis = allmoney;
                    suitgroup.Suitgrouprate = 1;
                    input.ItemSuitItemGroups.Add(suitgroup);
                }
                else
                {
                    var group = input.ItemSuitItemGroups.FirstOrDefault(o => o.ItemPrice!=o.PriceAfterDis &&   o.PriceAfterDis != 0);
                    group.PriceAfterDis = group.PriceAfterDis + allmoney;

                    //throw new FieldVerifyException("套餐价格存在差价", "请在科室设置中维护抹零项");
                }
                //throw new FieldVerifyException("套餐单价与组合折后价不一致！", "套餐单价与组合折后价不一致！");
            }

            if (input.ItemSuit.Id == Guid.Empty) input.ItemSuit.Id = Guid.NewGuid();
            var entity = input.ItemSuit.MapTo<TbmItemSuit>();
            entity.ItemSuitID = _idService.CreateItemSuitBM();

          
            // 组合
            entity.ItemSuitItemGroups = input.ItemSuitItemGroups.MapTo<List<TbmItemSuitItemGroupContrast>>();

            entity = _itemSuitRepository.Insert(entity);
            var dto = entity.MapTo<FullItemSuitDto>();
            return dto;
        }

        public void Del(EntityDto<Guid> input)
        {
            _itemSuitRepository.Delete(input.Id);
        }

        public FullItemSuitDto Edit(ItemSuitInput input)
        {
            if (string.IsNullOrEmpty(input.ItemSuit.ItemSuitName))
            {
                throw new FieldVerifyException("套餐名称不可为空！", "套餐名称不可为空！");
            }
            if (_itemSuitRepository.GetAll().Any(r => r.ItemSuitName == input.ItemSuit.ItemSuitName && r.Id != input.ItemSuit.Id))
            {
                throw new FieldVerifyException("套餐名称重复！", "套餐名称重复！");
            }
            if (input.ItemSuitItemGroups == null || input.ItemSuitItemGroups.Count == 0)
            {
                throw new FieldVerifyException("套餐必需有组合！", "套餐必需有组合！");
            }
            if (input.ItemSuit.CostPrice != input.ItemSuitItemGroups.Sum(m => m.ItemPrice))
            {
                throw new FieldVerifyException("套餐成本价与组合单价不一致！", "成本价与组合单价不一致！");
            }
            decimal money = 0;
            Guid guid = Guid.Empty;
            if (input.ItemSuit.Price != input.ItemSuitItemGroups.Sum(m => m.PriceAfterDis))
            {
                var zkmoney = input.ItemSuitItemGroups.Sum(m => m.PriceAfterDis);
                var allmoney = input.ItemSuit.Price - input.ItemSuitItemGroups.Sum(m => m.PriceAfterDis);
                guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == guid);
                var mlgroups = input.ItemSuitItemGroups.Where(o => o.ItemGroupId == guid).ToList();

                if (bmGrop != null)
                {
                    if (mlgroups.Count == 0)
                    {

                        ItemSuitItemGroupContrastDto suitgroup = new ItemSuitItemGroupContrastDto();
                        suitgroup.Id = Guid.NewGuid();
                        suitgroup.ItemGroupId = guid;
                        suitgroup.ItemPrice = allmoney;
                        suitgroup.PriceAfterDis = allmoney;
                        suitgroup.Suitgrouprate = 1;
                        input.ItemSuitItemGroups.Add(suitgroup);
                    }
                    else
                    { money = allmoney.Value; }
                }
                else
                {
                    var group = input.ItemSuitItemGroups.FirstOrDefault(o => o.ItemPrice != o.PriceAfterDis && o.PriceAfterDis != 0);
                    group.PriceAfterDis = group.PriceAfterDis + allmoney;
                   // throw new FieldVerifyException("套餐价格存在差价", "请在科室设置中维护抹零项");
                }
                //  throw new FieldVerifyException("套餐单价与组合折后价不一致！", "套餐单价与组合折后价不一致！");
            }
            else
            {//移除0元抹零项目
                guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var mlgroups = input.ItemSuitItemGroups.FirstOrDefault(o => o.ItemGroupId == guid);
                if (mlgroups != null && mlgroups.PriceAfterDis == 0)
                {
                    input.ItemSuitItemGroups.Remove(mlgroups);                    
                }

            }
            // 获取当前组合
            var itemSuitItemGroups = _itemSuitItemGroupRepository.GetAllList(m => m.ItemSuitId == input.ItemSuit.Id);
            var ItemGroupIds = new List<Guid>();
            foreach (var itemGroupEntity in itemSuitItemGroups)
            {
                var itemGroupDto = input.ItemSuitItemGroups.FirstOrDefault(m => m.ItemGroupId == itemGroupEntity.ItemGroupId);
                if (itemGroupDto == null)
                {
                    // 移除不存在组合
                    _itemSuitItemGroupRepository.Delete(itemGroupEntity);
                }
                else if (itemGroupEntity.ItemGroupId == guid && money != 0)
                {

                    var Moneyml = itemGroupEntity.ItemPrice + money;
                    if (Moneyml == 0)
                    {
                        _itemSuitItemGroupRepository.Delete(itemGroupEntity);
                        ItemGroupIds.Add(itemGroupEntity.ItemGroupId);
                    }
                    else
                    {
                        itemGroupDto.MapTo(itemGroupEntity);
                        if (itemGroupEntity.ItemGroupId == guid && money != 0)
                        {

                            itemGroupEntity.ItemPrice = itemGroupEntity.ItemPrice + money;
                            itemGroupEntity.PriceAfterDis = itemGroupEntity.PriceAfterDis + money;
                        }
                        _itemSuitItemGroupRepository.Update(itemGroupEntity);                     
                        ItemGroupIds.Add(itemGroupEntity.ItemGroupId);
                    }
                }
                else
                {
                    // 更新组合
                    itemGroupDto.MapTo(itemGroupEntity);                   
                    _itemSuitItemGroupRepository.Update(itemGroupEntity);               
                    ItemGroupIds.Add(itemGroupEntity.ItemGroupId);
                }
            }
            // 添加新的组合
            foreach (var itemGroupDto in input.ItemSuitItemGroups.Where(m => !ItemGroupIds.Contains(m.ItemGroupId)))
            {
                if (itemGroupDto.ItemGroupId == guid)
                {
                    var ml = _itemSuitItemGroupRepository.FirstOrDefault(o => o.ItemSuitId == input.ItemSuit.Id && o.ItemGroupId == guid);
                    if (ml != null)
                    {
                        decimal mlmoney = itemGroupDto.ItemPrice.Value;
                        decimal mlmoneyprice = itemGroupDto.PriceAfterDis.Value;
                        ml.ItemPrice = ml.ItemPrice + mlmoney;
                        ml.PriceAfterDis = ml.PriceAfterDis + mlmoneyprice;
                        _itemSuitItemGroupRepository.Update(ml);
                    }
                    else
                    {
                        itemGroupDto.Id = Guid.NewGuid();
                        var itemGroupEntity = itemGroupDto.MapTo<TbmItemSuitItemGroupContrast>();
                        itemGroupEntity.ItemSuitId = input.ItemSuit.Id;
                        _itemSuitItemGroupRepository.Insert(itemGroupEntity);
                    }


                }
                else
                {
                    itemGroupDto.Id = Guid.NewGuid();
                    var itemGroupEntity = itemGroupDto.MapTo<TbmItemSuitItemGroupContrast>();
                    itemGroupEntity.ItemSuitId = input.ItemSuit.Id;
                    _itemSuitItemGroupRepository.Insert(itemGroupEntity);
                }
            }

            var entity = _itemSuitRepository.Get(input.ItemSuit.Id);
            input.ItemSuit.MapTo(entity); // 赋值
            entity = _itemSuitRepository.Update(entity);
            var dto = entity.MapTo<FullItemSuitDto>();
            return dto;
        }

        public FullItemSuitDto Get(SearchItemSuitDto input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<FullItemSuitDto>();
            return dto;
        }

        public List<SimpleItemSuitDto> QuerySimples(SearchItemSuitDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<SimpleItemSuitDto>>();
        }
        /// <summary>
        /// 查询简单套餐信息缓存
        /// </summary>
        public async Task<List<SimpleItemSuitDto>> QuerySimplesCache()
        {
            var query = _itemSuitRepository.GetAll().AsNoTracking();
            query = query.OrderBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return await query.ProjectToListAsync<SimpleItemSuitDto>(GetConfigurationProvider<Core.Coding.TbmItemSuit, SimpleItemSuitDto>());
           // return query.MapTo<List<SimpleItemSuitDto>>();
        }
        public List<SimpleSubsetItemSuitDto> QuerySimpleSubsets(SearchItemSuitDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<SimpleSubsetItemSuitDto>>();
        }

        public List<ItemSuitDto> QueryNatives(SearchItemSuitDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<ItemSuitDto>>();
        }

        public List<FullItemSuitDto> QueryFulls(SearchItemSuitDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<FullItemSuitDto>>();
        }

        public PageResultDto<ItemSuitDto> PageNatives(PageInputDto<SearchItemSuitDto> input)
        {
            return Common.PageHelper.Paging<SearchItemSuitDto, TbmItemSuit, ItemSuitDto>(input, BuildQuery);
        }

        public PageResultDto<FullItemSuitDto> PageFulls(PageInputDto<SearchItemSuitDto> input)
        {
            return Common.PageHelper.Paging<SearchItemSuitDto, TbmItemSuit, FullItemSuitDto>(input, i =>
            {
                var query = BuildQuery(i);
                query = query.Include(r => r.ItemSuitItemGroups.Select(u => u.ItemGroup.Department));
                return query;
            });
        }

        private IQueryable<TbmItemSuit> BuildQuery(SearchItemSuitDto input)
        {
            var query = _itemSuitRepository.GetAll().AsNoTracking();
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty(input.QueryText))
                        query = query.Where(m => m.ItemSuitID.Contains(input.QueryText)
                        || m.ItemSuitName.Contains(input.QueryText)
                        || m.HelpChar.Contains(input.QueryText)
                        || m.WBCode.Contains(input.QueryText));
                    if (input.MinAge.HasValue)
                        query = query.Where(m => m.MinAge == input.MinAge);
                    if (input.MaxAge.HasValue)
                        query = query.Where(m => m.MaxAge == input.MaxAge);
                    if (input.Available.HasValue)
                        query = query.Where(m => m.Available == input.Available);
                    if (input.ItemSuitType.HasValue)
                        query = query.Where(m => m.ItemSuitType == input.ItemSuitType);
                    if (input.Sex.HasValue)
                        query = query.Where(m => m.Sex == input.Sex);
                }
            }
            query = query.OrderBy(m => m.ItemSuitName).ThenByDescending(m => m.CreationTime); //.ThenBy(m => m.ItemSuitID);
            return query;
        }
        /// <summary>
        /// 返回详细套餐列表
        /// </summary>
        /// <returns></returns>
        public List<ItemSuitItemGroupContrastFullDto> GetAllItemGroup()
        {
            return _itemSuitItemGroupRepository.GetAll().MapTo<List<ItemSuitItemGroupContrastFullDto>>();
        }
        /// <summary>
        /// 套餐关联项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ItemSuitGroupSimpDto> GetAllSuitItemGroup(EntityDto<Guid> input)
        {
            var list= _itemSuitItemGroupRepository.GetAll().AsNoTracking().Where(o => o.ItemSuitId == input.Id).Select(
                o => new ItemSuitGroupSimpDto {
                     Id=o.Id,
                    ItemSuitId=o.ItemSuitId,
                    ItemGroupId=o.ItemGroupId,
                    ItemGroupName= o.ItemGroup.ItemGroupName,
                    PriceAfterDis=o.PriceAfterDis,
                    ItemPrice=o.ItemPrice,
                    Suitgrouprate= o.Suitgrouprate,
                    dtpOrder = o.ItemGroup.Department.OrderNum==null?0: o.ItemGroup.Department.OrderNum.Value,
                    OrderNum= o.ItemGroup.OrderNum==null?0: o.ItemGroup.OrderNum.Value,
                    DeptmentName = o.ItemGroup.Department.Name,
                    DeptmentId = o.ItemGroup.DepartmentId
                }).OrderBy(o => o.dtpOrder).ThenBy(o => o.OrderNum).ToList();
            //var maplist= list.MapTo<List<ItemSuitGroupSimpDto>>();           
            return list;
        }
        public List<ItemSuitItemGroupContrastFullDto> GetAllSuitItemGroups(EntityDto<Guid> input)
        {            
          return _itemSuitItemGroupRepository.GetAll().AsNoTracking().Where(o => o.ItemSuitId == input.Id).MapTo<List<ItemSuitItemGroupContrastFullDto>>();
        }
    }
}
