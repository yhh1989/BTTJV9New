using Abp.Web.Mvc.Views;
using Sw.Hospital.HealthExaminationSystem.Core;

namespace Sw.Hospital.HealthExaminationSystem.Web.Views
{
    public abstract class MyProjectWebViewPageBase : MyProjectWebViewPageBase<dynamic>
    {

    }

    public abstract class MyProjectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MyProjectWebViewPageBase()
        {
            LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
        }
    }
}