using Abp.Application.Services.Dto;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmHistory : UserBaseForm
    {
        private ICustomerAppService customerSvr;//体检预约
        private Guid customerregid;
        private Guid mclientregid;

        /// <summary>
        /// 医生站
        /// </summary>
        private  IDoctorStationAppService _doctorStation;

        public FrmHistory()
        {
            InitializeComponent();
        }
        public FrmHistory(Guid cusregid,Guid clientregid)
        {
            InitializeComponent();
            customerregid = cusregid;
            mclientregid = clientregid;

        }
        private void FrmHistory_Load(object sender, EventArgs e)
        {
            _doctorStation = new DoctorStationAppService();
            customerSvr = new CustomerAppService();
            //加载1+X问卷
            LoadData();
            //加载1+X问卷
            if (customerregid != Guid.Empty)
            {
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                entityDto.Id = customerregid;
                InitialUpdate(entityDto);               
            }

        }
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
                var result = customerSvr.getOneAddXQuestionnaires();
                splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
                layoutControlGroupBase.BeginUpdate();
                foreach (var ques in result)
                {
                    var checkEdit = new CheckEdit
                    {
                        Text = ques.Name
                    };
                    checkEdit.Name = ques.Id.ToString("N");
                    if (ques.Category == "家族史")
                    {
                        checkEdit.Width = 150;
                        flowfamily.Controls.Add(checkEdit);
                    }
                    else if (ques.Category == "现病史")
                    {
                        checkEdit.Width = 150;
                        flownow.Controls.Add(checkEdit);
                    }
                   else  if (ques.Category == "既往史")
                    {
                        checkEdit.Width = 150;
                        flownowill.Controls.Add(checkEdit); 
                    }
                    else if (ques.Category == "手术史")
                    {
                        checkEdit.Width = 200;
                        flowShoushu.Controls.Add(checkEdit);
                    }
                    else if (ques.Category == "体检史")
                    {
                        checkEdit.Width = 150;
                        flowTijian.Controls.Add(checkEdit);
                    }
                    else if (ques.Category == "月经史")
                    {
                        checkEdit.Width = 100;
                        flowYujing.Controls.Add(checkEdit);
                    }
                    checkEdit.CheckedChanged += rad_CheckedChanged;
                }

                layoutControlGroupBase.EndUpdate();
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
        private void InitialUpdate(EntityDto<Guid> input)
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

                var modules = customerSvr.getCustomerQuestionDtos(input);
                splashScreenManager.SetWaitFormDescription(Variables.LoadingEditor);
                foreach (var module in modules)
                {
                    var name = module.OneAddXQuestionnaireid.Value.ToString("N");
                    if (module.QuestionType == "家族史")
                    {                       
                        var item = flowfamily.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "现病史")
                    {                       
                        var item = flownow.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "既往史")
                    {                       
                        var item = flownowill.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "手术史")
                    {
                        var item = flowShoushu.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "体检史")
                    {
                        var item = flowTijian.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "月经史")
                    {
                        var item = flowYujing.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                }

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
        
        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.CheckEdit checkedit = (DevExpress.XtraEditors.CheckEdit)sender;
                string Pname = checkedit.Parent.Name;
                MemoEdit memoEdit = new MemoEdit();
                switch (Pname)
                {
                    case "flowfamily":
                        memoEdit = memJiazu;
                        break;
                    case "flownow":
                        memoEdit = memXianbing;
                        break;
                    case "flownowill":
                        memoEdit = memJIwangshi;
                        break;
                    case "flowShoushu":
                        memoEdit = memshoushushi;
                        break;
                    case "flowTijian":
                        memoEdit = memTijianshi;
                        break;
                    case "flowYujing":
                        memoEdit = memYujingshi;
                        break;
                        
                }
                TextShow(checkedit.Text, memoEdit, checkedit.Checked);


            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }

        }
        private void TextShow(string sname, MemoEdit memoEdit, bool ischeck)
        {
            if (ischeck == true)
            {
                memoEdit.Text += sname + "；";               
            }
            else
            {
                memoEdit.Text = memoEdit.Text.Replace(sname + "；","");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<SaveCustomerQusTionDto> input = new List<SaveCustomerQusTionDto>();
                List<SaveCustomerQusTionDto> ls1 = getSaveCustomerQusTion(flowfamily, input);
                List<SaveCustomerQusTionDto> ls2 = getSaveCustomerQusTion(flownow, ls1);
                List<SaveCustomerQusTionDto> ls3 = getSaveCustomerQusTion(flownowill, ls2);
                List<SaveCustomerQusTionDto> ls4 = getSaveCustomerQusTion(flowShoushu, ls3);
                List<SaveCustomerQusTionDto> ls5 = getSaveCustomerQusTion(flowTijian, ls4);
                List<SaveCustomerQusTionDto> ls6 = getSaveCustomerQusTion(flowYujing, ls5);

                _doctorStation.SaveCustomerQuestion(ls6);
                MessageBox.Show("保存成功！");
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

        }
        private List<SaveCustomerQusTionDto> getSaveCustomerQusTion(FlowLayoutPanel flowLayoutPanel, List<SaveCustomerQusTionDto> quesList)
        {
           
            foreach (Control con in flowLayoutPanel.Controls)
            {
                DevExpress.XtraEditors.CheckEdit checkedit = (DevExpress.XtraEditors.CheckEdit)con;
                if (checkedit.Checked == true)
                {
                    SaveCustomerQusTionDto saveCustomerQusTionDto = new SaveCustomerQusTionDto();
                    saveCustomerQusTionDto.ClientRegId = mclientregid;
                    saveCustomerQusTionDto.CustomerRegId = customerregid;
                    saveCustomerQusTionDto.OneAddXQuestionnaireid = new Guid(con.Name);
                    quesList.Add(saveCustomerQusTionDto);
                }
            }
            return quesList;
        }
    }
}
