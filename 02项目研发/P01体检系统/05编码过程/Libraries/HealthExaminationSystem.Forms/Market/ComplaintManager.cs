using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 投诉管理
    /// </summary>
    public partial class ComplaintManager : UserBaseForm
    {
        /// <summary>
        /// 当前编辑器
        /// </summary>
        private UpdateComplaintInformationDto _currentComplaintInformationEditor;

        /// <summary>
        /// 当前阶段
        /// </summary>
        private Phase _currentPhase;

        /// <summary>
        /// 初始化投诉管理
        /// </summary>
        public ComplaintManager()
        {
            InitializeComponent();
            _currentComplaintInformationEditor = new UpdateComplaintInformationDto();
            _currentPhase = Phase.Empty;
        }

        /// <summary>
        /// 投诉列表数据源改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl投诉列表_DataSourceChanged(object sender, EventArgs e)
        {
            if (gridControl投诉列表.DataSource != null)
            {
                gridView投诉列表.BestFitColumns();
            }
        }

        /// <summary>
        /// 窗体第一次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComplaintManager_Shown(object sender, EventArgs e)
        {
            searchLookUpEdit处理人检索.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            searchLookUpEdit被投诉人检索.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            searchLookUpEdit被投诉人.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            searchLookUpEdit处理人.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            var complainWay = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ComplainWay);
            lookUpEdit投诉方式.Properties.DataSource = complainWay;
            var complainCategory = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ComplainCategory);
            lookUpEdit投诉类别.Properties.DataSource = complainCategory;
            repositoryItemLookUpEdit投诉方式.DataSource = complainWay;
            repositoryItemLookUpEdit投诉类别.DataSource = complainCategory;
            repositoryItemLookUpEdit用户列表.DataSource = DefinedCacheHelper.GetComboUsers();
            repositoryItemLookUpEdit处理状态.DataSource = ComplaintProcessStateHelper.ComplaintProcessStateCollection;
            repositoryItemLookUpEdit紧急级别.DataSource = ComplaintExigencyLevelHelper.ComplaintExigencyLevelCollection;
            if (complainWay.Count == 0)
            {
                ShowMessageBoxWarning("投诉方式没有任何值，请在字典功能里维护。");
            }
            if (complainCategory.Count == 0)
            {
                ShowMessageBoxWarning("投诉类别没有任何值，请在字典功能里维护。");
            }

            simpleButton查询.PerformClick();
        }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComplaintManager_Load(object sender, EventArgs e)
        {
            dateEdit投诉时间止.DateTime = DateTime.Now;
            dateEdit投诉时间起.DateTime = dateEdit投诉时间止.DateTime.AddDays(-7);

            dateEdit投诉时间.DateTime = DateTime.Now;
            searchLookUpEdit处理人.EditValue = CurrentUser.Id;
            dateEdit处理时间.DateTime = DateTime.Now;
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            var input = new QueryComplaintConditionInput();
            if (!string.IsNullOrWhiteSpace(textEdit姓名检索.Text))
            {
                input.CustomerName = textEdit姓名检索.Text;
            }
            if (!string.IsNullOrWhiteSpace(textEdit手机号检索.Text))
            {
                input.CustomerMobile = textEdit手机号检索.Text;
            }
            if (!string.IsNullOrWhiteSpace(textEdit身份证号检索.Text))
            {
                input.IdCard = textEdit身份证号检索.Text;
            }
            if (!string.IsNullOrWhiteSpace(textEdit体检号检索.Text))
            {
                input.CustomerRegisterCode = textEdit体检号检索.Text;
            }
            if (dateEdit投诉时间起.EditValue != null)
            {
                input.ComplainTimeStart = dateEdit投诉时间起.DateTime;
            }
            if (dateEdit投诉时间止.EditValue != null)
            {
                input.ComplainTimeEnd = dateEdit投诉时间止.DateTime;
            }

            if (searchLookUpEdit处理人检索.EditValue is long handlerId)
            {
                input.HandlerId = handlerId;
            }

            if (searchLookUpEdit被投诉人检索.EditValue is long complainUserId)
            {
                input.ComplainUserId = complainUserId;
            }

            input.ProcessState = new List<ComplaintProcessState>();
            if (checkEdit未处理.Checked)
            {
                input.ProcessState.Add(ComplaintProcessState.Undisposed);
            }
            if (checkEdit已处理.Checked)
            {
                input.ProcessState.Add(ComplaintProcessState.Processed);
            }
            if (checkEdit忽略.Checked)
            {
                input.ProcessState.Add(ComplaintProcessState.Ignored);
            }
            if (checkEdit已上报.Checked)
            {
                input.ProcessState.Add(ComplaintProcessState.Reported);
            }

            AutoLoading(() =>
            {
                var result = DefinedCacheHelper.DefinedApiProxy.ComplaintAppService
                    .QueryComplaintInformationCollection(input).Result;

                gridControl投诉列表.DataSource = result;
            });
        }

        /// <summary>
        /// 新增按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton新增_Click(object sender, EventArgs e)
        {
            if (_currentPhase == Phase.Empty)
            {
                var idCard = textEdit身份证号检索.Text;
                var mobile = textEdit手机号检索.Text;
                var customerRegisterCode = textEdit体检号检索.Text;
                ComplaintInformationDto row = null;
                if (string.IsNullOrWhiteSpace(idCard + mobile + customerRegisterCode))
                {
                    if (gridControl投诉列表.DataSource != null)
                    {
                        if (gridView投诉列表.RowCount != 0)
                        {
                            if (gridView投诉列表.GetFocusedRow() is ComplaintInformationDto row1)
                            {
                                row = row1;
                            }
                        }
                    }
                }

                if (row != null)
                {
                    // 将人导入编辑器
                    textEdit姓名.Tag = row.CustomerId;
                    textEdit姓名.Text = row.Customer.Name;
                    textEdit手机号.Text = row.Customer.Mobile;
                    textEdit身份证号.Text = row.Customer.IDCardNo;
                    textEdit单位.Tag = row.CompanyInformationId;
                    textEdit单位.Text = row.CompanyInformation?.ClientName;
                    textEdit预约.Tag = row.CompanyRegisterId;
                    textEdit预约.Text = row.CompanyRegister?.DisplayMember;
                    textEdit1分组.Tag = row.CompanyRegisterTeamId;
                    textEdit1分组.Text = row.CompanyRegisterTeamInformation?.TeamName;
                    textEdit体检号.Tag = row.CustomerRegisterId;
                    textEdit体检号.Text = row.CustomerRegister.CustomerBM;
                    textEdit体检日期.Text = row.CustomerRegister.LoginDate?.ToString("d") ??
                                        row.CustomerRegister.BookingDate?.ToString("d");
                    _currentPhase = Phase.Creating;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(idCard + mobile + customerRegisterCode))
                    {
                        ShowMessageBoxError("手机号，身份证号，或体检号至少填一项。");
                    }
                    else
                    {
                        // 从服务器加载患者信息
                        AutoLoading(() =>
                        {
                            var input = new QueryComplaintConditionInput
                            {
                                CustomerMobile = mobile,
                                IdCard = idCard,
                                CustomerRegisterCode = customerRegisterCode
                            };

                            var output = DefinedCacheHelper.DefinedApiProxy.ComplaintAppService
                                .QueryEmptyComplaintInformation(input).Result;

                            if (output == null)
                            {
                                ShowMessageBoxWarning("没有查询到任何体检人信息。");
                            }
                            else
                            {
                                textEdit姓名.Tag = output.Customer.Id;
                                textEdit姓名.Text = output.Customer.Name;
                                textEdit手机号.Text = output.Customer.Mobile;
                                textEdit身份证号.Text = output.Customer.IDCardNo;
                                textEdit单位.Tag = output.CompanyInformation?.Id;
                                textEdit单位.Text = output.CompanyInformation?.ClientName;
                                textEdit预约.Tag = output.CompanyRegister?.Id;
                                textEdit预约.Text = output.CompanyRegister?.DisplayMember;
                                textEdit1分组.Tag = output.CompanyRegisterTeamInformation?.Id;
                                textEdit1分组.Text = output.CompanyRegisterTeamInformation?.TeamName;
                                textEdit体检号.Tag = output.CustomerRegister.Id;
                                textEdit体检号.Text = output.CustomerRegister.CustomerBM;
                                textEdit体检日期.Text = output.CustomerRegister.LoginDate?.ToString("d") ??
                                                    output.CustomerRegister.BookingDate?.ToString("d");
                                _currentPhase = Phase.Creating;
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 阶段
        /// </summary>
        private enum Phase
        {
            /// <summary>
            /// 空
            /// </summary>
            Empty,
            /// <summary>
            /// 创建
            /// </summary>
            Creating,
            /// <summary>
            /// 查看
            /// </summary>
            Looking,
            /// <summary>
            /// 更新
            /// </summary>
            Updating
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton编辑_Click(object sender, EventArgs e)
        {
            if (_currentPhase == Phase.Empty)
            {
                if (gridControl投诉列表.DataSource != null)
                {
                    if (gridView投诉列表.RowCount != 0)
                    {
                        if (gridView投诉列表.GetFocusedRow() is ComplaintInformationDto row)
                        {
                            if (row.ProcessState == ComplaintProcessState.Processed &&
                                row.ProcessingTime < DateTime.Now.AddDays(-1))
                            {
                                ShowMessageBoxWarning("信息已经处理并且已经超过 24 小时，禁止编辑！");
                            }
                            else
                            {
                                // 将人导入编辑器
                                _currentComplaintInformationEditor.Id = row.Id;
                                textEdit姓名.Tag = row.CustomerId;
                                textEdit姓名.Text = row.Customer.Name;
                                textEdit手机号.Text = row.Customer.Mobile;
                                textEdit身份证号.Text = row.Customer.IDCardNo;
                                textEdit单位.Tag = row.CompanyInformationId;
                                textEdit单位.Text = row.CompanyInformation?.ClientName;
                                textEdit预约.Tag = row.CompanyRegisterId;
                                textEdit预约.Text = row.CompanyRegister?.DisplayMember;
                                textEdit1分组.Tag = row.CompanyRegisterTeamId;
                                textEdit1分组.Text = row.CompanyRegisterTeamInformation?.TeamName;
                                textEdit体检号.Tag = row.CustomerRegisterId;
                                textEdit体检号.Text = row.CustomerRegister.CustomerBM;
                                textEdit体检日期.Text = row.CustomerRegister.LoginDate?.ToString("d") ??
                                                    row.CustomerRegister.BookingDate?.ToString("d");
                                memoEdit投诉内容.Text = row.Description;
                                memoEdit处理结果.Text = row.Result;
                                lookUpEdit投诉方式.EditValue = row.ComplainWay;
                                lookUpEdit投诉类别.EditValue = row.ComplainCategory;
                                dateEdit投诉时间.DateTime = row.ComplainTime;
                                searchLookUpEdit被投诉人.EditValue = row.ComplainUserId;
                                searchLookUpEdit处理人.EditValue = row.HandlerId;
                                radioGroup处理状态.EditValue = (int)row.ProcessState;
                                if (row.ProcessingTime.HasValue)
                                {
                                    dateEdit处理时间.DateTime = row.ProcessingTime.Value;
                                }
                                radioGroup紧急级别.EditValue = (int)row.ExigencyLevel;
                                _currentPhase = Phase.Updating;
                            }
                        }
                        else
                        {
                            ShowMessageBoxWarning("请先在列表里选择一个投诉信息！");
                        }
                    }
                    else
                    {
                        ShowMessageBoxWarning("没有任何数据，无法编辑！");
                    }
                }
                else
                {
                    ShowMessageBoxWarning("没有任何数据，无法编辑！");
                }
            }
            else
            {
                ShowMessageBoxWarning("已经在编辑或创建数据，请先保存或取消。");
            }
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton保存_Click(object sender, EventArgs e)
        {
            if (_currentPhase == Phase.Creating || _currentPhase == Phase.Updating)
            {
                _currentComplaintInformationEditor.CustomerId = (Guid)textEdit姓名.Tag;
                _currentComplaintInformationEditor.CompanyInformationId = (Guid?)textEdit单位.Tag;
                _currentComplaintInformationEditor.CompanyRegisterId = (Guid?)textEdit预约.Tag;
                _currentComplaintInformationEditor.CompanyRegisterTeamId = (Guid?)textEdit1分组.Tag;
                _currentComplaintInformationEditor.CustomerRegisterId = (Guid)textEdit体检号.Tag;
                _currentComplaintInformationEditor.Description = memoEdit投诉内容.Text;
                _currentComplaintInformationEditor.Result = memoEdit处理结果.Text;
                _currentComplaintInformationEditor.ComplainWay = (int?)lookUpEdit投诉方式.EditValue;
                _currentComplaintInformationEditor.ComplainCategory = (int?)lookUpEdit投诉类别.EditValue;
                _currentComplaintInformationEditor.ComplainTime = dateEdit投诉时间.DateTime;
                _currentComplaintInformationEditor.ComplainUserId = (long?)searchLookUpEdit被投诉人.EditValue;
                _currentComplaintInformationEditor.HandlerId = (long)searchLookUpEdit处理人.EditValue;
                _currentComplaintInformationEditor.ProcessState = (ComplaintProcessState)(int)radioGroup处理状态.EditValue;
                _currentComplaintInformationEditor.ProcessingTime = dateEdit处理时间.DateTime;
                _currentComplaintInformationEditor.ExigencyLevel = (ComplaintExigencyLevel)(int)radioGroup紧急级别.EditValue;

                AutoLoading(() =>
                {
                    var output =
                        DefinedCacheHelper.DefinedApiProxy.ComplaintAppService.InsertOrUpdateComplaintInformation(
                            _currentComplaintInformationEditor).Result;

                    if (_currentPhase == Phase.Creating)
                    {
                        if (gridControl投诉列表.DataSource == null)
                        {
                            gridControl投诉列表.DataSource = new List<ComplaintInformationDto> { output };
                        }
                        else
                        {
                            if (gridControl投诉列表.DataSource is List<ComplaintInformationDto> rows)
                            {
                                rows.Add(output);
                                gridControl投诉列表.RefreshDataSource();
                            }
                        }
                    }
                    else
                    {
                        if (gridControl投诉列表.DataSource is List<ComplaintInformationDto> rows)
                        {
                            var row = rows.Find(r => r.Id == output.Id);
                            row.Description = output.Description;
                            row.Result = output.Result;
                            row.ComplainWay = output.ComplainWay;
                            row.ComplainCategory = output.ComplainCategory;
                            row.ComplainTime = output.ComplainTime;
                            row.ComplainUserId = output.ComplainUserId;
                            row.HandlerId = output.HandlerId;
                            row.ProcessState = output.ProcessState;
                            row.ProcessingTime = output.ProcessingTime;
                            row.ExigencyLevel = output.ExigencyLevel;
                            gridControl投诉列表.RefreshDataSource();
                        }
                    }
                });

                _currentComplaintInformationEditor.Id = Guid.Empty;
                textEdit姓名.Tag = null;
                textEdit姓名.MaskBox.Clear();
                textEdit手机号.MaskBox.Clear();
                textEdit身份证号.MaskBox.Clear();
                textEdit体检号.Tag = null;
                textEdit体检号.MaskBox.Clear();
                textEdit体检日期.MaskBox.Clear();
                textEdit单位.Tag = null;
                textEdit单位.MaskBox.Clear();
                textEdit预约.Tag = null;
                textEdit预约.MaskBox.Clear();
                textEdit1分组.Tag = null;
                textEdit1分组.MaskBox.Clear();
                memoEdit投诉内容.MaskBox.Clear();
                memoEdit处理结果.MaskBox.Clear();
                lookUpEdit投诉方式.EditValue = null;
                lookUpEdit投诉类别.EditValue = null;
                dateEdit投诉时间.DateTime = DateTime.Now;
                searchLookUpEdit被投诉人.EditValue = null;
                searchLookUpEdit处理人.EditValue = CurrentUser.Id;
                dateEdit处理时间.DateTime = DateTime.Now;
                radioGroup紧急级别.EditValue = 2;
                radioGroup处理状态.EditValue = 0;
                _currentPhase = Phase.Empty;
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton取消_Click(object sender, EventArgs e)
        {
            _currentComplaintInformationEditor.Id = Guid.Empty;
            textEdit姓名.Tag = null;
            textEdit姓名.MaskBox.Clear();
            textEdit手机号.MaskBox.Clear();
            textEdit身份证号.MaskBox.Clear();
            textEdit体检号.Tag = null;
            textEdit体检号.MaskBox.Clear();
            textEdit体检日期.MaskBox.Clear();
            textEdit单位.Tag = null;
            textEdit单位.MaskBox.Clear();
            textEdit预约.Tag = null;
            textEdit预约.MaskBox.Clear();
            textEdit1分组.Tag = null;
            textEdit1分组.MaskBox.Clear();
            memoEdit投诉内容.MaskBox.Clear();
            memoEdit处理结果.MaskBox.Clear();
            lookUpEdit投诉方式.EditValue = null;
            lookUpEdit投诉类别.EditValue = null;
            dateEdit投诉时间.DateTime = DateTime.Now;
            searchLookUpEdit被投诉人.EditValue = null;
            searchLookUpEdit处理人.EditValue = CurrentUser.Id;
            dateEdit处理时间.DateTime = DateTime.Now;
            radioGroup紧急级别.EditValue = 2;
            radioGroup处理状态.EditValue = 0;
            _currentPhase = Phase.Empty;
        }
    }
}