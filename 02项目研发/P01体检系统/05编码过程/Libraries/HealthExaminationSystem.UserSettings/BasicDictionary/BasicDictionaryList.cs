using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{
    public partial class BasicDictionaryList : UserBaseForm
    {
        
        private readonly IBasicDictionaryAppService service;
        public List<int> indexls = new List<int>();

        public BasicDictionaryList()
        {
            InitializeComponent();

            service = new BasicDictionaryAppService();
        }

        private void BasicDictionary_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            //LoadControlData();
            BinDingDataTreeS();
           // InitSearchControl();
            //lookUpEditBasicDictonaryType.EditValue = 0;
            System.Windows.Forms.Application.DoEvents();
            treeView1.Enabled = true;
        }
        /// <summary>
        /// 实现树节点的过滤查询
        /// </summary>
        private void InitSearchControl()
        {
            this.searchControl1.Client = this.treeView1;
            this.treeView1.FilterNode += (object sender, DevExpress.XtraTreeList.FilterNodeEventArgs e) =>
            {
                if (treeView1.DataSource == null)
                    return;
                string nodeText = e.Node.GetDisplayText("Name");//参数填写FieldName 
                if (string.IsNullOrWhiteSpace(nodeText))
                    return;
                bool isExist = nodeText.IndexOf(searchControl1.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                if (isExist)
                {
                    var node = e.Node.ParentNode;
                    while (node != null)
                    {
                        if (!node.Visible)
                        {
                            node.Visible = true;
                            node = node.ParentNode;
                        }
                        else
                            break;
                    }
                }
                e.Node.Visible = isExist;
                e.Handled = true;
            };
        }

        private void lueBasicDictonaryType_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void sbReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            
           
            var node = treeView1.GetFocusedRow();
            if (node==null)
            {
                ShowMessageBoxWarning("请选择字典类别。");
                return;
            }            
             var type = (BasicDictionaryType)treeView1.GetFocusedRowCellValue(treeListColumn1);
          
            using (var frm = new BasicDictionaryEditor(type))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.isadd == true)
                    { sbAdd.PerformClick(); }
                    else
                    {
                        LoadData();
                    }
                }
            }
        }

        private void simpleButtonEdit_Click(object sender, EventArgs e)
        {
            var id = gridViewBasicDictionary.GetRowCellValue(gridViewBasicDictionary.FocusedRowHandle,
                gridColumnBasicDictionaryId);
            if (id == null)
            {
                ShowMessageBoxWarning("请选中要编辑的字典数据行。");
                return;
            }
            var node = treeView1.GetFocusedRow();
            var type = (BasicDictionaryType)treeView1.GetFocusedRowCellValue(treeListColumn1); 

            using (var frm = new BasicDictionaryEditor((Guid)id, type))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<BasicDictionaryDto>();
            Del(dto);
        }

        private void gridViewBasicDictionary_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                //var dto = (BasicDictionaryDto)gridViewBasicDictionary.GetRow(e.RowHandle);
                //Edit(dto);
                simpleButtonEdit.PerformClick();
            }
        }

        /// <summary>
        /// 加载控件数据
        /// </summary>
        //private void LoadControlData()
        //{
        //    lookUpEditBasicDictonaryType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(BasicDictionaryType));
        //}

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            var node = treeView1.GetFocusedRow();
            if (node == null)
            {
                ShowMessageBoxWarning("请选择字典类别。");
                return;
            }
            if (node == null)
            {
                ShowMessageBoxWarning("请选择字典类别。");
                return;
            }

            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            try
            {
                var type = (BasicDictionaryType)treeView1.GetFocusedRowCellValue(treeListColumn1);
                gridControl.DataSource = null;
                var input = new BasicDictionaryInput { Type = type.ToString() };
                var output = service.Query(input);
                gridControl.DataSource = output;
                // 添加更新对应缓存操作
                ThreadPool.QueueUserWorkItem(UpdateCache);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    splashScreenManager.CloseWaitForm();
                }
            }

            //var closeWait = false;
            //if (!splashScreenManager.IsSplashFormVisible)
            //{
            //    splashScreenManager.ShowWaitForm();
            //    closeWait = true;
            //}
            //splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            //try
            //{
            //    var BasicDictionary = new BasicDictionaryDto();
            //    if (!string.IsNullOrWhiteSpace(txtdiction.Text.Trim()))
            //    {
            //       BasicDictionary.Text = txtdiction.Text.Trim();
            //    }
            //    try
            //    {
            //        indexls.Clear();
            //        var Search = service.Get(BasicDictionary);
            //        for (int i = 0; i < Search.OrderNum; i++)
            //        {
            //            var nodeDepartment = new TreeNode();
            //            //nodeDepartment.Tag = Search.Key;
            //            //nodeDepartment.Text = Search[i].Value;
            //            treeView1.Nodes.Add(nodeDepartment);
            //        }
            //    }
            //    catch (UserFriendlyException ex)
            //    {
            //        ShowMessageBoxError(ex.ToString());
            //    }


            //    //gridControl.DataSource = null;
            //    //var input = new BasicDictionaryInput { Type = type.ToString() };
            //    //var output = service.Query(input);
            //    //gridControl.DataSource = output;
            //    // 添加更新对应缓存操作
            //    ThreadPool.QueueUserWorkItem(UpdateCache);
            //}
            //catch (UserFriendlyException ex)
            //{
            //    ShowMessageBox(ex);
            //}
            //finally
            //{
            //    if (closeWait)
            //    {
            //        splashScreenManager.CloseWaitForm();
            //    }
            //}
        }

        private void UpdateCache(object obj)
        {
            DefinedCacheHelper.UpdateBasicDictionary();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        public void Del(BasicDictionaryDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择要删除的字典项。");
                return;
            }

            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;

            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingDelete);
            try
            {
                service.Del(new EntityDto<Guid> { Id = dto.Id });
                gridControl.RemoveDtoListItem(dto);
            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        //树点击事件
        //private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    var type = EnumHelper.GetEnumDescs(typeof(BasicDictionaryType));
        //    var  str = e.Node.Tag;          
        //    gridControl.DataSource = null;
        //    var input = new BasicDictionaryInput { Type = str.ToString() };
        //    var output = service.Query(input);
        //    gridControl.DataSource = output;
        //    // 添加更新对应缓存操作
        //    ThreadPool.QueueUserWorkItem(UpdateCache);

        //}
        //绑定树数据 
        public void BinDingDataTreeS()
        {

            var input = EnumHelper.GetEnumDescs(typeof(BasicDictionaryType));
            treeView1.DataSource = input;
            //for (var i = 0; i < input.Count; i++)
            //{
            //    var nodeDepartment = new TreeNode();
            //    nodeDepartment.Tag = input[i].Key;
            //    nodeDepartment.Text = input[i].Value;
            //    treeView1.Nodes.Add(nodeDepartment);
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void treeView1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var type = EnumHelper.GetEnumDescs(typeof(BasicDictionaryType));
            var str = e.Node.GetValue(treeListColumn1);
            gridControl.DataSource = null;
            var input = new BasicDictionaryInput { Type = str.ToString() };
            var output = service.Query(input);
            gridControl.DataSource = output;
            // 添加更新对应缓存操作
            ThreadPool.QueueUserWorkItem(UpdateCache);
        }

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InitSearchControl();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
           var keys = Convert.ToString(treeView1.FocusedNode.GetValue("Key"));
            var Names = Convert.ToString(treeView1.FocusedNode.GetValue("Value"));
            var ParentIds = Convert.ToString(treeView1.FocusedNode.GetValue("ParentId"));
            var data = gridViewBasicDictionary.GetFocusedRow() as BasicDictionaryDto;
          
            using (var frm = new BExcelIntput(ParentIds, Names, keys))
            {
                frm.ShowDialog();
                
                    simpleButton1.PerformClick();
                
            }
        }
    }
}