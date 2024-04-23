using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.QuestionnaireMaintain
{
    /// <summary>
    /// 问卷查看/编辑
    /// </summary>
    public partial class QuestionnaireEditor : UserBaseForm
    {
        /// <summary>
        /// 问卷标识
        /// </summary>
        private readonly Guid _id;

        /// <summary>
        /// 问卷应用服务
        /// </summary>
        private readonly IQuestionnaireAppService _questionnaireAppService;

        /// <summary>
        /// 页面数据的 JSON 字符串
        /// </summary>
        private string DataJson { get; set; }

        /// <summary>
        /// 页面数据的
        /// </summary>
        private List<QuestionBomDto> Data { get; set; }

        /// <summary>
        /// 初始化 问卷查看/编辑
        /// </summary>
        public QuestionnaireEditor(Guid id)
        {
            InitializeComponent();

            _questionnaireAppService = new QuestionnaireAppService();

            _id = id;
        }

        /// <summary>
        /// 窗体第一次加载后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionnaireEditor_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();

            LoadData();
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            AutoLoading(() =>
            {
                var result = _questionnaireAppService.QueryQuestionBomRecordByQuestionnaireId(new EntityDto<Guid>(_id));
                DataJson = JsonConvert.SerializeObject(result);
                Data = result;

                layoutControlGroup4.BeginUpdate();
                foreach (var row in result)
                {
                    var redStar = row.mustFill == 1 ? "<Color=Red>✶</Color>" : string.Empty;
                    var layoutControlGroup =
                        layoutControlGroup4.AddGroup($"{redStar} {result.IndexOf(row) + 1}、{row.bomItemName}");
                    layoutControlGroup.AllowHtmlStringInCaption = true;
                    switch (row.bomItemType)
                    {
                        case 1:
                            {
                                var layoutControlItem = layoutControlGroup.AddItem();
                                layoutControlItem.TextVisible = false;
                                var textEdit = new TextEdit();
                                textEdit.Text = row.answerContent;
                                textEdit.Tag = row;
                                textEdit.TextChanged += TextEdit_TextChanged;
                                layoutControlItem.Control = textEdit;
                                break;
                            }
                        case 2:
                            {
                                var layoutControlItem = layoutControlGroup.AddItem();
                                layoutControlItem.TextVisible = false;
                                var radioGroup = new RadioGroup();
                                radioGroup.Properties.ItemsLayout = RadioGroupItemsLayout.Flow;
                                radioGroup.AutoSizeInLayoutControl = true;
                                if (row.itemList != null)
                                {
                                    foreach (var dto in row.itemList)
                                    {
                                        radioGroup.Properties.Items.Add(new RadioGroupItem(dto.Id, dto.itemName));
                                        if (dto.isSelected == 1)
                                        {
                                            radioGroup.EditValue = dto.Id;
                                        }
                                    }
                                }

                                radioGroup.Tag = row;
                                radioGroup.SelectedIndexChanged += RadioGroup_SelectedIndexChanged;
                                layoutControlItem.Control = radioGroup;
                                break;
                            }
                        case 3:
                            {
                                if (row.itemList != null)
                                {
                                    layoutControlGroup.LayoutMode = LayoutMode.Flow;
                                    foreach (var dto in row.itemList)
                                    {
                                        var layoutControlItem = layoutControlGroup.AddItem();
                                        layoutControlItem.TextVisible = false;
                                        var checkEdit = new CheckEdit();
                                        checkEdit.Text = dto.itemName;
                                        checkEdit.AutoSizeInLayoutControl = true;
                                        if (dto.isSelected == 1)
                                        {
                                            checkEdit.Checked = true;
                                        }

                                        checkEdit.Tag = dto;
                                        checkEdit.CheckedChanged += CheckEdit_CheckedChanged;
                                        layoutControlItem.Control = checkEdit;
                                    }
                                }

                                break;
                            }
                        default:
                            break;
                    }
                }

                layoutControlGroup4.EndUpdate();
            });
        }

        /// <summary>
        /// 复选项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit)
            {
                if (checkEdit.Tag is QuestiontemDto dto)
                {
                    dto.isSelected = checkEdit.Checked ? 1 : 0;
                    OnDataChanged();
                }
            }
        }

        /// <summary>
        /// 单选框选项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is RadioGroup radioGroup)
            {
                if (radioGroup.Tag is QuestionBomDto row)
                {
                    if (radioGroup.EditValue is Guid id)
                    {
                        foreach (var dto in row.itemList)
                        {
                            dto.isSelected = dto.Id == id ? 1 : 0;
                        }
                        OnDataChanged();
                    }
                }
            }
        }

        /// <summary>
        /// 文本框文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextEdit_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextEdit textEdit)
            {
                if (textEdit.Tag is QuestionBomDto row)
                {
                    row.answerContent = textEdit.Text;
                    OnDataChanged();
                }
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 数据改变事件
        /// </summary>
        private void OnDataChanged()
        {
            if (DataJson == JsonConvert.SerializeObject(Data))
            {
                simpleButton1.Enabled = false;
            }
            else
            {
                simpleButton1.Enabled = true;
            }
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var canSave = true;
            foreach (var row in Data)
            {
                if (row.mustFill == 1)
                {
                    switch (row.bomItemType)
                    {
                        case 1:
                            {
                                if (string.IsNullOrWhiteSpace(row.answerContent))
                                {
                                    canSave = false;
                                }
                                break;
                            }
                        case 2:
                        case 3:
                            {
                                if (row.itemList != null && row.itemList.Count != 0)
                                {
                                    if (row.itemList.All(r => r.isSelected == 0))
                                    {
                                        canSave = false;
                                    }
                                }

                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            if (canSave)
            {
                if (_questionnaireAppService.UpdateQuestionBomRecord(Data))
                {
                    Close();
                }
                else
                {
                    ShowMessageBoxError("问卷修改保存失败！");
                }
            }
            else
            {
                ShowMessageBoxWarning("还有必填题目没有答案，不能保存！");
            }
        }

        private void QuestionnaireEditor_Load(object sender, EventArgs e)
        {

        }
    }
}