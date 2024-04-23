using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraLayout;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class PaymentMethod : UserBaseForm
    {
        public List<ChargeTypeDto> ChargeType;
        public List<CreatePaymentDto> CreatePaymentList;
        private decimal clientmoney;
        public decimal Receivable;
        public PaymentMethod()
        {
            InitializeComponent();
        }
        public PaymentMethod(decimal pmoney)
        {
            InitializeComponent();
            clientmoney = pmoney;
            txtSurplusMoney.Text = clientmoney.ToString();
        }

        private void PaymentMethod_Load(object sender, EventArgs e)
        {
            if (Receivable != null)
            {
                txtSurplusMoney.Text = Receivable.ToString();
            }
           // ChargeType = ChargeType.Where(o => o.ChargeName != "会员卡").ToList();
            for (int i = 0; i < ChargeType.Count; i=i+2)
            {
                layoutControlGroupControls.BeginUpdate();
                var textEditl = new TextEdit();
                textEditl.Name = ChargeType[i].ChargeName;
                textEditl.Tag = ChargeType[i];
                textEditl.Properties.Mask.UseMaskAsDisplayFormat = true;
                textEditl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                textEditl.Properties.Mask.EditMask = "c";
                textEditl.Text = "0.00";
                textEditl.TextChanged += new EventHandler(textEditl_TextChanged);
                var item1 = layoutControlGroupControls.AddItem(textEditl.Name, textEditl);         
                if (!string.IsNullOrEmpty(ChargeType[i].Remarks) && ChargeType[i].Remarks!="")
                {
                    var textEditNumBer = new TextEdit();
                    textEditNumBer.Name = ChargeType[i].Id.ToString();
                    var item2 = layoutControlGroupControls.AddItem(ChargeType[i].Remarks, textEditNumBer);
                    item2.Move(item1, InsertType.Right);
                    layoutControlGroupControls.EndUpdate();
                    continue;

                }
                if (i + 1 < ChargeType.Count)
                {
                    if (string.IsNullOrEmpty(ChargeType[i + 1].Remarks) || ChargeType[i + 1].Remarks != "")
                    {
                        i = i - 1;
                        layoutControlGroupControls.EndUpdate();
                        continue;
                        
                    }
                    var textEditr = new TextEdit();
                    textEditr.Name = ChargeType[i].ChargeName;
                    textEditr.Tag = ChargeType[i];
                    textEditr.Properties.Mask.UseMaskAsDisplayFormat = true;
                    textEditr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    textEditr.Properties.Mask.EditMask = "c";
                    textEditr.Text = "0.00";

                    var item2 = layoutControlGroupControls.AddItem(textEditr.Name, textEditr);                  
                    item2.Move(item1, InsertType.Right);                 
                    
                }
                layoutControlGroupControls.EndUpdate();
            }
        }
        private void textEditl_TextChanged(object sender, EventArgs e)
        {
            //decimal ResultNum;
            //if (decimal.TryParse(textEditl.EditValue.ToString(), out ResultNum))
            //{
               
            //}
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            CreatePaymentList = new List<CreatePaymentDto>();
            decimal allmoney = 0;
            foreach (var conm in layoutControlGroupControls.Items)
            {
                LayoutControlItem controlItem = (LayoutControlItem)conm;

                var con = controlItem.Control;
                if (con.GetType().Name == "TextEdit")
                {
                    TextEdit textBox=(TextEdit)con;
                    if (textBox.Tag!=null &&  textBox.Tag.GetType().Name == "ChargeTypeDto" && textBox.EditValue.ToString() != "" && decimal.Parse(textBox.EditValue.ToString()) >0)
                    {
                        ChargeTypeDto chargeType = (ChargeTypeDto)textBox.Tag;
                        CreatePaymentDto CreatePayment = new CreatePaymentDto();
                        CreatePayment.Actualmoney = decimal.Parse(textBox.EditValue.ToString());
                        CreatePayment.Discount = 1;
                        CreatePayment.MChargeTypeId = chargeType.Id;
                        CreatePayment.MChargeTypename = chargeType.ChargeName;
                        CreatePayment.Shouldmoney= decimal.Parse(textBox.EditValue.ToString().Trim('¥'));
                        allmoney += CreatePayment.Shouldmoney;
                        if (chargeType.Remarks != null && chargeType.Remarks.ToString() != "")
                        {
                            foreach (var con1 in layoutControlGroupControls.Items)
                            {
                                if (con1.GetType().Name == "TextEdit" && ((TextEdit)con1).Name == chargeType.Id.ToString())
                                {
                                    CreatePayment.CardNum = ((TextEdit)con1).Text;
                                }
                            }
                        }
                        CreatePaymentList.Add(CreatePayment);

                    }

                }
            }
            if (CreatePaymentList.Count > 0)
            {
                if (clientmoney != null && clientmoney > 0 && allmoney != clientmoney)
                {
                    MessageBox.Show("输入金额和应收金额不符，请重新输入！");
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请输入支付金额！");
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
