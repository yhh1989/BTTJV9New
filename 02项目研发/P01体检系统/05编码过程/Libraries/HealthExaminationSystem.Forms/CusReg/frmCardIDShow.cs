using Abp.Application.Services.Dto;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
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
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class frmCardIDShow : UserBaseForm
    {
        public List<QueryCustomerRegDto> Datas;
        public event Action<QueryCustomerRegDto> SelectData;
        public string cusName="";
        public int sex = 0;
        public int age = 0;
        public Image Image;
        public int butType = 0;
        public string dress = "";
        public string IdCard = "";
        public string OldName = "";
        public string OldIDCardNo = "";
        private IChargeAppService _chargeAppService { get; set; }
        private readonly IClientRegAppService _clientReg = new ClientRegAppService();
        private readonly ICommonAppService _commonAppService;
        public frmCardIDShow()
        {
            _commonAppService = new CommonAppService();
            _chargeAppService = new ChargeAppService();
            InitializeComponent();
          
        }

        private void frmCardIDShow_Load(object sender, EventArgs e)
        {
            var sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别
            var marrySateList = MarrySateHelper.GetMarrySateModelsForItemInfo();
            gridLookUpMarriageStatus.Properties.DataSource = marrySateList;//婚否
            gridControl1.DataSource = Datas;
            if (!string.IsNullOrEmpty(cusName))
            {
                if (Image != null)
                {
                    pictureCus.Image = Image;
                }
                txtName.EditValue = cusName;
                txtAge.EditValue = age;
                //gridLookUpSex.EditValue = sex;
               // MessageBox.Show(sex.ToString());
                txtIDCardNo.EditValue = IdCard;
                txtAdress.EditValue = dress;
                var data = VerificationHelper.GetByIdCard(IdCard);
                if (data != null)
                {                  
                    gridLookUpSex.EditValue = (int)data.Sex;                   
                }
                //txtAge.EditValue = age;
            }
            else
            {
                txtName.EditValue = Datas[0].Customer.Name;
                txtAge.EditValue = Datas[0].Customer.Age;
                gridLookUpSex.EditValue = Datas[0].Customer.Sex;
                txtIDCardNo.EditValue = Datas[0].Customer.IDCardNo;
                txtAdress.EditValue = Datas[0].Customer.Address;
            }
            txtMobile.EditValue = Datas[0].Customer.Mobile;
            gridLookUpMarriageStatus.EditValue = Datas[0].MarriageStatus;

            gridView1.Columns[CheckSate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CheckSate.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);

            gridView1.Columns[CostState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[CostState.FieldName].DisplayFormat.Format = new CustomFormatter(CostStateHelper.CostStateFormatter);
            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[SendToConfirm.FieldName].DisplayFormat.Format = new CustomFormatter(SendToConfirmHelper.SendToConfirmFormatter);

            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var data = gridView1.GetFocusedRow() as QueryCustomerRegDto;
            SelectData?.Invoke(data);
            butType = 3;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.Cancel;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName == colSex.FieldName)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((Sex)e.Value);
            }           
            if (e.Column.FieldName == colSummSate.FieldName)
            {
                if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                {
                    e.DisplayText = EnumHelper.GetEnumDesc((SummSate)e.Value);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(OldIDCardNo) && OldIDCardNo != txtIDCardNo.Text) ||
                (!string.IsNullOrEmpty(OldName) && OldName != txtName.Text))
            {
                string ts = "姓名或身份证号不符！不能修改，如果需要修改，请清空登记界面姓名和身份证信息！";
                MessageBox.Show(ts);

                return;
            }
                QueryCustomerRegDto queryCustomerRegDto = new QueryCustomerRegDto();
            queryCustomerRegDto.Customer = new QueryCustomerDto();
            queryCustomerRegDto.Customer.Name = txtName.Text;
            queryCustomerRegDto.Customer.Sex = (int)gridLookUpSex.EditValue;
            queryCustomerRegDto.Customer.Age = (int)txtAge.EditValue;
            queryCustomerRegDto.Customer.Address = txtAdress.Text;
            queryCustomerRegDto.Customer.IDCardNo = txtIDCardNo.Text;
            if (checkBox1.Checked == true)
            {
                
                queryCustomerRegDto.Customer.Mobile = txtMobile.Text;
                if (gridLookUpMarriageStatus.EditValue != null && gridLookUpMarriageStatus.EditValue.ToString() != "")
                {
                    queryCustomerRegDto.Customer.MarriageStatus = (int)gridLookUpMarriageStatus.EditValue;

                }
            }
            SelectData?.Invoke(queryCustomerRegDto);
               
            
            butType = 1;
            DialogResult = DialogResult.OK;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QueryCustomerRegDto queryCustomerRegDto = new QueryCustomerRegDto();
            queryCustomerRegDto.Customer = new QueryCustomerDto();
            queryCustomerRegDto.Customer.Name = txtName.Text;
            queryCustomerRegDto.Customer.Sex = (int)gridLookUpSex.EditValue;
            queryCustomerRegDto.Customer.Age = (int)txtAge.EditValue;
            queryCustomerRegDto.Customer.Address = txtAdress.Text;
            queryCustomerRegDto.Customer.IDCardNo = txtIDCardNo.Text;
            if (checkBox1.Checked == true)
            {

                queryCustomerRegDto.Customer.Mobile = txtMobile.Text;

                //queryCustomerRegDto.Customer.MarriageStatus = (int)gridLookUpMarriageStatus.EditValue;
                if (gridLookUpMarriageStatus.EditValue != null && gridLookUpMarriageStatus.EditValue.ToString() != "")
                {
                    queryCustomerRegDto.Customer.MarriageStatus = (int)gridLookUpMarriageStatus.EditValue;

                }


            }
            SelectData?.Invoke(queryCustomerRegDto);
            butType = 2;
            DialogResult = DialogResult.OK;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            var curCustomRegInfo = gridView1.GetFocusedRow() as QueryCustomerRegDto;
            if (curCustomRegInfo != null)
            {
                if (curCustomRegInfo.Id != Guid.Empty)
                {
                    EntityDto<Guid> cusName = new EntityDto<Guid>();
                    cusName.Id = curCustomRegInfo.Id;
                    CustomerRegCostDto customerReg = _chargeAppService.GetsfState(cusName);
                    if (customerReg.CostState != (int)PayerCatType.NoCharge)
                    {
                        MessageBox.Show("该体检人已收费不能删除！");
                        return;
                    }
                    if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
                    {
                        ShowMessageBoxInformation(string.Format("{0}已登记，无法删除！", curCustomRegInfo.Customer.Name));
                        return;
                    }
                    if (curCustomRegInfo.CheckSate != (int)ExaminationState.Alr)
                    {
                        ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", curCustomRegInfo.Customer.Name));
                        return;
                    }
                    DialogResult dr = XtraMessageBox.Show("是否删除该体检人？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        curCustomRegInfo.RegisterState = (int)RegisterState.No;
                        try
                        {
                            //操作数据库
                            AutoLoading(() =>
                            {
                                List<EntityDto<Guid>> listGuid = new List<EntityDto<Guid>>();
                                listGuid.Add(cusName
                                    );
                                _clientReg.DelCustomerReg(listGuid);
                            });
                            MessageBox.Show("删除成功！");
                            var dt = gridView1.DataSource as List<QueryCustomerRegDto>;
                            dt.Remove(curCustomRegInfo);
                            gridView1.RefreshData();
                          
                            //日志
                            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                            createOpLogDto.LogBM = curCustomRegInfo.CustomerBM;
                            createOpLogDto.LogName = curCustomRegInfo.Customer.Name;
                            createOpLogDto.LogText = "删除人员";
                            createOpLogDto.LogDetail = "";
                            createOpLogDto.LogType = (int)LogsTypes.ClientId;
                            _commonAppService.SaveOpLog(createOpLogDto);
                            curCustomRegInfo = null;
                             
                            return;
                        }
                        catch (UserFriendlyException ex)
                        {
                            curCustomRegInfo.RegisterState = (int)RegisterState.Yes;
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    else
                        return;

                }
            }
            ShowMessageBoxInformation("当前数据未保存不能取消删除！");
        }
    }
}
