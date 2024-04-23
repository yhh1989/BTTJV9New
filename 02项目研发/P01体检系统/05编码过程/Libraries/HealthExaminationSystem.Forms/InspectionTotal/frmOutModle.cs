using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmOutModle : UserBaseForm
    {
        private IItemSuitAppService itemSuitAppSvr;//套餐
        private IItemGroupAppService itemGroupAppService;
        private List<ImportDataDto> outcuslist;


        public frmOutModle()
        {
            itemSuitAppSvr = new ItemSuitAppService();
            itemGroupAppService = new ItemGroupAppService();
            InitializeComponent();
        }
        public frmOutModle(List<ImportDataDto> importDataDtos) :this()
        {
            outcuslist = importDataDtos;
        }

        private void frmOutModle_Load(object sender, EventArgs e)
        {
            gridAllGroups.DataSource = DefinedCacheHelper.GetItemGroups().ToList();
            searchLookSuit.Properties.DataSource = DefinedCacheHelper.GetItemSuit().ToList();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Add();
        }
        public void Add()
        {
            //获取选择dt
            var lstOptionalItemGroup = gridAllGroups.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (lstOptionalItemGroup.Count() == 0)
            {
                return;
            }
            var selectgroup = gridSelectGroups.DataSource as List<SimpleItemGroupDto>;
            if (selectgroup == null)
            { selectgroup = new List<SimpleItemGroupDto>(); }
            selectgroup.AddRange(lstOptionalItemGroup);

            gridSelectGroups.DataSource = selectgroup;
            gridSelectGroups.Refresh();
            gridViewSelect.RefreshData();
            RemoveGroup(lstOptionalItemGroup);


        }
        public void Del()
        {
            //获取选择dt
            var lstOptionalItemGroup = gridSelectGroups.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (lstOptionalItemGroup.Count() == 0)
            {
                return;
            }
            var selectgroup = gridAllGroups.DataSource as List<SimpleItemGroupDto>;
            if (selectgroup == null)
            { selectgroup = new List<SimpleItemGroupDto>(); }
            selectgroup.AddRange(lstOptionalItemGroup);

            gridAllGroups.DataSource = selectgroup;
            gridAllGroups.Refresh();

            gridViewAll.RefreshData();
            RemoveSelectGroup(lstOptionalItemGroup);

        }
        private void RemoveGroup(List<SimpleItemGroupDto> lstSimpleItemGroupDto )
        {
            //获取当前选择行
            var iindex = gridViewAll.GetFocusedDataSourceRowIndex();
            var lstOptionalItemGroup = gridAllGroups.GetDtoListDataSource<SimpleItemGroupDto>();
            foreach (var item in lstSimpleItemGroupDto)
            {
                if (lstOptionalItemGroup.Contains(item))
                {
                    gridAllGroups.RemoveDtoListItem(item);
                }
            }

            if (lstOptionalItemGroup.Count < iindex)
            {
                gridViewAll.SelectRow(iindex);
            }
            else
            {
                gridViewAll.SelectRow(lstOptionalItemGroup.Count - lstSimpleItemGroupDto.Count);
            }
        }
        private void RemoveSelectGroup(List<SimpleItemGroupDto> lstSimpleItemGroupDto)
        {
            //获取当前选择行
            
            var iindex = gridViewSelect.GetFocusedDataSourceRowIndex();

            var lstOptionalItemGroup = gridSelectGroups.GetDtoListDataSource<SimpleItemGroupDto>();
            
            foreach (var item in lstSimpleItemGroupDto)
            {
                if (lstOptionalItemGroup.Contains(item))
                {
                    gridSelectGroups.RemoveDtoListItem(item);
                }
            }

            if (lstOptionalItemGroup.Count < iindex)
            {
                gridViewSelect.SelectRow(iindex);
            }
            else
            {
                gridViewSelect.SelectRow(lstOptionalItemGroup.Count - lstSimpleItemGroupDto.Count);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void gridAllGroups_DoubleClick(object sender, EventArgs e)
        {
            Add();
        }

        private void gridSelectGroups_DoubleClick(object sender, EventArgs e)
        {
            Del();
        }

        private void searchLookSuit_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookSuit.EditValue == null)
                return;
            var data = searchLookSuit.GetSelectedDataRow() as SimpleItemSuitDto;
            if (data != null)
            {
                var itemsuit = data as SimpleItemSuitDto;
                FullItemSuitDto suitInfo = null;
                try
                {
                    var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto() { Id = itemsuit.Id });
                    if (ret != null)
                    {
                        if (ret.Count > 0)
                        {
                            suitInfo = ret.First();
                            suitInfo.ItemSuitItemGroups = suitInfo.ItemSuitItemGroups.OrderBy(o => o.ItemGroup?.Department?.OrderNum).ThenBy(o => o.ItemGroup?.OrderNum).ToList();
                            var SutGroupDto = new List<SimpleItemGroupDto>();
                            foreach (var group in suitInfo.ItemSuitItemGroups)
                            {
                                 
                                var simpleItemGroupDto = DefinedCacheHelper.GetItemGroups().FirstOrDefault(o=>o.Id== group.ItemGroupId);
                                SutGroupDto.Add(simpleItemGroupDto);
                            }
                            if (SutGroupDto.Count > 0)
                            {  //获取选择dt
                                var lstOptionalItemGroup = SutGroupDto;
                                if (lstOptionalItemGroup.Count() == 0)
                                {
                                    return;
                                }
                                var selectgroup = gridSelectGroups.DataSource as List<SimpleItemGroupDto>;
                                if (selectgroup == null)
                                { selectgroup = new List<SimpleItemGroupDto>(); }
                                selectgroup.AddRange(lstOptionalItemGroup);

                                gridSelectGroups.DataSource = lstOptionalItemGroup;

                                RemoveGroup(lstOptionalItemGroup);

                            }
                        }
                    }
                
                }
                catch
                { }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable sderfdxcvvvb  = new DataTable();
            sderfdxcvvvb.Columns.Add("体检号");
            sderfdxcvvvb.Columns.Add("姓名");
            sderfdxcvvvb.Columns.Add("性别");
            sderfdxcvvvb.Columns.Add("年龄");
            var strList = new List<string>() {
                 "体检号",
                 "姓名",
                "性别",
                "年龄"
            };
            SearIdsDto grouIDls = new SearIdsDto();
            var GroupNames = gridSelectGroups.DataSource as List<SimpleItemGroupDto>;
            if (GroupNames == null)
            {
                MessageBox.Show("请选择组合名称！"); return;
            }
            else if (GroupNames.Count == 0)
            {
                MessageBox.Show("请选择组合名称！"); return;
            }
            else
            {
                grouIDls.GroupIds = GroupNames.Select(o => o.Id).ToList();

                var items = itemGroupAppService.getItemNames(grouIDls).Distinct().ToList();
                foreach (var group in items)
                {
                    strList.Add(group.Name);
                    sderfdxcvvvb.Columns.Add(group.Name);
                }

            }
            if (outcuslist != null && outcuslist.Count > 0)
            {
                foreach (var cus in outcuslist)
                {
                    DataRow dr = sderfdxcvvvb.NewRow();
                    dr["体检号"] = cus.CustomerBM;
                    dr["姓名"] = cus.Name;
                    dr["性别"] = SexHelper.CustomSexFormatter(cus.Sex);
                    dr["年龄"] = cus.Age;
                    sderfdxcvvvb.Rows.Add(dr);

                }

                // ExcelHelper.ExportToExcel()
               

                var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.FileName = "外出数据";
                saveFileDialog.DefaultExt = "xls";
                saveFileDialog.Title = "导出Excel";
                saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
                saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
                var dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {

                    ExcelHelper ex = new ExcelHelper(saveFileDialog.FileName);
                    ex.DataTableToExcel(sderfdxcvvvb, "外出数据", true);
                    MessageBox.Show("导出成功！");
                }
            }        
            else
            {
                GridControlHelper.ExportByGridControl(strList, "项目结果导入");
            }
       
        }
    }
}
