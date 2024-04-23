using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmCusVisite : UserBaseForm
    {
        private readonly IOccReviewAppService _OccReviewAppService;
        private Guid CusRegid;
        private Guid Cusid;
        public frmCusVisite()
        {
            _OccReviewAppService = new OccReviewAppService();
            InitializeComponent();
        }
        public frmCusVisite(Guid _cusRegId,Guid _cusid) :this()
        {
            CusRegid = _cusRegId;
            Cusid = _cusid;
          
        }
        private void frmCusVisite_Load(object sender, EventArgs e)
        {
            CusVisitDto cusVistit = new CusVisitDto();
            cusVistit.CustomerRegID = CusRegid;
            var cusVisit = _OccReviewAppService.SearchVisit(cusVistit);
            if (cusVisit != null)
            {
                radioVisiteState.EditValue = cusVisit.VisitState;
                timeVistit.EditValue = cusVisit.VisitDate;
                comColour.EditValue = cusVisit.colour;
                memoRemark.Text = cusVisit.remarks;

            }

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            CusVisitDto cusVistit = new CusVisitDto();
            cusVistit.CustomerRegID = CusRegid;
            //cusVistit.CustomerId = Cusid;
            cusVistit.colour = comColour.Text;
            cusVistit.VisitState = (int)radioVisiteState.EditValue;
            cusVistit.VisitDate = timeVistit.DateTime;
            cusVistit.remarks = memoRemark.Text;
            _OccReviewAppService.SaveVisit(cusVistit);
            this.Close();

        }
    }
}
