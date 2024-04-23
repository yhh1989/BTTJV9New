using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.ORiskFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Core.Illness.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.TargetDisease
{
    public partial class TargetDisease : UserBaseForm
    {
        /// <summary>
        /// 目标疾病
        /// </summary>
        private readonly ITargetDiseaseAppService _itemSuitAppService;
        /// <summary>
        /// 目标疾病
        /// </summary>
        private readonly IRiskFactorAppService _riskFactorAppService;

        /// <summary>
        /// 岗位
        /// </summary>
        private readonly IPostStateAppService _postStateAppService;

        /// <summary>
        /// 缓存
        /// </summary>
        private SearchOccupationalDiseaseIncludeItemGroupDto searchOccupationalDiseaseIncludeItemGroupDto { get; set; }
        public TargetDisease()
        {
            InitializeComponent();
            _itemSuitAppService = new TargetDiseaseAppService();
            _riskFactorAppService = new RiskFactorAppService();
            _postStateAppService = new PostStateAppService();
        }

        private void TargetDisease_Load(object sender, EventArgs e)
        {
            //customGridLookUpEditWorkType.Properties.DataSource =
            //LoadFrom();
            customGridLookUpEditWorkTypeJobCategory.Properties.DataSource = _riskFactorAppService.GetAll().ToList();
            customGridLookUpEditJobCategory.Properties.DataSource = _postStateAppService.GetAll(new JobCategoryDto()).ToList();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelect_Click(object sender, EventArgs e)
        {
            LoadFrom();
        }
        /// <summary>
        /// 必选项目修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMandatory_Click(object sender, EventArgs e)
        {
            OccupationalDiseaseIncludeItemGroupAdministration update = new OccupationalDiseaseIncludeItemGroupAdministration();
            update.InItemGroupSys = searchOccupationalDiseaseIncludeItemGroupDto.MustHaveItemGroups.ToList();
            update.IsMandatory = true;
            update.OccupationalDiseaseIncludeItemGroup = searchOccupationalDiseaseIncludeItemGroupDto;
            if (update.ShowDialog() == DialogResult.OK)
            {
                LoadFrom();
            }
        }
        /// <summary>
        /// 可选项目修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonOptional_Click(object sender, EventArgs e)
        {
            OccupationalDiseaseIncludeItemGroupAdministration update = new OccupationalDiseaseIncludeItemGroupAdministration();
            update.InItemGroupSys = searchOccupationalDiseaseIncludeItemGroupDto.MayHaveItemGroups.ToList();
            update.IsMandatory = false;
            update.OccupationalDiseaseIncludeItemGroup = searchOccupationalDiseaseIncludeItemGroupDto;
            if (update.ShowDialog() == DialogResult.OK)
            {
                LoadFrom();
            }
        }
        /// <summary>
        /// 更新页面数据
        /// </summary>
        public void LoadFrom()
        {

            if (string.IsNullOrWhiteSpace(customGridLookUpEditWorkTypeJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            if (string.IsNullOrWhiteSpace(customGridLookUpEditJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            Empty();
            QueryTargetDisease TargetDisease = new QueryTargetDisease();
            if (customGridLookUpEditWorkTypeJobCategory.EditValue != null)
                TargetDisease.RiskFactorId = (Guid)customGridLookUpEditWorkTypeJobCategory.EditValue;
            if (customGridLookUpEditJobCategory.EditValue != null)
                TargetDisease.JobCategoryId = (Guid)customGridLookUpEditJobCategory.EditValue;
             searchOccupationalDiseaseIncludeItemGroupDto = _itemSuitAppService.GetOccupationalDiseaseIncludeItemGroup(TargetDisease);
            if (searchOccupationalDiseaseIncludeItemGroupDto != null)
            {
                if (searchOccupationalDiseaseIncludeItemGroupDto.MustHaveItemGroups != null)
                {
                    foreach (var item in searchOccupationalDiseaseIncludeItemGroupDto.MustHaveItemGroups)
                    {
                        richTextBoxMandatory.Text += item.ItemGroupName + ".";
                    }
                }
                if (searchOccupationalDiseaseIncludeItemGroupDto.MayHaveItemGroups != null)
                {
                    foreach (var item in searchOccupationalDiseaseIncludeItemGroupDto.MayHaveItemGroups)
                    {
                        richTextBoxOptional.Text += item.ItemGroupName + ".";
                    }
                }
                if (searchOccupationalDiseaseIncludeItemGroupDto.Symptoms != null )
                {
                    foreach (var item in searchOccupationalDiseaseIncludeItemGroupDto.Symptoms)
                    {
                        richTextBoxSymptomInquiry.Text += item.Name + ".";
                    }
                }
                if (searchOccupationalDiseaseIncludeItemGroupDto.HumanBodySystems != null)
                {
                    foreach (var item in searchOccupationalDiseaseIncludeItemGroupDto.HumanBodySystems)
                    {
                        richTextBoxInSpectionContents.Text += item.Name + ".";
                    }
                }
                if (searchOccupationalDiseaseIncludeItemGroupDto.DiseaseContraindicationExplains != null)
                {
                    gridControlOccupationalDisease.DataSource = searchOccupationalDiseaseIncludeItemGroupDto.DiseaseContraindicationExplains.Where(o => o.Type == DiseaseContraindicationType.Disease).ToList();
                    gridControlOccupationalContraindications.DataSource = searchOccupationalDiseaseIncludeItemGroupDto.DiseaseContraindicationExplains.Where(o => o.Type == DiseaseContraindicationType.Contraindication).ToList();
                }
                simpleButtonSymptomInquiry.Enabled = true;
                simpleButtonMandatory.Enabled = true;
                simpleButtonInSpectionContents.Enabled = true;
                simpleButtonOptional.Enabled = true;

            }
            textEditDiseaseContraindication.Text = string.Empty;
            richTextBoxDiseaseContraindication.Text = string.Empty;
            guidSelect = Guid.Empty;
            //alertControl.Show(this, "系统提示", "患者已锁定,已经不能修改啦!");
        }
        /// <summary>
        /// 症状修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSymptomInquiry_Click(object sender, EventArgs e)
        {
            SymptomAdministration update = new SymptomAdministration();
            update.searchOccupationalDiseaseIncludeItemGroupDto = searchOccupationalDiseaseIncludeItemGroupDto;
            if (update.ShowDialog() == DialogResult.OK)
            {
                LoadFrom();
            }
        }
        /// <summary>
        /// 检查内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonInSpectionContents_Click(object sender, EventArgs e)
        {
            HumanBodySystemAdministration update = new HumanBodySystemAdministration();
            update.InHumanBodySystem = searchOccupationalDiseaseIncludeItemGroupDto.HumanBodySystems.ToList();
            if (update.ShowDialog() == DialogResult.OK)
            {
                LoadFrom();
            }
        }
        /// <summary>
        /// 更新职业健康解释
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDisease_Click(object sender, EventArgs e)
        {
            UpdateDiseaseContraindicationExplainDto dto = new UpdateDiseaseContraindicationExplainDto();

            if (string.IsNullOrWhiteSpace(customGridLookUpEditWorkTypeJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            else
            {
                dto.JobCategoryId = (Guid)customGridLookUpEditJobCategory.EditValue;
            }
            if (string.IsNullOrWhiteSpace(customGridLookUpEditJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            else
            {
                dto.RiskFactorId = (Guid)customGridLookUpEditWorkTypeJobCategory.EditValue;
            }
            if (guidSelect != Guid.Empty)
            {
                dto.Id = guidSelect;
                dto.Type = typeSelect;
            }
            else
            {
                dto.Type = DiseaseContraindicationType.Disease;
            }
            dto.Name = textEditDiseaseContraindication.Text;
            dto.Explain = richTextBoxDiseaseContraindication.Text;
            _itemSuitAppService.UpdateUpdateDiseaseOrContraindication(dto);
            LoadFrom();
        }
        /// <summary>
        /// 更新职业健康禁忌证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonContraindication_Click(object sender, EventArgs e)
        {
            UpdateDiseaseContraindicationExplainDto dto = new UpdateDiseaseContraindicationExplainDto();
            if (string.IsNullOrWhiteSpace(customGridLookUpEditWorkTypeJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            else
            {
                dto.JobCategoryId = (Guid)customGridLookUpEditJobCategory.EditValue;
            }
            if (string.IsNullOrWhiteSpace(customGridLookUpEditJobCategory.EditValue?.ToString()))
            {
                MessageBox.Show("危害因素必须选择");
                return;
            }
            else
            {
                dto.RiskFactorId = (Guid)customGridLookUpEditWorkTypeJobCategory.EditValue;
            }
            if (guidSelect != Guid.Empty)
            {
                dto.Id = guidSelect;
                dto.Type = typeSelect;
            }
            else
            {
                dto.Type = DiseaseContraindicationType.Contraindication;
            }
            dto.Name = textEditDiseaseContraindication.Text;
            dto.Explain = richTextBoxDiseaseContraindication.Text;
            _itemSuitAppService.UpdateUpdateDiseaseOrContraindication(dto);
            LoadFrom();
        }
        private Guid guidSelect { get; set; }
        private DiseaseContraindicationType typeSelect { get; set; }
        /// <summary>
        /// 职业健康双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewOccupationalDisease_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                guidSelect = (Guid)gridViewOccupationalDisease.GetFocusedRowCellValue(gridColumnDiseaseId.FieldName);
                textEditDiseaseContraindication.Text = gridViewOccupationalDisease.GetFocusedRowCellValue(gridColumnDiseaseName.FieldName).ToString();
                richTextBoxDiseaseContraindication.Text = gridViewOccupationalDisease.GetFocusedRowCellValue(gridColumnDiseaseExplain.FieldName).ToString();
                typeSelect = DiseaseContraindicationType.Disease;
            }
            else if (e.Clicks == 1)
            {
                guidSelect = Guid.Empty;
                textEditDiseaseContraindication.Text = string.Empty;
                richTextBoxDiseaseContraindication.Text = string.Empty;
            }
        }
        /// <summary>
        /// 禁忌证双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewOccupationalContraindications_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                guidSelect = (Guid)gridViewOccupationalContraindications.GetFocusedRowCellValue(gridColumnContraindicationsId.FieldName);
                textEditDiseaseContraindication.Text = gridViewOccupationalContraindications.GetFocusedRowCellValue(gridColumnContraindicationsName.FieldName).ToString();
                richTextBoxDiseaseContraindication.Text = gridViewOccupationalContraindications.GetFocusedRowCellValue(gridColumnContraindicationsExplain.FieldName).ToString();
                typeSelect = DiseaseContraindicationType.Contraindication;
            }
            else if (e.Clicks == 1)
            {
                guidSelect = Guid.Empty;
                textEditDiseaseContraindication.Text = string.Empty;
                richTextBoxDiseaseContraindication.Text = string.Empty;
            }
        }
        private void Empty()
        {
            gridControlOccupationalDisease.DataSource = null;
            gridControlOccupationalContraindications.DataSource = null;
            richTextBoxSymptomInquiry.Text = string.Empty;
            richTextBoxInSpectionContents.Text = string.Empty;
            richTextBoxMandatory.Text = string.Empty;
            richTextBoxOptional.Text = string.Empty;
            simpleButtonSymptomInquiry.Enabled = false;
            simpleButtonInSpectionContents.Enabled = false;
            simpleButtonMandatory.Enabled = false;
            simpleButtonOptional.Enabled = false;
            
        }
    }
}
