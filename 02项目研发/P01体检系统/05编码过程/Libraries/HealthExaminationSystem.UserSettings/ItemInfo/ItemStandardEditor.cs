using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class ItemStandardEditor : UserBaseForm
    {
        ItemInfoAppService itemInfoService = new ItemInfoAppService();
        public ItemStandardDto editDto = null;
        private List<IllnessSateModel> illnessList;
        private List<IllnessLevelModel> illnessLevel;
        public Guid ItemId{get;set;}
        public string ItemName { get; set; }
        public ItemStandardEditor()
        {
            InitializeComponent();
            illnessList = IllnessSateHelp.GetIfTypeModels();
            illnessList.Insert(0, new IllnessSateModel());
            illnessLevel = IllnessLevelHelp.GetIfTypeModels();
            illnessLevel.Insert(0,new IllnessLevelModel());
            LookUpEditBind();
        }

        public ItemStandardEditor(ItemStandardDto input)
        {
            InitializeComponent();
            illnessList = IllnessSateHelp.GetIfTypeModels();
            illnessList.Insert(0,new IllnessSateModel ());
            illnessLevel = IllnessLevelHelp.GetIfTypeModels();
            illnessLevel.Insert(0, new IllnessLevelModel());
            LookUpEditBind();
            editDto = input;
        }

        private void btn_baocun_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (num_zxz.Value > num_zdz.Value)
            {
                dxErrorProvider.SetError(num_zxz, string.Format(Variables.GreaterThanTips, "最小","最大"));
                return;
            }
            try
            {
                ItemStandardDto addInput = new ItemStandardDto();
                addInput.CheckType = (int)lue_jgpdzt.EditValue;
                addInput.IsNormal = (int)lue_bszt.EditValue;
                addInput.MaxAge = int.Parse(num_zdnl.Text.Trim());
                addInput.MaxValue = num_zdz.Value;
                addInput.MinAge = int.Parse(num_zxnl.Text.Trim());
                addInput.MinValue = num_zxz.Value;
                addInput.OrderNum = (int)num_paixu.Value;
                addInput.Period = txt_qijian.Text.Trim();
                addInput.PositiveSate = (int)lue_jgyc.EditValue;
                addInput.Sex = lue_xingbie.EditValue.ToString();
                addInput.Summ = txt_jielun.Text.Trim();
                addInput.Symbol = txt_JibingZhuangtai.EditValue?.ToString();
                addInput.ItemId = ItemId;
                if (!string.IsNullOrEmpty(lookUpEditClientType.EditValue?.ToString()))
                {
                    addInput.PhysicalType = (int)lookUpEditClientType.EditValue;
                }
                if (!string.IsNullOrWhiteSpace(lue_IllLevel.EditValue?.ToString()))
                {
                    addInput.IllnessLevel = (int)(lue_IllLevel.EditValue ?? 0);
                }
                if (editDto != null)
                {
                    addInput.Id = editDto.Id;
                    var editResult = itemInfoService.EditItemStandard(addInput);
                    if (editResult != null)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    var addRsult = itemInfoService.AddItemStandard(addInput);
                    if (addRsult != null)
                    {
                        editDto=addRsult;
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
            
        }

        /// <summary>
        /// 下拉框绑定数据
        /// </summary>
        private void LookUpEditBind()
        {
            var xingbieList = SexHelper.GetSexModelsForItemInfo();
            lue_xingbie.Properties.DataSource = xingbieList;
            lue_xingbie.EditValue = (int)Sex.GenderNotSpecified;

            var leibieList = AbnormalResultsHelper.GetAbnormalResultsModels();
            lue_jgyc.Properties.DataSource = leibieList;
            lue_jgyc.EditValue = leibieList.FirstOrDefault().Id;

            var zhuangtaiList = SymbolHelper.GetLabelingStateModels();
            lue_bszt.Properties.DataSource = zhuangtaiList;
            lue_bszt.EditValue = zhuangtaiList.FirstOrDefault().Id;

            var jieguoList = ResultJudgementStateHelper.GetResultJudgementStateModels();
            lue_jgpdzt.Properties.DataSource = jieguoList;
            lue_jgpdzt.EditValue = jieguoList.FirstOrDefault().Id;

            txt_JibingZhuangtai.Properties.DataSource = illnessList;
            //txt_JibingZhuangtai.EditValue = (int)IllnessSate.Diagnosis;

            lue_IllLevel.Properties.DataSource = illnessLevel;
             
        }

        private void FrmItemStandardInfo_Load(object sender, EventArgs e)
        {
            //体检类别
            var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            if (Variables.ISZYB == "2")
            {
                checktype = checktype.Where(o => o.Text.Contains("职业")).ToList();
            }
            lookUpEditClientType.Properties.DataSource = checktype;
            txt_ssxm.Text = ItemName.ToString();
            if (editDto != null)
            {
                lue_jgpdzt.EditValue = editDto.CheckType;
                lue_bszt.EditValue = editDto.IsNormal;
                num_zdnl.Text = editDto.MaxAge.ToString();
                num_zdz.Value = (decimal)editDto.MaxValue;
                num_zxnl.Text = editDto.MinAge.ToString();
                num_zxz.Value = (decimal)editDto.MinValue;
                num_paixu.Value = (decimal)editDto.OrderNum;
                txt_qijian.Text = editDto.Period;
                lue_jgyc.EditValue = editDto.PositiveSate;
                lue_xingbie.EditValue =int.Parse(editDto.Sex);
                txt_jielun.Text = editDto.Summ;
                if (!string.IsNullOrEmpty(editDto.Symbol))
                {
                    txt_JibingZhuangtai.EditValue = int.Parse(editDto.Symbol);
                }
                lue_IllLevel.EditValue=editDto.IllnessLevel;
                if ( editDto.PhysicalType.HasValue)
                {
                    lookUpEditClientType.EditValue = editDto.PhysicalType;
                }
            }
        }

        private void btn_quxiao_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lookUpEditClientType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;              

            }
        }
    }
}