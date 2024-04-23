using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose
{
    [AbpAuthorize]
    public class ComposeAppService : MyProjectAppServiceBase, IComposeAppService
    {

        private readonly IRepository<TbmCompose, Guid> _composeRepository;
        private readonly IRepository<TbmComposeGroup, Guid> _composeGroupRepository;
        private readonly IRepository<TbmComposeGroupItem, Guid> _composeGroupItemRepository;
        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;

        public ComposeAppService(IRepository<TbmCompose, Guid> composeRepository,
            IRepository<TbmComposeGroup, Guid> composeGroupRepository,
            IRepository<TbmComposeGroupItem, Guid> composeGroupItemRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository)
        {
            _composeRepository = composeRepository;
            _composeGroupRepository = composeGroupRepository;
            _composeGroupItemRepository = composeGroupItemRepository;
            _itemGroupRepository = itemGroupRepository;
        }

        public List<FullComposeDto> QueryFulls(SearchComposeInput input)
        {
            return BuildComposeQuery(input).MapTo<List<FullComposeDto>>();
        }

        public PageResultDto<ComposeDto> PageComposes(PageInputDto<SearchComposeInput> input)
        {
            return Common.PageHelper.Paging<SearchComposeInput, TbmCompose, ComposeDto>(input, BuildComposeQuery);
        }

        public PageResultDto<FullComposeDto> PageFullComposes(PageInputDto<SearchComposeInput> input)
        {
            return Common.PageHelper.Paging<SearchComposeInput, TbmCompose, FullComposeDto>(input, BuildComposeQuery);
        }
        private IQueryable<TbmCompose> BuildComposeQuery(SearchComposeInput input)
        {
            var query = _composeRepository.GetAll().AsNoTracking();
            if (!string.IsNullOrEmpty(input.QueryText))
            {
                query = query.Where(m => m.Name.Contains(DbFunctions.AsNonUnicode(input.QueryText)) || m.HelpChar.Contains(DbFunctions.AsNonUnicode(input.QueryText)) || m.WBCode.Contains(DbFunctions.AsNonUnicode(input.QueryText)));
            }
            query = query.OrderBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return query;
        }

        public List<FullComposeGroupDto> GetComposeGroupByComposeId(EntityDto<Guid> input)
        {
            var query = _composeGroupRepository.GetAll().Where(m => m.ComposeId == input.Id);
            query = query.OrderBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return query.MapTo<List<FullComposeGroupDto>>();
        }

        public FullComposeDto CreateCompose(CreateOrUpdateComposeInput input)
        {
            if (string.IsNullOrEmpty(input.Compose.Name))
            {
                throw new FieldVerifyException("组单名称不可为空！", "组单名称不可为空！");
            }
            if (input.ComposeGroups == null || input.ComposeGroups.Count == 0)
            {
                throw new FieldVerifyException("未添加分组！", "未添加分组！");
            }
            if (input.ComposeGroups.Any(m => string.IsNullOrEmpty(m.ComposeGroup.TeamName)))
            {
                throw new FieldVerifyException("有分组名为空！", "有分组名为空。");
            }
            if (input.ComposeGroups.Any(m => m.ComposeGroupItems == null || m.ComposeGroupItems.Count == 0))
            {
                throw new FieldVerifyException("有分组未添加项目！", "有分组未添加项目。");
            }
            if (_composeRepository.GetAll().Any(r => r.Name == input.Compose.Name))
            {
                throw new FieldVerifyException("组单名称重复！", "组单名称重复！");
            }

            var compose = input.Compose.MapTo<TbmCompose>();
            compose.Id = Guid.NewGuid();
            compose.ComposeGroups = new List<TbmComposeGroup>();
            foreach (var cgInput in input.ComposeGroups)
            {
                var cgEntity = cgInput.ComposeGroup.MapTo<TbmComposeGroup>();
                cgEntity.Id = Guid.NewGuid();
                cgEntity.ComposeGroupItems = new List<TbmComposeGroupItem>();
                foreach (var cgiDto in cgInput.ComposeGroupItems)
                {
                    var cgiEntity = cgiDto.MapTo<TbmComposeGroupItem>();
                    cgiEntity.Id = Guid.NewGuid();
                    cgiEntity.ItemGroup = _itemGroupRepository.Get(cgiDto.ItemGroupId);
                    cgEntity.ComposeGroupItems.Add(cgiEntity);
                }
                compose.ComposeGroups.Add(cgEntity);
            }
            _composeRepository.Insert(compose);
            return compose.MapTo<FullComposeDto>();
        }

        public FullComposeDto UpdateCompose(CreateOrUpdateComposeInput input)
        {
            if (string.IsNullOrEmpty(input.Compose.Name))
            {
                throw new FieldVerifyException("组单名称不可为空！", "组单名称不可为空！");
            }
            if (input.ComposeGroups == null || input.ComposeGroups.Count == 0)
            {
                throw new FieldVerifyException("未添加分组！", "未添加分组！");
            }
            if (input.ComposeGroups.Any(m => string.IsNullOrEmpty(m.ComposeGroup.TeamName)))
            {
                throw new FieldVerifyException("有分组名为空！", "有分组名为空。");
            }
            if (input.ComposeGroups.Any(m => m.ComposeGroupItems == null || m.ComposeGroupItems.Count == 0))
            {
                throw new FieldVerifyException("有分组未添加项目！", "有分组未添加项目。");
            }
            if (_composeRepository.GetAll().Any(r => r.Name == input.Compose.Name && r.Id != input.Compose.Id))
            {
                throw new FieldVerifyException("组单名称重复！", "组单名称重复！");
            }

            // 移除分组及其项目
            var cgIds = input.ComposeGroups.Where(m => m.ComposeGroup.Id != Guid.Empty).Select(m => m.ComposeGroup.Id);
            _composeGroupRepository.Delete(m => m.ComposeId == input.Compose.Id && !cgIds.Contains(m.Id));
            _composeGroupItemRepository.Delete(m => m.ComposeGroup.ComposeId == input.Compose.Id  && !cgIds.Contains(m.ComposeGroupId));
            // 移除分组下的项目
            foreach (var cgId in cgIds)
            {
                var cgiIds = input.ComposeGroups.FirstOrDefault(m => m.ComposeGroup.Id == cgId)?.ComposeGroupItems.Select(m => m.ItemGroupId).ToList();
                _composeGroupItemRepository.Delete(m => m.ComposeGroupId == cgId && !cgiIds.Contains(m.ItemGroupId));
            }

            var entity = _composeRepository.Get(input.Compose.Id);

            // 分组
            foreach (var cgInput in input.ComposeGroups)
            {
                if (cgInput.ComposeGroup.Id == Guid.Empty)
                {
                    // 添加分组
                    var cgEntity = cgInput.ComposeGroup.MapTo<TbmComposeGroup>();
                    cgEntity.Id = Guid.NewGuid();
                    cgEntity.Compose = entity;
                    cgEntity.ComposeGroupItems = new List<TbmComposeGroupItem>();
                    _composeGroupRepository.Insert(cgEntity);

                    foreach (var cgiDto in cgInput.ComposeGroupItems)
                    {
                        var cgiEntity = cgiDto.MapTo<TbmComposeGroupItem>();
                        cgiEntity.Id = Guid.NewGuid();
                        cgiEntity.ItemGroup = _itemGroupRepository.Get(cgiDto.ItemGroupId);
                        cgEntity.ComposeGroupItems.Add(cgiEntity);
                    }
                }
                else
                {
                    // 更新分组
                    var cgEntity = _composeGroupRepository.Get(cgInput.ComposeGroup.Id);
                    cgInput.ComposeGroup.MapTo(cgEntity);
                    _composeGroupRepository.Update(cgEntity);
                    
                    foreach (var cgiDto in cgInput.ComposeGroupItems)
                    {
                        var cgiEntity = _composeGroupItemRepository.FirstOrDefault(m => m.ComposeGroupId == cgEntity.Id && m.ItemGroupId == cgiDto.ItemGroupId);
                        if (cgiEntity == null)
                        {
                            cgiEntity = cgiDto.MapTo<TbmComposeGroupItem>();
                            cgiEntity.Id = Guid.NewGuid();
                            cgiEntity.ItemGroup = _itemGroupRepository.Get(cgiDto.ItemGroupId);
                            cgiEntity.ComposeGroup = _composeGroupRepository.Get(cgEntity.Id);
                            _composeGroupItemRepository.Insert(cgiEntity);
                        }
                        else
                        {
                            cgiDto.Id = cgiEntity.Id;
                            cgiDto.MapTo(cgiEntity);
                            _composeGroupItemRepository.Update(cgiEntity);
                        }
                    }
                }
            }

            input.Compose.MapTo(entity);
            entity = _composeRepository.Update(entity);
            return entity.MapTo<FullComposeDto>();
        }

    }
}
