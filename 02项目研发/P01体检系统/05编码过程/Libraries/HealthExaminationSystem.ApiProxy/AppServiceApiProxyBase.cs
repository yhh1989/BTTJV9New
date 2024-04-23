using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.His.Common.Functional.Unit.NetworkTool.Models;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy
{
    public abstract class AppServiceApiProxyBase
    {
        public DynamicUriBuilder DynamicUriBuilder { get; set; }

        public static int LoginFrequency;

        protected AppServiceApiProxyBase() : this("ApiUrl", "AppService")
        {

        }

        protected AppServiceApiProxyBase(string key, string classEndRule)
        {
            DynamicUriBuilder = new DynamicUriBuilder(key)
            {
                ClassEndRule = classEndRule,
                MethodEndRule = "无"
            };
            if (StaticWebHelper.WebClient.Cookie == null)
            {
                // 使用中文本地化
                StaticWebHelper.WebClient.Cookie = new CookieContainer();
                var url = new Uri(DynamicUriBuilder.ToString());
                StaticWebHelper.WebClient.Cookie.Add(new Cookie("Abp.Localization.CultureName", "zh-CN", "/", url.Host));
            }
            StaticWebHelper.WebClient.Timeout = 1000 * 60 * 100;
            StaticWebHelper.WebClient.ReadWriteTimeout = 1000 * 60 * 100;
        }

        public TResult GetResult<TInput, TResult>(TInput value, Uri uri)
        {
            var json = JsonConvert.SerializeObject(value);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri, json);

            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
           {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    var userViewDto = JsonConvert.DeserializeObject<ApiResponse<TResult>>(userViewDtoJson);
                    return userViewDto.Result;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            return GetResult<TInput, TResult>(value, uri);
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
           }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    return GetResult<TInput, TResult>(value, uri);
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        public void GetResult<TInput>(TInput value, Uri uri)
        {
            //var uri = DynamicUriBuilder.GetAppSettingValue();
            var json = JsonConvert.SerializeObject(value);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri, json);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    JsonConvert.DeserializeObject<ApiResponse>(userViewDtoJson);
                    return;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            GetResult(value, uri);
                            return;
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    GetResult<TInput>(value, uri);
                    return;
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        /// <summary>
        /// 从指定 Uri 地址获取资源
        /// </summary>
        /// <param name="uri"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public TResult GetResult<TResult>(Uri uri)
        {
            //var uri = DynamicUriBuilder.GetAppSettingValue();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    var userViewDto = JsonConvert.DeserializeObject<ApiResponse<TResult>>(userViewDtoJson);
                    return userViewDto.Result;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            return GetResult<TResult>(uri);
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    return GetResult<TResult>(uri);
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        public TResult GetResult<TResult>(string file, NameValueCollection value, Uri uri)
        {
            //var uri = DynamicUriBuilder.GetAppSettingValue();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.UploadFile(uri, file, value);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    var userViewDto = JsonConvert.DeserializeObject<ApiResponse<TResult>>(userViewDtoJson);
                    return userViewDto.Result;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            foreach (Cookie item in StaticWebHelper.WebClient.Cookie.GetCookies(new Uri(DynamicUriBuilder.ToString())))
                            {
                                item.Expired = true;
                            }
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            return GetResult<TResult>(file, value, uri);
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    return GetResult<TResult>(file, value, uri);
                }
            }

            var e = ResponseError(result);
            throw e;

        }

        public TResult GetResult<TResult>(NameValueCollection value, Uri uri)
        {
            //var uri = DynamicUriBuilder.GetAppSettingValue();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri, value);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    var userViewDto = JsonConvert.DeserializeObject<ApiResponse<TResult>>(userViewDtoJson);
                    return userViewDto.Result;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            return GetResult<TResult>(value, uri);
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    return GetResult<TResult>(value, uri);
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        public void GetResult(NameValueCollection value, Uri uri)
        {
            //var uri = DynamicUriBuilder.GetAppSettingValue();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri, value);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    JsonConvert.DeserializeObject<ApiResponse>(userViewDtoJson);
                    return;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            GetResult(value, uri);
                            return;
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    GetResult(value, uri);
                    return;
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        public void GetResult(Uri uri)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = StaticWebHelper.WebClient.Post(uri);
            stopwatch.Stop();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("****************************分 割 线****************************");
            stringBuilder.AppendLine($"API Type：{GetType().FullName}");
            stringBuilder.AppendLine($"API 地址：{uri}");
            stringBuilder.AppendLine($"API 调用耗时：{stopwatch.ElapsedMilliseconds} 毫秒");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), stringBuilder.ToString());
            }
            catch
            {
                // ignored
            }

            if (result.Success)
            {
                LoginFrequency = 0;
                var userViewDtoJson = result.Response.ToString();
                try
                {
                    JsonConvert.DeserializeObject<ApiResponse>(userViewDtoJson);
                    return;
                }
                catch (JsonReaderException)
                {
                    if (result.StatusCode == (int)HttpStatusCode.OK && userViewDtoJson.Contains("<head>") && userViewDtoJson.Contains("</head>"))
                    {
                        if (LoginFrequency <= 3)
                        {
                            LoginFrequency = LoginFrequency + 1;
                            new AccountController().Authenticate(AccountController.LoginModel);
                            GetResult(uri);
                            return;
                        }
                    }
                    var e1 = ResponseError(result);
                    throw e1;
                }
            }

            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (LoginFrequency <= 3)
                {
                    LoginFrequency = LoginFrequency + 1;
                    new AccountController().Authenticate(AccountController.LoginModel);
                    GetResult(uri);
                    return;
                }
            }

            var e = ResponseError(result);
            throw e;
        }

        private UserFriendlyException ResponseError(Result result)
        {
            var responseJson = result.Response.ToString();
            SaveApiJsonError(responseJson);
            ApiResponse<string> response;
            try
            {
                response = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);
            }
            catch (JsonReaderException)
            {
                response = null;
            }
            var e = new UserFriendlyException();
            if (response != null && response.Abp)
            {
                e.Code = response.Error.Code;
                var errorStringBuilder = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(response.Error.Message))
                    errorStringBuilder.AppendFormat("Message:{0}", response.Error.Message);
                if (!string.IsNullOrWhiteSpace(response.Error.Details))
                {
                    if (errorStringBuilder.Length != 0)
                    {
                        errorStringBuilder.AppendLine();
                    }

                    errorStringBuilder.AppendFormat("Details:{0}", response.Error.Details);
                }

                e.Description = errorStringBuilder.ToString();
                e.Buttons = MessageBoxButtons.OK;
                e.Icon = MessageBoxIcon.Error;
            }
            else
            {
                e.Code = result.StatusCode;
                e.Description = result.StatusDescription.ToString();
                e.Buttons = MessageBoxButtons.OK;
                e.Icon = MessageBoxIcon.Error;
            }

            GC.Collect();
            return e; //401
        }

        private void SaveApiJsonError(string response)
        {
            var sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now);
            sb.AppendLine("详情：");
            sb.AppendLine(response);
            sb.AppendLine("***************************************************************");
            try
            {
                File.AppendAllText(Path.Combine(Variables.LogDirectory, $"LastApiJsonErrorInfo-{DateTime.Now:yyyyMMdd}.txt"), sb.ToString());
            }
            catch
            {
                // ignored
            }
        }
    }
}