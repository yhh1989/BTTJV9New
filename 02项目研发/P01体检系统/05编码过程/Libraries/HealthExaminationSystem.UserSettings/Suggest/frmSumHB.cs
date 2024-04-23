using Abp.Application.Services.Dto;
using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest
{
    public partial class frmSumHB : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        public frmSumHB()
        {
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            InitializeComponent();
        }

        private void frmSumHB_Load(object sender, EventArgs e)
        {
            var sumHB = _summarizeAdviceAppService.SearchSumHB();
            gridadlist.DataSource = sumHB;

            searchLookAd.Properties.DataSource = DefinedCacheHelper.GetSummarizeAdvices();
            repositoryItemSearchLookUpEdit1.DataSource = DefinedCacheHelper.GetSummarizeAdvices();
            
            searchLookadName.Properties.DataSource = DefinedCacheHelper.GetSummarizeAdvices();



        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var sto = gridView1.GetFocusedRow() as TbmSummHBDto;
            if (sto != null)
            {
                gridsum.DataSource = sto.Advices;
                searchLookadName.EditValue = sto.SummarizeAdviceId;
                searchLookadName.Tag = sto.Id;
                butEdit(true);
            }
            else
            {
                MessageBox.Show("请选择复合诊断");
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            var sumlist = gridsum.DataSource as List<SummarizeAdviceDto>;
            if (sumlist == null)
            {
                MessageBox.Show("请选择需要合并的诊断列表");
                return;
            }
            if (sumlist.Count == 0)
            {
                MessageBox.Show("请选择需要合并的诊断列表");
                return;
            }
            if (string.IsNullOrEmpty(searchLookadName.EditValue?.ToString()))
            {
                MessageBox.Show("请选择诊断名称");
                return;
            }
            TbmSummHBDto tbmSummHB = new TbmSummHBDto();
            if (searchLookadName.Tag != null && Guid.TryParse(searchLookadName.Tag.ToString(), out Guid nowId))
            {
                tbmSummHB.Id = nowId;
            }
            tbmSummHB.Advices = sumlist;
            tbmSummHB.SummarizeAdviceId = (Guid)searchLookadName.EditValue;
            tbmSummHB.AdviceName = searchLookadName.Text;
            _summarizeAdviceAppService.SaveSumHB(tbmSummHB);
            MessageBox.Show("保存成功！");
            var sumHB = _summarizeAdviceAppService.SearchSumHB();
            gridadlist.DataSource = sumHB;
            butEdit(true);

        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            gridsum.DataSource = null;
            searchLookadName.EditValue = null;
            searchLookadName.Tag = null;
            butEdit(false);
        }

        private void butEdit(bool isEnble)
        {
            butAdd.Enabled = isEnble;
            butEidt.Enabled= isEnble;

            butSave.Enabled = !isEnble;
        }
        private void searchLookAd_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookAd.GetSelectedDataRow() != null)
            {
                var RowData = (SummarizeAdviceDto)searchLookAd.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridsum.DataSource as List<SummarizeAdviceDto>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.Id == RowData.Id)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<SummarizeAdviceDto>();

                    }

                    dataresult.Add(RowData);
                    gridsum.DataSource = dataresult;
                    gridsum.RefreshDataSource();
                    gridsum.Refresh();


                }
                //var seachresult = searchLookAd.Properties.DataSource as List<OutOccHazardFactor>;
                //seachresult.Remove(RowData);
                //searchLookAd.Refresh();
                searchLookAd.EditValue = null;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentItem = gridView2.GetFocusedRow() as SummarizeAdviceDto;
            if (currentItem == null)
                return;
            var dataresult = gridsum.DataSource as List<SummarizeAdviceDto>;
            dataresult.Remove(currentItem);
            gridsum.DataSource = dataresult;
            gridsum.RefreshDataSource();
            gridsum.Refresh();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentAdvice = gridView1.GetFocusedRow() as TbmSummHBDto;
            if (currentAdvice == null)
            {
                MessageBox.Show("请选择需要删除的数据！");
                return;
            }
            else
            {
                EntityDto<Guid> entity = new EntityDto<Guid>();
                entity.Id = currentAdvice.Id;
                _summarizeAdviceAppService.DelSumHB(entity);
                MessageBox.Show("删除成功！");
                var sumHB = _summarizeAdviceAppService.SearchSumHB();
                gridadlist.DataSource = sumHB;
            }

        }

        private void butEidt_Click(object sender, EventArgs e)
        {
            var currentAdvice = gridView1.GetFocusedRow() as TbmSummHBDto;
            if (currentAdvice == null)
            {
                MessageBox.Show("请选择需要修改的数据！");
                return;
            }
            butEdit(false);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
