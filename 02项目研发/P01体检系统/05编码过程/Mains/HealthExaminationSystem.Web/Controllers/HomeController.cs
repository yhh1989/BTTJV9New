using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MyProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}