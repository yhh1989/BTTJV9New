using System;
using System.Configuration;
using System.IO;
#if Common
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Common.GlobalSources
{
    /// <summary>
    /// 静态变量定义
    /// </summary>
    public static class Variables
    {
        static Variables()
        {
            LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
            else
            {
                var files = Directory.GetFiles(LogDirectory);
                var time = DateTime.Today.AddDays(-3);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.LastWriteTime < time)
                    {
                        fileInfo.Delete();
                    }
                }
            }
        }
#if Common
        /// <summary>
        /// 登陆用户数据
        /// </summary>
        public static UserViewDto User { get; set; }
#endif

        /// <summary>
        /// 是否启用权限
        /// </summary>
        public static bool PermissionEnabled { get; set; }

        /// <summary>
        /// API 地址
        /// </summary>
        public const string ApiUrl = "ApiUrl";

        /// <summary>
        /// 体检数据库连接 
        /// </summary>
        public const string tj = "tj";
        /// <summary>
        /// 排队数据库连接 
        /// </summary>
        public const string pd = "pd";
        /// <summary>
        /// 登陆地址
        /// </summary>
        public const string LoginUrl = "LoginUrl";

        /// <summary>
        /// UserName
        /// </summary>
        public const string UserName = "UserName";
        /// <summary>
        /// UserID
        /// </summary>
        public const string UserID = "UserID";

        /// <summary>
        /// 网站地址
        /// </summary>
        public const string Url = "Url";

        /// <summary>
        /// 报表模板地址
        /// </summary>
        public const string GrfDirectory = "GridppTemplate";

        /// <summary>
        /// 星号
        /// </summary>
        public const string Star = "✶";

        /// <summary>
        /// 红色星号
        /// </summary>
        public const string RedStar = "<Color=Red>✶</Color>";

        /// <summary>
        /// 左箭头
        /// </summary>
        public const string LeftArrows = "◄";

        /// <summary>
        /// 右箭头
        /// </summary>
        public const string RightArrows = "►";

        /// <summary>
        /// 必填项提示
        /// </summary>
        public const string MandatoryTips = "{0}为必填项！";

        /// <summary>
        /// 数字项提示
        /// </summary>
        public const string NumberTips = "{0}必须为数字！";

        /// <summary>
        /// 大于判断提示
        /// </summary>
        public const string GreaterThanTips = "{0}值不能大于{1}值！";

        /// <summary>
        /// 主窗体文本
        /// </summary>
        public static string Company { get; set; }

        /// <summary>
        /// 正在从云端加载数据
        /// </summary>
        public const string LoadingForCloud = "正在从云端加载数据...";

        /// <summary>
        /// 正在加载用户权限
        /// </summary>
        public const string LoadingForPermission = "正在加载用户权限...";

        /// <summary>
        /// 开始初始化窗体
        /// </summary>
        public const string LoadingForForm = "开始初始化窗体...";

        /// <summary>
        /// 加载完毕
        /// </summary>
        public const string LoadingForEnd = "加载完毕！";

        /// <summary>
        /// 加载出错
        /// </summary>
        public const string LoadingForError = "加载出错！";

        /// <summary>
        /// 正在保存数据
        /// </summary>
        public const string LoadingSaveing = "正在保存数据...";

        /// <summary>
        /// 正在删除数据
        /// </summary>
        public const string LoadingDelete = "正在删除数据...";

        /// <summary>
        /// 正在更新数据
        /// </summary>
        public const string LoadingUpdate = "正在更新数据...";

        /// <summary>
        /// 准备更新数据
        /// </summary>
        public const string LoadingReadyUpdate = "准备更新数据...";

        /// <summary>
        /// 初始化待编辑数据
        /// </summary>
        public const string LoadingEditor = "初始化待编辑数据...";

        /// <summary>
        /// 分页格式化
        /// </summary>
        public const string PageFormat = "第{0}页，共{1}页";

        /// <summary>
        /// 日期分割符
        /// </summary>
        public const string DateSeparator = "-";

        /// <summary>
        /// 时间分割符
        /// </summary>
        public const string TimeSeparator = ":";

        /// <summary>
        /// 短日期模式
        /// </summary>
        public const string ShortDatePattern = "yyyy-MM-dd";

        /// <summary>
        /// 长日期模式
        /// </summary>
        public const string LongDatePattern = "yyyy'年'M'月'd'日'";

        /// <summary>
        /// 短时间模式
        /// </summary>
        public const string ShortTimePattern = "HH:mm:ss";

        /// <summary>
        /// 长时间模式
        /// </summary>
        public const string LongTimePattern = "HH'时'mm'分'ss'秒'";

        /// <summary>
        /// 短完整日期/时间模式
        /// </summary>
        public const string ShortDateTimePattern = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 长完整日期/时间模式
        /// </summary>
        public const string FullDateTimePattern = "yyyy'年'M'月'd'日' HH'时'mm'分'ss'秒'";

        /// <summary>
        /// 菜单类型
        /// </summary>
        public const string MenuType = "菜单";

        /// <summary>
        /// 按钮类型
        /// </summary>
        public const string ButtonType = "按钮";

        /// <summary>
        /// 输入格式
        /// </summary>
        public const string BoxFormat = "{0}输入格式不正确";

        /// <summary>
        /// 输入格式
        /// </summary>
        public const string Negative = "{0}不能为负值";

        /// <summary>
        /// 日志目录
        /// </summary>
        public static string LogDirectory { get; }
        /// <summary>
        /// 是否注册
        /// </summary>
        public static string ISReg { get; set; }
        /// <summary>
        /// 是否包含职业健康 1全部 2职业健康3健康体检
        /// </summary>

        public static string ISZYB { get; set; }
        /// <summary>
        /// 注册单位名称
        /// </summary>

        public static string RegName { get; set; }

        /// <summary>
        /// 登录ID
        /// </summary>

        public static Guid? LoginKey { get; set; }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return ConfigurationManager.AppSettings.Get(Url);
        }
    }
}