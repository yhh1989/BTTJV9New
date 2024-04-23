using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class frmZKJE : UserBaseForm
    {
        public decimal INReMoney = 0;
        public decimal OutReMoney = 0;
        public frmZKJE()
        {
            InitializeComponent();
        }
        public frmZKJE(decimal _ReMoney) :this()
        {
            INReMoney = _ReMoney;
            txtpayMoney.EditValue = INReMoney;
        }

        private void frmZKJE_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpayMoney.EditValue?.ToString()))
            {
                MessageBox.Show("请输入回款金额！");
                return;
            }
            if (decimal.Parse(txtpayMoney.EditValue?.ToString()) <= 0)
            {
                MessageBox.Show("回款金额必须大于0！");
                return;
            }
            if ( decimal.Parse(txtpayMoney.EditValue?.ToString())>INReMoney)
            {
                MessageBox.Show("回款金额不能大于申请单金额！");
                return;
            }
            OutReMoney = decimal.Parse(txtpayMoney.EditValue?.ToString());
            this.DialogResult = DialogResult.OK;
        }
    }
}
