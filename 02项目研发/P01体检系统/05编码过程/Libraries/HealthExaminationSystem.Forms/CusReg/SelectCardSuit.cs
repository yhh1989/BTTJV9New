using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class SelectCardSuit : UserBaseForm
    {
        public CardToSuitNameDto OutSuit = new CardToSuitNameDto();
        public SelectCardSuit()
        {
            InitializeComponent();
        }
        public SelectCardSuit(List<CardToSuitNameDto> ItemSuits) 
        {
            InitializeComponent();
            gridControl1.DataSource = ItemSuits;
        }

        private void SelectCardSuit_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var data = gridView1.GetFocusedRow() as CardToSuitNameDto;
            OutSuit = data;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
