using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
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
    public partial class frmPayMent : UserBaseForm
    {
        public ChargeTypeDto PaymentId;
        private IChargeAppService ChargeAppService;
        public frmPayMent()
        {
            ChargeAppService = new ChargeAppService();
            InitializeComponent();
        }

        private void frmPayMent_Load(object sender, EventArgs e)
        {
            List<ChargeTypeDto> ChargeType = new List<ChargeTypeDto>();
            int type = 2;
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = type.ToString();
            ChargeType = ChargeAppService.ChargeType(chargeBM);
            comPaymentMethod.Properties.DataSource = ChargeType;
        }

        private void butOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comPaymentMethod.EditValue?.ToString()))
            {
                PaymentId = comPaymentMethod.EditValue as ChargeTypeDto;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请选择支付方式");
            }
        }
    }
}
