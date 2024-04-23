using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmClientTeamAdd : UserBaseForm
    {
        private readonly IItemSuitAppService _itemSuitAppService;
        private readonly IClientRegAppService _clientRegAppService;
        private readonly IIDNumberAppService _IDNumberAppService;
        private readonly Guid client_Id;
        private readonly Guid Team_Id;      
        public CreateClientTeamInfoesDto TeamInfoes;
        public List<CreateClientTeamInfoesDto> TeamInfoesList;
        
        public int teamCode; //默认分组编码
        /// <summary>
        /// 判断保存状态
        /// </summary>
        public int EditMode;

        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;

        public FrmClientTeamAdd()
        {
            InitializeComponent();
            _itemSuitAppService = new ItemSuitAppService();
            _clientRegAppService = new ClientRegAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            TeamLoad();
        }
        public FrmClientTeamAdd(Guid id, ClientTeamInfosDto dto)
        {
            InitializeComponent();
            client_Id = id;
            _itemSuitAppService = new ItemSuitAppService();
            _clientRegAppService = new ClientRegAppService();
            _IDNumberAppService = new IDNumberAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            //Risks = Risks;
            Team_Id = Guid.NewGuid();
        }
        /// <summary>
        /// 编辑赋值
        /// </summary>
        /// <param name="dto"></param>
        public void TeamInfoItem(CreateClientTeamInfoesDto dto)
        {
            txtTeamName.Text = dto.TeamName;
            txtTeamBM.Text = dto.TeamBM.ToString();

            txtMaritalStatus.EditValue = Enum.Parse(typeof(MarrySate), dto.MaritalStatus.ToString()); //Convert.ToInt32(dto.MaritalStatus);
            if (dto.Sex == 1)
            {
                txtSex.SelectedIndex = 0;
            }
            else if (dto.Sex == 2)
            {
                txtSex.SelectedIndex = 1;
            }
            else
            {
                txtSex.SelectedIndex = 2;
            }

            txtMinAge.Text = dto.MinAge.ToString();
            txtMaxAge.Text = dto.MaxAge.ToString();
            txtConceiveStatus.EditValue = Enum.Parse(typeof(BreedState), dto.ConceiveStatus.ToString());
            txtExamPlace.EditValue = Enum.Parse(typeof(ExamPlace), dto.ExamPlace.ToString());
            //txtPersonAmount.Text = dto.PersonAmount.ToString();
            txtTJType.EditValue = dto.TJType;
            if (dto.BreakfastStatus == 2)
            {
                txtBreakfastStatus.Checked = true;
            }
            if (dto.EmailStatus == 2)
            {
                checkEmailStatus.Checked = true;
            }
            if (dto.BlindSate == 2)
            {
                checkBlindSate.Checked = true;
            }
            if (dto.MessageStatus == 2)
            {
                checkMessageStatus.Checked = true;
            }
            if (dto.HealthyMGStatus == 2)
            {
                checkHealthyMGStatus.Checked = true;
            }
            if (dto.Locking == 2)
            {
                checkLocking.Checked = true;
            }
            //职业相关
            txtRiskName.Text = dto.RiskName;
            if (dto.ClientTeamRisk != null && dto.ClientTeamRisk.Count > 0)
            {
                txtRiskName.Tag = dto.ClientTeamRisk;
            }      
            txtOPostState_Id.Text = dto.CheckType?.ToString();
            txtWorkShop.Text = dto.WorkShop;
            txtWorkType.Text = dto.WorkType;


        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getEditValue();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        private void getEditValue()
        {
            dxErrorProvider.ClearErrors();


            var TeamName = txtTeamName.Text.Trim();
            if (string.IsNullOrWhiteSpace(TeamName))
            {
                dxErrorProvider.SetError(txtTeamName, string.Format(Variables.MandatoryTips, "名称"));
                txtTeamName.Focus();
                return;
            }
            var OPostState = txtOPostState_Id.Text.Trim();

            var TeamBM = txtTeamBM.Text.Trim();
            if (string.IsNullOrWhiteSpace(TeamBM))
            {
                dxErrorProvider.SetError(txtTeamBM, string.Format(Variables.MandatoryTips, "编码"));
                txtTeamBM.Focus();
                return;
            }
            var Sex = txtSex.EditValue;

            var MaritalStatus = txtMaritalStatus.EditValue;
            if (MaritalStatus == null)
            {
                dxErrorProvider.SetError(txtMaritalStatus, string.Format(Variables.MandatoryTips, "婚姻状态"));
                txtMinAge.Focus();
                return;
            }
            var MinAge = txtMinAge.Text.Trim();
            if (string.IsNullOrWhiteSpace(MinAge))
            {
                dxErrorProvider.SetError(txtMinAge, string.Format(Variables.MandatoryTips, "最小年龄"));
                txtMinAge.Focus();
                return;
            }
            var MaxAge = txtMaxAge.Text.Trim();
            if (string.IsNullOrWhiteSpace(MaxAge))
            {
                dxErrorProvider.SetError(txtMaxAge, string.Format(Variables.MandatoryTips, "最大年龄"));
                txtMaxAge.Focus();
                return;
            }
            var RiskName = txtRiskName.Text.Trim();

            var ExamPlace = txtExamPlace.EditValue;
            if (ExamPlace == null)
            {
                dxErrorProvider.SetError(txtExamPlace, string.Format(Variables.MandatoryTips, "体检场所"));
                txtExamPlace.Focus();
                return;
            }
            var ConceiveStatus = txtConceiveStatus.EditValue;
            //var PersonAmount = txtPersonAmount.Text.Trim();
            //if (string.IsNullOrWhiteSpace(PersonAmount))
            //{
            //    dxErrorProvider.SetError(txtPersonAmount, string.Format(Variables.MandatoryTips, "人数"));
            //    txtPersonAmount.Focus();
            //    return;
            //}
            var BreakfastStatus = txtBreakfastStatus.EditValue;
            var MessageStatus = checkMessageStatus.EditValue;
            var EmailStatus = checkEmailStatus.EditValue;
            var HealthyMGStatus = checkHealthyMGStatus.EditValue;
            var Locking = checkLocking.EditValue;
            var BlindSate = checkBlindSate.EditValue;
            var TJType = txtTJType.EditValue;
            if (TJType == null)
            {
                dxErrorProvider.SetError(txtTJType, string.Format(Variables.MandatoryTips, "体检类型"));
                txtTJType.Focus();
                return;
            }

            if (EditMode == (int)EditModeType.Edit)
            {
                TeamInfoes.Id = TeamInfoes.Id;
                TeamInfoes.ClientRegId = TeamInfoes.ClientRegId;
                TeamInfoes.CostType = TeamInfoes.CostType;
            }
            else
            {
                TeamInfoes.Id = Team_Id;
                TeamInfoes.ClientRegId = client_Id;
                TeamInfoes.EditModle = false;
                TeamInfoes.CostType = 3;
            }
            TeamInfoes.TeamBM = Convert.ToInt32(TeamBM);
            TeamInfoes.TeamName = TeamName;
            TeamInfoes.Sex = (int)Sex;
            TeamInfoes.MinAge = Convert.ToInt32(MinAge);
            TeamInfoes.MaxAge = Convert.ToInt32(MaxAge);
            TeamInfoes.MaritalStatus = (int)MaritalStatus;
            //职业相关

            if (txtOPostState_Id.EditValue != null)
            {
                TeamInfoes.CheckType = txtOPostState_Id.EditValue.ToString();
            }
            else
            { TeamInfoes.CheckType = ""; }
            if (txtWorkShop.EditValue != null)
            {
                TeamInfoes.WorkShop = txtWorkShop.EditValue.ToString();
            }
            else
            {
                TeamInfoes.WorkShop = "";
            }
            if (txtWorkType.EditValue != null)
            {
                TeamInfoes.WorkType = txtWorkType.EditValue.ToString();
            }
            else
            { TeamInfoes.WorkType = ""; }
            if (txtRiskName.Tag != null)
            {
                TeamInfoes.RiskName = RiskName;
                TeamInfoes.ClientTeamRisk = (List<Guid> )txtRiskName.Tag;
            }
            TeamInfoes.ExamPlace = ((int)ExamPlace).ToString();
            TeamInfoes.ConceiveStatus = ConceiveStatus == null ? 0 : (int)ConceiveStatus;
            //TeamInfoes.PersonAmount = Convert.ToInt32(PersonAmount);
           
            TeamInfoes.BreakfastStatus = BreakfastStatus == null ? 1 : (bool)BreakfastStatus == true ? 2 : 3;
            TeamInfoes.MessageStatus = MessageStatus == null ? 1 : (bool)MessageStatus == true ? 2 : 3;
            TeamInfoes.EmailStatus = EmailStatus == null ? 1 : (bool)EmailStatus == true ? 2 : 3;
            TeamInfoes.HealthyMGStatus = HealthyMGStatus == null ? 1 : (bool)HealthyMGStatus == true ? 2 : 3;
            TeamInfoes.BlindSate = BlindSate == null ? 1 : (bool)BlindSate == true ? 2 : 3;
            TeamInfoes.Locking = Locking == null ? 1 : (bool)Locking == true ? 2 : 3;
            TeamInfoes.TJType = (int)TJType;
            //bug3334 【单位预约】分组编码可以修改，修改性别和此分组下套餐性别有冲突的没有提示
            if (TeamInfoes.Sex != (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified)
            {
                var suit = DefinedCacheHelper.GetItemSuit().FirstOrDefault(o => o.Id == TeamInfoes.ItemSuit_Id);
                if (suit != null)
                    if (suit.Sex != TeamInfoes.Sex && suit.Sex != (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified)
                    {
                        ShowMessageBoxWarning("当前选择性别与已选套餐性别不一致，请重新选择。");
                        return;
                    }
            }
            try
            {
                //if (EditMode == (int)EditModeType.Add) 
                //    _clientRegAppService.CreateTeamInfos(TeamInfoes);
                //if (EditMode == (int)EditModeType.Edit)
                //    _clientRegAppService.EditTeam(TeamInfoes);
                if (TeamInfoesList != null)
                {
                    var TeamCount = TeamInfoesList.Where(o => o.TeamName == TeamInfoes.TeamName);
                    if (TeamCount.Count() > 0)
                    {
                        ShowMessageSucceed("分组名称不可重复！");
                        return;
                    }
                    else
                    {
                        ShowMessageSucceed("保存成功！");
                        SaveDataComplate?.Invoke(TeamInfoes);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    ShowMessageSucceed("保存成功！");
                    SaveDataComplate?.Invoke(TeamInfoes);
                    this.DialogResult = DialogResult.OK;
                }




            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }

        /// <summary>
        /// 回传事件
        /// </summary>
        public event Action<CreateClientTeamInfoesDto> SaveDataComplate;


        private void FrmClientTeamAdd_Load(object sender, EventArgs e)
        {

            if (EditMode == (int)EditModeType.Edit)
            {
                TeamInfoItem(TeamInfoes);
            }
            var list = _itemSuitAppService.QuerySimples(new SearchItemSuitDto { });
            if (EditMode == (int)EditModeType.Add)
            {
                txtTeamBM.Text = teamCode.ToString();
                TeamInfoes = new CreateClientTeamInfoesDto();
                TeamLoad();

                txtConceiveStatus.EditValue = BreedState.No;
                txtMaritalStatus.EditValue = MarrySate.Unstated;
                txtExamPlace.ItemIndex = 0;
                txtTJType.ItemIndex = 0;
            }
        }
        /// <summary>
        /// 下拉框数据绑定
        /// </summary>
        private void TeamLoad()
        {
            txtExamPlace.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ExamPlace));
            txtExamPlace.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

          var exam=  DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ExaminationType);
            if (Variables.ISZYB == "2")
            {
                exam = exam.Where(o=>o.Text.Contains("职业")).ToList();
            }
            txtTJType.Properties.DataSource = exam;
            txtTJType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            txtConceiveStatus.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(BreedState));
            txtConceiveStatus.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            txtMaritalStatus.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(MarrySate));
            //txtMaritalStatus.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

        }
        #region 清除按钮
        private void txtConceiveStatus_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtConceiveStatus.EditValue = null;
        }
        private void txtTJType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                txtTJType.EditValue = null;
            }
        }

        private void txtExamPlace_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                txtExamPlace.EditValue = null;
            }
        }

        #endregion

        private void txtTJType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                txtTJType.EditValue = null;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void but_Click(object sender, EventArgs e)
        {
            frmSelectHazard frmSelectHazard = new frmSelectHazard();            
            if (txtRiskName.Tag != null && txtRiskName.Text != "")
            {
                var riskls = (List<Guid>)txtRiskName.Tag;
                frmSelectHazard.outRisck = riskls;

            }
            if (frmSelectHazard.ShowDialog() == DialogResult.OK)
            {
                txtRiskName.Tag = null;
                var Hazard= frmSelectHazard.outOccHazardFactors;
                txtRiskName.Tag = Hazard.Select(o => o.Id).ToList(); 
                txtRiskName.Text = string.Join("，", Hazard.Select(o=>o.Text).ToList()).TrimEnd('，');
            }

        }

        private void txtTJType_EditValueChanged(object sender, EventArgs e)
        {
            if ((Variables.ISZYB == "1" && txtTJType.EditValue != null && txtTJType.Text.ToString().Contains("职业")) || Variables.ISZYB == "2")
            {
                //检查类型
                ChargeBM chargeBM = new ChargeBM();
                
                chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
                var lis1= _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtOPostState_Id.Properties.DataSource = lis1;
                //车间
                chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                var lis2 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtWorkShop.Properties.DataSource = lis2;
                //工种
                chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
                var lis3 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtWorkType.Properties.DataSource = lis3;
                groupzyb.Visibility = LayoutVisibility.Always;
            }
            else
            {
                groupzyb.Visibility = LayoutVisibility.Never;
            }
            
        }
    }
}
