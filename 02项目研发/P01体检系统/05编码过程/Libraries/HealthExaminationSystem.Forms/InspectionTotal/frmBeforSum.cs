using Abp.Application.Services.Dto;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.CusReg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmBeforSum : UserBaseForm
    {
        private readonly IPrintPreviewAppService _printPreviewAppService;
        private List<CustomerRegisterItemPictureDto> _currentItemPictures;
        private List<CustomerRegisterItemDto> _CustomerRegisterItemDto;
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private IInspectionTotalAppService inspectionTotalAppService = new InspectionTotalAppService();
        private ICrossTableAppService crossTableAppService;
        private readonly IDoctorStationAppService _DoctorStationAppService = new DoctorStationAppService();
        private readonly ICommonAppService _commonAppService;
        private ICustomerAppService customerSvr;//体检预约
        private PictureArg _pictureArg;
        public System.Drawing.Point mouseDownPoint;//存储鼠标焦点的全局变量
        public bool isSelected = false;
        public frmBeforSum()
        {
            customerSvr = new CustomerAppService();
            crossTableAppService = new CrossTableAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
            _printPreviewAppService = new PrintPreviewAppService();
            InitializeComponent();
        }

        private void frmBeforSum_Load(object sender, EventArgs e)
        {
             
            //体检日期
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date;

            //套餐
            var result = DefinedCacheHelper.GetItemSuit().Where(o => o.Available == 1).ToList();
            searchLookUpEditSuit.Properties.DataSource = result;
            repositoryItemLookUpEdit7.DataSource = ProjectIStateHelper.GetProjectIStateModels();

            gridViewCusGroup.Columns[gridColumnState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCusGroup.Columns[gridColumnState.FieldName].DisplayFormat.Format =
                new CustomFormatter(ProjectIStateHelper.ProjectIStateFormatter
                );

            gridViewPic.Columns[gridColumnPICState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewPic.Columns[gridColumnPICState.FieldName].DisplayFormat.Format =
                new CustomFormatter(ProjectIStateHelper.ProjectIStateFormatter
                );

            // 加载性别数据
            lookUpEditSex.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lookUpEditSex.EditValue = (int)Sex.GenderNotSpecified;


            // 加载单位数据
            searchLookUpEditCompany.Properties.DataSource = _printPreviewAppService.GetClientInfos();

            // 加载体检类别数据
            // lookUpEditExaminationCategories.Properties.DataSource = ExaminationCategoryHelper.GetExaminationCategories();
            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            //体检类别
            lookUpEditCustomerType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();

            gridViewCustomerReg.Columns[gridColumnCustomerRegSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCustomerReg.Columns[gridColumnCustomerRegSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
            repositoryItemLookUpEditSummSate.DataSource = SummSateHelper.GetSelectList();
            repositoryItemLookUpEditPhysicalType.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();

            repositoryItemLookUpEditCheckSate.DataSource = PhysicalEStateHelper.YYGetList();

            repositoryItemLookUpEdit3.DataSource = DefinedCacheHelper.GetDepartments();
            repositoryItemLookUpEdit4.DataSource = DefinedCacheHelper.GetItemGroups();
            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetComboUsers();


            repositoryItemLookUpEdit5.DataSource = DefinedCacheHelper.GetItemInfos();


        }

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
 
        }

        private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var name = textEditName.Text.Trim();
                var customerBm = textEditCustomerBm.Text.Trim();
                var idCardNo = textEditIdCardNo.Text.Trim();
                //var FPName = txtFp.Text.Trim();
                var sex = lookUpEditSex.EditValue as int?;
                var clientRegId = searchLookUpEditClientReg.EditValue as Guid?;
                var clientId = searchLookUpEditCompany.EditValue as Guid?;
                //var printSate = lookUpEditPrintSate.EditValue as int?;

                var examinationCategory = lookUpEditExaminationCategories.EditValue as int?;
                if (examinationCategory.HasValue && examinationCategory == -1)
                {
                    examinationCategory = null;
                }
                var examinationStatus = lookUpEditExaminationStatus.EditValue as int?;
                var sumstatus = lookUpEditSumStatus.EditValue as int?;
                var input = new SearchCustomerRegForPrintPreviewDto
                {
                    Name = name,
                    CustomerBM = customerBm,
                    IdCardNo = idCardNo,
                    Sex = sex,
                    ClientRegId = clientRegId,                   
                    CheckSate = examinationStatus,
                    PhysicalType = examinationCategory,
                    SummSate = sumstatus,
                    ClientId = clientId                   


                };
                if (!string.IsNullOrEmpty(searchLookUpEditTeam.EditValue?.ToString()))
                {
                    input.ClientTeamId= searchLookUpEditTeam.EditValue as Guid ?;

                }
                if (!string.IsNullOrEmpty(searchLookUpEditSuit.EditValue?.ToString()))
                {
                    input.SuitId = searchLookUpEditSuit.EditValue as Guid?;

                }
                if (!string.IsNullOrEmpty(comboBoxEditYQ.EditValue?.ToString()))
                {
                    if (comboBoxEditYQ.Text == "是")
                    {
                        input.isYQ = 1;

                    }
                    else if (comboBoxEditYQ.Text == "否")
                    {
                        input.isYQ = 2;
                    }
                }
                input.StartDate = dateEditStart.DateTime;
                input.EndtDate = dateEditEnd.DateTime;
                input.DateType = 1;
                if (comTimeType.Text.Contains("审核"))
                {
                    input.DateType = 2;

                }
                if (comTimeType.Text.Contains("体检"))
                {
                    input.DateType = 3;

                } 
                
                if (!string.IsNullOrEmpty(lookUpEditCustomerType.EditValue?.ToString()))
                {
                    input.CustomerType = (int)lookUpEditCustomerType.EditValue;
                }
                
                var data = _printPreviewAppService.GetCustomerRegs(input);
                gridControlCustomerReg.DataSource = data;
            });
        }

        private void gridControlCustomerReg_Click(object sender, EventArgs e)
        {

        }

        private void gridViewCustomerReg_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        
        }

        private void gridView5_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            #region 隐藏
            //if (e.Column == gridColumn13)
            //{
            //    if (e.CellValue is Guid id)
            //    {
            //        if (_currentItemPictures != null)
            //        {
            //            //var button = new RepositoryItemButtonEdit();
            //           // repositoryItemButtonEdit5.TextEditStyle = TextEditStyles.HideTextEditor;
            //            if (_currentItemPictures.Any(r => r.ItemBMID == id))
            //            {
            //                var item = new RepositoryItemButtonEdit();
            //                item.Buttons.Clear();
            //                item.Buttons.Add(new EditorButton(ButtonPredefines.Search));
            //                item.TextEditStyle = TextEditStyles.HideTextEditor;
            //                item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
            //                item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;

            //                var pictureArg = new PictureArg();
            //                pictureArg.CurrentItemId = id;
            //                pictureArg.Pictures = _currentItemPictures;
            //                item.Buttons[0].Tag = pictureArg;
            //                e.RepositoryItem = item;
            //            }

            //        }
            //    }
            //} 
            #endregion
        }
        private void GridButtonChaKanTuPian_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            // 显示图片
            if (e.Button.Tag is PictureArg arg)
            {
                using (var frm = new InspectionTotalPicture(arg))
                {
                    frm.ShowDialog(this);
                }
            }

        }
        /// <summary>
        /// 预总检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YZJ_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            simpleButton4.PerformClick();

        }
        

        private void gridView3_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnPIC)
            {
                if (e.CellValue is Guid id)
                {
                    if (_currentItemPictures != null)
                    {
                        //var button = new RepositoryItemButtonEdit();
                        // repositoryItemButtonEdit5.TextEditStyle = TextEditStyles.HideTextEditor;
                        var ItemIDlist = _CustomerRegisterItemDto.Where(p => p.CustomerItemGroupBMid == id).Select(p => p.Id).ToList();
                        foreach (var ItemID in ItemIDlist)
                        {
                            if (_currentItemPictures.Any(r => r.ItemBMID == ItemID))
                            {
                                var item = new RepositoryItemButtonEdit();
                                item.Buttons.Clear();
                                item.Buttons.Add(new EditorButton(ButtonPredefines.Search));
                                item.TextEditStyle = TextEditStyles.HideTextEditor;
                                item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
                                item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;

                                var pictureArg = new PictureArg();
                                pictureArg.CurrentItemId = ItemID;
                                pictureArg.Pictures = _currentItemPictures.Where(r => r.ItemBMID == ItemID).ToList();
                                item.Buttons[0].Tag = pictureArg;
                                e.RepositoryItem = item;
                            }
                        }

                    }
                }
            }
        }
        private void gridViewCustomerReg_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnZJ)
            {
                if (e.CellValue is int state)
                {
                    var sumstate = gridViewCustomerReg.GetRowCellValue(e.RowHandle, gridColumnCustomerRegSummSate)?.ToString();

                    if (state == 3 && sumstate == "1")
                    {
                        var item = new RepositoryItemButtonEdit();
                        item.Buttons.Clear();
                        EditorButton but = new EditorButton(ButtonPredefines.Glyph);
                        but.Caption = "可总检";
                        but.Appearance.ForeColor = Color.Blue;
                        item.Buttons.Add(but);
                        item.TextEditStyle = TextEditStyles.HideTextEditor;
                        //item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
                        //item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;
                        item.ButtonClick -= YZJ_ButtonClick;
                        item.ButtonClick += YZJ_ButtonClick;
                        e.RepositoryItem = item;
                    }

                }
            }
             

        }
        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto = gridControlCusGroup.GetFocusedRowDto<CusGroupShowDto>();
            if (dto != null)
            {
             
                var CusItemlist = _CustomerRegisterItemDto.Where(p => p.CustomerItemGroupBMid == dto.Id).ToList();
                gridControlCusItem.DataSource = CusItemlist;
            }
        }

        private void gridView3_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto = gridControlpic.GetFocusedRowDto<CusGroupShowDto>();
            if (dto != null)
            {
                this.pictureEditTuPianZhanShi.Left = 0;
                this.pictureEditTuPianZhanShi.Top =0;
                pictureEditTuPianZhanShi.Width = panel_Picture.Width - 6;
                pictureEditTuPianZhanShi.Height = panel_Picture.Height - 6;

                _pictureArg = new PictureArg();
                CustomerPicSys = 0;
                if (_currentItemPictures != null)
                {
                    checkEdit1.Checked = false;

                       var ItemIDlist = _CustomerRegisterItemDto.Where(p => p.CustomerItemGroupBMid == dto.Id).Select(p => p.Id).ToList();
                    foreach (var ItemID in ItemIDlist)
                    {
                        if (_currentItemPictures.Any(r => r.ItemBMID == ItemID))
                        {

                            _pictureArg.CurrentItemId = ItemID;
                            _pictureArg.Pictures = _currentItemPictures.Where(r => r.ItemBMID == ItemID).ToList();                           
                        }
                    }

                }


                if (_pictureArg != null && _pictureArg.CurrentItemId != Guid.Empty)
                {

                    var itemPicture = _pictureArg?.Pictures.Where(r => r.ItemBMID == _pictureArg.CurrentItemId).ToList();
                    if (itemPicture.Count > 0 && itemPicture.FirstOrDefault()?.PictureBM != null)
                    {
                        var picture =
                            DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.FirstOrDefault().PictureBM.Value);
                        if (picture != null)
                        {
                            pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
                        }
                    }
                    labelControlZongShu.Text = itemPicture.Count.ToString();
                    if (itemPicture.Count > 0)
                    {
                        labelControlDangQian.Text = "1";
                    }
                    else
                    { labelControlDangQian.Text = "0"; }
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();
                if (dto != null)
                {
                    if (dto.SummSate == (int)SummSate.HasInitialReview ||
                        dto.SummSate == (int)SummSate.Audited)
                    {
                        MessageBox.Show("已审核不能提交！");
                        return;
                    }
                    if (dto.SummSate == (int)SummSate.HasBeenEvaluated)
                    {
                        MessageBox.Show("已提交无需重复提交！");
                        return;
                    }
                    BeforSaveSumDto sumDto = new BeforSaveSumDto();

                    sumDto.Id = dto.Id;
                    sumDto.SummSate = (int)SummSate.HasBeenEvaluated;
                    inspectionTotalAppService.SavePerSum(sumDto);

                    MessageBox.Show("发送成功！");
                    dto.SummSate= (int)SummSate.HasBeenEvaluated;                    
                    gridControlCustomerReg.RefreshDataSource();
                    textEditCustomerBm.Focus();
                    textEditCustomerBm.SelectAll();
                }
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();
                if (dto != null)
                {
                    if (dto.SummSate == (int)SummSate.HasInitialReview ||
                       dto.SummSate == (int)SummSate.Audited)
                    {
                        MessageBox.Show("已审核不能取消提交！");
                        return;
                    }
                    if (dto.SummSate == (int)SummSate.NotAlwaysCheck)
                    {
                        MessageBox.Show("未提交无需取消！");
                        return;
                    }
                    BeforSaveSumDto sumDto = new BeforSaveSumDto();

                    sumDto.Id = dto.Id;
                    sumDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    inspectionTotalAppService.SavePerSum(sumDto);

                    MessageBox.Show("发送成功！");
                    dto.SummSate = (int)SummSate.NotAlwaysCheck;
                    gridControlCustomerReg.RefreshDataSource();
                }
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var dto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();
            if (dto != null)
            {
              
                var itemList = crossTableAppService.QueryCustomerItems(new QuerCustomerItemsDto { CustomerRegId = dto.Id });
                //var checkList = treeListItemGroup.GetAllCheckedNodes();
                var checkList = gridViewCusGroup.GetSelectedRows();
                if (checkList.Count() == 0)
                {
                    MessageBox.Show("请选择组合");
                    return;
                }
                EditItemGroupStateInput inputList = new EditItemGroupStateInput();
                inputList.Ids = new List<Guid>();
                var Names = new List<string>();
                string InfoStr = "";
                foreach (var item in checkList)
                {
                    
                    var group = gridViewCusGroup.GetRow(item) as CusGroupShowDto;
                 

                    //var group = (CustomerItemGroupDto)item.Tag;
                    var items = itemList.Where(i => i.CustomerItemGroupBM?.Id == group.Id).ToList();
                    if (items.Any(c => c.ProcessState == (int)ProjectIState.Complete))
                    {
                        InfoStr += group.ItemGroupName + "、";
                    }
                    else
                    {
                        if (group.CheckState != (int)ProjectIState.GiveUp)
                        {
                            inputList.CheckState = (int)ProjectIState.GiveUp;
                            inputList.Ids.Add(group.Id);
                            Names.Add(group.ItemGroupName);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(InfoStr))
                {
                    InfoStr = InfoStr.Trim('、');
                    InfoStr += "，项目组中包含已检查项目，不能进行放弃操作！";
                    ShowMessageBoxInformation(InfoStr);
                    return;
                }
                FrmGiveUpMessage fromGiveUpMessage = new FrmGiveUpMessage((int)ProjectIState.GiveUp);
                fromGiveUpMessage.ShowDialog();
                if (fromGiveUpMessage.DialogResult == DialogResult.Cancel)
                    return;

                inputList.Record = new CusGiveUpDto { remart = fromGiveUpMessage.Remarks };

                try
                {
                    var resultList = crossTableAppService.EditItemGroupState(inputList);
                    //处理体检状态
                    EntityDto<Guid> input = new EntityDto<Guid>();
                    input.Id = dto.Id;
                    _DoctorStationAppService.UpCheckState(input);
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = dto.CustomerBM;
                    createOpLogDto.LogName = dto.Customer.Name;

                    createOpLogDto.LogText = "预总检放弃项目：" + string.Join(",", Names);

                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);

                    //组合

                    var cusGroup = _inspectionTotalService.getCusGroup(input);
                    gridControlCusGroup.DataSource = cusGroup;
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var dto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();
            if (dto != null)
            {

                var itemList = crossTableAppService.QueryCustomerItems(new QuerCustomerItemsDto { CustomerRegId = dto.Id });
                //var checkList = treeListItemGroup.GetAllCheckedNodes();
                var checkList = gridViewCusGroup.GetSelectedRows();
                if (checkList.Count() == 0)
                {
                    MessageBox.Show("请选择组合");
                    return;
                }
                EditItemGroupStateInput inputList = new EditItemGroupStateInput();
                inputList.Ids = new List<Guid>();
                var Names = new List<string>();
                string InfoStr = "";
                foreach (var item in checkList)
                {
                   
                    
                    var group = gridViewCusGroup.GetRow(item) as CusGroupShowDto;

                    var items = itemList.Where(i => i.CustomerItemGroupBM.Id == group.Id);
                    if (items.Any(c => c.ProcessState == (int)ProjectIState.Complete))
                    {
                        InfoStr += group.ItemGroupName + "、";
                    }
                    else
                    {
                        if (group.CheckState != (int)ProjectIState.Stay)
                        {
                            inputList.CheckState = (int)ProjectIState.Stay;
                            inputList.Ids.Add(group.Id);
                            Names.Add(group.ItemGroupName);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(InfoStr))
                {
                    InfoStr = InfoStr.Trim('、');
                    InfoStr += "，项目组中包含已检查项目，不能进行待检操作！";
                    ShowMessageBoxInformation(InfoStr);
                    return;
                }
                FrmGiveUpMessage fromGiveUpMessage = new FrmGiveUpMessage((int)ProjectIState.Stay);
                fromGiveUpMessage.ShowDialog();
                if (fromGiveUpMessage.DialogResult == DialogResult.Cancel)
                    return;

                inputList.Record = new CusGiveUpDto { remart = fromGiveUpMessage.Remarks, stayDate = fromGiveUpMessage.NextDate };

                try
                {
                    var resultList = crossTableAppService.EditItemGroupState(inputList);
                    //处理体检状态
                    EntityDto<Guid> input = new EntityDto<Guid>();
                    input.Id = dto.Id;
                    _DoctorStationAppService.UpCheckState(input);
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = dto.CustomerBM;
                    createOpLogDto.LogName = dto.Customer.Name;

                    createOpLogDto.LogText = "交表界面待查项目：" + string.Join(",", Names);

                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    //组合

                    var cusGroup = _inspectionTotalService.getCusGroup(input);
                    gridControlCusGroup.DataSource = cusGroup;

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
        }

        private void searchLookUpEditCompany_EditValueChanged(object sender, EventArgs e)
        {
            // 加载单位预约信息
            if (searchLookUpEditCompany.EditValue == null || searchLookUpEditCompany.EditValue.Equals(string.Empty))
            {
                searchLookUpEditClientReg.EditValue = null;
                searchLookUpEditClientReg.Properties.DataSource = null;
            }
            else
            {
                var id = (Guid)searchLookUpEditCompany.EditValue;
                var regs = _printPreviewAppService.GetClientRegs(new EntityDto<Guid> { Id = id });
                searchLookUpEditClientReg.Properties.DataSource = regs;
                if (regs.Count != 0)
                {
                    searchLookUpEditClientReg.EditValue = regs.First().Id;
                }
            }
        }

        private void searchLookUpEditClientReg_EditValueChanged(object sender, EventArgs e)
        {
            //var control = sender as SearchLookUpEdit;
            if (searchLookUpEditClientReg.EditValue == null)
            {
                searchLookUpEditTeam.Properties.DataSource = null;             
            }
            else
            {
                try
                {
                    //显示单位预约信息
                    var clietreg = searchLookUpEditClientReg.GetSelectedDataRow() as ClientRegForPrintPreviewDto;
                    
                    var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(searchLookUpEditClientReg.EditValue?.ToString()) });




                    searchLookUpEditTeam.Properties.DataSource = list;
                
               
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

            }
        }
        int CustomerPicSys = 0;
        private void pictureEdit3_Click(object sender, EventArgs e)
        {
            if (_pictureArg != null && _pictureArg.CurrentItemId != Guid.Empty)
            {
                //var index = _pictureArg.Pictures.FindIndex(r => r.ItemBMID == _pictureArg.CurrentItemId);
                if (CustomerPicSys == _pictureArg.Pictures.Count - 1)
                    CustomerPicSys = 0;
                else
                    CustomerPicSys = CustomerPicSys + 1;
                //if (index == _pictureArg.Pictures.Count - 1)
                //{
                //    index = 0;
                //}

                var itemPicture = _pictureArg?.Pictures[CustomerPicSys];
                if (itemPicture.ItemBMID != null)
                {
                    _pictureArg.CurrentItemId = itemPicture.ItemBMID.Value;
                }
                if (itemPicture.PictureBM != null)
                {
                    AutoLoading(() =>
                    {
                        var picture =
                            DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.PictureBM.Value);
                        if (picture != null)
                        {
                            pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
                        }
                    });
                }
                labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
            }
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            if (_pictureArg != null && _pictureArg.CurrentItemId != Guid.Empty)
            {
                //var index = _pictureArg.Pictures.FindIndex(r => r.ItemBMID == _pictureArg.CurrentItemId);
                if (CustomerPicSys == 0)
                    CustomerPicSys = _pictureArg.Pictures.Count - 1;
                else
                    CustomerPicSys = CustomerPicSys - 1;
                //if (index == 0)
                //{
                //    index = _pictureArg.Pictures.Count - 1;
                //}

                var itemPicture = _pictureArg?.Pictures[CustomerPicSys];
                if (itemPicture.ItemBMID != null)
                {
                    _pictureArg.CurrentItemId = itemPicture.ItemBMID.Value;
                }

                if (itemPicture.PictureBM != null)
                {
                    AutoLoading(() =>
                    {
                        var picture =
                            DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.PictureBM.Value);
                        if (picture != null)
                        {
                            pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
                        }
                    });
                }
                labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
            }
        }

        private void gridViewPic_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewPic_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumntwpic)
            {
                if (e.CellValue is Guid id)
                {
                    if (_currentItemPictures != null)
                    {
                        //var button = new RepositoryItemButtonEdit();
                        // repositoryItemButtonEdit5.TextEditStyle = TextEditStyles.HideTextEditor;
                        var ItemIDlist = _CustomerRegisterItemDto.Where(p => p.CustomerItemGroupBMid == id).Select(p => p.Id).ToList();
                        foreach (var ItemID in ItemIDlist)
                        {
                            if (_currentItemPictures.Any(r => r.ItemBMID == ItemID))
                            {
                                var item = new RepositoryItemButtonEdit();
                                item.Buttons.Clear();
                                item.Buttons.Add(new EditorButton(ButtonPredefines.Search));
                                item.TextEditStyle = TextEditStyles.HideTextEditor;
                                item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
                                item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;

                                var pictureArg = new PictureArg();
                                pictureArg.CurrentItemId = ItemID;
                                pictureArg.Pictures = _currentItemPictures.Where(r => r.ItemBMID == ItemID).ToList();
                                item.Buttons[0].Tag = pictureArg;
                                e.RepositoryItem = item;
                            }
                        }

                    }
                }
            }
        }

        private void gridViewCustomerReg_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ShowCusItemResult();
        }
        private void ShowCusItemResult()
        {
            var dto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();
            if (dto != null)
            {
                var input = new EntityDto<Guid>(dto.Id);
                //检查结果
                var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                        .GetCustomerRegisterItem(input);
                //屏蔽职业检数据
                //var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                gridControl3.DataSource = customerRegisterItemTask.Result;
                _CustomerRegisterItemDto = customerRegisterItemTask.Result;
                //图片
                var itemPictureTask =
                        DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemPictureAppService
                            .GetItemPictureByCustomerRegisterId(input);
                _currentItemPictures = itemPictureTask.Result;

                //组合

                var cusGroup = _inspectionTotalService.getCusGroup(input);
                gridControlCusGroup.DataSource = cusGroup;

                //查找图片组合
                var picGroupID = _currentItemPictures.Select(p => p.CustomerItemGroupID).Distinct().ToList();
                var picCusGroup = cusGroup.Where(p => picGroupID.Contains(p.Id)).ToList();
                gridControlpic.DataSource = picCusGroup;
            }
        }

        #region 图片鼠标事件     

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double scale = 1;
            if (pictureEditTuPianZhanShi.Height > 0)
            {
                scale = (double)pictureEditTuPianZhanShi.Width / (double)pictureEditTuPianZhanShi.Height;
            }
            pictureEditTuPianZhanShi.Width += (int)(e.Delta * scale);
            pictureEditTuPianZhanShi.Height += e.Delta;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Focus();
            pictureEditTuPianZhanShi.Cursor = Cursors.SizeAll;
        }

        //在MouseDown处获知鼠标是否按下，并记录下此时的鼠标坐标值；
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型  
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        //在MouseUp处获知鼠标是否松开，终止拖动操作；
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
             isSelected = false;

        }

        private bool IsMouseInPanel()
        {
            var pointX = PointToClient(Cursor.Position).X;
            var pointY = PointToClient(Cursor.Position).Y;
            if (this.panel_Picture.Left < pointX
                    && pointX < this.panel_Picture.Left
                    + this.panel_Picture.Width && this.panel_Picture.Top
                    < pointY && pointY
                    < this.panel_Picture.Top + this.panel_Picture.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //图片平移,在MouseMove处添加拖动函数操作
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //&& IsMouseInPanel()
            if (isSelected )//确定已经激发MouseDown事件，和鼠标在picturebox的范围内
            {
                this.pictureEditTuPianZhanShi.Left = this.pictureEditTuPianZhanShi.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureEditTuPianZhanShi.Top = this.pictureEditTuPianZhanShi.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        #endregion

        private void panel_Picture_SizeChanged(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Width = panel_Picture.Width - 6;
            pictureEditTuPianZhanShi.Height = panel_Picture.Height - 6;
        }

        private void repositoryItemButtonEdit8_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            HuoQuGetData();
        }
    
        public void HuoQuGetData()
        {
            var _currentInputSys = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();

            var dto = gridControlCusGroup.GetFocusedRowDto<CusGroupShowDto>();
            if (_currentInputSys != null )
            {
                //获取当前选择科室id              
                TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                tdbInterfaceWhere.inactivenum = _currentInputSys.CustomerBM;

                if (dto == null)
                {
                    
                   ShowMessageSucceed("请选组合!");
                    return;
                }
                else
                {
                   
                    tdbInterfaceWhere.GroupID = dto.ItemGroupBM_Id;
                }
                InterfaceBack Back = new InterfaceBack();
                AutoLoading(() =>
                {
                    Back = _DoctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                }, "正在读取接口数据并保存!");
                //if (Back.IdList != null && Back.IdList.Count > 0)
                //{
                if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                {

                  
                    XtraMessageBox.Show(Back.StrBui.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //刷新
                #region MyRegion

                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = _currentInputSys.Id;
                var cusGroup = _inspectionTotalService.getCusGroup(input);
                gridControlCusGroup.DataSource = cusGroup;

                //检查结果
                var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                        .GetCustomerRegisterItem(input);
                //屏蔽职业检数据
                //var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                gridControl3.DataSource = customerRegisterItemTask.Result;
                _CustomerRegisterItemDto = customerRegisterItemTask.Result;
                //图片
                var itemPictureTask =
                        DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemPictureAppService
                            .GetItemPictureByCustomerRegisterId(input);
                _currentItemPictures = itemPictureTask.Result;

                //组合 

                //查找图片组合
                var picGroupID = _currentItemPictures.Select(p => p.CustomerItemGroupID).Distinct().ToList();
                var picCusGroup = cusGroup.Where(p => picGroupID.Contains(p.Id)).ToList();
                gridControlpic.DataSource = picCusGroup;
                #endregion
            }
          
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var _tjlCustomerRegDto = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();

            if (_tjlCustomerRegDto != null)
            {
                //if (Variables.ISReg == "0")
                //{
                //	XtraMessageBox.Show("试用版本，不能预览报告！");
                //	return;
                //}
                var printReport = new PrintReportNew();
                //printReport.StrReportTemplateName = "000";
                var cusNameInput = new CusNameInput();
                cusNameInput.Id = _tjlCustomerRegDto.Id;
                printReport.cusNameInput = cusNameInput;
                var MBNamels = DefinedCacheHelper
                  .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
                string[] mbls = MBNamels.Split('|');

                if (mbls.Length > 0)
                {
                    int iszyb = 0;
                    string mb = mbls[0];
                    //从业体检模板绑定
                    if (_tjlCustomerRegDto.PhysicalType.HasValue)
                    {
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        if (tjlb.Contains("从业") || tjlb.Contains("健康证"))
                        {
                            var jkzmb = mbls.Where(p => p.Contains("健康卡") || p.Contains("健康证")).ToList();
                            if (jkzmb.Count > 0)
                            {
                                mb = jkzmb[0];
                            }
                        }
                    }
                    printReport.Print(true, "", "", "0", 2);
                     
                    
                }

            }
        }

        private void textEditCustomerBm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textEditCustomerBm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditCustomerBm.Text))
            {
                SearchCustomerRegForPrintPreviewDto input = new SearchCustomerRegForPrintPreviewDto();
                input.CustomerBM = textEditCustomerBm.Text;
                var data = _printPreviewAppService.GetCustomerRegs(input);
                gridControlCustomerReg.DataSource = data;
                ShowCusItemResult();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            var _currentInputSys = gridControlCustomerReg.GetFocusedRowDto<CustomerRegForPrintPreviewDto>();

            if (_currentInputSys != null)
            {
                //获取当前选择科室id
                var DepartmentId = Guid.Empty;
                TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                tdbInterfaceWhere.inactivenum = _currentInputSys.CustomerBM;



                InterfaceBack Back = new InterfaceBack();
                AutoLoading(() =>
                {
                    Back = _DoctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                }, "正在读取接口数据并保存!");

                if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                {

                    alertControl.Show(this, "温馨提示", Back.StrBui.ToString());
                    //XtraMessageBox.Show(Back.StrBui.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //刷新
                #region MyRegion

                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = _currentInputSys.Id;
                var cusGroup = _inspectionTotalService.getCusGroup(input);
                gridControlCusGroup.DataSource = cusGroup;

                //检查结果
                var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                        .GetCustomerRegisterItem(input);
                //屏蔽职业检数据
                //var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                gridControl3.DataSource = customerRegisterItemTask.Result;
                _CustomerRegisterItemDto = customerRegisterItemTask.Result;
                //图片
                var itemPictureTask =
                        DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemPictureAppService
                            .GetItemPictureByCustomerRegisterId(input);
                _currentItemPictures = itemPictureTask.Result;

                //组合 

                //查找图片组合
                var picGroupID = _currentItemPictures.Select(p => p.CustomerItemGroupID).Distinct().ToList();
                var picCusGroup = cusGroup.Where(p => picGroupID.Contains(p.Id)).ToList();
                gridControlpic.DataSource = picCusGroup;
                #endregion



            }

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                if (_currentItemPictures != null)
                {
                    //实现照片按科室，组合序号排序

                    var picidls = _currentItemPictures.Select(p => p.ItemBMID).Distinct().ToList();
                    var ItemIDlist = _CustomerRegisterItemDto.Where(p=> picidls.Contains(p.Id)).ToList();

                    var itemPicture = _currentItemPictures.ToList();
                    _pictureArg.Pictures = new List<CustomerRegisterItemPictureDto>();
                    foreach (var cusgrpup in ItemIDlist)
                    {
                        var piclist = itemPicture.Where(p => p.ItemBMID == cusgrpup.Id).ToList();
                        _pictureArg.Pictures.AddRange(piclist);
                       
                    }
                    itemPicture = _pictureArg.Pictures.ToList();
                    if (itemPicture.Count > 0 && itemPicture.FirstOrDefault()?.PictureBM != null)
                    {
                        var picture =
                            DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(itemPicture.FirstOrDefault().PictureBM.Value);
                        if (picture != null)
                        {
                            pictureEditTuPianZhanShi.LoadAsync(picture.RelativePath);
                        }
                    }
                    labelControlZongShu.Text = itemPicture.Count.ToString();
                    if (itemPicture.Count > 0)
                    {
                        labelControlDangQian.Text = "1";
                    }
                    else
                    { labelControlDangQian.Text = "0"; }
                }
            }
        }
    }
}
