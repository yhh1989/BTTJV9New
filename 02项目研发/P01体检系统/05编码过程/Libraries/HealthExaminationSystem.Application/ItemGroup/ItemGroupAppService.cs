using System;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System.Collections.Generic;
using System.Data.Entity;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Abp.Authorization;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Abp.Domain.Entities;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using System.Threading.Tasks;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Z.EntityFramework.Plus;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Core;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup
{
    /// <summary>
    /// 项目组合
    /// <para>http://{host}:{port}/api/services/app/ItemGroup/Get</para>
    /// <para>http://{host}:{port}/api/services/app/{appService}/{function}</para>
    /// </summary>
    [AbpAuthorize]
    public class ItemGroupAppService : MyProjectAppServiceBase, IItemGroupAppService
    {
        private readonly IRepository<TbmItemSuit, Guid> _itemSuitRepository;
        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<TbmItemInfo, Guid> _itemInfoRepository;
        private readonly IRepository<TjlCustomerItemGroup, Guid> _TjlCustomerItemGroupRepository;
        private readonly IRepository<TjlClientTeamRegitem, Guid> _TjlTjlClientTeamRegitemRepository;
        private readonly IIDNumberAppService _idService;
        private readonly IRepository<TbmPriceSynchronize, Guid> _TbmPriceSynchronize;
        private readonly IRepository<TbmGroupRePriceSynchronizes, Guid> _TbmGroupRePriceSynchronizes;

        private readonly IRepository<TbmItemSuitItemGroupContrast, Guid> _ItemSuitItemGroupContrast;

        private readonly ISqlExecutor _sqlExecutor;

        public ItemGroupAppService(IRepository<TbmItemSuit, Guid> itemSuitRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository,
            IRepository<TbmItemInfo, Guid> itemInfoRepository,
            IDNumberAppService idService,
             IRepository<TjlCustomerItemGroup, Guid> TjlCustomerItemGroupRepository,
             IRepository<TjlClientTeamRegitem, Guid> TjlTjlClientTeamRegitemRepository,
        IRepository<TbmPriceSynchronize, Guid> TbmPriceSynchronize,
        IRepository<TbmGroupRePriceSynchronizes, Guid> TbmGroupRePriceSynchronizes,
         IRepository<TbmItemSuitItemGroupContrast, Guid> ItemSuitItemGroupContrast,
         ISqlExecutor sqlExecutor)
        {
            _itemSuitRepository = itemSuitRepository;
            _itemGroupRepository = itemGroupRepository;
            _itemInfoRepository = itemInfoRepository;
            _idService = idService;
            _TjlCustomerItemGroupRepository = TjlCustomerItemGroupRepository;
            _TjlTjlClientTeamRegitemRepository = TjlTjlClientTeamRegitemRepository;
            _TbmPriceSynchronize=TbmPriceSynchronize;
            _TbmGroupRePriceSynchronizes = TbmGroupRePriceSynchronizes;
            _ItemSuitItemGroupContrast = ItemSuitItemGroupContrast;
            _sqlExecutor = sqlExecutor;
        }

        public FullItemGroupDto Add(ItemGroupInput input)
        {
            if (string.IsNullOrEmpty(input.ItemGroup.ItemGroupName))
            {
                throw new FieldVerifyException("项目组合名称不可为空！", "项目组合名称不可为空！");
            }
            if (_itemGroupRepository.GetAll().Any(r => r.ItemGroupName == input.ItemGroup.ItemGroupName))
            {
                throw new FieldVerifyException("项目组合名称重复！", "项目组合名称重复！");
            }
            if (_itemGroupRepository.GetAll().Any(r => r.ItemGroupBM == input.ItemGroup.ItemGroupBM))
            {
                throw new FieldVerifyException("项目组合编码重复！", "项目组合编码重复！");
            }
            if (input.ItemGroup.Id == Guid.Empty) input.ItemGroup.Id = Guid.NewGuid();
            input.ItemGroup.ItemCount = input.ItemInfoIds?.Count;
            var entity = input.ItemGroup.MapTo<TbmItemGroup>();
            if(string.IsNullOrEmpty(entity.ItemGroupBM))
                entity.ItemGroupBM = _idService.CreateItemGroupBM();
            if (input.ItemInfoIds != null)
            {
                if (entity.ItemInfos == null) entity.ItemInfos = new List<TbmItemInfo>();
                foreach (var itemInfoId in input.ItemInfoIds)
                {
                    entity.ItemInfos.Add(_itemInfoRepository.Get(itemInfoId));
                }
            }
            var pric = input.PriceSyn?.ToList();
            //nput.PriceSyn = null;
            entity = _itemGroupRepository.Insert(entity);
            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            entity.GroupRePriceSynchronizes = new List<TbmGroupRePriceSynchronizes>();
            if (input.PriceSyn != null)
            { 
                foreach (var itemInfoId in input.PriceSyn)
                {

                    var GroupRePrice= itemInfoId.MapTo<TbmGroupRePriceSynchronizes>();
                    GroupRePrice.Id = Guid.NewGuid();
                    GroupRePrice.PriceSynchronize = null;
                    //GroupRePrice.PriceSynchronize = _TbmPriceSynchronize.Get(GroupRePrice.PriceSynchronizeId);
                    _TbmGroupRePriceSynchronizes.Insert(GroupRePrice);
                    GroupRePrice.PriceSynchronize = _TbmPriceSynchronize.Get(GroupRePrice.PriceSynchronizeId);
                    entity.GroupRePriceSynchronizes.Add(GroupRePrice);
                }
            }
            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            //entity = _itemGroupRepository.Get(entity.Id);
            var dto = entity.MapTo<FullItemGroupDto>();
            return dto;
        }

        public void Del(EntityDto<Guid> input)
        {
            var cusgroupls = _TjlCustomerItemGroupRepository.FirstOrDefault(o => o.ItemGroupBM_Id == input.Id);

            if (cusgroupls != null)
            {
                throw new FieldVerifyException("删除失败！", "该组合已使用不能删除！");
            }
            var clientgroupls = _TjlTjlClientTeamRegitemRepository.FirstOrDefault(o => o.TbmItemGroupId == input.Id);

            if (clientgroupls != null)
            {
                throw new FieldVerifyException("删除失败！", "该组合已使用不能删除！");
            }
            _itemGroupRepository.Delete(input.Id);
        }

        public FullItemGroupDto Edit(ItemGroupInput input)
        {
            //var itemGroupDbSet = _sqlExecutor.DbContext.Set<TbmItemGroup>();
            //var entity = itemGroupDbSet.Find(input.ItemGroup.Id);
            //_sqlExecutor.DbContext.Entry(entity).Collection(r => r.ItemInfos).Load();
            //if (entity.ItemInfos == null)
            //{
            //    entity.ItemInfos = new List<TbmItemInfo>();
            //}
            //else
            //{
            //    entity.ItemInfos.Clear();
            //}
            //foreach (var item in input.ItemInfoIds)
            //{
            //    entity.ItemInfos.Add(_itemInfoRepository.Get(item));
            //}
            ////_itemGroupRepository.Update(entity);
            ////CurrentUnitOfWork.SaveChanges();
            //_sqlExecutor.DbContext.Entry(entity).State = EntityState.Modified;
            //_sqlExecutor.DbContext.SaveChanges();
            //return ObjectMapper.Map<FullItemGroupDto>(entity);


            if (string.IsNullOrEmpty(input.ItemGroup.ItemGroupName))
            {
                throw new FieldVerifyException("项目组合名称不可为空！", "项目组合名称不可为空！");
            }
            if (_itemGroupRepository.GetAll().Any(r => r.Department != null && r.ItemGroupName == input.ItemGroup.ItemGroupName && r.Id != input.ItemGroup.Id))
            {
                throw new FieldVerifyException("项目组合名称重复！", "项目组合名称重复！");
            }
            input.ItemGroup.ItemCount = input.ItemInfoIds?.Count;
            var entity = _itemGroupRepository.Get(input.ItemGroup.Id);
            entity.ItemInfos.Clear();
            _itemGroupRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            var itemprice = entity.Price;
            input.ItemGroup.MapTo(entity); // 赋值
            //entity = ObjectMapper.Map < TbmItemGroup >(input.ItemGroup);
            entity.ItemInfos.Clear();
            _itemGroupRepository.Update(entity);
            CurrentUnitOfWork.SaveChanges();
            if (input.ItemInfoIds != null)
            {
                foreach (var itemInfoId in input.ItemInfoIds)
                {
                    entity.ItemInfos.Add(_itemInfoRepository.Get(itemInfoId));
                }
            }
            var pricls = input.PriceSyn.ToList();
            //input.PriceSyn = null;
            entity = _itemGroupRepository.Update(entity);
            //跟新套餐组合金额
            if (itemprice != entity.Price)
            {
                decimal price = entity.Price.Value;
                var suitgroups = _ItemSuitItemGroupContrast.GetAll().Where(o => o.ItemGroupId == entity.Id).ToList();
                foreach (var suitrgrp in suitgroups)
                {
                    _ItemSuitItemGroupContrast.Update(suitrgrp.Id, e => { e.ItemPrice = price; });

                }
            }
            if (pricls != null)
            {
                _TbmGroupRePriceSynchronizes.GetAll().Where(o => o.ItemGroupId == input.ItemGroup.Id).Delete();
                foreach (var itemInfoId in pricls)
                {

                    var RePrice = itemInfoId.MapTo<TbmGroupRePriceSynchronizes>();
                    RePrice.PriceSynchronize = null;
                    RePrice.Id = Guid.NewGuid();
                    RePrice.ItemGroupId = entity.Id;
                    RePrice.PriceSynchronize = _TbmPriceSynchronize.Get(RePrice.PriceSynchronizeId);
                    _TbmGroupRePriceSynchronizes.Insert(RePrice);
                    //RePrice.PriceSynchronize = _TbmPriceSynchronize.Get(RePrice.PriceSynchronizeId);
                }
            }
            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            //entity = _itemGroupRepository.Get(entity.Id);
            var dto = entity.MapTo<FullItemGroupDto>();

            return dto;
        }

        public FullItemGroupDto Get(SearchItemGroupDto input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<FullItemGroupDto>();
            return dto;
        }

        public List<SimpleItemGroupDto> QuerySimples(SearchItemGroupDto input)
        {
            var query = BuildQuery(input);
            query = query.Where(r => r.IsActive);
            query = query.Include(r => r.Department);
            var result = query.OrderBy(o => o.Department.OrderNum).ThenBy(o => o.OrderNum).ToList();
            return result.MapTo<List<SimpleItemGroupDto>>();
        }

        public async Task< List<SimpleItemGroupDto>> SimpleGroup()
        {
            var query = _itemGroupRepository.GetAll();
            query = query.Where(r => r.IsActive && r.Department !=null && r.Department.IsActive!=true);
            query = query.Include(r => r.Department);
            return await query.OrderBy(o => o.Department.OrderNum).ThenBy(o => o.OrderNum)
                .ProjectToListAsync<SimpleItemGroupDto>(GetConfigurationProvider<Core.Coding.TbmItemGroup, SimpleItemGroupDto>(new Tuple<Type, Type>(typeof(TbmDepartment), typeof(DepartmentSimpleDto))));
            //var result = await query.OrderBy(o => o.Department.OrderNum).ThenBy(o => o.OrderNum).ToListAsync();
            //return result.MapTo<List<SimpleItemGroupDto>>();
        }

        public List<ItemGroupDto> QueryNatives(SearchItemGroupDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<ItemGroupDto>>();
        }

        public List<FullItemGroupDto> QueryFulls(SearchItemGroupDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<FullItemGroupDto>>();
        }

        public PageResultDto<ItemGroupDto> PageNatives(PageInputDto<SearchItemGroupDto> input)
        {
            return Common.PageHelper.Paging<SearchItemGroupDto, TbmItemGroup, ItemGroupDto>(input, BuildQuery);
        }

        public PageResultDto<FullItemGroupDto> PageFulls(PageInputDto<SearchItemGroupDto> input)
        {
            return Common.PageHelper.Paging<SearchItemGroupDto, TbmItemGroup, FullItemGroupDto>(input, BuildQuery);
        }

        private IQueryable<TbmItemGroup> BuildQuery(SearchItemGroupDto input)
        {
            var query = _itemGroupRepository.GetAll().Where(o=>o.Department !=null && o.Department.IsActive!=true);// .GetAllIncluding(m => m.Department);
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (input.DepartmentId != Guid.Empty)
                        query = query.Where(m => m.DepartmentId == input.DepartmentId);
                    if (!string.IsNullOrEmpty(input.QueryText))
                        query = query.Where(m => m.ItemGroupBM.Contains(input.QueryText)
                        || m.ItemGroupName.Contains(input.QueryText)
                        || m.HelpChar.Contains(input.QueryText)
                        || m.WBCode.Contains(input.QueryText));
                    if (!string.IsNullOrEmpty(input.ChartCode))
                        query = query.Where(m => m.ChartCode.Contains(input.ChartCode));
                    if (!string.IsNullOrEmpty(input.ChartName))
                        query = query.Where(m => m.ChartName.Contains(input.ChartName));
                    if (!string.IsNullOrEmpty(input.Notice))
                        query = query.Where(m => m.Notice.Contains(input.Notice));
                    if (input.Sex != null)
                        query = query.Where(m => m.Sex == input.Sex);
                    if (input.ISSFItemGroup != null)
                        query = query.Where(m => m.ISSFItemGroup == input.ISSFItemGroup);
                    if (input.SpecimenType != null)
                        query = query.Where(m => m.SpecimenType == input.SpecimenType);
                    if (input.BarState != null)
                        query = query.Where(m => m.BarState == input.BarState);
                    if (input.PrivacyState != null)
                        query = query.Where(m => m.PrivacyState == input.PrivacyState);
                    if (input.DrawState != null)
                        query = query.Where(m => m.DrawState == input.DrawState);
                    if (input.MealState != null)
                        query = query.Where(m => m.MealState == input.MealState);
                    if (input.WomenState != null)
                        query = query.Where(m => m.WomenState == input.WomenState);
                    if (!string.IsNullOrEmpty(input.TubeType))
                        query = query.Where(m => m.TubeType == input.TubeType);
                    if (input.Breakfast != null)
                        query = query.Where(m => m.Breakfast == input.Breakfast);
                    if (input.AutoVIP != null)
                        query = query.Where(m => m.AutoVIP == input.AutoVIP);
                    if (input.OutgoingState > 0)
                        query = query.Where(m => m.OutgoingState == input.OutgoingState);
                    if (input.VoluntaryState != null)
                        query = query.Where(m => m.VoluntaryState == input.VoluntaryState);
                    if (input.IsActive == 1)
                        query = query.Where(o => o.IsActive == true);
                    if (input.IsActive == 0)
                        query = query.Where(o => o.IsActive == false);
                }
            }
            query = query.OrderBy(m => m.Department.OrderNum)
                .ThenBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return query;
        }

        /// <summary>
        /// 获取所有项目组合
        /// </summary>
        /// <returns></returns>
        public List<FullItemGroupDto> GetAll()
        {
            var ItemGroupList = _itemGroupRepository.GetAll().ToList();
            return ItemGroupList.MapTo<List<FullItemGroupDto>>();
        }
        /// <summary>
        /// 组合排序
        /// </summary>
        /// <param name="input"></param>
        public void UpdateOrder(ChargeBM input)
        {
            TbmItemGroup tbmItemGroup = _itemGroupRepository.Get(input.Id);
            tbmItemGroup.OrderNum = int.Parse(input.Name);
            _itemGroupRepository.Update(tbmItemGroup);
        }
        public void adMLXM(EntityDto<Guid> deptID)
        {
            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == guid);
            if (bmGrop == null)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == guid);
                    if (bmGrop != null)
                    {
                        bmGrop.IsDeleted = false;
                        bmGrop.DepartmentId = deptID.Id;
                        _itemGroupRepository.Update(bmGrop);
                        return;
                    }
                }
                
                    ItemInfoDto itemInfoDto = new ItemInfoDto();
                    itemInfoDto.Id = Guid.NewGuid();
                    itemInfoDto.Name = "抹零项";
                    itemInfoDto.OrderNum = 0;
                    itemInfoDto.ItemBM = "8888";
                    itemInfoDto.NamePM = "mlx";

                    var entity = itemInfoDto.MapTo<TbmItemInfo>();
                    entity.DepartmentId = deptID.Id;
                    var result = _itemInfoRepository.Insert(entity);
                    CurrentUnitOfWork.SaveChanges();
                    CreateOrUpdateItemGroup itemGroupInput = new CreateOrUpdateItemGroup();

                    itemGroupInput.Id = guid;
                    itemGroupInput.ItemGroupName = "抹零项";
                    itemGroupInput.Sex = 9;
                    itemGroupInput.Price = 0;
                    itemGroupInput.CostPrice = 0;
                    itemGroupInput.MaxAge = 150;
                    itemGroupInput.DepartmentId = deptID.Id;
                    itemGroupInput.MinAge = 0;
                    itemGroupInput.ItemCount = 1;
                    itemGroupInput.MaxDiscount = 0;
                    itemGroupInput.HelpChar = "MLX";
                    var entitygroup = itemGroupInput.MapTo<TbmItemGroup>();
                    entitygroup.ItemGroupBM = _idService.CreateItemGroupBM();
                    entitygroup.IsActive = true;
                    entitygroup.ItemInfos = new List<TbmItemInfo>();

                    entitygroup.ItemInfos.Add(result);

                    var groupentity = _itemGroupRepository.Insert(entitygroup);
                    CurrentUnitOfWork.SaveChanges();
                
                

            }
        }
        public int? GetMaxOrderNum()
        {
            var result = _itemGroupRepository.Count();
            if (result != 0)
            {
                result = (int)_itemGroupRepository.GetAll().Max(o => o.OrderNum);
            }
            return result;
        }
        //获取医嘱项目
        public List<priceSynDto> GetPriceSyns()
        {
            var pric = _TbmPriceSynchronize.GetAll();
            return pric.MapTo<List<priceSynDto>>();

        }
        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ChargeBM> getItemNames(SearIdsDto input)
        {
            var groups = _itemGroupRepository.GetAll().Where(o=> input.GroupIds.Contains( o.Id)).SelectMany(o => o.ItemInfos).Select(o => new ChargeBM
            {
                Id = o.Id,
                Name = o.Name
            }).Distinct().ToList();
            return groups;
        }

        public ConfiStrDto getItemConf(ConfiITemDto input)
        {
            ConfiStrDto confiStr = new ConfiStrDto();
            var checkItemIds = _itemGroupRepository.GetAll().Where(p => input.CheckGroupIds.Contains(p.Id)).SelectMany(p=>p.ItemInfos).Select(p=>p.Id).ToList();
            var list = _itemGroupRepository.GetAll().Where(p => input.HasGroupIds.Contains(p.Id) &&
            p.ItemInfos.Any(n=> checkItemIds.Contains(n.Id))).ToList();
            if (list.Count > 0)
            {
                confiStr.StrTS = "已有组合：";
                foreach (var group in list)
                {
                    string ts = group.ItemGroupName;
                    var Items = group.ItemInfos.Where(p => checkItemIds.Contains(p.Id))?.Select(p => p.Name).ToList();
                    string ItemNames = string.Join(",", Items);
                    ts = ts + ":" + ItemNames + ";";
                    confiStr.StrTS = confiStr.StrTS + ts;
                }
                confiStr.StrTS = confiStr.StrTS+ "和选中组合包含的项目冲突";
            }
            else
            {
                confiStr.StrTS = "";
            }
            return confiStr;
        }

    }
}