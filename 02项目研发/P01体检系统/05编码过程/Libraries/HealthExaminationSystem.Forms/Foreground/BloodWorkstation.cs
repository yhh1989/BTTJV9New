using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System.IO;
using System.Diagnostics;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Foreground
{
    /// <summary>
    /// 抽血工作站
    /// </summary>
    public partial class BloodWorkstation : UserBaseForm
    {
        System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(BloodWorkstation));

        /// <summary>
        /// 初始化 抽血工作站
        /// </summary>
        public BloodWorkstation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BloodWorkstation_Load(object sender, EventArgs e)
        {
            dateEdit抽血时间检索.DateTime = DateTime.Now;
            searchLookUpEdit抽血人检索.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            searchLookUpEdit抽血人检索.EditValue = CurrentUser.Id;
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton查询按钮_Click(object sender, EventArgs e)
        {
            var input = new CustomerRegisterBarCodePrintInformationConditionInput();
            input.BarCodeNumber = textEdit条码号检索.Text;
            if (dateEdit抽血时间检索.EditValue != null)
            {
                input.BloodTime = dateEdit抽血时间检索.DateTime;
            }
            input.BloodUserId = searchLookUpEdit抽血人检索.EditValue as long?;
            input.HaveBlood = radioGroup抽血状态检索.EditValue as bool?;
            input.AutoBlood = checkEdit扫码自动抽血设置.Checked;
            input.AutoReceive = checkEdit2.Checked;
            input.AutoSend = checkEdit1.Checked;

            AutoLoading(() =>
            {
                var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                    .QueryBarCodePrintRecord(input).Result;
                gridView条码记录列表.FocusInvalidRow();

                gridControl条码记录列表.DataSource = result;

                gridView条码记录列表.FocusedRowHandle = 0;
            });
        }

        /// <summary>
        /// 条码记录列表数据源改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl条码记录列表_DataSourceChanged(object sender, EventArgs e)
        {
            gridView条码记录列表.BestFitColumns();
        }

        /// <summary>
        /// 数据行上操作按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit操作_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //alertControlBloodWorkstation.Show(this, "提示", gridView条码记录列表.FocusedRowHandle.ToString());
            if (e.Button.Tag is int index)
            {
                if (gridView条码记录列表.GetFocusedRow() is CustomerRegisterBarCodePrintInformationDto row)
                {
                    if (index == 0)
                    {
                        // 抽血
                        if (!row.HaveBlood)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = true, Id = row.Id }).Result;

                            row.HaveBlood = result.HaveBlood;
                            row.BloodTime = result.BloodTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }
                    else if (index == 1)
                    {
                        // 取消抽血
                        if (row.HaveBlood)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = false, Id = row.Id }).Result;

                            row.HaveBlood = result.HaveBlood;
                            row.BloodTime = result.BloodTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }                   
                    else if (index == 3)
                    {
                        // 取消抽血
                        if (!row.HaveSend)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = false, Id = row.Id, type = 2 }).Result;

                            row.HaveSend = result.HaveSend;
                            row.SendTime = result.SendTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }
                    else if (index == 4)
                    {
                        // 取消抽血
                        if (row.HaveSend)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = false, Id = row.Id , type=2}).Result;

                            row.HaveSend = result.HaveSend;
                            row.SendTime = result.SendTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }
                    else if (index ==5)
                    {
                        // 取消抽血
                        if (!row.HaveReceive)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = false, Id = row.Id, type = 3 }).Result;

                            row.HaveReceive = result.HaveReceive;
                            row.ReceiveTime = result.ReceiveTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }
                    else if (index == 6)
                    {
                        // 取消抽血
                        if (row.HaveReceive)
                        {
                            var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                                .UpdateBarCodeHaveBlood(new UpdateBarCodeHaveBloodInput
                                { HaveBlood = false, Id = row.Id, type = 3 }).Result;

                            row.HaveReceive = result.HaveReceive;
                            row.ReceiveTime = result.ReceiveTime;

                            gridView条码记录列表.RefreshRow(gridView条码记录列表.FocusedRowHandle);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 焦点行改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView条码记录列表_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView条码记录列表.GetFocusedRowCellValue(gridColumn2) is Guid customerRegisterId)
            {
                if (gridView条码记录列表.GetFocusedRowCellValue(gridColumn11) is string tubeColor)
                {
                    emptySpaceItem3.Text = tubeColor;
                    if (tubeColor.Contains("红"))
                    {
                        colorEdit1.Color = Color.Red;
                    }
                    else if (tubeColor.Contains("黄"))
                    {
                        colorEdit1.Color = Color.Yellow;
                    }
                    else if (tubeColor.Contains("黑"))
                    {
                        colorEdit1.Color = Color.Black;
                    }
                    else if (tubeColor.Contains("橙"))
                    {
                        colorEdit1.Color = Color.Orange;
                    }
                    else if (tubeColor.Contains("紫"))
                    {
                        colorEdit1.Color = Color.Purple;
                    }
                    else
                    {
                        colorEdit1.Color = Color.Empty;
                    }
                }
                if (pictureEdit体检人头像.Tag != null && pictureEdit体检人头像.Tag.Equals(customerRegisterId))
                {
                    // 忽略
                }
                else
                {
                    DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                        .QueryCustomerRegisterById(new EntityDto<Guid>(customerRegisterId)).ContinueWith(task =>
                        {
                            var result = task.Result;

                            pictureEdit体检人头像.Tag = result.Id;
                            layoutControlBase.BeginInvoke(new Action<CustomerRegister52Dto>(cr =>
                            {
                                simpleLabelItem姓名.Text = cr.Name;
                                simpleLabelItem性别.Text = SexHelper.CustomSexFormatter(cr.Sex);
                                simpleLabelItem年龄.Text = cr.Age.GetValueOrDefault().ToString();
                                simpleLabelItem单位.Text = cr.ClientName ?? "无";
                            }), result);
                            if (result.CusPhotoBmId.HasValue)
                            {
                                var pictureDto =
                                    DefinedCacheHelper.DefinedApiProxy.PictureController.GetUrl(result.CusPhotoBmId.Value);
                                if (pictureDto != null)
                                {
                                    pictureEdit体检人头像.BeginInvoke(new Action<PictureDto>(p =>
                                    {
                                        pictureEdit体检人头像.LoadAsync(p.RelativePath);
                                    }), pictureDto);
                                }
                            }
                        });
                }
            }
            else
            {
                pictureEdit体检人头像.Tag = null;
                pictureEdit体检人头像.EditValue = _resources.GetObject("pictureEdit体检人头像.EditValue");
                simpleLabelItem姓名.Text = @"姓名";
                simpleLabelItem性别.Text = @"性别";
                simpleLabelItem年龄.Text = @"年龄";
                simpleLabelItem单位.Text = @"单位";
                colorEdit1.Color = Color.Empty;
                emptySpaceItem3.Text = @"管颜色";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //调用程序：
            var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
            }
            pictureEdit体检人头像.Image.Save(url);
            string args = url;
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\人证核验";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();

            while (!KHMsg.HasExited) { } //如果exe还没关闭，则等待
            if (KHMsg.ExitCode != 1)
            {
                MessageBox.Show("认证核验失败！");
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked==true)
            {
                layoutControlItem5.Text = "送检人";
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                layoutControlItem5.Text = "接收人";
            }
        }

        private void checkEdit扫码自动抽血设置_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit扫码自动抽血设置.Checked == true)
            {
                layoutControlItem5.Text = "接收人";
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            #region 人脸识别
            //调用程序：
            var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
            }         
            string args = "人脸识别";
            var path = AppDomain.CurrentDomain.BaseDirectory + "人证核验";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.StartInfo.RedirectStandardOutput = true;
            KHMsg.StartInfo.UseShellExecute = false;
            KHMsg.Start();
            //KHMsg.WaitForExit(60000);
            while (!KHMsg.HasExited)
            {
            } //如果exe还没关闭，则等待
            if (KHMsg.ExitCode != 1)
            {
                MessageBox.Show("人脸识别失败！");
            }
            else
            {
                string xmldata = KHMsg.StandardOutput.ReadToEnd();//读取exe中内存流数据

                //自己实现的序列化
                var ss = xmldata.Replace("\r\n", "$");
                var pic = ss.Split('$').ToList();
                var picnow = pic.FirstOrDefault(p => p.Contains("@"));
                if (!string.IsNullOrEmpty(picnow))
                {
                    picnow = picnow.Replace("@","");
                    var input = new CustomerRegisterBarCodePrintInformationConditionInput();
                    input.BarCodeNumber = picnow;
                    if (dateEdit抽血时间检索.EditValue != null)
                    {
                        input.BloodTime = dateEdit抽血时间检索.DateTime;
                    }
                    input.BloodUserId = searchLookUpEdit抽血人检索.EditValue as long?;
                    input.HaveBlood = radioGroup抽血状态检索.EditValue as bool?;
                    input.AutoBlood = checkEdit扫码自动抽血设置.Checked;
                    input.AutoReceive = checkEdit2.Checked;
                    input.AutoSend = checkEdit1.Checked;

                    AutoLoading(() =>
                    {
                        var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                            .QueryBarCodePrintRecord(input).Result;
                        gridView条码记录列表.FocusInvalidRow();

                        gridControl条码记录列表.DataSource = result;

                        gridView条码记录列表.FocusedRowHandle = 0;
                    });

                }
            }

            #endregion           

        }
    }
}