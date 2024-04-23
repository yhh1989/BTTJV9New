using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Properties;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Regist;
using Sw.Hospital.HealthExaminationSystem.Application.Regist;
using Sw.Hospital.HealthExaminationSystem.Application.Regist.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HealthExaminationSystem.Win
{
    public partial class frmRegisEdit : XtraForm
    {
        private readonly IRegistAppService _RegistAppService;
        TbmRegsitDto tbmRegsitDto = new TbmRegsitDto();
        public frmRegisEdit()
        {
            InitializeComponent();
            _RegistAppService = new RegistAppService();
        }

        private void frmRegisEdit_Load(object sender, EventArgs e)
        {
            var reg = _RegistAppService.SearchRegsit();
        
             
                string GroupType = string.Concat(Settings.Default.TenantName, ",", Variables.GetUrl());
                GroupType = Encrypt(GroupType, "BTV9BTV9");
            //测试
            //GroupType = string.Concat(Settings.Default.TenantName, ",", "http://101.200.81.160");
            //GroupType = Encrypt(GroupType, "BTV9BTV9");
            txtCode.Text = GroupType;
             
            reg = reg.Where(p=>p.MachineCode == GroupType).ToList();             
            if (reg.Count > 0)
            {
                

                tbmRegsitDto = reg[0];
                txtRegCode.Text = reg[0].RegistCode;
                txtClientName.Text = reg[0].ClientName;


            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtClientName.Text))
            {
                dxErrorProvider.SetError(txtClientName, string.Format("{0}为必填项！", "客户名称"));
                txtClientName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                dxErrorProvider.SetError(txtCode, string.Format("{0}为必填项！", "机构代码"));
                txtCode.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtRegCode.Text))
            {
                dxErrorProvider.SetError(txtRegCode, string.Format("{0}为必填项！", "注册码"));
                txtCode.Focus();
                return;
            }

            //  var endTime = RsaHelper.Decrypt(txtRegCode.Text, txtCode.Text, txtClientName.Text);
            var Key = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQClpPjlXwi2HfHwracTUbNbj1fb
osN5kLA4VUAKHn4rfe2B/HNvJPnpMhkP1LlHb+B3KsoXoNz+yEnh3huQn3QiiuMW
AwfFP4i5JW48bKfqg6pVijFvUWMR7B+ICS0X6/1nwbbpg6nJw/KwjGm6xDzP/LiI
/M2WJte5QXvw4/iqPwIDAQAB
-----END PUBLIC KEY-----";
            var OutRes = RsaCrypt.RsaPublicKeyDecryptUnrestrictedLength(Key, txtRegCode.Text);
            var lis = OutRes.Split(',');

            if (lis.Length >= 3 && lis[0] != txtCode.Text)
            {
                XtraMessageBox.Show("机器码不匹配！");
                return;
            }
            if (lis.Length >= 3 && lis[1] != txtClientName.Text)
            {
                XtraMessageBox.Show("用户名称不匹配！");
                return;
            }
            DateTime endTime;
            try
            {

                endTime =     DateTime.Parse(lis[2].ToString());
            }
            catch
            {
                XtraMessageBox.Show("解码时间格式不正确！" + lis[2].ToString());
                return;
            }
            
            if (lis.Length >= 3 && endTime <= DateTime.Now)
            {
                XtraMessageBox.Show("解码时间："+ lis[2].ToString() + "必须大于当前日期！" + DateTime.Now);
                return;
            }
            if (lis.Length>=3 && lis[0]== txtCode.Text && lis[1]== txtClientName.Text && endTime > DateTime.Now)
            {
                
                tbmRegsitDto.ClientName = txtClientName.Text;
                tbmRegsitDto.MachineCode = txtCode.Text;
                tbmRegsitDto.RegistCode = txtRegCode.Text;
                _RegistAppService.SaveRegsit(tbmRegsitDto);
                XtraMessageBox.Show("授权成功！授权至" + endTime);                

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                XtraMessageBox.Show("授权失败！");
            }
        }
        public string Encrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
