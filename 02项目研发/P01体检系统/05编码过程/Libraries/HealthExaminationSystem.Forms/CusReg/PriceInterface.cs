using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
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
    public partial class PriceInterface : UserBaseForm
    {
        private readonly ICustomerAppService _CustomerAppService=new CustomerAppService();

        public PriceInterface()
        {
            InitializeComponent();
        }

        private void PriceInterface_Load(object sender, EventArgs e)
        {
            InCarNumDto inCarNum = new InCarNumDto();
            var Pricedata = _CustomerAppService.getPrice(inCarNum);
        }
    }
}
