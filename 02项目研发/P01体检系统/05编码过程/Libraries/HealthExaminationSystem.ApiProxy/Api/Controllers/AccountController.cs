using System;
using Sw.His.Common.Functional.Unit.NetworkTool;
using Sw.Hospital.HealthExaminationSystem.WebApi.Api.Models;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers
{
    public class AccountController : AppServiceApiProxyBase
    {
        public AccountController() : base("LoginUrl", "Controller")
        {

        }

        public static LoginModel LoginModel { get; set; }

        public string Authenticate(LoginModel loginModel)
        {
            if (loginModel.Password == null)
            {
                return AuthenticateNo1(loginModel.UsernameOrEmailAddress, loginModel.TenancyName);
            }
            LoginModel = loginModel;
            var result = GetResult<LoginModel, string>(loginModel, DynamicUriBuilder.GetAppSettingValue());
            StaticWebHelper.WebClient.Token = result;
            return result;
        }

        public string AuthenticateNo1(string usernameOrEmailAddress, string tenancyName)
        {
            LoginModel = new LoginModel
            {
                UsernameOrEmailAddress = usernameOrEmailAddress,
                Password = null,
                TenancyName = tenancyName
            };
            var nv = new NameValueCollectionExpert();
            nv.Set("usernameOrEmailAddress", usernameOrEmailAddress);
            nv.Set("tenancyName", tenancyName);
            var result = GetResult<string>(nv, DynamicUriBuilder.GetAppSettingValue());

            StaticWebHelper.WebClient.Token = result;
            return result;
        }
    }
}