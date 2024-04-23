using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet
{
    public partial class OccduseaseAdd : UserBaseForm
    {

        private readonly IOccDiseaseAppService services=new OccDiseaseAppService();
        
        private readonly IOccDisProposalNewAppService _OccDisproAppService=new OccDisProposalNewAppService();

        private readonly Application.Common.ICommonAppService _commonAppService = new Application.Common.CommonAppService();
        private readonly Guid _id;

        public OccDieaseAndStandardDto OccDieaseAndStandardDto { get; private set; }
        public OutOccDiseaseDto _Model { get; private set; }

        public OccduseaseAdd()
        {
            InitializeComponent();
            if (_Model == null) _Model = new OutOccDiseaseDto();
        }

        public OccduseaseAdd(Guid id, OutOccDiseaseDto outOccDiseaseDto) : this()
        {
            _id = id;
            _Model = outOccDiseaseDto;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            CreatOccDiseaseDto dtos = new CreatOccDiseaseDto();
            if (int.TryParse(textOrderNum.Text.Trim(), out int OrderNum))
            {
                dtos.OrderNum = Convert.ToInt32(textOrderNum.Text.Trim());
            }
            // 分类名称
            dtos.Text = txtName.Text.Trim();
            //助记码s
            dtos.HelpChar = helpchar.Text.Trim();
            //是否启用
            //if (ishave.SelectedIndex == 0)
            //{
            //    dtos.IsActive = 1;
            //}
            //else
            //{
            //    dtos.IsActive = 2;
            //}
            //内容
            dtos.IsActive = int.Parse( ishave.EditValue.ToString());
           dtos.Remarks = remarks.Text.Trim();
            if (_Model != null && _Model.Id != null)
            {
                dtos.Id = _Model.Id;           

            }
            if (comboBoxEdit1.EditValue != null)
            {
                dtos.ParentId = (Guid)comboBoxEdit1.EditValue;
            }
            //标准
            List<OccDiseaseStandardDto> standl = new List<OccDiseaseStandardDto>();
            standl = (List<OccDiseaseStandardDto>)gridControl1.DataSource;
            if (standl != null && standl.Count == 1)
            {
                standl[0].IsShow = 1;
            }
            bool res = false;
            OutOccDiseaseDto dto = null;
            AutoLoading(() =>
            {
             
                OccDieaseAndStandardDto input = new OccDieaseAndStandardDto()

                {

                    occDisease =dtos,
                     listStandard= standl
                };
             
                if (_Model.Id == Guid.Empty)
                {
                    dto = services.Add(input);
                }
                else
                {
                    dto = services.Edit(input);
                }
                _Model = dto;
                res = true;
            });
            return res;
        }

        private void OccduseaseAdd_Load(object sender, EventArgs e)
        {
            repositoryItemCheckEdit2.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            //复选框加载的状态     实心   空心   空心打勾  
            repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            StandrdLoadData();
            if (_id != Guid.Empty)
            {
                LoadData();
            }          

        
        }
        void LoadData()
        {
            try
            {
             var data = services.GetById(new EntityDto<Guid> { Id = _id });
                if (data.IsActive == 1)
                {
                    ishave.SelectedIndex = 0;
                }
                else
                {
                    ishave.SelectedIndex = 1;
                }
             txtName.EditValue = data.Text;
             helpchar.EditValue = data.HelpChar;
             remarks.EditValue = data.Remarks;
             comboBoxEdit1.EditValue = data.ParentId;
                if (data.OrderNum.HasValue)
                {
                    textOrderNum.EditValue = data.OrderNum;
                }
                gridControl1.DataSource = data.TbmOccStandards;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }

        }

        void StandrdLoadData()
        {
            try
            {
                //一级分类
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = ZYBBasicDictionaryType.Occupational.ToString();
                var dl = _OccDisproAppService.getOutOccDictionaryDto(chargeBM);
                comboBoxEdit1.Properties.DataSource = dl;
                //职业健康标准

                chargeBM.Name = ZYBBasicDictionaryType.diagnose.ToString();
                var dl1 = _OccDisproAppService.getOutOccDictionaryDto(chargeBM);
                OccStandard.Properties.DataSource = dl1;

            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
           
        }


        private void helpchar_Leave(object sender, EventArgs e)
        {
       
        }

        private void OccStandard_EditValueChanged(object sender, EventArgs e)
        {
            if (OccStandard.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)OccStandard.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl1.DataSource as List<OccDiseaseStandardDto>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.StandardName == RowData.Text)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<OccDiseaseStandardDto>();

                    }
                    var DiseaseStandardDto = new OccDiseaseStandardDto();
                    DiseaseStandardDto.IsShow = 0;
                    DiseaseStandardDto.StandardName = RowData.Text;
                    DiseaseStandardDto.StandardNo = RowData.HelpChar;        
                    dataresult.Add(DiseaseStandardDto);
                    gridControl1.DataSource = dataresult;
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();


                }
                OccStandard.EditValue = null;
            }
        }

        private void OccStandard_Properties_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Name_Leave(object sender, EventArgs e)
        {
          
            var name = txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = _commonAppService.GetHansBrief(new Application.Common.Dto.ChineseDto { Hans = name });
                    helpchar.Text = result.Brief;
                }
                catch (ApiProxy.UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                helpchar.Text = string.Empty;
            }
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {  //删除
            //MessageBox.Show("删除");
            var currentItem = gridView1.GetFocusedRow() as OccDiseaseStandardDto;
            if (currentItem == null)
                return;
            
            var dataresult = gridControl1.DataSource as List<OccDiseaseStandardDto>;
            dataresult.Remove(currentItem);
            gridControl1.DataSource = dataresult;
            gridControl1.RefreshDataSource();
            gridControl1.Refresh();

        }
        private void repositoryItemCheckEdit2_QueryCheckStateByValue_1(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";//默认为不选   
            }
            switch (val)
            {
                case "True":
                case "Yes":
                case "1":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                case "No":
                case "0":
                    e.CheckState = CheckState.Unchecked;
                    break;
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
    }
}
