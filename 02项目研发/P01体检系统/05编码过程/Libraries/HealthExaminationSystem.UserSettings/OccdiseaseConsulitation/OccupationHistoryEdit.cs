using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseConsulitation
{
    
    public partial class OccupationHistoryEdit : UserBaseForm
    {
        private readonly IOccdiseaseConsulitationAppService _OccdiseaseConsulitationAppService = new OccdiseaseConsulitationAppService();
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService = new OccHazardFactorAppService();
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService=new OccDisProposalNewAppService();
        private OccdieaseBasicInformationDto CusReg=null;
        public OccupationHistoryEdit()
        {
            InitializeComponent();
        }
        public OccupationHistoryEdit(OccdieaseBasicInformationDto cusreginfo)
        {
            InitializeComponent();
            CusReg = cusreginfo;
        }
        private readonly Guid _id;
        public OccupationHistoryDto occpast = new OccupationHistoryDto();
        private void OccupationHistoryEdit_Load(object sender, EventArgs e)
        {
            //危害因素
            OutOccHazardFactorDto show = new OutOccHazardFactorDto();
            show.IsActive = 3;
            var data = _OccHazardFactorAppService.ShowOccHazardFactor(show);
            OccStandard.Properties.DataSource = data;
            //防护措施
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Protect.ToString();
            var dl1 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            searchLookUpEdit1.Properties.DataSource = dl1;

            //工种
            chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
            var lis3 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            textEdit2.Properties.DataSource = lis3;
            if (CusReg != null)
            {
                textEdit1.Text = CusReg.InjuryAge;
                comunit.Text = CusReg.InjuryAgeUnit;
                if (int.TryParse(CusReg.InjuryAge,out int age))
                {
                    if (CusReg.InjuryAgeUnit.Contains("月"))
                    {
                        Startdate.Text = System.DateTime.Now.AddMonths(-age).ToString();
                        Enddate.Text = System.DateTime.Now.ToString();
                    }
                    else if (CusReg.InjuryAgeUnit.Contains("日"))
                    {
                        Startdate.Text = System.DateTime.Now.AddDays(-age).ToString();
                        Enddate.Text = System.DateTime.Now.ToString();
                    }
                    else
                    {
                        Startdate.Text = System.DateTime.Now.AddYears(-age).ToString();
                        Enddate.Text = System.DateTime.Now.ToString();
                    }

                }
                client.Text = CusReg.ClientName;
                textEdit2.Text = CusReg.TypeWork;
               
                var     dataresult = new List<OccdieaseHurtDto>();
             var dieaseProtect = new List<OccdieaseProtectDto>();

                foreach (var risk in CusReg.OccHazardFactors)
                {
                    var OccdieaseHurtDto = new OccdieaseHurtDto();
                    OccdieaseHurtDto.CASBM = risk.CASBM;
                    OccdieaseHurtDto.Text = risk.Text;
                    dataresult.Add(OccdieaseHurtDto);

                    foreach (var proc in risk.Protectivis)
                    {
                        if (dieaseProtect.Where(p => p.Text == proc.Text).Count() == 0)
                        {
                            var OccdieasefhDto = new OccdieaseProtectDto();
                            OccdieasefhDto.BM = proc.OrderNum.ToString();
                            OccdieasefhDto.Text = proc.Text;
                            dieaseProtect.Add(OccdieasefhDto);
                        }
                    }
                 

                }
                gridControl1.DataSource = dataresult;
                gridControl1.RefreshDataSource();
                gridControl1.Refresh();

                gridControl2.DataSource = dieaseProtect;
                gridControl2.RefreshDataSource();
                gridControl2.Refresh();

            }
        }
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            occpast = new OccupationHistoryDto();

            occpast.CustomerRegBMId = OccdieaseConsultation.Cusid;
            occpast.StarTime = Startdate.DateTime;
            occpast.EndTime = Enddate.DateTime;
            occpast.WorkClient = client.Text.Trim();
            //occpast.WorkType = workType.EditValue.ToString();
            occpast.WorkType = textEdit2.Text;
            occpast.WorkYears = Convert.ToDecimal(textEdit1.Text);
            occpast.UnitAge = comunit.Text;
            //防护措施危害因素
            occpast.OccHisHazardFactors = new List<OccdieaseHurtDto>();
            occpast.OccHisProtectives = new List<OccdieaseProtectDto>();
            var occha = gridControl1.DataSource as List<OccdieaseHurtDto>;
            if (occha != null)
            {
                occpast.OccHisHazardFactors = occha;
            }
            var occpro = gridControl2.DataSource as List<OccdieaseProtectDto>;
            if (occpro != null)
            {
                occpast.OccHisProtectives = occpro;
            }
            if (_id == Guid.Empty)
            {

                DialogResult = System.Windows.Forms.DialogResult.OK;


            }
            bool res = true;
            return res;
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {

           
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void OccStandard_EditValueChanged(object sender, EventArgs e)
        {
            if (OccStandard.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccHazardFactorDto)OccStandard.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl1.DataSource as List<OccdieaseHurtDto>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.Text == RowData.Text)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<OccdieaseHurtDto>();

                    }
                  var  OccdieaseHurtDto = new OccdieaseHurtDto();
                    OccdieaseHurtDto.CASBM = RowData.CASBM;
                    OccdieaseHurtDto.Text = RowData.Text;
                    dataresult.Add(OccdieaseHurtDto);
                    gridControl1.DataSource = dataresult;
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();


                }
                OccStandard.EditValue = null;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentItem = gridView1.GetFocusedRow() as OccdieaseHurtDto;
            if (currentItem == null)
                return;
            var dataresult = gridControl1.DataSource as List<OccdieaseHurtDto>;
            dataresult.Remove(currentItem);
            gridControl1.DataSource = dataresult;
            gridControl1.RefreshDataSource();
            gridControl1.Refresh();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit1.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)searchLookUpEdit1.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl2.DataSource as List<OccdieaseProtectDto>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.Text == RowData.Text)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<OccdieaseProtectDto>();

                    }
                    var OccdieaseHurtDto = new OccdieaseProtectDto();
                    OccdieaseHurtDto.BM = RowData.OrderNum.ToString();
                    OccdieaseHurtDto.Text = RowData.Text;
                    dataresult.Add(OccdieaseHurtDto);
                    gridControl2.DataSource = dataresult;
                    gridControl2.RefreshDataSource();
                    gridControl2.Refresh();


                }
                searchLookUpEdit1.EditValue = null;
            }
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentItem = gridView3.GetFocusedRow() as OccdieaseProtectDto;
            if (currentItem == null)
                return;
            var dataresult = gridControl2.DataSource as List<OccdieaseProtectDto>;
            dataresult.Remove(currentItem);
            gridControl2.DataSource = dataresult;
            gridControl2.RefreshDataSource();
            gridControl2.Refresh();
        }

        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ////删除
            ////MessageBox.Show("删除");
            //var currentItem = gridControl1.GetFocusedRow() as OccupationHistoryDto;
            //if (currentItem == null)
            //    return;

            //var dataresult = gridzys.DataSource as List<OccupationHistoryDto>;
            //dataresult.Remove(currentItem);
            //gridzys.DataSource = dataresult;
            //gridzys.RefreshDataSource();
            //gridzys.Refresh();
        }
    }
}
