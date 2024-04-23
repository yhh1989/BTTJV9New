using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{
    public partial class GuidanceSet : UserBaseForm
    {
        private readonly IBasicDictionaryAppService service;
        public GuidanceSet()
        {
            InitializeComponent();
            service = new BasicDictionaryAppService();
        }

        private void GuidanceSet_Load(object sender, EventArgs e)
        {
           
            var str = BasicDictionaryType.GuidanceSet;
            gridControl.DataSource = null;
            var input = new BasicDictionaryInput { Type = str.ToString() };
            var output = service.Query(input);
            gridControl.DataSource = output.Where(o=>o.Value!=10 && o.Value!=180 && o.Value!=80);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
            var selectRow = gridView1.GetSelectedRows()[0];
            var idBM = gridView1.GetRowCellValue(selectRow, "Value").ToString();
            if (idBM == "1000")
            {

            }
            else
            {
                var coont = gridView1.GetRowCellValue(selectRow, "Remarks").ToString();
                frmDepartSelect frmDepartSelect = new frmDepartSelect(coont);
                if (frmDepartSelect.DialogResult == DialogResult.OK)
                {
                    gridView1.SetRowCellValue(selectRow, "Value", frmDepartSelect.guids);
                }

                
            }
        }
        
    }
}
