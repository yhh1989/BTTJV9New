using Abp.Web.Mvc.Authorization;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class ResultStaticController: Controller
    {
        private readonly CustomerAppService _customerAppService; 

        public ResultStaticController
         (
            CustomerAppService customerAppService
         )
        {
            _customerAppService = customerAppService;
        }
        // GET: ResultStatic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetResult(string TeamName, string sdateTime, string edateTime, string select,string select1)
        {
            SickNessDto sickNessDto = new SickNessDto();
            sickNessDto.SatrtDateTime = Convert.ToDateTime(sdateTime);
            sickNessDto.EndDateTime = Convert.ToDateTime(edateTime);
            sickNessDto.Name = select;
            sickNessDto.ItemName = TeamName;
            sickNessDto.NucleicAcidType = select1;
            var result = _customerAppService.GetDiseaseAppServices(sickNessDto);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJg()
        {
            List<AbpTenantsDto> lists = new List<AbpTenantsDto>();
            lists = _customerAppService.GetAbpTenants();
            var data = new List<Select2>();
            lists.ForEach(r =>
            {
                data.Add(new Select2 { id = r.Id, text = r.Names });
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCzAll()
        {
            List<BdSatticDto> list = new List<BdSatticDto>();
            list = _customerAppService.GetBdCz();
            var data1 = new List<Select3>();
            list.ForEach(r =>
            {
                data1.Add(new Select3 { Value = r.Value, Text = r.Text });
            });
            return Json(data1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCZ(string TeamName)
        {
            if (TeamName == "")
            {
                
               TeamName = "1";
            }
            List<SimBasicDictionariesDto> lists = new List<SimBasicDictionariesDto>();
            AbpTenantsDto abpTenantsDto = new AbpTenantsDto();
            abpTenantsDto.Id = int.Parse(TeamName);
            lists = _customerAppService.GetCZ(abpTenantsDto);
           
            var data = new List<Select2>();
            data.Add(new Select2 { text = "" });
            lists.ForEach(r =>
            {
                data.Add(new Select2 { id = r.Value, text = r.Text });
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
  }

    public class Select2
    {
        public int? id { get; set; }

        public string text { get; set; }
    }
    public class Select3
    {
        public int? Value { get; set; }

        public string Text { get; set; }
    }
}