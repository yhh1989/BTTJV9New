using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Compose;
using Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Compose
{
    /// <summary>
    /// 组单工具
    /// </summary>
    public partial class ComposeTool : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService = new CommonAppService();

        private readonly IComposeAppService _composeAppService = new ComposeAppService();

        private readonly ItemSuitAppService _itemSuitAppService = new ItemSuitAppService();

        /// <summary>
        /// 组单-分组 缓存
        /// </summary>
        private readonly Dictionary<Guid, List<FullComposeGroupDto>> ComposeGroupDictionary =
            new Dictionary<Guid, List<FullComposeGroupDto>>();

        public ComposeTool()
        {
            InitializeComponent();

            // 简单加粗标识当前选中分组，但不理想
            //gvComposeGroup.Appearance.FocusedRow.Font = new Font(gvComposeGroup.Appearance.FocusedRow.Font, FontStyle.Bold);
            //gvComposeGroup.Appearance.HideSelectionRow.Font = new Font(gvComposeGroup.Appearance.FocusedRow.Font, FontStyle.Bold);
        }

        private ComposeDto SrcCompose { get; set; }

        private ComposeDto TempCompose { get; set; }

        private bool IsCopy { get; set; }

        private void ComposeTool_Load(object sender, EventArgs e)
        {
            LoadControlData();
           // ReloadCompose();
        }

        private void LoadControlData()
        {
            gvComposeGroupLueSex.DataSource = SexHelper.GetSexModelsForItemInfo();
            gvComposeGroupLueMaritalStatus.DataSource = MarrySateHelper.GetMarrySateModels();
            repositoryItemLookUpEditgvComposeGroupConceiveStatus.DataSource = IfTypeHelper.GetIfTypeModels();

            gcItemGroupWait.DataSource = CacheHelper.GetItemGroups(true);

            LoadItemSuitWait();
        }

        private void teComposeName_Leave(object sender, EventArgs e)
        {
            var name = teComposeName.Text.Trim();
            gvComposeGroup.ViewCaption = name;
            if (!string.IsNullOrWhiteSpace(teComposeHelpChar.Text))
                return;

            if (!string.IsNullOrWhiteSpace(name))
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    teComposeHelpChar.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            else
                teComposeHelpChar.Text = string.Empty;
        }

        private void ComposeGroupRefreshDataSource()
        {
            gcComposeGroup.RefreshDataSource();
            gvComposeGroup.BeginUpdate();
            try
            {
                for (var i = 0; i < gvComposeGroup.RowCount; i++)
                    gvComposeGroup.SetMasterRowExpanded(i, true);
            }
            finally
            {
                gvComposeGroup.EndUpdate();
                GC.Collect();
            }
        }

        // 导出组单excel
        private void sbExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel($"组单_{teComposeName.Text.Trim()}", gcComposeGroup);
        }

        private void sbReloadCompose_Click(object sender, EventArgs e)
        {
            ReloadCompose();
        }

        private void dataNavigator_gcCompose_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            ReloadCompose();
        }

        /// <summary>
        /// 加载组单
        /// </summary>
        private void ReloadCompose()
        {
            ComposeGroupDictionary.Clear();
            gcCompose.DataSource = null;
            SrcCompose = null;
            TempCompose = null;
            AutoLoading(() =>
            {
                var input = new PageInputDto<SearchComposeInput>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new SearchComposeInput
                    {
                        QueryText = teQuery.Text.Trim()
                    }
                };
                var output = _composeAppService.PageComposes(input);
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator_gcCompose);
                gcCompose.DataSource = output.Result;
                gvCompose.BestFitColumns();
            });
        }

        private void gvCompose_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            var dto = (ComposeDto)gvCompose.GetRow(e.FocusedRowHandle);
            SrcCompose = dto;
            TempCompose = ModelHelper.DeepCloneByJson(dto);
            LoadComposeInfo(TempCompose);
        }

        private void gvComposeGroup_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            var dto = (FullComposeGroupDto)gvComposeGroup.GetRow(e.FocusedRowHandle);
        }

        private void gvComposeGroup_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if(gvComposeGroup.FocusedRowHandle >= 0)
            {
                if(gvComposeGroup.FocusedRowHandle == e.RowHandle)
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Regular);
                }
            }
        }

        private void sbNew_Click(object sender, EventArgs e)
        {
            lcgCompose.Enabled = false;
            IsCopy = false;
            ClearComposeInfo();
            teComposeName.Focus();
        }

        private void sbCopy_Click(object sender, EventArgs e)
        {
            lcgCompose.Enabled = false;
            IsCopy = true;
            SrcCompose = null;
            TempCompose = ModelHelper.DeepCloneByJson(TempCompose);
            LoadComposeInfo(TempCompose);
            TempCompose.Id = Guid.Empty;
            teComposeName.Focus();
        }

        private void sbSave_Click(object sender, EventArgs e)
        {
            SaveCompose();
        }

        private void SaveCompose()
        {
            dxErrorProvider.ClearErrors();
            if (string.IsNullOrEmpty(teComposeHelpChar.Text))
            {
                dxErrorProvider.SetError(teComposeName, string.Format(Variables.MandatoryTips, "科室"));
                teComposeHelpChar.Focus();
                return;
            }

            var ComposeGroups = gcComposeGroup.GetDtoListDataSource<FullComposeGroupDto>();
            if (ComposeGroups == null || ComposeGroups.Count == 0)
            {
                ShowMessageBoxWarning("未添加分组。");
                return;
            }

            if (ComposeGroups.Any(m => string.IsNullOrEmpty(m.TeamName)))
            {
                ShowMessageBoxWarning("有分组名为空。");
                return;
            }

            if (ComposeGroups.Any(m => m.ComposeGroupItems == null || m.ComposeGroupItems.Count == 0))
            {
                ShowMessageBoxWarning("有分组未添加项目。");
                return;
            }

            AutoLoading(() =>
            {
                var input = new CreateOrUpdateComposeInput();
                input.Compose = TempCompose == null
                    ? new CreateOrUpdateComposeDto()
                    : ModelHelper.CustomMapTo2<ComposeDto, CreateOrUpdateComposeDto>(TempCompose);
                input.Compose.Name = teComposeName.Text.Trim();
                input.Compose.HelpChar = teComposeHelpChar.Text.Trim();
                input.Compose.Remarks = teComposeRemarks.Text.Trim();
                input.ComposeGroups = new List<CreateOrUpdateComposeGroupInput>();
                foreach (var cg in ComposeGroups)
                    input.ComposeGroups.Add(new CreateOrUpdateComposeGroupInput
                    {
                        ComposeGroup = ModelHelper.CustomMapTo2<FullComposeGroupDto, CreateOrUpdateComposeGroupDto>(cg),
                        ComposeGroupItems = cg.ComposeGroupItems.Select(m => new CreateOrUpdateComposeGroupItemDto
                        {
                            Id = m.Id,
                            ItemGroupId = m.ItemGroup.Id,
                            ItemGroupMoney = m.ItemGroupMoney,
                            Discount = m.Discount,
                            ItemGroupDiscountMoney = m.ItemGroupDiscountMoney
                        }).ToList()
                    });

                if (TempCompose == null || TempCompose.Id == Guid.Empty)
                {
                    var output = _composeAppService.CreateCompose(input);
                    gcCompose.AddDtoListItem((ComposeDto)output);
                    gcCompose.RefreshDataSource();
                    ComposeGroupDictionary.Remove(output.Id);
                }
                else
                {
                    var output = _composeAppService.UpdateCompose(input);
                    ModelHelper.CustomMapTo2(output, SrcCompose);
                    gcCompose.RefreshDataSource();
                    ComposeGroupDictionary.Remove(output.Id);
                }

                lcgCompose.Enabled = true;
            });
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            SrcCompose = (ComposeDto)gvCompose.GetFocusedRow();
            if (SrcCompose != null)
            {
                TempCompose = ModelHelper.DeepCloneByJson(SrcCompose);
                LoadComposeInfo(TempCompose);
            }
            else
            {
                ClearComposeInfo();
            }

            lcgCompose.Enabled = true;
        }

        /// <summary>
        /// 加载组单信息
        /// </summary>
        /// <param name="compose"></param>
        private void LoadComposeInfo(ComposeDto compose)
        {
            teComposeName.Text = compose.Name;
            teComposeHelpChar.Text = compose.HelpChar;
            teComposeRemarks.Text = compose.Remarks;

            gvComposeGroup.ViewCaption = compose.Name;

            var groups = ModelHelper.DeepCloneByJson(GetComposeGroup(compose.Id));
            gcComposeGroup.DataSource = groups;
            gvComposeGroup.FocusedRowHandle = 0;

            // 展开子
            gvComposeGroup.BeginUpdate();
            try
            {
                for (var i = 0; i < gvComposeGroup.RowCount; i++)
                    gvComposeGroup.SetMasterRowExpanded(i, true);
            }
            finally
            {
                gvComposeGroup.EndUpdate();
                GC.Collect();
            }
        }

        /// <summary>
        /// 清除组单信息
        /// </summary>
        private void ClearComposeInfo()
        {
            teComposeName.Text = string.Empty;
            teComposeHelpChar.Text = string.Empty;
            teComposeRemarks.Text = string.Empty;
            SrcCompose = null;
            TempCompose = new ComposeDto();
            gvComposeGroup.ViewCaption = string.Empty;
            gcComposeGroup.DataSource = new List<FullComposeGroupDto>();
        }

        private void sbReloadComposeGroup_Click(object sender, EventArgs e)
        {
            var dto = gcCompose.GetFocusedRowDto<ComposeDto>();
            if (dto == null) return;

            gcComposeGroup.DataSource = GetComposeGroup(dto.Id);

            // 展开子
            gvComposeGroup.BeginUpdate();
            try
            {
                for (var i = 0; i < gvComposeGroup.RowCount; i++)
                    gvComposeGroup.SetMasterRowExpanded(i, true);
            }
            finally
            {
                gvComposeGroup.EndUpdate();
                GC.Collect();
            }
        }

        /// <summary>
        /// 获取组单分组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reload"></param>
        private List<FullComposeGroupDto> GetComposeGroup(Guid id, bool reload = false)
        {
            List<FullComposeGroupDto> list = null;
            if (reload)
                ComposeGroupDictionary.Remove(id);
            if (ComposeGroupDictionary.ContainsKey(id))
                list = ComposeGroupDictionary[id];
            else
                AutoLoading(() =>
                {
                    list = _composeAppService.GetComposeGroupByComposeId(new EntityDto<Guid> { Id = id });
                    foreach (var item in list)
                        item.ComposeGroupItems = item.ComposeGroupItems.OrderBy(m => m.ItemGroup.Department.OrderNum).ThenBy(m => m.ItemGroup.OrderNum)
                            .ThenBy(m => m.ItemGroup.ItemGroupName).ToList();
                    ComposeGroupDictionary.Add(id, list);
                });
            return list;
        }

        private void sbAddComposeGroup_Click(object sender, EventArgs e)
        {
            AddComposeGroup();
        }

        private void sbDelComposeGroup_Click(object sender, EventArgs e)
        {
            if (gvComposeGroup.FocusedRowHandle < 0)
            {
                ShowMessageBoxWarning("请选择分组。");
                return;
            }

            DelComposeGroup(gvComposeGroup.FocusedRowHandle);
        }

        private void gvComposeGroup_RowClick(object sender, RowClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            //{
            //    var dto = gcComposeGroup.GetFocusedRowDto<FullComposeGroupDto>();
            //    EditComposeGroup(dto);
            //}
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        private void AddComposeGroup()
        {
            if (TempCompose == null)
            {
                ShowMessageBoxWarning("请选择组单。");
                return;
            }

            var list = gcComposeGroup.GetDtoListDataSource<FullComposeGroupDto>();
            if (list == null)
            {
                list = new List<FullComposeGroupDto>();
                gcComposeGroup.DataSource = list;
            }

            list.Add(new FullComposeGroupDto
            {
                Sex = (int)Sex.GenderNotSpecified,
                MinAge = 0,
                MaxAge = 120,
                OrderNum = list.Count == 0 ? 0 : list.Max(m => m.OrderNum ?? 0) + 1,
                ComposeGroupItems = new List<ComposeGroupItemDto>()
            });
            ComposeGroupRefreshDataSource();

            //gvComposeGroup.AddNewRow();
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DelComposeGroup(int rowHandle)
        {
            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;

            gvComposeGroup.DeleteRow(rowHandle);
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void gvItemGroup_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
                Del();
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == xtpItemGroup)
            {
                AddItemGroup();
            }
            else if (xtraTabControl.SelectedTabPage == xtpItemSuit)
            {
                if (gcItemSuitWait.FocusedView == gvItemSuitWait)
                    AddItemSuit();
                else if (gcItemSuitWait.FocusedView == gvItemSuitItemGroups)
                    AddItemSuitItemGroup();
            }
        }

        private void gvItemGroupWait_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
                AddItemGroup();
        }

        private void gvItemSuitWait_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
                AddItemSuit();
        }

        private void gvItemSuitItemGroups_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
                AddItemSuitItemGroup();
        }

        /// <summary>
        /// 添加项目组合
        /// </summary>
        private void AddItemGroup()
        {
            var itemGroupDtos = gcItemGroupWait.GetSelectedRowDtos<SimpleItemGroupDto>();
            AddToSelected(itemGroupDtos);
        }

        /// <summary>
        /// 添加项目套餐（全部项目组合）
        /// </summary>
        private void AddItemSuit()
        {
            var itemSuitDto = (FullItemSuitDto)gvItemSuitWait.GetFocusedRow();
            if (itemSuitDto == null) return;

            var composeGroupDto = (FullComposeGroupDto)gvComposeGroup.GetFocusedRow();
            if (composeGroupDto == null)
            {
                ShowMessageBoxWarning("当前没有分组。");
                return;
            }

            if (XtraMessageBox.Show($"是否将套餐[{itemSuitDto.ItemSuitName}]中的组合全部加入分组[{composeGroupDto.TeamName}]？", "询问",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var itemGroupDtos = itemSuitDto.ItemSuitItemGroups.Select(m => m.ItemGroup).ToList();
                AddToSelected(itemGroupDtos);
            }
        }

        /// <summary>
        /// 添加项目套餐下的指定项目组合
        /// </summary>
        private void AddItemSuitItemGroup()
        {
            var gridView = (GridView)gcItemSuitWait.FocusedView;
            var itemGroupDtos = new List<SimpleItemGroupDto>();
            foreach (var item in gridView.GetSelectedRows())
                itemGroupDtos.Add(((ItemSuitItemGroupContrastFullDto)gridView.GetRow(item)).ItemGroup);
            AddToSelected(itemGroupDtos);
        }

        /// <summary>
        /// 添加项目组合至已选列表
        /// </summary>
        /// <param name="itemGroupDtos"></param>
        private void AddToSelected(List<SimpleItemGroupDto> itemGroupDtos)
        {
            var dto = (FullComposeGroupDto)gvComposeGroup.GetFocusedRow();
            if (dto == null)
            {
                ShowMessageBoxWarning("当前没有分组。");
                return;
            }

            if (string.IsNullOrEmpty(dto.TeamName))
            {
                ShowMessageBoxWarning("当前分组还未命名。");
                return;
            }

            itemGroupDtos.RemoveAll(m => dto.ComposeGroupItems.Any(s => s.ItemGroup.Id == m.Id));
            dto.ComposeGroupItems.AddRange(itemGroupDtos.Select(m => new ComposeGroupItemDto
            {
                ItemGroup = m,
                ItemGroupMoney = m.Price ?? 0,
                Discount = 1,
                ItemGroupDiscountMoney = m.Price ?? 0
            }));
            dto.ComposeGroupItems = dto.ComposeGroupItems.OrderBy(m => m.ItemGroup.Department.OrderNum).ThenBy(m => m.ItemGroup.OrderNum)
                .ThenBy(m => m.ItemGroup.ItemGroupName).ToList();
            ComposeGroupRefreshDataSource();
        }

        /// <summary>
        /// 移除所选项目组合
        /// </summary>
        private void Del()
        {
            var gridView = (GridView)gcComposeGroup.FocusedView;
            if (gridView.FocusedRowHandle < 0) return;

            gridView.DeleteRow(gridView.FocusedRowHandle);
            ComposeGroupRefreshDataSource();
        }

        // 计算打折
        private void gvItemGroup_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (e.Column.Name == gvComposeGroupItemDiscount.Name)
            {
                var dto = gridView.GetRow(e.RowHandle) as ComposeGroupItemDto;
                if (dto.ItemGroup.MaxDiscount != null && dto.Discount > dto.ItemGroup.MaxDiscount)
                    dto.Discount = dto.ItemGroup.MaxDiscount.Value;
                dto.ItemGroupDiscountMoney = dto.ItemGroupMoney * dto.Discount;
            }
            else if (e.Column.Name == gvComposeGroupItemItemGroupDiscountMoney.Name)
            {
                var dto = gridView.GetRow(e.RowHandle) as ComposeGroupItemDto;
                dto.Discount = dto.ItemGroupMoney == 0 ? 0 : dto.ItemGroupDiscountMoney / dto.ItemGroupMoney;
                dto.Discount = Math.Ceiling(dto.Discount * 100) / 100; // 进一法
            }
        }
        /// <summary>
        /// 总价计算
        /// </summary>
        private void gvComposeGroup_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // summary的sum不能直接计算子集合计
            var ds = gcComposeGroup.DataSource as List<FullComposeGroupDto>;
            if (ds != null)
            {
                decimal sum = ds.Sum(m => m.ComposeGroupItems.Sum(n => n.ItemGroupMoney));
                e.TotalValue = $"总价：{sum:0.##}";
            }
        }

        private void LoadItemSuitWait()
        {
            gcItemSuitWait.DataSource = null;
            AutoLoading(() =>
            {
                var output = _itemSuitAppService.PageFulls(new PageInputDto<SearchItemSuitDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = new SearchItemSuitDto
                    {
                        QueryText = teQueryItemSuit.Text.Trim()
                    }
                });
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator_gcItemGroupWait);
                gcItemSuitWait.DataSource = output.Result;
                gvItemSuitWait.BestFitColumns();
            });
        }

        // 分页
        private void dataNavigator_gcItemSuitWait_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadItemSuitWait();
        }

        private void sbQueryItemSuit_Click(object sender, EventArgs e)
        {
            LoadItemSuitWait();
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            if (SrcCompose == null) return;

            var groups = GetComposeGroup(SrcCompose.Id);

            var list = new List<ComposeGroupItemView>();
            foreach (var group in groups)
            foreach (var item in group.ComposeGroupItems)
                list.Add(new ComposeGroupItemView
                {
                    TeamName = group.TeamName,
                    Sex = group.Sex,
                    MinAge = group.MinAge,
                    MaxAge = group.MaxAge,
                    MaritalStatus = group.MaritalStatus,
                    ConceiveStatus = group.ConceiveStatus,
                    DepartmentName = item.ItemGroup.Department.Name,
                    ItemGroupName = item.ItemGroup.ItemGroupName,
                    ItemGroupMoney = item.ItemGroupMoney,
                    Discount = item.Discount,
                    ItemGroupDiscountMoney = item.ItemGroupDiscountMoney
                });
            var jsonObj = new
            {
                Master = new[]
                {
                    new
                    {
                        ReportTitle = SrcCompose.Name
                    }
                },
                Detail = list
            };
            GridppHelper.GridReportPrintPreview("组单.grf", "组单打印", jsonObj);
        }

        /// <summary>
        /// 分组项目视图
        /// </summary>
        private class ComposeGroupItemView
        {
            /// <summary>
            /// 分组名称
            /// </summary>
            public virtual string TeamName { get; set; }

            /// <summary>
            /// 适用性别
            /// </summary>
            public virtual int Sex { get; set; }

            /// <summary>
            /// 最小年龄
            /// </summary>
            public virtual int MinAge { get; set; }

            /// <summary>
            /// 最大年龄
            /// </summary>
            public virtual int MaxAge { get; set; }

            /// <summary>
            /// 结婚状态
            /// </summary>
            public virtual int MaritalStatus { get; set; }

            /// <summary>
            /// 是否备孕 1备孕2不备孕
            /// </summary>
            public virtual int? ConceiveStatus { get; set; }

            /// <summary>
            /// 科室名称
            /// </summary>
            public virtual string DepartmentName { get; set; }

            /// <summary>
            /// 项目组合名称
            /// </summary>
            public virtual string ItemGroupName { get; set; }

            /// <summary>
            /// 组合价格
            /// </summary>
            public virtual decimal ItemGroupMoney { get; set; }

            /// <summary>
            /// 组合折扣率
            /// </summary>
            public virtual decimal Discount { get; set; }

            /// <summary>
            /// 组合折扣后价格
            /// </summary>
            public virtual decimal ItemGroupDiscountMoney { get; set; }
        }

    }
}