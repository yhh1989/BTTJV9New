using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DecodeKey;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Win.Common;
using HealthExaminationSystem.Win.Navigation;
using HealthExaminationSystem.Win.Properties;
using Newtonsoft.Json;
using SunwayFortune.Hospital.Updater.Plugin;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Regist;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.CompanyReport;
using Sw.Hospital.HealthExaminationSystem.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Foreground;
using Sw.Hospital.HealthExaminationSystem.Market;
using Sw.Hospital.HealthExaminationSystem.SchedulingSecondEdition;
using Sw.Hospital.HealthExaminationSystem.UserSettings.DynamicColumnDirectory;

namespace HealthExaminationSystem.Win
{
    internal static class Program
    {
        internal static Mutex Mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] agrs)
        {
            SettingHelper.UpgradeSetting();

            if (Array.Find(agrs, r => r == "HomePage") != null)
            {
                Application.Run(new HomePage());
                return;
            }

            if (Array.Find(agrs, r => r == "ComplaintManager") != null)
            {
                Application.Run(new ComplaintManager());
                return;
            }
            if (Array.Find(agrs, r => r == "BloodWorkstation") != null)
            {
                Application.Run(new BloodWorkstation());
                return;
            }
            if (Array.Find(agrs, r => r == "DynamicColumn") != null)
            {
                using (var frm = new LoginNew())
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                Application.Run(new FormDynamicColumnTest());
                return;
            }
            if (Array.Find(agrs, r => r == "AppointmentCalendar") != null)
            {
                using (var frm = new LoginNew())
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                Application.Run(new FormAppointmentCalendar());
                return;
            }
            if (Array.Find(agrs, r => r == "ParentCompanyRegisterReportPrinter") != null)
            {
                using (var frm = new LoginNew())
                {
                    if (frm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                Application.Run(new XtraFormParentCompanyRegisterReportPrinter());
                return;
            }
            using (Mutex = new Mutex(true, "82C53F9E-BC14-476E-8A0F-AC433131C2C9", out var createdNew))
            {
                //if (createdNew)
                if (true)
                {
                    if (agrs.Length == 0 || agrs[0] != "NotAutoUpdate")
                    {
                        UpdateUtils.CheckNewVersionSnyc();
                    }

                    // 设置全局异常
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    Application.ThreadException += Application_ThreadException;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();
                    UserLookAndFeel.Default.SetSkinStyle(Settings.Default.SkinStyle);
                    AppearanceObject.DefaultFont = new Font(AppearanceObject.DefaultFont.Name, Settings.Default.EmSize);
                    AppearanceObject.DefaultMenuFont = new Font(AppearanceObject.DefaultMenuFont.Name, Settings.Default.EmSize);

                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateFormatString = "yyyy-MM-dd HH:mm:ss",
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    JsonConvert.DefaultSettings = () => jsonSerializerSettings;
                    //if (Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
                    //{
                    //    var currentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                    //    currentCulture.DateTimeFormat.DateSeparator = Variables.DateSeparator;
                    //    currentCulture.DateTimeFormat.TimeSeparator = Variables.TimeSeparator;
                    //    currentCulture.DateTimeFormat.ShortDatePattern = Variables.ShortDatePattern;
                    //    currentCulture.DateTimeFormat.LongDatePattern = Variables.LongDatePattern;
                    //    currentCulture.DateTimeFormat.ShortTimePattern = Variables.ShortTimePattern;
                    //    currentCulture.DateTimeFormat.LongTimePattern = Variables.LongTimePattern;
                    //    currentCulture.DateTimeFormat.FullDateTimePattern = Variables.FullDateTimePattern;
                    //    Thread.CurrentThread.CurrentCulture = currentCulture;
                    //}
                    //Application.Run(new StartupTest());
                    //Application.Run(new DoctorDeskNew());

                    var SSOParm = Array.Find(agrs, r => r.ToString().Length > 100);
                    bool isWh5ySSO = false;
                    bool is3ySSO = false;
                    var sso3YuanParm = agrs.FirstOrDefault(x => x.Contains("runtype"));
                    if (sso3YuanParm != null)
                    {
                        if (sso3YuanParm.Contains("ekingsso"))
                        {

                            is3ySSO = true;

                        }

                    }
                    if (is3ySSO)
                    {
                        string sso3yAppName = string.Empty;
                        string sso3yticket = string.Empty;
                        var sso3YuanParm_ticket = agrs.FirstOrDefault(x => x.Contains("ticket"));
                        if (sso3YuanParm_ticket != null)
                        {
                            sso3yticket = sso3YuanParm_ticket.Trim().TrimStart('-').TrimStart('–').Trim().TrimStart("ticket".ToCharArray()).Trim().TrimStart('=');

                        }
                        //var sso3YuanParm_appName = agrs.FirstOrDefault(x => x.Contains("appName"));
                        //if (sso3YuanParm_appName != null)
                        //{
                        //    sso3yAppName = sso3YuanParm_appName.Trim().TrimStart('-').TrimStart('–').Trim().TrimStart("appName".ToCharArray()).Trim().TrimStart('=');

                        //}
                        string appGroup = "TJ";
                       
                        if (string.IsNullOrWhiteSpace(sso3yticket))
                        {
                            MessageBox.Show("解析不到ticket");
                        }
                        if (string.IsNullOrWhiteSpace(sso3yAppName))
                        {
                            MessageBox.Show("解析不到AppName");
                        }
                        string url = "comm/api/ssoLogin?appGroup=" + appGroup + "&ticket=" + sso3yticket + "&appName=体检系统" ;
                        RestClient rc = new RestClient("http://132.147.66.18");
                        bool success = false;
                        string rData = rc.Get(out success, url);
                        var Resultdata = Newtonsoft.Json.JsonConvert.DeserializeObject<SanYuanSSoResultClass>(rData);

                        if (Resultdata.response.info.Trim()== "已经登录成功")
                        {
                            SSOParm = Resultdata.response.userId.Trim();
                            using (var frm = new LoginNew())
                            {

                                frm.Authenticate(SSOParm.ToString(), null);
                                if (frm.DialogResult != DialogResult.OK)
                                {
                                    return;
                                }
                            }
                        }

                    }

                    if (SSOParm != null)
                    {
                        try
                        {
                            CredentialEntity credentialEntity = CredentialUtil.GetCredential(SSOParm);
                            SSOParm = credentialEntity.username;
                            isWh5ySSO = true;

                        }
                        catch (Exception ex)
                        {
                            isWh5ySSO = false;
                        }

                    }
                    if (isWh5ySSO)
                    {
                        using (var frm = new LoginNew())
                        {

                            frm.Authenticate(SSOParm.ToString(), null);
                            if (frm.DialogResult != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        using (var frm = new LoginNew())
                        {
                            if (frm.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }
                    //1包含职业健康，0不包含
                    Variables.ISZYB = "1";
                    Variables.ISReg = "1";
                    RegistAppService _RegistAppService = new RegistAppService();
                    var reg = _RegistAppService.SearchRegsit();
                    //公钥
                    var Key = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQClpPjlXwi2HfHwracTUbNbj1fb
osN5kLA4VUAKHn4rfe2B/HNvJPnpMhkP1LlHb+B3KsoXoNz+yEnh3huQn3QiiuMW
AwfFP4i5JW48bKfqg6pVijFvUWMR7B+ICS0X6/1nwbbpg6nJw/KwjGm6xDzP/LiI
/M2WJte5QXvw4/iqPwIDAQAB
-----END PUBLIC KEY-----";
                    if (reg.Count > 0)
                    {
                        Variables.RegName = reg[0].ClientName;
                        var OutRes = RsaCrypt.RsaPublicKeyDecryptUnrestrictedLength(Key, reg[0].RegistCode);
                        var lis = OutRes.Split(',');
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
                    if (Variables.ISZYB == "2")
                    {
                        Application.Run(new RibbonStartupZYB());
                    }
                    else
                    {
                        Application.Run(new RibbonStartup());
                    }

                    Mutex.Close();
                }
                else
                {
                    XtraMessageBox.Show("程序已经有运行的实例，请停止后再次运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                var str = GetExceptionMsg(e.Exception, e.ToString());
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"LastExceptionInfo-{DateTime.Now:yyyyMMdd}.txt"), str);
            }
            catch
            {
                // ignored
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"LastExceptionInfo-{DateTime.Now:yyyyMMdd}.txt"), str);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>  
        /// 生成自定义异常消息  
        /// </summary>  
        /// <param name="ex">异常对象</param>  
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>  
        /// <returns>异常字符串文本</returns>  
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            var sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now);
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);

                if (ex.InnerException != null)
                {
                    var result = GetExceptionMsg(ex.InnerException, backStr);
                    sb.AppendLine(result);
                }
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
    public class SanYuanSSoResultClass
    {
        public SanYuanSSoResultClass_response response { get; set; }
    }

    public class SanYuanSSoResultClass_response
    {
        public string info { get; set; }
        public string userId { get; set; }

    }
    
}
