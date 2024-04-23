using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo
{
    [AbpAuthorize]
    public class ItemInfoAppService : MyProjectAppServiceBase, IItemInfoAppService
    {
        private readonly IRepository<TbmDepartment, Guid> _departmentRepository;
        private readonly IRepository<TbmItemInfo, Guid> _itemInfoRepository;
        private readonly IRepository<TbmItemStandard, Guid> _itemStandardRepository;
        private readonly IRepository<TbmItemProcExpress, Guid> _ItemProcExpress;

        public ItemInfoAppService(IRepository<TbmItemInfo, Guid> itemInfoRepository
            , IRepository<TbmItemStandard, Guid> itemStandardRepository
            , IRepository<TbmDepartment, Guid> departmentRepository,
            IRepository<TbmItemProcExpress, Guid> ItemProcExpress)
        {
            _itemInfoRepository = itemInfoRepository;
            _itemStandardRepository = itemStandardRepository;
            _departmentRepository = departmentRepository;
            _ItemProcExpress = ItemProcExpress;
        }

        public ItemInfoDto AddItemInfo(ItemInfoDto input)
        {
            if (_itemInfoRepository.GetAll().Any(a => a.Name == input.Name && a.DepartmentId == input.DepartmentId))
                throw new FieldVerifyException("项目名称已存在！", "项目名称已存在！");
            var allData = _itemInfoRepository.GetAll();
            var orderNum = allData.Max(a => a.OrderNum);
            input.Id = Guid.NewGuid();
            input.OrderNum = orderNum == null ? 0 : orderNum + 1;
            List<ItemInfoSimpleDto> hcitems = new List<ItemInfoSimpleDto>();
            if (input.ItemInfos != null)
            {
                hcitems = input.ItemInfos.ToList();
            }
              // var hcitems = input.ItemInfos.ToList();
            var entity = input.MapTo<TbmItemInfo>();
            entity.ItemInfos = new List<TbmItemInfo>();
            foreach (ItemInfoSimpleDto item in hcitems)
            {
                var tbmitem = _itemInfoRepository.Get(item.Id);
                entity.ItemInfos.Add(tbmitem);
            }
            entity.DepartmentId = input.DepartmentId;
            var result = _itemInfoRepository.Insert(entity);
            return result.MapTo<ItemInfoDto>();
        }

        public ItemStandardDto AddItemStandard(ItemStandardDto input)
        {
            var allData = _itemStandardRepository.GetAll().Where(i => i.ItemId == input.ItemId);
            var orderNum = allData.Max(a => a.OrderNum);
            input.Id = Guid.NewGuid();
            input.OrderNum = orderNum == null ? 0 : orderNum + 1;
            var entity = input.MapTo<TbmItemStandard>();
            entity.ItemId = input.ItemId;
            var itemEntity = _itemInfoRepository.Get(input.ItemId);
            if (itemEntity == null)
                throw new FieldVerifyException("提示!", "未找到所属项目信息!");
            entity.Item = itemEntity;
            var result = _itemStandardRepository.Insert(entity);
            return result.MapTo<ItemStandardDto>();
        }

        public List<ItemInfoViewDto> GetAll()
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                var result = _itemInfoRepository.GetAllIncluding(r => r.Department);
                result = result.OrderBy(m => m.Department.OrderNum)
    .ThenBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
                return result.MapTo<List<ItemInfoViewDto>>();
            //}
        }


        public ItemInfoDto GetById(EntityDto<Guid> input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var data = _itemInfoRepository.Get(input.Id);
                return data.MapTo<ItemInfoDto>();
            }

        }

        public void DeleteItemInfo(EntityDto<Guid> input)
        {
            var entity = _itemInfoRepository.FirstOrDefault(input.Id);
            if (entity != null)
                _itemInfoRepository.Delete(entity);
        }

        public void DeleteItemStandard(ItemStandardDto input)
        {
            _itemStandardRepository.Delete(input.Id);
        }

        public ItemInfoDto EditItemInfo(ItemInfoDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var itemInfo = _itemInfoRepository.Get(input.Id);
                var itemGroups = itemInfo.ItemGroups.ToList();
                List<ItemInfoSimpleDto> hcITems = new List<ItemInfoSimpleDto>();
                if (input.ItemInfos!= null)
                {
                    hcITems= input.ItemInfos.ToList();
                }             
                // 知不知道 Dto 里包含的多个重要字段在编辑界面压根没有
                // 直接保存导致相关的所有体检数据全部废了
                // 真为写这个功能以及测试这个功能的人的智商感到心碎
                input.MapTo(itemInfo);
                if (itemInfo.ItemGroups == null)
                {
                    itemInfo.ItemGroups = new List<TbmItemGroup>();
                }
                foreach (var itemGroup in itemGroups)
                {
                    itemInfo.ItemGroups.Add(itemGroup);
                }
                itemInfo.ItemInfos = new List<TbmItemInfo>();
                foreach (ItemInfoSimpleDto itemsm in hcITems)
                {
                    var tbmitem = _itemInfoRepository.Get(itemsm.Id);
                    itemInfo.ItemInfos.Add(tbmitem);
                }

                var result = _itemInfoRepository.Update(itemInfo);
                //CurrentUnitOfWork.SaveChanges();
                return result.MapTo<ItemInfoDto>();
            }
        }

        public ItemStandardDto EditItemStandard(ItemStandardDto input)
        {
            var itemStandard = _itemStandardRepository.Get(input.Id);
            input.MapTo(itemStandard);
            return _itemStandardRepository.Update(itemStandard).MapTo<ItemStandardDto>();
        }

        public List<DepartmentIdNameDto> DepartmentGetAll()
        {
            var department = _departmentRepository.GetAll();
            return department.MapTo<List<DepartmentIdNameDto>>();
        }

        public List<ItemInfoViewDto> QueryItemInfo(SearchItemInfoDto input)
        {
            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
                var itemInfo = _itemInfoRepository.GetAllIncluding(r => r.Department);
                if (input.DepartmentId.HasValue && input.DepartmentId != Guid.Empty)
                    itemInfo = itemInfo.Where(i => i.DepartmentId == input.DepartmentId);
                if (!string.IsNullOrWhiteSpace(input.Name))
                    itemInfo = itemInfo.Where(i => i.Name.Contains(input.Name) || i.HelpChar.Contains(input.Name) || i.WBCode.Contains(input.Name) || i.NameEngAbr.Contains(input.Name));
                itemInfo = itemInfo.OrderBy(m => m.Department.OrderNum)
               .ThenBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
                if (input.IsActive.HasValue)
                {
                    if (input.IsActive == 1)
                    {
                        itemInfo = itemInfo.Where(i => i.IsActive != 2);
                    }
                    else
                    {
                        itemInfo = itemInfo.Where(i => i.IsActive == input.IsActive );
                    }
                }
                return itemInfo.MapTo<List<ItemInfoViewDto>>();
            //}
        }

        public List<ItemStandardDto> QueryItemStandardByItemId(EntityDto<Guid> input)
        {
            var itemStandard = _itemStandardRepository.GetAllIncluding(r => r.Item);
            itemStandard = itemStandard.Where(i => i.ItemId == input.Id);
            return itemStandard.MapTo<List<ItemStandardDto>>();
        }

        public List<ItemStandardDto> QueryItemStandardBySum()
        {
            var itemStandard = _itemStandardRepository.GetAllList(r => r.Summ == "正常");
            return itemStandard.MapTo<List<ItemStandardDto>>();
        }
        public async Task<List<ItemStandardDto>> GetAllItemStandard()
        {
            var itemStandard =  _itemStandardRepository.GetAll();
            return await itemStandard.ProjectToListAsync<ItemStandardDto>(GetConfigurationProvider<Core.Coding.TbmItemStandard, ItemStandardDto>());
            // return itemStandard.MapTo<List<ItemStandardDto>>();
        }
        public async Task<List<ItemInfoSimpleDto>> QuerySimples()
        {
            return await _itemInfoRepository.GetAll().OrderBy(m => m.Department.OrderNum).ThenBy(m => m.OrderNum)
                .ProjectToListAsync<ItemInfoSimpleDto>(GetConfigurationProvider<Core.Coding.TbmItemInfo, ItemInfoSimpleDto>());
            //return _itemInfoRepository.GetAll().OrderBy(m => m.Department.OrderNum).ThenBy(m => m.OrderNum).MapTo<List<ItemInfoSimpleDto>>();
        }
        /// <summary>
        /// 更新计算型项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ItemProcExpressDto SaveItemExpress(ItemProcExpressDto input)
        {
            ItemProcExpressDto itemProcExpress = new ItemProcExpressDto();
            if (input.Id == Guid.Empty)
            {
                TbmItemProcExpress tbmItemProcExpress = new TbmItemProcExpress();
                tbmItemProcExpress.ItemId = input.ItemId;
                tbmItemProcExpress.ItemName = input.ItemName;
                tbmItemProcExpress.FormulaText = input.FormulaText;
                tbmItemProcExpress.FormulaValue = input.FormulaValue;
                tbmItemProcExpress.ItemInfoReRelations = new List<TbmItemInfo>();
                foreach (ItemInfoSimpleDto item in input.ItemInfoReRelations)
                {
                    TbmItemInfo tbmItemInfo = _itemInfoRepository.Get(item.Id);
                    tbmItemProcExpress.ItemInfoReRelations.Add(tbmItemInfo);
                }             
                tbmItemProcExpress.Id = Guid.NewGuid();               
               var  itemProcExprels= _ItemProcExpress.Insert(tbmItemProcExpress);
                //CurrentUnitOfWork.SaveChanges();
                itemProcExpress = itemProcExprels.MapTo<ItemProcExpressDto>();
                // CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                TbmItemProcExpress tbmItemProcExpress = new TbmItemProcExpress();
                tbmItemProcExpress = _ItemProcExpress.Get(input.Id);               
                tbmItemProcExpress.ItemId = input.ItemId;
                tbmItemProcExpress.ItemName = input.ItemName;
                tbmItemProcExpress.FormulaText = input.FormulaText;
                tbmItemProcExpress.FormulaValue = input.FormulaValue;
                tbmItemProcExpress.ItemInfoReRelations = new List<TbmItemInfo>();
                foreach (ItemInfoSimpleDto item in input.ItemInfoReRelations)
                {
                    TbmItemInfo tbmItemInfo = _itemInfoRepository.Get(item.Id);
                    tbmItemProcExpress.ItemInfoReRelations.Add(tbmItemInfo);
                }
                itemProcExpress = _ItemProcExpress.Update(tbmItemProcExpress).MapTo<ItemProcExpressDto>();

            }
            return itemProcExpress;
        }
        /// <summary>
        /// 删除计算型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelteItemExpress(EntityDto<Guid> input)
        {
            TbmItemProcExpress tbmItemProcExpress = _ItemProcExpress.Get(input.Id);
            _ItemProcExpress.Delete(tbmItemProcExpress);
        }
        /// <summary>
        /// 项目排序
        /// </summary>
        /// <param name="input"></param>
        public void UpdateOrder(ChargeBM input)
        {
            TbmItemInfo tbmItemInfo = _itemInfoRepository.Get(input.Id);
            tbmItemInfo.OrderNum = int.Parse(input.Name);
            _itemInfoRepository.Update(tbmItemInfo);
        }
        public int? GetMaxOrderNum()
        {
            var result = _itemInfoRepository.Count();
            if (result != 0)
            {
                result = (int)_itemInfoRepository.GetAll().Max(o => o.OrderNum);
            }
            return result;
        }     
        /// <summary>
        /// 修改单位编码
        /// </summary>
        /// <param name="input"></param>
        public void InputUnit(UpItemUnit input)
        {
            var itemInfo = _itemInfoRepository.GetAll().FirstOrDefault(p=>p.StandardCode== input.StandardCode);
            if (itemInfo != null)
            {
                itemInfo.Unit = input.Unit;

                itemInfo.UnitBM = input.UnitBM;

                _itemInfoRepository.Update(itemInfo);
            }
        }
    }
}