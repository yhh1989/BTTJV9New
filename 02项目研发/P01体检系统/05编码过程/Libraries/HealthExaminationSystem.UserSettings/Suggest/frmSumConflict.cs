using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest
{
    public partial class frmSumConflict : UserBaseForm
    {
        private readonly Guid _id;
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        public SumConflictDto _Model { get; private set; }
        public frmSumConflict()
        {
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            _Model = new SumConflictDto();
            InitializeComponent();
        }
        public frmSumConflict(Guid id) : this()
        {
            _id = id;

        }
        private void frmSumConflict_Load(object sender, EventArgs e)
        {
           var sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别
            if (_id != null && _id != Guid.Empty)
            {
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = _id;
               var sumconlist = _summarizeAdviceAppService.SearchSumConflict(chargeBM);
              
                if (sumconlist.Count > 0)
                {
                    var sumCon = sumconlist[0];
                    _Model = sumCon;
                    textWord.Text = sumCon.SumWord;
                    if (sumCon.IsSex == 1)
                    {
                        checkSex.Checked = true;
                        gridLookUpSex.EditValue = sumCon.Sex;
                    }
                    else
                    {
                        checkSex.Checked = false;
                        gridLookUpSex.EditValue = null;
                    }
                    if (sumCon.IsAge == 1)
                    {
                        checkAge.Checked = true;
                        teMinAge.EditValue = sumCon.MaxAge;


                    }
                    else
                    {
                        checkAge.Checked = false;
                        teMaxAge.EditValue = sumCon.MaxAge;
                    }

                }
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            _Model.SumWord = textWord.Text;


            if (checkAge.Checked == true)
            {
                _Model.IsAge = 1;
                _Model.MaxAge = int.Parse(teMaxAge.EditValue?.ToString());
                _Model.MinAge = int.Parse(teMinAge.EditValue?.ToString());

            }
            else
            { _Model.IsAge = 0; }
            if (checkSex.Checked == true)
            {
                _Model.IsSex = 1;
                _Model.Sex = (int)gridLookUpSex.EditValue;
            }
            else
            { _Model.IsSex = 0; }

            _Model =  _summarizeAdviceAppService.SaveSumConflict(_Model);
            this.DialogResult = DialogResult.OK;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
