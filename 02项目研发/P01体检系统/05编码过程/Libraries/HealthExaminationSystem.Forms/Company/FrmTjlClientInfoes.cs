using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using CacheHelper = Sw.Hospital.HealthExaminationSystem.Common.Helpers.CacheHelper;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    /// <summary>
    /// 单位查询 by yjh 2018-08-10
    /// </summary>
    public partial class FrmTjlClientInfoes : UserBaseForm
    {
        /// <summary>
        /// 单位服务
        /// </summary>
        private readonly IClientInfoesAppService _clientInfoesAppService;

        private readonly IClientRegAppService _clientRegAppService;

        private bool _treeNodeExpandOrCollapse;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserAppService _userAppService;
        
        private List<UserForComboDto> _userList;

        

        public FrmTjlClientInfoes()
        {
            InitializeComponent();

            _clientInfoesAppService = new ClientInfoesAppService();
            _clientRegAppService = new ClientRegAppService();
            _userAppService = new UserAppService();
        }

        /// <summary>
        /// 清除窗体
        /// </summary>
        public void ClearUi()
        {
            txtClientBM.Text = "";
            txtClientName.Text = "";
            txtCreationTime1.EditValue = null;
            txtCreationTime2.EditValue = null;
            txtClientDegree.EditValue = null;
            txtLinkMan.Text = "";
            txtClientSource.EditValue = null;
            txtClientSource.RefreshEditValue();
        }

        /// <summary>
        /// 查询搜索信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCreationTime1.DateTime > txtCreationTime2.DateTime)
            {
                ShowMessageBoxWarning("开始时间大于结束时间，请修改后查询");
                return;
            }

            Reload();
        }

        public void Export()
        {
            //// 重名则会提示重复是否要覆盖
            //if (saveFileDialog.ShowDialog() != DialogResult.OK)
            //    return;

            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName, treClientInfo);
            //if (Path.GetExtension(fileName) == ".xlsx")
            //{
            //    var xlsxExportOptions = new XlsxExportOptions();
            //    treClientInfo.ExportToXlsx(fileName, xlsxExportOptions);
            //}
            //else
            //{
            //    var xlsExportOptions = new XlsExportOptions();
            //    treClientInfo.ExportToXls(fileName, xlsExportOptions);
            //}

            //if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            //    DialogResult.Yes)
            //    Process.Start(fileName); //打开指定路径下的文件
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Reload()
        {
            treClientInfo.DataSource = null;
            AutoLoading(() =>
            {
                //var page = new PageInputDto<ClientInfoesListInput> { TotalPages = TotalPages, CurentPage = CurentPage };

                var dto = new ClientInfoesListInput();

                //单位编码信息
                if (!string.IsNullOrWhiteSpace(txtClientBM.Text))
                {
                    dto.ClientBM = txtClientBM.Text;
                }

                //单位名称
                if (!string.IsNullOrWhiteSpace(txtClientName.Text))
                {
                    dto.ClientName = txtClientName.Text;
                }

                //来源
                if (txtClientSource.EditValue != null && !txtClientSource.EditValue.Equals(""))
                {
                    dto.ClientSource = txtClientSource.EditValue?.ToString();
                }

                //联系人
                if (!string.IsNullOrWhiteSpace(txtLinkMan.Text))
                {
                    dto.LinkMan = txtLinkMan.Text;
                }

                //开始时间
                if (!string.IsNullOrWhiteSpace(txtCreationTime1.Text))
                {
                    dto.StartTime = txtCreationTime1.DateTime;
                }

                //结束时间
                if (!string.IsNullOrWhiteSpace(txtCreationTime2.Text))
                {
                    dto.EndTime = txtCreationTime2.DateTime;
                }

                //专属客服
                if (txtClientDegree.EditValue != null && !txtClientDegree.EditValue.Equals(""))
                {
                    dto.UserId = Convert.ToInt32(txtClientDegree.EditValue);
                }

                AutoLoading(() =>
                {
                    var output = _clientInfoesAppService.PageFulls(new PageInputDto<ClientInfoesListInput>
                    {
                        TotalPages = TotalPages,
                        CurentPage = CurrentPage,
                        Input = dto
                    });
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator);
                    treClientInfo.DataSource = output.Result;
                });
               // var output = _clientInfoesAppService.Query(dto);

                //page.Input = dto;
                //var output = ClientInfoService.PageFulls(page);
                //TotalPages = output.TotalPages;
                //CurentPage = output.CurentPage;
                //InitialNavigator(dataNav);

                //treClientInfo.DataSource = output;
            });
        }

        /// <summary>
        /// 系统加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTjlClientInfoes_Load(object sender, EventArgs e)
        {
            //行号宽度
            treClientInfo.IndicatorWidth = 30;
            //绑定体检来源
            InitializeFormData();

            //绑定单位信息
            Reload();
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var frmEditTjlClientInfoes = new FrmEditTjlClientInfoes {EditMode = (int)EditModeType.Add})
            {
                if (frmEditTjlClientInfoes.ShowDialog() == DialogResult.OK)
                {
                    Reload();
                }
            }
        }

        /// <summary>
        /// 增加子级单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubClient_Click(object sender, EventArgs e)
        {
            var row = treClientInfo.FocusedNode;
            if (row == null)
            {
                ShowMessageBoxInformation("请选择父级单位！");

                //XtraMessageBox.Show("请选择父级单位！");
                return;
            }

            using (var frmed = new FrmEditTjlClientInfoes())
            {
                var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
                var clientName = treClientInfo.FocusedNode.GetValue("ClientName").ToString();
                if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(clientName))
                {
                    frmed.ParentId = id;
                    frmed.ParentName = clientName;
                }

                frmed.EditMode = (int)EditModeType.Add;
                if (frmed.ShowDialog() == DialogResult.OK)
                {
                    Reload();
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //var Id = gridViewClientInfo.GetRowCellValue(this.gridViewClientInfo.FocusedRowHandle, "Id").ToString();

            var row = treClientInfo.FocusedNode;
            if (row == null)
            {
                ShowMessageBoxInformation("请选择要修改的数据！");

                //XtraMessageBox.Show("请选择要修改的数据！");
                return;
            }

            var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
            using (var frm = new FrmEditTjlClientInfoes {EditMode = (int)EditModeType.Edit, clientId = Guid.Parse(id)})
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Reload();
                }
            }
        }

        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treClientInfo_DoubleClick_1(object sender, EventArgs e)
        {
            if (_treeNodeExpandOrCollapse)
            {
                _treeNodeExpandOrCollapse = false;
                return;
            }

            var row = treClientInfo.FocusedNode;
            if (row == null)
            {
                ShowMessageBoxInformation("请选择要修改的数据！");

                //XtraMessageBox.Show("请选择要修改的数据！");
                return;
            }

            var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
            using (var frm = new FrmEditTjlClientInfoes {EditMode = (int)EditModeType.Edit, clientId = Guid.Parse(id)})
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Reload();
                }
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNav_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearUi();
        }

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        public void InitializeFormData()
        {
            AutoLoading(() =>
            {
                //var lisClientSource = CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);
                var lisClientSource = DefinedCacheHelper.GetBasicDictionary()
                    .Where(r => r.Type == BasicDictionaryType.ClientSource.ToString());
                txtClientSource.Properties.DataSource = lisClientSource;

                if (_userList == null)
                {
                   // _userList = _userAppService.GetUsers();
                    _userList= DefinedCacheHelper.GetComboUsers();

                }

                if (_userList != null && _userList.Count > 0)
                {
                    //var mod = new ModelHandler<UserFormDto>();
                    //var dtuser = mod.FillDataTable(_userList);
                    //BindCustomGridLookUpEdit<UserFormDto>.BindGridLookUpEdit(txtClientDegree, _userList, "Name", "Id", "Name", 15, "Id");
                    txtClientDegree.Properties.DataSource = _userList;
                }
            }, Variables.LoadingForForm);
        }

        private void treClientInfo_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == treeListColumnClientSource.FieldName)
            {
                var clientSources = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);
                var source = clientSources.Find(o => o.Value.ToString() == e.Value?.ToString());
                if (source != null)
                {
                    e.DisplayText = source.Text;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var row = treClientInfo.FocusedNode;
                if (row == null)
                {
                    ShowMessageBoxInformation("请选择要修改的数据！");

                    //XtraMessageBox.Show("请选择要修改的数据！");
                    return;
                }

                var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
                var client = _clientInfoesAppService.Get(new ClientInfoesListInput {Id = Guid.Parse(id)});
                if (client.Parent == null)
                {
                    //父级单位
                    var clientInfo = _clientInfoesAppService.Query(new ClientInfoesListInput {ParentId = client.Id});
                    if (clientInfo.Count != 0)
                    {
                        foreach (var item in clientInfo)
                        {
                            var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> {Id = item.Id});
                            if (clientregs != null)
                            {
                                if (clientregs.Count > 0)
                                {
                                    ShowMessageBoxInformation("已有预约信息，无法删除！");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> {Id = client.Id});
                        if (clientregs != null)
                        {
                            if (clientregs.Count > 0)
                            {
                                ShowMessageBoxInformation("已有预约信息，无法删除！");
                                return;
                            }
                        }
                    }

                    _clientInfoesAppService.Del(new ClientInfoesDto {Id = Guid.Parse(id)});
                    foreach (var item in clientInfo)
                        _clientInfoesAppService.Del(new ClientInfoesDto {Id = item.Id});
                    Reload();
                    return;
                }

                //以下子级单位
                //查询子级单位的预约信息
                var clientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput {ParentId = Guid.Parse(id)});
                if (clientInfoes != null)
                {
                    foreach (var item in clientInfoes)
                    {
                        var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> {Id = item.Id});
                        if (clientregs != null)
                        {
                            if (clientregs.Count > 0)
                            {
                                ShowMessageBoxInformation("已有预约信息，无法删除！");
                                return;
                            }
                        }
                    }

                    foreach (var item in clientInfoes)
                        if (!GetQuery(item.Id))
                        {
                            ShowMessageBoxInformation("已有预约信息，无法删除！");
                            return;
                        }
                }

                var clientreg = _clientRegAppService.GetAll(new EntityDto<Guid> {Id = Guid.Parse(id)});
                if (clientreg != null)
                {
                    if (clientreg.Count > 0)
                    {
                        ShowMessageBoxInformation("已有预约信息，无法删除！");
                    }
                    else
                    {
                        _clientInfoesAppService.Del(new ClientInfoesDto {Id = Guid.Parse(id)});
                        var delclientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput
                            {ParentId = Guid.Parse(id)});
                        foreach (var item in delclientInfoes)
                            _clientInfoesAppService.Del(new ClientInfoesDto {Id = item.Id});
                        Reload();
                    }
                }
                else
                {
                    _clientInfoesAppService.Del(new ClientInfoesDto {Id = Guid.Parse(id)});
                    var delclientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput
                        {ParentId = Guid.Parse(id)});
                    foreach (var item in delclientInfoes)
                        _clientInfoesAppService.Del(new ClientInfoesDto {Id = item.Id});
                    Reload();
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        public bool GetQuery(Guid Id)
        {
            var clientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput {ParentId = Id});
            if (clientInfoes != null)
            {
                foreach (var item in clientInfoes)
                {
                    var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> {Id = item.Id});
                    if (clientregs != null)
                    {
                        if (clientregs.Count > 0)
                        {
                            ShowMessageBoxInformation("已有预约信息，无法删除！");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void treClientInfo_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tmpTree = sender as TreeList;
            var args = e.ObjectArgs as IndicatorObjectInfoArgs;
            if (args != null)
            {
                var rowNum = tmpTree.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }

            e.ImageIndex = -1;
        }

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }
    }
}