using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseConsulitation
{
    public partial class frmzz : UserBaseForm
    {
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        public   List<OutOccDictionaryDto> outOccDictionaries = new List<OutOccDictionaryDto>();
        public frmzz()
        {
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            InitializeComponent();
        }
       
        private void frmzz_Load(object sender, EventArgs e)
        {
           
            //症状小类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Symptom.ToString();
            var dll = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            gridControl1.DataSource = dll;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //var zzdt = gridControl1.DataSource as List<OutOccDictionaryDto>;

            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                foreach (var index in selectIndexes)
                {
                    var dt = gridView1.GetRow(index) as OutOccDictionaryDto;
                    if (dt!=null)
                    {
                        outOccDictionaries.Add(dt);

                    }
                }
            }
            if (outOccDictionaries.Count == 0)
            {
                MessageBox.Show("请至少选择一个症状");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
