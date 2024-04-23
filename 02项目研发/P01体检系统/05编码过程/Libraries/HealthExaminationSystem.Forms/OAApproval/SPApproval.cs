using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.OAApproval
{
    public partial class SPApproval : UserBaseForm
    {
        private Guid Guid = new Guid();
        public CreatOAApproValcsDto creatOAApproValcsDto = new CreatOAApproValcsDto();
        private IOAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        private readonly IClientInfoesAppService _ClientInfoesAppService;

        private UserAppService userAppService = new UserAppService();

        public SPApproval()
        {
            _ClientInfoesAppService = new ClientInfoesAppService();
            InitializeComponent();
        }
        public SPApproval(Guid guid,bool isOK=true):  this()
        {
            ChargeBM chargeBM = new ChargeBM();
            var clientPaaent = _ClientInfoesAppService.QueryClientName(chargeBM);

            txtClientRegID.Properties.DataSource = clientPaaent;
            var spuser = userAppService.GetClientZKUsers();
            txtSPDoctor.Properties.DataSource = spuser;
            txtCSDoc.Properties.DataSource = DefinedCacheHelper.GetComboUsers().ToList();

            Guid = guid;
            SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
            searchOAApproValcsDto.Id = Guid;
            var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
            if (zk.Count > 0)
            {
                creatOAApproValcsDto = zk[0];

                txtClientRegID.EditValue = creatOAApproValcsDto.ClientInfoId;
                textZK.EditValue = creatOAApproValcsDto.DiscountRate;
                textAddZK.EditValue = creatOAApproValcsDto.AddDiscountRate;
                txtSPDoctor.EditValue = creatOAApproValcsDto.ApprovalUserId;
                txtRemark.EditValue = creatOAApproValcsDto.Remark;
                txtappUser.EditValue = creatOAApproValcsDto.Applicant;
                txtTitle.Text = creatOAApproValcsDto.TitleName;
                txtCSDoc.EditValue = creatOAApproValcsDto.CCUserId;
                memoEdit1.EditValue = creatOAApproValcsDto.Comments;
            }
            if (isOK == false)
            {
                layoutControlItem6.Visibility = LayoutVisibility.Never;
                layoutControlItem7.Visibility= LayoutVisibility.Never;
            }
        }
        private void SPApproval_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                UpOAApproValcsDto upOAApproValcsDto = new UpOAApproValcsDto();
            upOAApproValcsDto.Id = creatOAApproValcsDto.Id;
            upOAApproValcsDto.Comments = memoEdit1.Text;
            upOAApproValcsDto.AppliState = (int)OAApState.HasAp;

          var upOApp=  oAApprovalAppService.upOAApproValcsDto(upOAApproValcsDto);
                creatOAApproValcsDto.Comments = memoEdit1.Text;
                creatOAApproValcsDto.AppliState = (int)OAApState.HasAp;
                this.DialogResult = DialogResult.OK;
            });
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                UpOAApproValcsDto upOAApproValcsDto = new UpOAApproValcsDto();
            upOAApproValcsDto.Id = creatOAApproValcsDto.Id;
            upOAApproValcsDto.Comments = memoEdit1.Text;
                upOAApproValcsDto.AppliState = (int)OAApState.reAp;

                creatOAApproValcsDto.Comments = memoEdit1.Text;
                creatOAApproValcsDto.AppliState = (int)OAApState.reAp;

                var upOApp = oAApprovalAppService.upOAApproValcsDto(upOAApproValcsDto);
        });
        }
    }
}
