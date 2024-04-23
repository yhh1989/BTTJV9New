using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
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
    public partial class AddZKApproval : UserBaseForm
    {
        private IOAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        private Guid GID =new Guid();
        public CreatOAApproValcsDto creatOAApproValcsDto = new CreatOAApproValcsDto();
        private IIDNumberAppService iIDNumberAppService=new IDNumberAppService();
        private CreateClientXKSetDto createClientXKSetDto = new CreateClientXKSetDto();
        private readonly IClientInfoesAppService _ClientInfoesAppService;
        private UserAppService userAppService = new UserAppService(); 

        public AddZKApproval()
        {
            _ClientInfoesAppService = new ClientInfoesAppService();
            InitializeComponent();
        }
        public AddZKApproval(Guid guid) :this()
        {
            GID = guid;
        }

        private void AddZKApproval_Load(object sender, EventArgs e)
        {
            createClientXKSetDto= oAApprovalAppService.getCreateClientXKSet();
            if (createClientXKSetDto == null)
            {
                MessageBox.Show("请设置单位折扣设置！");
            }
            ChargeBM chargeBM = new ChargeBM();
        
            var clientPaaent = _ClientInfoesAppService.QueryClientName(chargeBM);

            txtClientRegID.Properties.DataSource = clientPaaent;
            var user = DefinedCacheHelper.GetComboUsers().ToList();
            var spuser = userAppService.GetClientZKUsers();
            txtSPDoctor.Properties.DataSource = spuser;
            txtCSDoc.Properties.DataSource = user;

            if (GID != null && GID != Guid.Empty)
            {
                SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                searchOAApproValcsDto.Id = GID;
                var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                if (zk.Count > 0)
                {
                    creatOAApproValcsDto = zk[0];
                    txtBM.Text = creatOAApproValcsDto.TitleBM;
                    txtClientRegID.EditValue = creatOAApproValcsDto.ClientInfoId;
                    textZK.EditValue = creatOAApproValcsDto.DiscountRate;
                    textAddZK.EditValue = creatOAApproValcsDto.AddDiscountRate;
                    txtSPDoctor.EditValue = creatOAApproValcsDto.ApprovalUserId;
                    txtRemark.EditValue = creatOAApproValcsDto.Remark;
                    txtappUser.EditValue = creatOAApproValcsDto.Applicant;
                    txtTitle.Text = creatOAApproValcsDto.TitleName;
                    txtCSDoc.EditValue = creatOAApproValcsDto.CCUserId;
                    
                }
              

            }
            else
            {
                if (creatOAApproValcsDto == null)
                {
                    creatOAApproValcsDto = new CreatOAApproValcsDto();

                }
                txtBM.Text = iIDNumberAppService.CreateClientZKBM();

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(createClientXKSetDto==null)
            {
                MessageBox.Show("请设置单位折扣设置！");
                return;
            }

            AutoLoading(() =>
            {        

            if (txtClientRegID.EditValue == null)
            {
                dxErrorProvider.SetError(txtClientRegID, string.Format(Variables.MandatoryTips, "单位"));
                txtClientRegID.Focus();
                return;
            }
            if (textZK.EditValue == null)
            {
                dxErrorProvider.SetError(textZK, string.Format(Variables.MandatoryTips, "团体最大折扣"));
                textZK.Focus();
                return;
            }
            if (textAddZK.EditValue == null)
            {
                dxErrorProvider.SetError(textAddZK, string.Format(Variables.MandatoryTips, "团体加项最大折扣"));
                textAddZK.Focus();
                return;
            }
                if (creatOAApproValcsDto.AppliState != (int)OAApState.NoAp && 
                (creatOAApproValcsDto.DiscountRate< createClientXKSetDto.DiscountRate || creatOAApproValcsDto.AddDiscountRate< createClientXKSetDto.DiscountRate))
                {
                    MessageBox.Show("未审核记录才能修改！");
                    return;
                }

                creatOAApproValcsDto.ClientInfoId = (Guid)txtClientRegID.EditValue ;
            creatOAApproValcsDto.DiscountRate = decimal.Parse(textZK.EditValue.ToString().Replace("%", ""));
            creatOAApproValcsDto.AddDiscountRate = decimal.Parse(textAddZK.EditValue.ToString().Replace("%",""));
                if (txtSPDoctor.EditValue != null && txtSPDoctor.EditValue !="")
                {
                    creatOAApproValcsDto.ApprovalUserId = (long)txtSPDoctor.EditValue;
                }
            creatOAApproValcsDto.Remark = txtRemark.Text;
            creatOAApproValcsDto.TitleBM = txtBM.Text;
            creatOAApproValcsDto.TitleName = txtTitle.Text;
                if (txtCSDoc.EditValue != null && txtCSDoc.EditValue != "")
                {
                    creatOAApproValcsDto.CCUserId = (long)txtCSDoc.EditValue;
                }
                if (creatOAApproValcsDto.DiscountRate >= createClientXKSetDto.DiscountRate 
                && creatOAApproValcsDto.DiscountRate >= creatOAApproValcsDto.AddDiscountRate)
                {
                    creatOAApproValcsDto.AppliState = (int)OAApState.HasAp;
                }
                else
                {
                    if (txtSPDoctor.EditValue == null )
                    {
                        MessageBox.Show("小于系统折扣，需要选择审批人！");
                        return;
                    }
                    if (txtSPDoctor.EditValue == "")
                    {
                        MessageBox.Show("小于系统折扣，需要选择审批人！");
                        return;
                    }
                    creatOAApproValcsDto.AppliState = (int)OAApState.NoAp;
                }
               
                creatOAApproValcsDto.OKState = (int)OAOKState.NoOK;              
                creatOAApproValcsDto.Applicant = txtappUser.Text;


             var outent = oAApprovalAppService.creatOAApproValcs(creatOAApproValcsDto);
                this.DialogResult = DialogResult.OK;
            });
          
        }

        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtClientRegID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) && txtClientRegID.EditValue !=null)
            {
                txtTitle.Text = txtBM.Text + txtClientRegID.Text;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClientRegID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
          
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtClientRegID.EditValue = null;             

            }
        }

        private void txtSPDoctor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtSPDoctor.EditValue = null;

            }
        }

        private void txtCSDoc_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtCSDoc.EditValue = null;

            }
        }
    }
}
