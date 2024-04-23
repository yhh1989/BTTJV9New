using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 合同附件管理
    /// </summary>
    public partial class ContractAdjunctManager : UserBaseForm
    {
        /// <summary>
        /// 合同标识
        /// </summary>
        private readonly Guid _contractId;

        /// <summary>
        /// 初始化 合同附件管理
        /// </summary>
        public ContractAdjunctManager(Guid contractId)
        {
            InitializeComponent();
            _contractId = contractId;
        }

        /// <summary>
        /// 合同附件列表数据源改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl合同附件列表_DataSourceChanged(object sender, EventArgs e)
        {
            if (gridControl合同附件列表.DataSource != null)
            {
                gridView合同附件列表.BestFitColumns();
            }
        }

        /// <summary>
        /// 打开合同附件文件成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog打开文件_FileOk(object sender, CancelEventArgs e)
        {
            AutoLoading(() =>
            {
                var row = DefinedCacheHelper.DefinedApiProxy.ContractAdjunctController.Upload(_contractId,
                    openFileDialog打开文件.FileName);
                var data = gridControl合同附件列表.DataSource as List<ContractAdjunctDto> ?? new List<ContractAdjunctDto>();
                data.Insert(0, row);
                gridControl合同附件列表.DataSource = data;
                gridControl合同附件列表.RefreshDataSource();
                gridView合同附件列表.BestFitColumns();
            }, "正在上传文件...");
        }

        /// <summary>
        /// 上传附件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton上传附件_Click(object sender, EventArgs e)
        {
            openFileDialog打开文件.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog打开文件.ShowDialog(this);
        }

        /// <summary>
        /// 窗体首次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractAdjunctManager_Shown(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                gridControl合同附件列表.DataSource =
                    DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractAdjunct(
                        new EntityDto<Guid>(_contractId)).Result;
            });
        }

        /// <summary>
        /// 删除附件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton删除附件_Click(object sender, EventArgs e)
        {
            if (gridView合同附件列表.GetFocusedRow() is ContractAdjunctDto row)
            {
                AutoLoading(() =>
                {
                    DefinedCacheHelper.DefinedApiProxy.ContractAdjunctController.Delete(row.Id);
                    gridView合同附件列表.DeleteRow(gridView合同附件列表.FocusedRowHandle);
                });
            }
        }

        /// <summary>
        /// 操作按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit操作_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView合同附件列表.GetFocusedRow() is ContractAdjunctDto row)
            {
                saveFileDialog下载文件.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFileDialog下载文件.FileName = row.Name;
                saveFileDialog下载文件.ShowDialog(this);
            }
        }

        /// <summary>
        /// 下载文件成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileDialog下载文件_FileOk(object sender, CancelEventArgs e)
        {
            AutoLoading(() =>
            {
                if (gridView合同附件列表.GetFocusedRow() is ContractAdjunctDto row)
                {
                    var url = ConfigurationManager.AppSettings["Url"];
                    var uri = new UriBuilder(url);
                    var fileName = row.FileName.Replace("\\", "/");
                    uri.Path = fileName;
                    using (var wc = new WebClient())
                    {
                        wc.DownloadFile(uri.Uri, saveFileDialog下载文件.FileName);
                    }

                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                    if (XtraMessageBox.Show(this, "是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        Process.Start(saveFileDialog下载文件.FileName);
                    }
                }
            }, "正在下载文件...");
        }
    }
}