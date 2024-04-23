using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    /// <summary>
    /// 批量登记
    /// </summary>
    public partial class BatchCustomerReg : UserBaseForm
    {
        /// <summary>
        /// 团体预约服务
        /// </summary>
        private readonly IClientRegAppService _clientRegAppService;

        /// <summary>
        /// 个人预约服务
        /// </summary>
        private readonly ICustomerAppService _customerAppService; //体检预约服务

        public BatchCustomerReg()
        {
            InitializeComponent();

            _clientRegAppService = new ClientRegAppService();
            _customerAppService = new CustomerAppService();
        }

        private void BatchCustomerReg_Load(object sender, EventArgs e)
        {
            var clientRegs = _customerAppService.QuereyClientRegInfos(new FullClientRegDto { SDState = (int)SDState.Unlocked });
            searchLookUpEditClient.Properties.DataSource = clientRegs;
            //登记状态
            lUpDJState.Properties.DataSource = RegisterStateHelper.GetSelectList();
            lUpDJState.ItemIndex = 1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            AutoLoading(() =>
            {
                if (!string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
                {
                    try
                    {
                        var dto = new CustomerRegsInputDto
                        {
                            ClientRegId = Guid.Parse(searchLookUpEditClient.EditValue?.ToString()),
                            RegState = int.Parse(lUpDJState.EditValue?.ToString())

                        };
                        var customerReg = _clientRegAppService.GetCustomerReg(dto);
                        gridControlCustomList.DataSource = customerReg;
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                    }
                }
                else
                {
                    ShowMessageBoxWarning("请选择单位后查询。");
                }
            });
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            #region 锁定不能登记
            if (!string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
            {
               var  ClientRegId = Guid.Parse(searchLookUpEditClient.EditValue?.ToString());
                EntityDto<Guid> entity = new EntityDto<Guid>();
                entity.Id = ClientRegId;
                var outCode = _clientRegAppService.getSd(entity);
                if (outCode.code == "1")
                {
                    MessageBox.Show("该单位已锁定，不能登记！");
                    return;
                }

            }
            #endregion
            List<CustomerRegsViewDto> cusreglist = new List<CustomerRegsViewDto>();
            AutoLoading(() =>
            {
                try
                {

                    var ids = new List<Guid>();
                    var rows = gridViewCustomerList.GetSelectedRows();
                    //水平进度条
                    progressBarControl1.Properties.Minimum = 0;
                    progressBarControl1.Properties.Maximum = rows.Length;
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                    label1.Text = "共：" + rows.Length + "条数据";
                    System.Windows.Forms.Application.DoEvents();
                    int num = 1;
                   
                    foreach (var i in rows)
                    {
                        if (gridViewCustomerList.GetRow(i) is CustomerRegsViewDto row && row.RegisterState == (int)RegisterState.No)
                        {
                            ids.Add(row.Id);
                            cusreglist.Add(row);
                        }
                        if (ids.Count >= 100)
                        {
                            label2.Text = "第：0条";
                            label2.Refresh();
                            _customerAppService.BatchReg(new BatchRegInputDto { IsReg = true, RegIds = ids });
                            #region HIS接口
                            //var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                            //if (HISjk == "1")
                            //{
                            //    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;

                            //    foreach (var data in cusreglist)
                            //    {
                            //        if (HISName == "东软"  )
                            //        {

                                                           

                            //            ICustomerAppService _customerSvr = new CustomerAppService();
                            //            TJSQDto input = new TJSQDto();
                            //            //input.BRCS = data.Customer.Birthday;
                            //            input.BRH = data.CustomerBM;
                            //            input.BRKH = "";
                            //            input.BRLXDH = data.Customer.Mobile;
                            //            input.BRLXDZ = "";
                            //            input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                            //            input.BRXM = data.Customer.Name;                                       
                            //            input.DJTIME = System.DateTime.Now;
                            //            input.HISName = HISName;
                            //            input.CustomerRegId = data.Id;
                            //            var appliy = _customerSvr.InsertSFCharg(input);
                            //            if (appliy != null && !string.IsNullOrEmpty(appliy.ApplicationNum))
                            //            {
                            //                //收费
                            //                //WindowsFormsApp1.SFInterface sFInterface = new WindowsFormsApp1.SFInterface();
                            //                //sFInterface.InsertSFInforAsync(appliy.ApplicationNum);

                            //                NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                            //                var outstr = neInterface.shijianFH(data.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);                                        

                            //                if (appliy.ApplicationNum.Contains("失败"))
                            //                {
                            //                    //MessageBox.Show(appliy.ApplicationNum);
                            //                }
                            //            }
                            //            else
                            //            {
                            //                NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                            //                var outstr = neInterface.shijianFH(data.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);
                                           

                            //                //curCustomRegInfo = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });

                            //            }                                      
                            //        }
                            //        else if (HISName.Contains("江苏鑫亿"))
                            //        {
                            //            string nm = "";
                            //            var kdysnow = CurrentUser.EmployeeNum;
                            //            var kdysname = CurrentUser.Name;
                            //            if (HISName.Contains("四院"))
                            //            {
                            //                #region MyRegion
                            //                //开单医生
                            //                var kdys = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ForegroundFunctionControl.ToString() && o.Value == 150);
                            //                if (kdys != null && kdys.Remarks != "")
                            //                {
                            //                    var list = kdys.Remarks.Split('|');
                            //                    if (list.Length > 0)
                            //                    {
                            //                        var usernow = DefinedCacheHelper.GetComboUsers().Where(o => list.Contains(o.Name)).ToList();
                            //                        if (usernow.Count > 0)
                            //                        {                                                      
                            //                             var cuuser = usernow.FirstOrDefault(o => o.Name == list[0]);
                            //                            kdysnow = cuuser.EmployeeNum;
                            //                            kdysname = cuuser.Name;

                            //                        }
                            //                    }


                            //                }
                            //                #endregion
                                          

                            //                nm = "四院";
                            //            }
                            //            WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                            //            neInterface.CreateAp(data.Customer.ArchivesNum, data.CustomerBM, kdysnow, kdysname + nm);
                            //            //neInterface.CreateApAsync(data.Customer.ArchivesNum, data.CustomerBM, kdysnow, kdysname + nm);
                            //        }
                            //    }
                             
                            //}
                            #endregion

                            //执行步长
                            progressBarControl1.Properties.Step = ids.Count;
                            progressBarControl1.PerformStep();                            
                            num = num + ids.Count;
                            //处理当前消息队列中的所有windows消息,不然进度条会不同步
                            System.Windows.Forms.Application.DoEvents();
                            ids.Clear();
                           // cusreglist.Clear();
                        }
                    }

                    if (ids.Count == 0)
                    {
                        ShowMessageBoxInformation("未勾选需登记的人员");
                        return;
                    }

                    _customerAppService.BatchReg(new BatchRegInputDto { IsReg = true, RegIds = ids });
                   
                    //执行步长
                    progressBarControl1.Properties.Step = ids.Count;
                    progressBarControl1.PerformStep();
                    num = num + ids.Count;
                    //处理当前消息队列中的所有windows消息,不然进度条会不同步
                    System.Windows.Forms.Application.DoEvents();
                    foreach (var i in rows)
                    {
                        if (gridViewCustomerList.GetRow(i) is CustomerRegsViewDto row)
                        {
                            row.RegisterState = (int)RegisterState.Yes;
                        }
                    }

                 
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
            });
            try
            {
                #region HIS接口
                //var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;

                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    labelTS.Text = "正在上传接口数据，共：" + cusreglist.Count + "条";
                    labelTS.Refresh();
                    int count = 0;

                    foreach (var data in cusreglist)
                    {
                        if (HISName == "东软")
                        {

                            ICustomerAppService _customerSvr = new CustomerAppService();
                            TJSQDto input = new TJSQDto();
                            //input.BRCS = data.Customer.Birthday;
                            input.BRH = data.CustomerBM;
                            input.BRKH = "";
                            input.BRLXDH = data.Customer.Mobile;
                            input.BRLXDZ = "";
                            input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                            input.BRXM = data.Customer.Name;
                            input.DJTIME = System.DateTime.Now;
                            input.HISName = HISName;
                            input.CustomerRegId = data.Id;
                            var appliy = _customerSvr.InsertSFCharg(input);
                            if (appliy != null && !string.IsNullOrEmpty(appliy.ApplicationNum))
                            {
                                //收费                                  

                                NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                                var outstr = neInterface.shijianFH(data.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);

                                if (appliy.ApplicationNum.Contains("失败"))
                                {
                                    //MessageBox.Show(appliy.ApplicationNum);
                                }
                            }
                            else
                            {
                                NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                                var outstr = neInterface.shijianFH(data.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);
                                //curCustomRegInfo = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });

                            }
                        }
                        else if (HISName.Contains("江苏鑫亿"))
                        {

                            string nm = "";
                            var kdysnow = CurrentUser.EmployeeNum;
                            var kdysname = CurrentUser.Name;
                            if (HISName.Contains("四院"))
                            {
                                #region MyRegion
                                //开单医生
                                var kdys = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ForegroundFunctionControl.ToString() && o.Value == 150);
                                if (kdys != null && kdys.Remarks != "")
                                {
                                    var list = kdys.Remarks.Split('|');
                                    if (list.Length > 0)
                                    {
                                        var usernow = DefinedCacheHelper.GetComboUsers().Where(o => list.Contains(o.Name)).ToList();
                                        if (usernow.Count > 0)
                                        {
                                            var cuuser = usernow.FirstOrDefault(o => o.Name == list[0]);
                                            kdysnow = cuuser.EmployeeNum;
                                            kdysname = cuuser.Name;

                                        }
                                    }


                                }
                                #endregion


                                nm = "四院";
                            }
                            WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                            neInterface.CreateAp(data.Customer.ArchivesNum, data.CustomerBM, kdysnow, kdysname + nm +"批量");
                            // neInterface.CreateApAsync(data.Customer.ArchivesNum, data.CustomerBM, kdysnow, kdysname + nm);
                        }
                        count = count + 1;
                        labelTS.Text ="上传进度：" + count + "/" + cusreglist.Count;
                        labelTS.Refresh();
                    }

                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            gridViewCustomerList.RefreshData();
            ShowMessageBoxInformation("登记成功");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                try
                {
                   
                    //水平进度条
                    progressBarControl1.Properties.Minimum = 0;
                    progressBarControl1.Properties.Maximum =1;
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                    label1.Text = "";


                    var ids = new List<Guid>();
                    var rows = gridViewCustomerList.GetSelectedRows();
                    foreach (var i in rows)
                    {
                        if (gridViewCustomerList.GetRow(i) is CustomerRegsViewDto row && row.RegisterState != (int)RegisterState.No)
                        {
                            if (row.SendToConfirm == (int)SendToConfirm.Yes ||
                                row.SummSate != (int)SummSate.NotAlwaysCheck && row.SummSate.HasValue)
                            {
                            }
                            else
                            {
                                ids.Add(row.Id);
                            }
                        }
                    }

                    if (ids.Count == 0)
                    {
                        if (rows.Length > 0)
                        {
                            ShowMessageBoxInformation("所选人员状态不支持取消登记，请重新选择");

                        }
                        else
                        {
                            ShowMessageBoxInformation("未勾选需取消登记的人员");
                        }
                        return;
                    }

                    _customerAppService.BatchReg(new BatchRegInputDto { IsReg = false, RegIds = ids });
                    foreach (var i in rows)
                    {
                        
                        if (gridViewCustomerList.GetRow(i) is CustomerRegsViewDto row)
                        {
                            if (ids.Contains(row.Id))
                            {
                                #region HIS接口

                                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                                if (HISjk == "1")
                                {
                                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;

                                    if (HISName.Contains("江苏鑫亿"))
                                    {
                                        WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                                        // neInterface.CancelTTPay(curCustomRegInfo.Customer.ArchivesNum, curCustomRegInfo.CustomerBM, CurrentUser.EmployeeNum, CurrentUser.Name);
                                        var outMess = neInterface.CancelTTPay(row.CustomerBM, ("删除申请单" + "|" + HISName));
                                        if (outMess.Code != "0")
                                        {
                                            MessageBox.Show(outMess.ReSult);
                                            return;
                                        }

                                    }

                                }
                                #endregion
                                row.RegisterState = (int)RegisterState.No;
                            }
                        }
                    }

                    gridViewCustomerList.RefreshData();
                    ShowMessageBoxInformation("取消登记成功");
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
            });
        }

        private void searchLookUpEditClient_EditValueChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
            //{
            //    LoadData();
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
            {
                XtraMessageBox.Show("请选择单位");
                return;
            }
            if (!string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
            {
                LoadData();
            }
        }
    }
}