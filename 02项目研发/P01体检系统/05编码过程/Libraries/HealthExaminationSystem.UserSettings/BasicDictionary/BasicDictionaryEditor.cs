using System;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary
{ 
    public partial class BasicDictionaryEditor : UserBaseForm
    {
        private readonly IBasicDictionaryAppService _basicDictionaryAppService;

        private readonly Guid _id;

        private readonly BasicDictionaryType _type;

        private string Word;

        public BasicDictionaryEditor(BasicDictionaryType type)
        {
            InitializeComponent();

            _basicDictionaryAppService = new BasicDictionaryAppService();
            _type = type;
        }

        public BasicDictionaryEditor(Guid id, BasicDictionaryType type) : this(type)
        {
            _id = id;
        }
        public BasicDictionaryEditor(string _word, BasicDictionaryType type) : this(type)
        {
            Word = _word;
            var input = new BasicDictionaryInput { Type = type.ToString() };
            var output = _basicDictionaryAppService.Query(input);
            var maxBm = output.Max(p => p.Value);
            teValue.EditValue = maxBm + 1;
            if (Word.Length < 60)
            {
                teText.Text = Word;
            }
            else
            {
                teText.Text = Word.Substring(0,50);
            }
            meRemarks.Text = Word;

        }

        private void BasicDictionaryEdit_Load(object sender, EventArgs e)
        {
            meRemarks.Properties.MaxLength = 3000;
            if (_id != Guid.Empty)
            {
           
                LoadData();
            }
            var typeName = _type.ToString();
            if ( _type.ToString() == "SumHide")
            {
                layoutControlItem6.Text = "不启用的体检类型编码";
            }
            if (_type.ToString() == "ExaminationType")
            {
                layoutControlItem6.Text = "出报告天数";
            }
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingSaveing);
            try
            {
                if (_type.ToString() == "ExaminationType")
                {
                    if (!string.IsNullOrEmpty(textCode.Text) && 
                        !int.TryParse(textCode.Text,out int days))
                    {
                        MessageBox.Show("出报告天数必须为数值！");
                        return;
                    }
                }
                dxErrorProvider.ClearErrors();
                var valueString = teValue.Text.Trim();
                if (!int.TryParse(valueString, out var value))
                {
                    dxErrorProvider.SetError(teValue, string.Format(Variables.NumberTips, "值"));
                    return;
                }

                var text = teText.Text.Trim();
                if (string.IsNullOrWhiteSpace(text))
                {
                    dxErrorProvider.SetError(teText, string.Format(Variables.MandatoryTips, "文本"));
                    return;
                }

                var remarks = meRemarks.Text.Trim();
                var code = textCode.Text.Trim();

                if (_id == Guid.Empty)
                {
                    var input = new CreateBasicDictionaryDto
                    {
                        Value = value,
                        Text = text,
                        Remarks = remarks,
                        Type = _type.ToString(),
                        Code = code,
                        OrderNum = Convert.ToInt32(txtOrderNum.EditValue)
                    };
                    _basicDictionaryAppService.Add(input);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    var input = new UpdateBasicDictionaryDto
                    {
                        Id = _id,
                        Value = value,
                        Text = text,
                        Remarks = remarks,
                        Type = _type.ToString(),
                        Code = code,
                        OrderNum = Convert.ToInt32(txtOrderNum.EditValue)

                    };
                    _basicDictionaryAppService.Edit(input);
                    DialogResult = DialogResult.OK;
                }
            }
            catch (UserFriendlyException exception)
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                    ShowMessageBox(exception);
                }
               
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                var data = _basicDictionaryAppService.Get(new EntityDto<Guid> { Id = _id });
                teValue.Text = data.Value.ToString();
                teText.Text = data.Text;
                meRemarks.Text = data.Remarks;
                textCode.Text = data.Code;
                teValue.ReadOnly = true;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        private void textCode_EditValueChanged(object sender, EventArgs e)
        {

        }
        public bool isadd = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButtonSave.PerformClick();
               isadd = true;
        }
    }
}