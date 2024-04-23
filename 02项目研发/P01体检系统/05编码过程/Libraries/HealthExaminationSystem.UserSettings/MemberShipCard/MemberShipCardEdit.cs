using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.MemberShipCard
{
    public partial class MemberShipCardEdit : UserBaseForm
    {
        public TbmItemSuitDto occpast = new TbmItemSuitDto();
        private readonly Guid _id;
        public TbmCardTypeDto _Model { get; private set; }
        private readonly IMemberShipCardAppService _MemberShipCardAppService;
         MemberShipCardAppService MemberShipAppService = new MemberShipCardAppService();
        
        public MemberShipCardEdit()
        {
            InitializeComponent();
            _MemberShipCardAppService = new MemberShipCardAppService();
            if (_Model == null) _Model = new TbmCardTypeDto();
        }
        public string OrderNums;
        public string ItemSuitNames;

        private void MemberShipCardEdit_Load(object sender, EventArgs e)
        {
            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 1, Name = "有期限" });
            list.Add(new EnumModel { Id = 2, Name = "无期限" });
            comboBoxEdit1.Properties.DataSource = list;

            if (_id != Guid.Empty)
            {   
                LoadData();
            }
            if (_id == Guid.Empty)
            {
                var query = _MemberShipCardAppService.GetAll();
                textEdit1.EditValue = query + 1;
            }

        }
        public MemberShipCardEdit(Guid id) : this()
        {
            _id = id;
            //_Model = model;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {          
            using (var frm = new ItemSuitLists())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var griddatda = gridControl1.DataSource as List<TbmItemSuitDto>;
                    if (griddatda == null)
                    {
                        griddatda = new List<TbmItemSuitDto>();
                    }
                    griddatda.Add(frm.occpast);
                    gridControl1.DataSource = griddatda;
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();
                }
            }
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.EditValue.ToString() == "1")
            {
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (string.IsNullOrWhiteSpace(textEdit1.Text))
            {
                dxErrorProvider.SetError(textEdit1, string.Format(Variables.MandatoryTips, "编号"));
                textEdit1.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textEdit2.Text))
            {
                dxErrorProvider.SetError(textEdit2, string.Format(Variables.MandatoryTips, "卡名"));
                textEdit2.Focus();
                return false;
            }
            if (comboBoxEdit1.EditValue==null)
            {
                dxErrorProvider.SetError(comboBoxEdit1, string.Format(Variables.MandatoryTips, "是否有期限"));
                comboBoxEdit1.Focus();
                return false;
            }
            dxErrorProvider.ClearErrors();
            TbmCardTypeDto dto = new TbmCardTypeDto();
            dto.CardNum = Convert.ToInt32(textEdit1.Text);
            dto.CardName = textEdit2.Text.Trim();
            dto.Term = (int)comboBoxEdit1.EditValue;
            dto.Available = 1;
            if (textEdit4.Text != null)
            {
                dto.Months = Convert.ToInt32(textEdit4.Text);
            }
            dto.CardType = textEdit3.Text.Trim();
            dto.Available = 1;
            List<TbmItemSuitDto> ItemSuitDto = new List<TbmItemSuitDto>();
            ItemSuitDto = (List<TbmItemSuitDto>)gridControl1.DataSource;
            
            dto.FaceValue = Convert.ToDecimal(textEdit5.EditValue);
            if (_id != null)
            {
                dto.Id = _id;

            }
            bool res = false;
            TbmCardTypeDto dtos = null;
            AutoLoading(() =>
            {
                FullTbmCardTypeDto input = new FullTbmCardTypeDto()
                {
                    OneTbmCardType = dto,
                    ManyTbmItemSuit = ItemSuitDto?.Select(o => o.Id).ToList(),              
                };
                if (_id == Guid.Empty)
                {
                    dtos = _MemberShipCardAppService.Add(input);

                }
                else
                {
                    dtos = _MemberShipCardAppService.Edit(input);
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
                var data = _MemberShipCardAppService.GetTbmCardType(new EntityDto<Guid> { Id = _id });
                textEdit1.EditValue = data.CardNum;
                textEdit2.EditValue = data.CardName;
                comboBoxEdit1.EditValue = data.Term;
                if (data.Months != null)
                {
                    textEdit4.EditValue = data.Months;
                }
                textEdit5.EditValue = data.FaceValue;
                textEdit3.EditValue = data.CardType;              
                gridControl1.DataSource = data.ItemSuits;
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            var currentItem = gridView1.GetFocusedRow() as TbmItemSuitDto;
            if (currentItem == null)
                return;

            var dataresult = gridControl1.DataSource as List<TbmItemSuitDto>;
            dataresult.Remove(currentItem);
            gridControl1.DataSource = dataresult;
            gridControl1.RefreshDataSource();
            gridControl1.Refresh();
        }
    }
}
