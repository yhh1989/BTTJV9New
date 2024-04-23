using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
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
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class frmProcExpress : UserBaseForm
    {
        ItemInfoAppService _itemInfoAppService = new ItemInfoAppService();
        public List<ItemInfoSimpleDto> itemlis;
        public ItemProcExpressDto ItemProcExpress;
        public List<ItemInfoSimpleDto> additemlis;

        public frmProcExpress()
        {
            InitializeComponent();
            additemlis = new List<ItemInfoSimpleDto>();
        }
        public frmProcExpress(ItemProcExpressDto itemProcExpressls)
        {
            InitializeComponent();
            additemlis = new List<ItemInfoSimpleDto>();
            ItemProcExpress = itemProcExpressls;
        }

        private void userBaseControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void frmProcExpress_Load(object sender, EventArgs e)
        {
            rtbExpress.ReadOnly = true;
            gridViewItem.OptionsView.ShowIndicator = false;//不显示指示器
            gridViewItem.OptionsBehavior.ReadOnly = false;
            gridViewItem.OptionsBehavior.Editable = false;
            //绑定项目
            int itemtype = (int)ItemType.Calculation;

            // itemlis = _itemInfoAppService.GetAll().OrderBy(o => o.Department.Name).ThenBy(o => o.OrderNum).ToList();
            itemlis = DefinedCacheHelper.GetItemInfos();           
            txtItemName.Properties.DataSource = itemlis.Where(o=>o.moneyType== itemtype).ToList();
            txtItemName.Properties.DisplayMember = "Name";
            txtItemName.Properties.ValueMember = "Id";
            if (ItemProcExpress != null)
            {
                txtItemName.EditValue = ItemProcExpress.ItemId;
                rtbExpress.Text = ItemProcExpress.FormulaText;
                rtbExpress.Tag = ItemProcExpress.FormulaValue;

            }
        }
       

        private void txtItemName_EditValueChanged(object sender, EventArgs e)
        {
            if (txtItemName.EditValue !=null)
            {
                
                var itemData =  (ItemInfoSimpleDto)txtItemName.GetSelectedDataRow();
                if (itemData == null)
                {
                    return;
                }
                int itemtype = (int)ItemType.Number;
                var itemshow = itemlis.Where(o =>o.Department!=null && o.Department.Id == itemData.Department.Id &&  o.moneyType== itemtype).ToList();
                gridControlItem.DataSource = itemshow;
            }
        }

        private void gridViewItem_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
               
                var itemrow=  gridControlItem.GetFocusedRowDto<ItemInfoSimpleDto>();
                if (itemrow !=null)
                {
                    rtbExpress.Text += "[" + itemrow.Name + "]";
                    rtbExpress.Tag += "[" + itemrow.Id + "]";
                    additemlis.Add(itemrow);
                }
            }
        }

        private void but1_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "1";
            rtbExpress.Tag += "1";
        }

        private void but2_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "2";
            rtbExpress.Tag += "2";
        }

        private void but3_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "3";
            rtbExpress.Tag += "3";
        }

        private void but4_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "4";
            rtbExpress.Tag += "4";
        }

        private void but5_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "5";
            rtbExpress.Tag += "5";
        }

        private void but6_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "6";
            rtbExpress.Tag += "6";
        }

        private void but7_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "7";
            rtbExpress.Tag += "7";
        }

        private void but8_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "8";
            rtbExpress.Tag += "8";
        }

        private void but9_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "9";
            rtbExpress.Tag += "9";
        }

        private void but0_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "0";
            rtbExpress.Tag += "0";
        }

        private void butjia_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "+";
            rtbExpress.Tag += "+";
        }

        private void butjian_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "-";
            rtbExpress.Tag += "-";
        }

        private void butcheng_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "*";
            rtbExpress.Tag += "*";
        }

        private void butchu_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "/";
            rtbExpress.Tag += "/";
        }

        private void butdian_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += ".";
            rtbExpress.Tag += ".";
        }

        private void butLeft_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "(";
            rtbExpress.Tag += "(";
        }

        private void butRight_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += ")";
            rtbExpress.Tag += ")";
        }

        private void butSQR_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "SQR(";
            rtbExpress.Tag += "SQR(";
        }

        private void butSQRT_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "SQRT(";
            rtbExpress.Tag += "SQRT(";
        }

        private void butMin_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "MIN(";
            rtbExpress.Tag += "MIN(";
          
        }

        private void butMax_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += "MAX(";
            rtbExpress.Tag += "MAX(";
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            if (rtbExpress.Tag == null || txtItemName.EditValue == null)
            {
                MessageBox.Show("请完善信息！");
                return;
            }

            butOK.Enabled = false;
            if (ItemProcExpress == null)
            {
                ItemProcExpress = new ItemProcExpressDto();
            }
            ItemProcExpress.FormulaText = rtbExpress.Text;
            ItemProcExpress.FormulaValue = rtbExpress.Tag.ToString();
            ItemProcExpress.ItemId =Guid.Parse(txtItemName.EditValue.ToString());
            ItemProcExpress.ItemName = txtItemName.Text.ToString();
            ItemProcExpress.ItemInfoReRelations = additemlis;           
            ItemProcExpressDto itemProcExpress = _itemInfoAppService.SaveItemExpress(ItemProcExpress);
            butOK.Enabled = true;
            DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void butDh_Click(object sender, EventArgs e)
        {
            rtbExpress.Text += ",";
            rtbExpress.Tag += ",";
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            rtbExpress.Text = "";
            rtbExpress.Tag = "";
            additemlis.Clear(); ;
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            string bExpressText = rtbExpress.Text;
            string bExpressValue = rtbExpress.Tag.ToString();
            string lsword = bExpressValue.Substring(bExpressValue.Length-1, 1);
            if (lsword != "]")
            {
                rtbExpress.Text = bExpressText.Substring(0, bExpressText.Length - 1);
                rtbExpress.Tag = bExpressValue.Substring(0, bExpressValue.Length - 1);

            }
            else
            {
                rtbExpress.Text = bExpressText.Substring(0, bExpressText.LastIndexOf("["));
                rtbExpress.Tag = bExpressValue.Substring(0, bExpressValue.LastIndexOf("["));
                var itemid= bExpressValue.Substring(bExpressValue.LastIndexOf("[")+1 , bExpressValue.LastIndexOf("]")- bExpressValue.LastIndexOf("[")-1);
                additemlis= additemlis.Where(o => o.Id != Guid.Parse(itemid)).ToList();
                    
            }
        }
    }
}
