using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet
{
    public partial class frmProtective : UserBaseForm
    {
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;

        public List<OutOccDictionaryDto> outOccHazardFactors = new List<OutOccDictionaryDto>();
        public List<Guid> outRisck = new List<Guid>();
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        public frmProtective()
        {
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            InitializeComponent();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
        }

        private void frmSelectHazard_Load(object sender, EventArgs e)
        {
            //防护措施
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Protect.ToString();
            var outresult =  _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);

        
            if (outRisck != null && outRisck.Count > 0)
            {
                gridHazardFactor.DataSource = outresult.Where(o=> outRisck.Contains(o.Id)).ToList();
                
            }
            if (outOccHazardFactors != null && outOccHazardFactors.Count > 0)
            {
                var ids = outOccHazardFactors.Select(o=>o.Id).ToList();
                gridHazardFactor.DataSource = outresult.Where(o => ids.Contains(o.Id)).ToList();
            }
            txtHazardFactor.Properties.DataSource = outresult;

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentItem = gridView1.GetFocusedRow() as OutOccDictionaryDto;
            if (currentItem == null)
                return;
            var dataresult = gridHazardFactor.DataSource as List<OutOccDictionaryDto>;
            dataresult.Remove(currentItem);
            gridHazardFactor.DataSource = dataresult;
            gridHazardFactor.RefreshDataSource();
            gridHazardFactor.Refresh();

            
        }

        private void butAdd_Click(object sender, EventArgs e)
        {

        }

        private void txtHazardFactor_EditValueChanged(object sender, EventArgs e)
        {
            if (txtHazardFactor.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)txtHazardFactor.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult=gridHazardFactor.DataSource as List<OutOccDictionaryDto>;
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
                        dataresult = new List<OutOccDictionaryDto>();

                    }

                    dataresult.Add(RowData);
                    gridHazardFactor.DataSource = dataresult;
                    gridHazardFactor.RefreshDataSource();
                    gridHazardFactor.Refresh();


                }
                //var seachresult = txtHazardFactor.Properties.DataSource as List<OutOccHazardFactor>;
                //seachresult.Remove(RowData);
                //txtHazardFactor.Refresh();
                txtHazardFactor.EditValue = null;
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            outOccHazardFactors= gridHazardFactor.DataSource as List<OutOccDictionaryDto>;           
           
            this.DialogResult = DialogResult.OK;

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
