using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Properties;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Regist;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Helpers
{
    /// <summary>
    /// 窗体帮助器
    /// </summary>
    public class FormHelper
    {
     

        /// <summary>
        /// 设置授权
        /// </summary>
        /// <param name="form"></param>
        /// <param name="item"></param>
        /// <param name="authorization"></param>
        public static void SetAuthorization(XtraForm form, BarButtonItem item, BarStaticItem authorization)
        {
            RegistAppService _RegistAppService=new RegistAppService();
            var reg = _RegistAppService.SearchRegsit();                    
            var date = DateTime.Now.AddDays(-1);
            var IP = Variables.GetUrl();
            string GroupType = string.Concat(Settings.Default.TenantName, ",", Variables.GetUrl());
            GroupType = Encrypt(GroupType, "BTV9BTV9");
            //公钥
            var Key = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQClpPjlXwi2HfHwracTUbNbj1fb
osN5kLA4VUAKHn4rfe2B/HNvJPnpMhkP1LlHb+B3KsoXoNz+yEnh3huQn3QiiuMW
AwfFP4i5JW48bKfqg6pVijFvUWMR7B+ICS0X6/1nwbbpg6nJw/KwjGm6xDzP/LiI
/M2WJte5QXvw4/iqPwIDAQAB
-----END PUBLIC KEY-----";
            reg = reg.Where(o => o.MachineCode == GroupType).ToList();
            if (reg.Count > 0)
            {
                try
                {
                  
                  var  OutRes = RsaCrypt.RsaPublicKeyDecryptUnrestrictedLength(Key,reg[0].RegistCode);
                    var lis = OutRes.Split(',');
                    if (lis.Length >= 3 && lis[0] == GroupType && lis[1] == reg[0].ClientName &&
                DateTime.TryParse(lis[2].ToString(), out DateTime endTime) && endTime > DateTime.Now)
                    {
                        date= endTime;
                        if (lis.Length >= 4)
                        {
                            if (lis[3] == "1")
                            {
                                Variables.ISZYB = "1";
                            }
                            else if (lis[3] == "2")
                            {
                                Variables.ISZYB = "2";
                            }
                            else
                            {
                                Variables.ISZYB = "0";
                            }
                        }
                    }
                    else
                    {

                        Variables.ISReg = "0";
                        var dialogResult = XtraMessageBox.Show(form, "解码失败请重新授权！", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Stop);
                        if (dialogResult == DialogResult.OK)
                        {
                            item.PerformClick();
                            SetAuthorization(form, item, authorization);
                            return;
                        }
                        else
                        {

                            System.Windows.Forms.Application.Exit();
                        }


                    }
                }
                catch (Exception)
                {
                    Variables.ISReg = "0";
                    var dialogResult = XtraMessageBox.Show(form, "解码失败请重新授权！", "提示", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Stop);
                    if (dialogResult == DialogResult.OK)
                    {
                        item.PerformClick();
                        SetAuthorization(form, item, authorization);
                        return;
                    }
                    else
                    {

                        System.Windows.Forms.Application.Exit();
                    }

                }
            }
            else
            {
                Variables.ISReg = "0";
                //增加30天试用版
                
                if (string.IsNullOrWhiteSpace(Settings.Default.Key))
                {
                    var result = RsaHelper.Encryption(DateTime.Now.AddDays(30));
                    Settings.Default.Key = result;
                    Settings.Default.Save();
                }
                var key = Settings.Default.Key;
                var dateend = RsaHelper.Decrypt(key);
                authorization.Caption = $@"试用使用至{dateend:D}";
                form.Text = $@"{form.Text}【试用版】";
                date = dateend;
            }
            authorization.Caption = $@"授权使用至{date:D}";
            TimeSpan ts = date - DateTime.Now;
            int sub = ts.Days;
            if (sub <= 10 && sub >= 0)
            {

                form.Text = $@"{form.Text}【即将到期】";
                XtraMessageBox.Show(form, "当前版本" + date + "到期，请尽快联系管理员授权！", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (date <= DateTime.Now)
            {
                Variables.ISReg = "0";
                var dialogResult = XtraMessageBox.Show(form, "授权已经到期，必须授权！", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Stop);
                if (dialogResult == DialogResult.OK)
                {
                    item.PerformClick();
                    SetAuthorization(form, item, authorization);
                }
                else
                {

                    System.Windows.Forms.Application.Exit();
                }
            }
        }
        public static string Encrypt(string pToEncrypt, string sKey)
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
    }
}