using Abp.Application.Services.Dto;
using DevExpress.DataProcessing;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew
{
    public partial class OccTargetDiseaseEdit : UserBaseForm
    {
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private readonly IOccTargetDiseaseAppService _OccTargetDiseaseAppServicee;
        private List<SimpleItemGroupDto> lstsimpleItemGroupDtos;
        public OutTbmOccTargetDiseaseDto _Model { get; private set; }
        private readonly Guid _id;
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        private readonly IOccDiseaseAppService _OccDiseaseAppService;
        private readonly IOccDisProposalNewAppService _OccDisproAppService = new OccDisProposalNewAppService();
        private List<SimpleItemGroupDto> groupls = new List<SimpleItemGroupDto>();

        public OccTargetDiseaseEdit()
        {
            InitializeComponent();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _OccTargetDiseaseAppServicee = new OccTargetDiseaseAppService();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            _OccDiseaseAppService = new OccDiseaseAppService();
            if (_Model == null) _Model = new OutTbmOccTargetDiseaseDto();

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public OccTargetDiseaseEdit(Guid id, OutTbmOccTargetDiseaseDto model) : this()
        {
            _id = id;
            _Model = model;
        }
        private void OccTargetDisease_Load(object sender, EventArgs e)
        {
            //危害因素
            OutOccHazardFactorDto show = new OutOccHazardFactorDto();
            show.IsActive = 3;
            var data = _OccHazardFactorAppService.ShowOccHazardFactor(show);
            searchLookUpEdit1.Properties.DataSource = data;

            ChargeBM chargeBM = new ChargeBM();         
            //检查类型
            chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
            var dll = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            searchLookUpEdit2.Properties.DataSource = dll;

            //职业健康 
            //chargeBM.Name = ZYBBasicDictionaryType.diagnose.ToString();
            //var dl1 = _OccDisproAppService.getOutOccDictionaryDto(chargeBM);
            //searchLookUpEdit3.Properties.DataSource = dl1;

            var OccDiseaseDtos = new Occdieaserucan();
            OccDiseaseDtos.IsActive = 1;
            var ConDictionary = _OccDiseaseAppService.GetAllOccDisease(OccDiseaseDtos);
            searchLookUpEdit3.Properties.DataSource = ConDictionary;

            //职业健康禁忌证
            chargeBM.Name = ZYBBasicDictionaryType.Contraindication.ToString();
            var dls = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            searchLookUpEdit4.Properties.DataSource = dls;
            BindgrdOptionalItemGroup();

            chargeBM.Name = ZYBBasicDictionaryType.SymptomType.ToString();
            var dler = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            searchLookUpEdit5.Properties.DataSource = dler;

            searchLookUpItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();
            BindgrdOptionalItemGroup();

            if (_id != Guid.Empty)
            {
                LoadData();
            }

        }

        public void BindgrdOptionalItemGroup()
        {
            var output = new List<SimpleItemGroupDto>();
            lstsimpleItemGroupDtos = DefinedCacheHelper.GetItemGroups();
            output = lstsimpleItemGroupDtos.ToList();
            gridControl5.DataSource = output.OrderBy(n => n.Department?.OrderNum).ThenBy(n => n.OrderNum)?.ToList();
            gridControl5.RefreshDataSource();
            groupls = DefinedCacheHelper.GetItemGroups().ToList();
        }

        private void searchLookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit5.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)searchLookUpEdit5.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl3.DataSource as List<TbmOccTargetDiseaseSymptomsDto>;
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
                        dataresult = new List<TbmOccTargetDiseaseSymptomsDto>();

                    }
                    var FactorsDto = new TbmOccTargetDiseaseSymptomsDto();
                    FactorsDto.Text = RowData.Text;
                    FactorsDto.OrderNum = RowData.OrderNum;
                    FactorsDto.Id = RowData.Id;
                    dataresult.Add(FactorsDto);
                    gridControl3.DataSource = dataresult;
                    gridControl3.RefreshDataSource();
                    gridControl3.Refresh();


                }
                searchLookUpEdit5.EditValue = null;
            }
        }

        private void searchLookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit3.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDiseaseDto)searchLookUpEdit3.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl1.DataSource as List<TbmOccDiseaseDto>;
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
                        dataresult = new List<TbmOccDiseaseDto>();

                    }
                    var FactorsDto = new TbmOccDiseaseDto();
                    FactorsDto.IsShow = 0;
                    FactorsDto.Text = RowData.Text;
                    FactorsDto.HelpChar = RowData.HelpChar;
                    FactorsDto.Id = RowData.Id;
                    dataresult.Add(FactorsDto);
                    gridControl1.DataSource = dataresult;
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();


                }
                 searchLookUpEdit3.EditValue = null;
            }
        }

        private void searchLookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit4.GetSelectedDataRow() != null)
            {
                var RowData = (OutOccDictionaryDto)searchLookUpEdit4.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridControl2.DataSource as List<TbmOccTargetDiseaseContraindicationDto>;
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
                        dataresult = new List<TbmOccTargetDiseaseContraindicationDto>();

                    }
                    var FactorsDto = new TbmOccTargetDiseaseContraindicationDto();                   
                    FactorsDto.Text = RowData.Text;
                    FactorsDto.OrderNum = RowData.OrderNum;
                    dataresult.Add(FactorsDto);
                    gridControl2.DataSource = dataresult;
                    gridControl2.RefreshDataSource();
                    gridControl2.Refresh();


                }
                 searchLookUpEdit4.EditValue = null;
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void Add()
        {
            var dtos = gridControl5.GetSelectedRowDtos<SimpleItemGroupDto>();
            var itemSuitItemGroups = dtos.Select(m => new TbmItemGroupDto
            {
                ItemGroup = m,
                ItemGroupId = m.Id,
                ItemGroupName = m.ItemGroupName,
                Category = m.Department.Category,
                SelectType = comboBoxEdit1.SelectedItem.ToString(),
                Department = m.Department

                 
            }).ToList();

            var list = gridControl4.GetDtoListDataSource<TbmItemGroupDto>();
            if (list == null)
            {
                list = new List<TbmItemGroupDto>();
                gridControl4.DataSource = list;
            }
            list = list.OrderBy(p=>p.Department?.OrderNum).ToList();
           

            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.ItemGroupId == m.ItemGroup.Id));
            list.AddRange(itemSuitItemGroups);
            gridControl4.DataSource = list;
            gridControl4.RefreshDataSource();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var dtos = gridControl4.GetSelectedRowDtos<TbmItemGroupDto>();
            if (dtos.Count == 0) return;            
            gridControl4.GetDtoListDataSource<TbmItemGroupDto>()?.RemoveAll(m => dtos.Any(i => i.ItemGroupId == m.ItemGroupId));
            gridControl4.RefreshDataSource();
        }
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            CreateOrUpdateTargetDiseaseDto dto = new CreateOrUpdateTargetDiseaseDto();            
            //危害因素
            if (searchLookUpEdit1.EditValue != null)
            {
                dto.OccHazardFactorsId = (Guid)searchLookUpEdit1.EditValue;              
            }
            if (searchLookUpEdit2.EditValue!= null)
            {
                dto.CheckType = searchLookUpEdit2.EditValue.ToString();
            }
            dto.IsActive = radioGroup2.SelectedIndex;
            //检查对象
            dto.Crowd = textEdit1.Text.Trim();
            //处理意见
            dto.Opinions = memoEdit2.Text.Trim();
            //问诊提示          
            dto.InquiryTips = memoEdit1.Text.Trim();
            dto.InspectionCycle = memoEditInspectionCycle.Text.Trim();
            var s = gridControl4.GetDtoListDataSource<TbmItemGroupDto>();

            var Mustitem = s.Where(o => o.SelectType == "必选").Select(o => o.ItemGroupId).ToList();

            var Maytitem = s.Where(o => o.SelectType == "可选").Select(o => o.ItemGroupId).ToList();
            var item = gridItem.GetDtoListDataSource<ItemInfoSimpleDto>();

            if (_Model != null && _Model.Id != null)
            {
                dto.Id = _Model.Id;

            }
            List<TbmOccDiseaseDto> Disease = new List<TbmOccDiseaseDto>();
            List<TbmOccTargetDiseaseSymptomsDto> Symptoms = new List<TbmOccTargetDiseaseSymptomsDto>();
            List<TbmOccTargetDiseaseContraindicationDto> Contraindication = new List<TbmOccTargetDiseaseContraindicationDto>();
            Disease = (List<TbmOccDiseaseDto>)gridControl1.DataSource;
            Symptoms = (List<TbmOccTargetDiseaseSymptomsDto>)gridControl3.DataSource;
            Contraindication = (List<TbmOccTargetDiseaseContraindicationDto>)gridControl2.DataSource;          
            
            bool res = false;
            OutTbmOccTargetDiseaseDto dtos = null;
            AutoLoading(() =>
            {
                FullTargetDiseaseDto input = new FullTargetDiseaseDto()
                {
                    OneTargetDisease = dto,                    
                    OneDisease = Disease?.Select(o => o.Id).ToList(),
                    ManyTargetDiseaseSymptoms = Symptoms,
                    ManyTargetDiseaseContraindication= Contraindication,
                    MustGroups=Mustitem,
                    mayGroups= Maytitem,
                    ItemInfo= item?.Select(o=>o.Id).ToList(),
                };
                if (_Model.Id == Guid.Empty)
                {
                    dtos = _OccTargetDiseaseAppServicee.Add(input);

                }
                else
                {
                    dtos = _OccTargetDiseaseAppServicee.Edit(input);
                }
                _Model = dtos;
                res = true;
            });
            return res;
        }


        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridView3.GetFocusedRow() as TbmOccTargetDiseaseSymptomsDto;
            if (currentItem == null)
                return;

            var dataresult = gridControl3.DataSource as List<TbmOccTargetDiseaseSymptomsDto>;
            dataresult.Remove(currentItem);
            gridControl3.DataSource = dataresult;
            gridControl3.RefreshDataSource();
            gridControl3.Refresh();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridView1.GetFocusedRow() as TbmOccDiseaseDto;
            if (currentItem == null)
                return;

            var dataresult = gridControl1.DataSource as List<TbmOccDiseaseDto>;
            dataresult.Remove(currentItem);
            gridControl1.DataSource = dataresult;
            gridControl1.RefreshDataSource();
            gridControl1.Refresh();
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridView2.GetFocusedRow() as TbmOccTargetDiseaseContraindicationDto;
            if (currentItem == null)
                return;

            var dataresult = gridControl2.DataSource as List<TbmOccTargetDiseaseContraindicationDto>;
            dataresult.Remove(currentItem);
            gridControl2.DataSource = dataresult;
            gridControl2.RefreshDataSource();
            gridControl2.Refresh();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadData()
        {
            try
            {
                var data = _OccTargetDiseaseAppServicee.GetOccTargetDisease(new EntityDto<Guid> { Id = _id });
                searchLookUpEdit1.EditValue = data.OccHazardFactorsId;
                searchLookUpEdit2.EditValue = data.CheckType;
                radioGroup2.SelectedIndex = data.IsActive;
                textEdit1.EditValue = data.Crowd;
                memoEdit2.EditValue = data.Opinions;
                memoEdit1.EditValue = data.InquiryTips;
                memoEditInspectionCycle.EditValue = data.InspectionCycle;
                gridControl3.DataSource = data.Symptoms;
                gridControl1.DataSource = data.OccDiseases;
                gridControl2.DataSource = data.Contraindications;
                data.MayandMustIemandGroups=data.MustIemGroups;
                data.MayandMustIemandGroups.AddRange(data.MayIemGroups);
                gridControl4.DataSource = data.MayandMustIemandGroups.OrderBy(p=>p.Department?.OrderNum).ToList();
                if (data.MustIemGroups.Count > 0)
                {
                    foreach (var s in data.MustIemGroups)
                    {
                        
                        s.SelectType = "必选";
                        s.ItemGroupId = s.Id;
                        s.Category = s.Department?.Category;
                    }
                }
                if (data.MayIemGroups.Count > 0)
                {
                    foreach (var s in data.MayIemGroups)
                    {
                        s.SelectType = "可选";
                        s.ItemGroupId = s.Id;
                        s.Category = s.Department?.Category;
                    }
                }
                if (data.ItemInfo.Count > 0)
                {
                    gridItem.DataSource = data.ItemInfo;
                }


            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        private void gridView5_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Add();
            }
        }

        private void repositoryItemCheckEdit1_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
                  BindgrdOptionalItemGroups();
        }
      
        public void BindgrdOptionalItemGroups()
        {
            if (searchControl1.Text != "")
            {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<SimpleItemGroupDto>();
                output = groupls.Where(o => o.ItemGroupName.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();
                gridControl5.DataSource = output;
            }
            else
            {
                gridControl5.DataSource = groupls;
            }

        }

        private void searchLookUpItem_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpItem.GetSelectedDataRow() != null)
            {
                var RowData = (ItemInfoSimpleDto)searchLookUpItem.GetSelectedDataRow();
                bool IsHave = false;
                var dataresult = gridItem.DataSource as List<ItemInfoSimpleDto>;
                if (dataresult != null)
                {
                    foreach (var item in dataresult)
                    {
                        if (item.Name == RowData.Name)
                        {
                            IsHave = true;
                        }
                    }
                }
                if (!IsHave)
                {
                    if (dataresult == null)
                    {
                        dataresult = new List<ItemInfoSimpleDto>();

                    }
                    
                    dataresult.Add(RowData);
                    gridItem.DataSource = dataresult;
                    gridItem.RefreshDataSource();
                    gridItem.Refresh();


                }
                searchLookUpItem.EditValue = null;
            }
        }

        private void repositoryItemButtonEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridView6.GetFocusedRow() as ItemInfoSimpleDto;
            if (currentItem == null)
                return;
            
            var dataresult = gridItem.DataSource as List<ItemInfoSimpleDto>;
            dataresult.Remove(currentItem);
            gridItem.DataSource = dataresult;
            gridItem.RefreshDataSource();
            gridItem.Refresh();
        }

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                simpleButton2.PerformClick();
            }
        }
    }
}
