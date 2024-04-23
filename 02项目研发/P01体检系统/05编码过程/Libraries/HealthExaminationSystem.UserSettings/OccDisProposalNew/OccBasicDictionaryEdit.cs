using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Abp.Application.Services.Dto;
using System.Data;
using Sw.Hospital.HealthExaminationSystem.Common.CommonFormat;
using System.Web.UI.WebControls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew
{
    public partial class OccBasicDictionaryEdit : UserBaseForm
    {
        private readonly IOccDisProposalNewAppService   _OccDisProposalNewAppService;
        private readonly ICommonAppService _commonAppService = new CommonAppService();

        private string isWZ = "";
      
        private readonly Guid _id;

        public OccBasicDictionaryEdit()
        {
            InitializeComponent();

            _OccDisProposalNewAppService = new OccDisProposalNewAppService();

        }
        public OccBasicDictionaryEdit(string _iswh):this()
        {
            isWZ = _iswh;

        }
        private void OccBasicDictionaryEdit_Load(object sender, EventArgs e)
        {
          
            var parentId = OccBasicDictionaryList.ParentIds;
            var Name = OccBasicDictionaryList.Names;
            var key = OccBasicDictionaryList.keys;
            if (isWZ == "1")
            {
                  parentId = OccdieaseConsultation.ParentIds;
                  Name = OccdieaseConsultation.Names;
                  key = OccdieaseConsultation.keys;
                textEdit3.Text = "0";
            }

            if (parentId == "")
            {
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = parentId;
                var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                comboBoxEdit1.Properties.DataSource = dl;
            }
            if (_id != Guid.Empty)
            {
                LoadData();
            }
        }
        public OccBasicDictionaryEdit(Guid id) : this()
        {
            _id = id;
        }
        
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProvider.ClearErrors();
                TbmOccDictionaryDto dto = new TbmOccDictionaryDto();
                dto.Text = textEdit1.Text.Trim();
                dto.IsActive = radioGroup2.SelectedIndex;
                dto.HelpChar = textEdit2.Text.Trim();
                dto.Remarks = memoEdit1.Text.Trim();
                dto.OrderNum = Convert.ToInt32(textEdit3.Text);
                dto.code = textEditCode.Text.Trim();
                var Key = OccBasicDictionaryList.keys;
                var Name = OccBasicDictionaryList.Names;
                if (isWZ == "1")
                {
                    
                    Name = OccdieaseConsultation.Names;
                    Key = OccdieaseConsultation.keys;
                }
                dto.Type = Key;
                if (comboBoxEdit1.EditValue != null)
                {
                    dto.ParentId = (Guid)comboBoxEdit1.EditValue;
                }
                if (_id == Guid.Empty)
                {
                    var addresult = _OccDisProposalNewAppService.Add(dto);
                    if (addresult != null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    dto.Id = _id;
                    var editresult = _OccDisProposalNewAppService.Edit(dto);
                    if (editresult != null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }
        private void LoadData()
        {
            try
            {
                var parentId = OccBasicDictionaryList.ParentIds;
                var Name = OccBasicDictionaryList.Names;
                if (isWZ == "1")
                {
                    parentId = OccdieaseConsultation.ParentIds;
                    Name = OccdieaseConsultation.Names;
                    
                }
                var data = _OccDisProposalNewAppService.GetOccDictionarys(new EntityDto<Guid> { Id = _id });
                textEdit1.EditValue = data.Text; 
                textEdit2.EditValue = data.HelpChar;
                comboBoxEdit1.EditValue = data.ParentId;
                radioGroup2.SelectedIndex = data.IsActive;
                memoEdit1.EditValue = data.Remarks;
                textEdit3.EditValue = data.OrderNum;
                textEditCode.EditValue = data.code;
                //data.OrderNum = data.OrderNum;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        private void textEdit2_Leave(object sender, EventArgs e)
        {      
                if (!string.IsNullOrWhiteSpace(textEdit2.Text))
                    return;
                var name = textEdit1.Text.Trim();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    try
                    {
                        var result = _commonAppService.GetHansBrief(new Application.Common.Dto.ChineseDto { Hans = name });
                        textEdit2.Text = result.Brief;
                    }
                    catch (ApiProxy.UserFriendlyException exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
                else
                {
                    textEdit2.Text = string.Empty;
                }
            
        }
    }

}
