using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting
{
    [AbpAuthorize]
    public class BarSettingAppService : MyProjectAppServiceBase, IBarSettingAppService
    {
        private readonly IRepository<TbmBaritem, Guid> _baritemRepository;

        private readonly IRepository<TbmBarSettings, Guid> _barSettingRepository;

        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;

        public BarSettingAppService(IRepository<TbmBarSettings, Guid> barSettingRepository,
            IRepository<TbmBaritem, Guid> baritemRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository)
        {
            _barSettingRepository = barSettingRepository;
            _baritemRepository = baritemRepository;
            _itemGroupRepository = itemGroupRepository;
        }

        public FullBarSettingDto Add(BarSettingInput input)
        {
            if (string.IsNullOrEmpty(input.BarSetting.BarName))
                throw new FieldVerifyException("条码名称不可为空！", "条码名称不可为空！");
            
            if (_barSettingRepository.GetAll().Any(r => r.BarName == input.BarSetting.BarName))
                throw new FieldVerifyException("条码名称重复！", "条码名称重复！");

            if (input.BarSetting.Id == Guid.Empty) input.BarSetting.Id = Guid.NewGuid();
            var entity = input.BarSetting.MapTo<TbmBarSettings>();

            if (input.Baritems != null && input.Baritems.Count > 0)
            {
                if (entity.Baritems == null) entity.Baritems = new List<TbmBaritem>();
                var baritems = input.Baritems.MapTo<List<TbmBaritem>>();
                foreach (var baritem in baritems)
                {
                    if (baritem.Id == Guid.Empty) baritem.Id = Guid.NewGuid();
                    baritem.BarSettingId = entity.Id;
                    baritem.ItemGroup = _itemGroupRepository.Get(baritem.ItemGroupId);
                    entity.Baritems.Add(baritem);
                }
            }

            entity = _barSettingRepository.Insert(entity);
            var dto = entity.MapTo<FullBarSettingDto>();

            return dto;
        }
        /// <summary>
        /// 删除条码设置主细
        /// </summary>
        /// <param name="input">条码设置主表ID</param>
        public void Del(EntityDto<Guid> input)
        {
            var Items = _baritemRepository.GetAll().Where(n => n.BarSettingId == input.Id);
            foreach (var item in Items)
            {
                _baritemRepository.Delete(item.Id);
            }
            _barSettingRepository.Delete(input.Id);
        }

        public FullBarSettingDto Edit(BarSettingInput input)
        {
            if (string.IsNullOrEmpty(input.BarSetting.BarName))
                throw new FieldVerifyException("条码名称不可为空！", "条码名称不可为空！");

            if (_barSettingRepository.GetAll()
                .Any(r => r.BarName == input.BarSetting.BarName && r.Id != input.BarSetting.Id))
                throw new FieldVerifyException("条码名称重复！", "条码名称重复！");

            if (input.Baritems != null)
            {
                var itemSuitItemGroups = _baritemRepository.GetAllList(m => m.BarSettingId == input.BarSetting.Id);
                var ItemGroupIds = new List<Guid>();
                foreach (var baritemEntity in itemSuitItemGroups)
                {
                    var baritemDto = input.Baritems.FirstOrDefault(m => m.ItemGroupId == baritemEntity.ItemGroupId);
                    if (baritemDto == null)
                    {
                        _baritemRepository.Delete(baritemEntity);
                    }
                    else
                    {
                        baritemDto.Id = baritemEntity.Id;
                        baritemDto.MapTo(baritemEntity);
                        _baritemRepository.Update(baritemEntity);
                        ItemGroupIds.Add(baritemEntity.ItemGroupId);
                    }
                }

                foreach (var baritemDto in input.Baritems.Where(m => !ItemGroupIds.Contains(m.ItemGroupId)))
                {
                    baritemDto.Id = Guid.NewGuid();
                    var baritemEntity = baritemDto.MapTo<TbmBaritem>();
                    baritemEntity.BarSettingId = input.BarSetting.Id;
                    _baritemRepository.Insert(baritemEntity);
                }
            }

            var entity = _barSettingRepository.Get(input.BarSetting.Id);
            input.BarSetting.MapTo(entity); // 赋值
            entity = _barSettingRepository.Update(entity);
            var dto = entity.MapTo<FullBarSettingDto>();
            return dto;
        }

        public FullBarSettingDto Get(SearchBarSettingDto input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<FullBarSettingDto>();
            return dto;
        }

        public List<SimpleBarSettingDto> QuerySimples(SearchBarSettingDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<SimpleBarSettingDto>>();
        }

        public List<BarSettingDto> QueryNatives(SearchBarSettingDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<BarSettingDto>>();
        }

        public List<FullBarSettingDto> QueryFulls(SearchBarSettingDto input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<FullBarSettingDto>>();
        }

        public PageResultDto<BarSettingDto> PageNatives(PageInputDto<SearchBarSettingDto> input)
        {
            return PageHelper.Paging<SearchBarSettingDto, TbmBarSettings, BarSettingDto>(input, BuildQuery);
        }

        public PageResultDto<FullBarSettingDto> PageFulls(PageInputDto<SearchBarSettingDto> input)
        {
            var result = PageHelper.Paging<SearchBarSettingDto, TbmBarSettings, FullBarSettingDto>(input, BuildQuery);
            return result;
        }

        [UnitOfWork(false)]
        public PageResultDto<BarCodeViewDto> GetAll(PageInputDto input)
        {
            var query = _barSettingRepository.GetAllIncluding(r => r.Baritems);
            query = query.OrderBy(m => m.OrderNum);
            var result = new PageResultDto<BarCodeViewDto>();
            result.CurrentPage = input.CurentPage;
            result.Calculate(query.Count());
            query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
            result.Result = query.MapTo<List<BarCodeViewDto>>();
            return result;
        }

        public List<BarItembViewDto> GetBarItemGroupFulls(SearchBarItemDto searchBarItemDto)
        {
            var query = _baritemRepository.GetAllList(p =>p.BarSetting.IsRepeatItemBarcode !=2 && searchBarItemDto.AllItemGroupID.Contains(p.ItemGroupId)).OrderBy(p=>p.BarSetting?.OrderNum);
            var ls = query.Where(o=>o.BarSetting?.IsDeleted==false).ToList();
            return ls.MapTo<List<BarItembViewDto>>();
        }
        /// <summary>
        /// 获取所有条码组合明细表
        /// </summary>
        /// <returns></returns>
        public List<BarItemDto> GetBarItems()
        {
            var query = _baritemRepository.GetAllList();
            return query.MapTo<List<BarItemDto>>();
        }
        
        private IQueryable<TbmBarSettings> BuildQuery(SearchBarSettingDto input)
        {
            var query = _barSettingRepository.GetAll();
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty(input.BarName))
                        query = query.Where(m => m.BarName.Contains(input.BarName));
                    if (!string.IsNullOrEmpty(input.TubeColor))
                        query = query.Where(m => m.TubeColor.Contains(input.TubeColor));
                    if (input.testType != null)
                        query = query.Where(m => m.testType == input.testType);
                    if (input.IsRepeatItemBarcode != null)
                        query = query.Where(m => m.IsRepeatItemBarcode == input.IsRepeatItemBarcode);
                    if (input.BarNUM != null)
                        query = query.Where(m => m.BarNUM == input.BarNUM);
                    if (input.PrintAdress != null)
                        query = query.Where(m => m.PrintAdress == input.PrintAdress);
                }
            }
                
            query = query.OrderBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return query;
        }
    }
}