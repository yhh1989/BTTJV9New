using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmBeforInspection : UserBaseForm
    {
        /// <summary>
        /// 当前体检人
        /// </summary>
        // private ATjlCustomerRegDto _currentInputSys;
        private IInspectionTotalAppService inspectionTotalAppService = new InspectionTotalAppService();
        /// <summary>
        /// 当前体检人
        /// </summary>
        private BeforSumCustomerRegDto _currentInputSys;
        private PictureController _pictureController;
        private List<Guid> cusGroupIdList = new List<Guid>();

        private  IDoctorStationAppService _doctorStationAppService;

        public frmBeforInspection()
        {
            InitializeComponent();
        }

        private void frmBeforInspection_Load(object sender, EventArgs e)
        {
            _doctorStationAppService = new DoctorStationAppService();
            _pictureController = new PictureController();
            comSumSate.Properties.DataSource = SummSateHelper.GetSelectList();
            comSumSate.EditValue = (int)SummSate.NotAlwaysCheck;
            
            searchLookUpClientReg.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            comState.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            comState.EditValue = (int)PhysicalEState.Complete;

            gridViewDangTianHuanZhe.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewDangTianHuanZhe.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);

          
            gridViewGroup.Columns[1].DisplayFormat.FormatType = FormatType.Custom;
            gridViewGroup.Columns[1].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.ProjectIStateFormatter);

            gridViewItem.Columns[conProcessState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewItem.Columns[conProcessState.FieldName].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.ProjectIStateFormatter);

            gridViewDangTianHuanZhe.Columns[conCheckSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewDangTianHuanZhe.Columns[conCheckSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);

            gridViewDangTianHuanZhe.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewDangTianHuanZhe.Columns[conSummSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(SummSateHelper.SummSateFormatter);
            gridViewDangTianHuanZhe.Columns[conPrintSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewDangTianHuanZhe.Columns[conPrintSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(PrintSateHelper.PrintSateFormatter);
            //Point point = new Point();
            //point.X = butCus.Location.X + butCus.Width;
            //point.Y= butCus.Location.Y- butCus.Height;
            //dockPanel1.FloatLocation  = point;
            //dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            //dockPanel1.Height = this.Height + butCus.Location.Y;


        }
        /// <summary>
        /// 绑定体检人信息
        /// </summary>
        /// <param name="CustomerBM">体检号</param>
        public void LoadCurrentCustomerReg(string CustomerBM,Guid?  regId)
        {
           
            AutoLoading(() =>
            {
                cusGroupIdList.Clear();
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = CustomerBM;
                if (regId.HasValue)
                {
                    chargeBM.Id = regId.Value;
                }
                //基本信息
                var cusGroup = inspectionTotalAppService.GetCustomerRegGroupSum(chargeBM);
                var cus = inspectionTotalAppService.GetCustomerReg(chargeBM);
                _currentInputSys = cus;
                if (cus == null)
                {
                    ShowMessageBoxWarning("没有该体检人信息！");
                    return;
                }
                if (cus.PhysicalType.HasValue)
                {
                    var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
                    labClientType.Text = checktype.FirstOrDefault(p => p.Value == cus.PhysicalType)?.Text;
                }
                memoEditRegRemark.Text = cus.Remarks;

                labdepart.Text = cus.Customer.Department;
                //档案号
                labelDangAnHao.Text = cus.Customer.ArchivesNum;

                //体检号
                labelTiJianHao.Text = cus.CustomerBM;

                //姓名
                textEditXingMing.Text = cus.Customer.Name;
                //性别
                if (cus.Customer.Sex != null)
                    labXingBie.Text = SexHelper.CustomSexFormatter(_currentInputSys.Customer.Sex);

                //年龄
                labNianLing.Text = _currentInputSys.RegAge?.ToString();

                //单位
                if (_currentInputSys.ClientInfo != null && _currentInputSys.ClientInfo != null)
                    textEditDanWei.Text = _currentInputSys.ClientInfo.ClientName;
                else
                    textEditDanWei.Text = "";

                //婚否
                if (_currentInputSys.MarriageStatus != null)
                    labHunFou.Text = MarrySateHelper.CustomMarrySateFormatter(_currentInputSys.MarriageStatus);
                if (_currentInputSys.PersonnelCategoryId.HasValue)
                {
                    //if (personnelCategoryViewDtos != null && personnelCategoryViewDtos.Count > 0)
                    //{
                    //    var cusType = personnelCategoryViewDtos.FirstOrDefault(
                    //        p => p.Id == _currentInputSys.PersonnelCategoryId)?.Name;
                    //    labelCategory.Text = cusType;
                }
                if (_currentInputSys.Customer.CusPhotoBmId.HasValue && _currentInputSys.Customer.CusPhotoBmId != Guid.Empty)
                {
                    var url = _pictureController.GetUrl(_currentInputSys.Customer.CusPhotoBmId.Value);
                    pictureZhaoPian.LoadAsync(url.Thumbnail);                   

                }

                labelCheckstate.Text = CheckSateHelper.PhysicalEStateFormatter(_currentInputSys.CheckSate);
                labSumsate.Text = SummSateHelper.SummSateFormatter(_currentInputSys.SummSate);
                labDengJiShiJian.Text = _currentInputSys.LoginDate?.ToString();
            memoEditRemark.Text = _currentInputSys.Customer.Remarks?.ToString();
                gridControlGroup.DataSource = cusGroup;
                 


            //if (splitContainerControl1.Collapsed) BinDingSurplusPatientGrid();
            }, Variables.LoadingForCloud);
       }

        private void butON_Click(object sender, EventArgs e)
        {
            try
            {
                BeforSaveSumDto sumDto = new BeforSaveSumDto();

               var crouplis=   gridControlGroup.DataSource as List<CustomerGroupSumDto>;
                // sumDto.cusGroup = crouplis.Where(p=>cusGroupIdList.Contains(p.Id)).ToList();
                sumDto.cusGroup = crouplis;
                sumDto.Id = _currentInputSys.Id;
                sumDto.SummSate = 5;
                inspectionTotalAppService.SavePerSum(sumDto);
                LoadCurrentCustomerReg("", _currentInputSys.Id);
                MessageBox.Show("设置成功！");
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                BeforSaveSumDto sumDto = new BeforSaveSumDto();
                sumDto.cusGroup = gridControlGroup.DataSource as List<CustomerGroupSumDto>;
                sumDto.Id = _currentInputSys.Id;
                sumDto.SummSate = 2;
                inspectionTotalAppService.SavePerSum(sumDto);
                LoadCurrentCustomerReg("", _currentInputSys.Id);
                MessageBox.Show("发送成功！");
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void searchHuanZheXinXi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(searchHuanZheXinXi.Text))
            {
                var txt = searchHuanZheXinXi.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt))
                {

                    LoadCurrentCustomerReg(txt,null);

                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                BeforSaveSumDto sumDto = new BeforSaveSumDto();
                sumDto.cusGroup = gridControlGroup.DataSource as List<CustomerGroupSumDto>;
                sumDto.Id = _currentInputSys.Id;
                sumDto.SummSate = 1;
                inspectionTotalAppService.SavePerSum(sumDto);
                LoadCurrentCustomerReg("", _currentInputSys.Id);
                MessageBox.Show("撤回成功！");
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }

        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            AutoLoading(() =>
            {
                //var input = new PageInputDto<TjlCustomerQuery>
                var input = new TjlCustomerQuery()
                {
                    Name = textName.Text.Trim()
                };
              
                

                if (textName.Text.Trim() == string.Empty)
                {
                    if (dateLoginStart.EditValue != null)
                        input.BeginDate = dateLoginStart.DateTime;
                    if (dateLoginEnd.EditValue != null)
                        input.EndDate = dateLoginEnd.DateTime.AddDays(1);
                    if (input.BeginDate > input.EndDate)
                    {
                        ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                        return;
                    }
                    if (comSumSate.EditValue != null && comSumSate.Text != "全部")
                    {
                        input.SumState = (int)comSumSate.EditValue;
                    }

                    if (comState.EditValue != null && comState.Text != "全部")
                    {
                        input.CheckState = (int)comState.EditValue;
                    }
                }

                if (searchLookUpClientReg.EditValue != null && searchLookUpClientReg.EditValue != "")
                {
                    input.ClientrRegID = (Guid)searchLookUpClientReg.EditValue;
                }
                var output = inspectionTotalAppService.GetCustomerRegList(input).Where(o => o.RegisterState == (int)RegisterState.Yes).ToList();
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);
                var zjcoutn = output.Count(o => o.SummSate == (int)SummSate.HasInitialReview);
                var SHcoutn = output.Count(o => o.SummSate == (int)SummSate.Audited);
                var tjzWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Process && o.SummSate == (int)SummSate.NotAlwaysCheck);
                var twcWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Complete && o.SummSate == (int)SummSate.NotAlwaysCheck);
                labelControl1.Text = "总人数：" + sum + "已审核:" + SHcoutn + "已总检：" + zjcoutn + "体检完成未总检查：" + twcWSh + "体检中未总检：" + tjzWSh;
                gridControlDangTianHuanZhe.DataSource = output;
                gridViewDangTianHuanZhe.BestFitColumns();

               

            });
        }
        public void LoadData1()
        {
            AutoLoading(() =>
            {
                gridControlDangTianHuanZhe.DataSource = null;

                // gridView1.FocusedRowHandle = -1;
                var input = new InSearchCusDto();
                if (!string.IsNullOrEmpty(textName.Text.Trim()))
                {
                    input.CusNameBM = textName.Text.Trim();
                }

                if (textName.Text.Trim() == string.Empty)
                {
                    if (dateLoginStart.EditValue != null)
                        input.LoginStar = dateLoginStart.DateTime;
                    if (dateLoginEnd.EditValue != null)
                        input.LoginEnd = dateLoginEnd.DateTime.AddDays(1);
                    if (input.LoginStar > input.LoginEnd)
                    {
                        ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                        return;
                    }
                    if (comSumSate.EditValue != null && comSumSate.Text != "全部")
                    {
                        input.SumSate = (int)comSumSate.EditValue;
                    }

                    if (comState.EditValue != null && comState.Text != "全部")
                    {
                        input.Sate = (int)comState.EditValue;
                    }
                }

                if (searchLookUpClientReg.EditValue != null && searchLookUpClientReg.EditValue != "")
                {
                    input.ClientRegId = (Guid)searchLookUpClientReg.EditValue;
                }
               
                var output = inspectionTotalAppService.GetOutCus(input).ToList();
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);
                // labelControl1.Text = "总人数：" + sum + "未总检:" + not + "已总检：" + (sum - not);
                gridControlDangTianHuanZhe.DataSource = output;


                //if (output.Count > 0)
                //{
                //    gridView1.FocusedRowHandle = 0;
                //}

            });
        }

        private void gridViewDangTianHuanZhe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (gridViewDangTianHuanZhe.IsDataRow(e.FocusedRowHandle))
            //{
            //    if (gridViewDangTianHuanZhe.GetFocusedRowCellValue(colDangTianId) is Guid id)
            //    {
            //        LoadCurrentCustomerReg("", id);
            //    }
            //}
        }

        private void gridViewDangTianHuanZhe_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewDangTianHuanZhe.IsDataRow(gridViewDangTianHuanZhe.FocusedRowHandle))
            {
                if (gridViewDangTianHuanZhe.GetFocusedRowCellValue(colDangTianId) is Guid id)
                {
                    LoadCurrentCustomerReg("", id);
                }
            }
        }

        private void labelDangAnHao_Click(object sender, EventArgs e)
        {
            var labelControl = (LabelControl)sender;
            if (string.IsNullOrWhiteSpace(labelDangAnHao.Text))
            {
                return;
            }
            try
            {
                Clipboard.SetText(labelDangAnHao.Text);
            }
            catch (ExternalException)
            {

            }
            alertInfo.Show(this, "复制提示", $"“{labelDangAnHao.Text}”已复制到剪贴板！");
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            var  cusGroupId = gridViewGroup.GetFocusedRowCellValue(concusGroupId.FieldName);
            if (cusGroupId != null)
            {

                cusGroupIdList.Add((Guid)cusGroupId);
            }
        }

        private void comState_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comState.EditValue = null;
            }
        }

        private void comSumSate_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comSumSate.EditValue = null;
            }
        }

        private void butCus_Click(object sender, EventArgs e)
        {

            //if (dockPanel1.Visibility == DockVisibility.Hidden)
            //{
            //    dockPanel1.Visibility = DockVisibility.Visible;
            //    Point point = new Point();
            //    point.X = butCus.Location.X + butCus.Width;
            //    point.Y = butCus.Location.Y;
            //    dockPanel1.FloatLocation = point;
            //    dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            //    dockPanel1.Height = this.Height + butCus.Location.Y;
            //    //dockPanel1.re
            //}
            //if (dockPanel1.Visibility == DockVisibility.Visible)
            //{
            //    dockPanel1.Visibility = DockVisibility.Hidden;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dockPanel1.Tag != null && dockPanel1.Tag.Equals(true))
            {
                dockPanel1.HideSliding();
            }
            else
            {
                dockPanel1.ShowSliding();
            }
        }

        private void dockPanel1_Collapsed(object sender, DockPanelEventArgs e)
        {
            dockPanel1.Tag = false;
        }

        private void dockPanel1_Expanded(object sender, DockPanelEventArgs e)
        {
            dockPanel1.Tag = true;
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //水平进度条


            AutoLoading(() =>
            {

                if (_currentInputSys != null)
                {

                    string errarm = "";
                    //体检人信息

                    TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                    tdbInterfaceWhere.inactivenum = _currentInputSys.CustomerBM;
                    InterfaceBack Back = new InterfaceBack();

                    Back = _doctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                    if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                    {
                        errarm = Back.StrBui.ToString();

                    }

                    if (errarm != "")
                    {
                        XtraMessageBox.Show(_currentInputSys.Customer.Name + "-" + _currentInputSys.CustomerBM + ",失败上传, 异常详情：" + errarm);

                    }
                    else
                    {
                        XtraMessageBox.Show(_currentInputSys.Customer.Name + "-" + _currentInputSys.CustomerBM + ",成功！");
                    }
                }
                else
                {
                    XtraMessageBox.Show("请选择数据！");

                }
            });
        }
    }
}
