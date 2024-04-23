using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    
    public partial class frmWebCharge : UserBaseForm
    {
        private IChargeAppService ChargeAppService;
        private readonly ICommonAppService _commonAppService;
        public frmWebCharge()
        {
            InitializeComponent();
            ChargeAppService = new ChargeAppService();
            _commonAppService = new CommonAppService();
        }

        private void frmWebCharge_Load(object sender, EventArgs e)
        {
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now.ToShortDateString();
          
            //dtFormat.ShortDatePattern = "yyyy/MM/dd";
            dateEditStarGR.EditValue = date;
            dateEditendGR.EditValue = date; 

            gridView1.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InSearchBusiCusDto input = new InSearchBusiCusDto();
            if (!string.IsNullOrEmpty(dateEditStarGR.EditValue?.ToString()))
            {
                input.StarDate = dateEditStarGR.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditendGR.EditValue?.ToString()))
            {
                input.EndDate = dateEditendGR.DateTime.AddDays(1);
            }
            if (!string.IsNullOrEmpty(cglueJianChaYiSheng1.EditValue?.ToString()))
            {
                input.LinkName = cglueJianChaYiSheng1.EditValue?.ToString();
            }
            var result = ChargeAppService.getWebCharge(input);
            gridControlCus.DataSource = result;
        }
    }
}
