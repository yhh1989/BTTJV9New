using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccHazardFactor
{
    public partial class OccHazardFactorEdit : UserBaseForm
    {
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        public OutOccHazardFactorDto _Model { get; private set; }
        private readonly Guid _id;
        public OccHazardFactorEdit()
        {
            InitializeComponent();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            if (_Model == null) _Model = new OutOccHazardFactorDto();
        }

        private void OccHazardFactorEdit_Load(object sender, EventArgs e)
        {
            //一级分类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.HazardHactors.ToString();
            var dl = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxEdit2.Properties.DataSource = dl;

            chargeBM.Name = ZYBBasicDictionaryType.Protect.ToString();
            var dl1 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            comboBoxEdit1.Properties.DataSource = dl1;
            //var data = _OccHazardFactorAppService.GetHazardFactorsProtective();
            //comboBoxEdit1.Properties.DataSource = data;

            if (_id != Guid.Empty)
            {
                LoadData();
            }
        }
        // 助记码
        private void teAdviceName_Leave(object sender, EventArgs e)
        {

            var name = textEdit1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    textEdit3.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                textEdit3.Text = string.Empty;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           // layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public OccHazardFactorEdit(Guid id, OutOccHazardFactorDto model) : this()
        {
            _id = id;
            _Model = model;
        }
        
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            CreateOrUpdateHazardFactorDto dto = new CreateOrUpdateHazardFactorDto();
            // 分类名称
            dto.Text = textEdit1.Text.Trim();
            //助记码
            dto.HelpChar = textEdit3.Text.Trim();
            //CAS码
            dto.CASBM = textEdit2.Text.Trim();
            //是否启用
            if (radioGroup2.SelectedIndex == 0)
            {
                dto.IsActive = 1;
            }
            else
            {
                dto.IsActive = 0;
            }
            dto.OrderNum = Convert.ToInt32(textEdit4.Text.Trim());
            //内容
            //_Model.Protectivis = comboBoxEdit1.SelectedText;
            dto.Remarks = memoEdit1.Text.Trim();
            //上级分类
            if (comboBoxEdit2.EditValue != null)
            {
                dto.ParentId = (Guid)comboBoxEdit2.EditValue;
            }
            if (_Model != null && _Model.Id != null)
            {
                dto.Id = _Model.Id;

            }
            List<HazardFactorsProtective> stand = new List<HazardFactorsProtective>();
            stand = (List<HazardFactorsProtective>)gridControl1.DataSource;
            bool res = false;
            OutOccHazardFactorDto dtos = null;
            AutoLoading(() =>
            {
                FullHarardFactor input = new FullHarardFactor()
                {
                    HazardFactorDto = dto,
                    HazardFactorsProtectiveDto = stand
                };
                
                if (_Model.Id == Guid.Empty)
                {
                    dtos = _OccHazardFactorAppService.Add(input);
                   
                }
                else
                {
                    dtos = _OccHazardFactorAppService.Edit(input);
                }
                _Model = dtos;
                res = true;
            });
            return res;
        }

       
        private void LoadData()
        {
            try
            {
                
                var data = _OccHazardFactorAppService.GetOccHazardFactor(new EntityDto<Guid> { Id = _id });
                // 分类名称
                 textEdit1.EditValue=data.Text;
                //助记码
                 textEdit3.Text=data.HelpChar;
                //上级分类
                 comboBoxEdit2.EditValue = data.ParentId;
                //cas码
                textEdit2.Text = data.CASBM;
                textEdit4.EditValue = data.OrderNum;
                //是否启用
                if (data.IsActive == 0)
                {
                    radioGroup2.SelectedIndex = 1;
                }
                else
                {
                    radioGroup2.SelectedIndex = 0;
                }
                //防护措施
                gridControl1.DataSource = data.Protectivis;
                //内容
                memoEdit1.Text=data.Remarks;
               
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridView1.GetFocusedRow() as HazardFactorsProtective;
            if (currentItem == null)
                return;

            var dataresult = gridControl1.DataSource as List<HazardFactorsProtective>;
            dataresult.Remove(currentItem);
            gridControl1.DataSource = dataresult;
            gridControl1.RefreshDataSource();
            gridControl1.Refresh();
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)comboBoxEdit1.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl1.DataSource as List<HazardFactorsProtective>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.Text == RowData.Text)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<HazardFactorsProtective>();

                    }
                    var FactorsDto = new HazardFactorsProtective();
                    FactorsDto.Text = RowData.Text;
                    FactorsDto.OrderNum = RowData.OrderNum;
                    FactorsDto.Category = RowData.ParentName;
                    dataresult.Add(FactorsDto);
                    gridControl1.DataSource = dataresult;
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();


                }
                comboBoxEdit1.EditValue = null;
            }
        }

        public bool isadd = false;
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            simpleButton2.PerformClick();
            isadd = true;
        }
    }
}
