using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Domain.Repositories;
using Abp.Web.Mvc.Authorization;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Market;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    /// <summary>
    /// 合同附件管理器
    /// </summary>
    [AbpMvcAuthorize]
    public class ContractAdjunctController : MyProjectControllerBase
    {
        /// <summary>
        /// 合同附件仓储
        /// </summary>
        private readonly IRepository<ContractAdjunct, Guid> _contractAdjunctRepository;

        /// <summary>
        /// 初始化合同附件管理器
        /// </summary>
        /// <param name="contractAdjunctRepository"></param>
        public ContractAdjunctController(IRepository<ContractAdjunct, Guid> contractAdjunctRepository)
        {
            _contractAdjunctRepository = contractAdjunctRepository;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Upload(Guid contractId, HttpPostedFileWrapper file)
        {
            var baseDirectory = Server.MapPath("~/");
            var fileDirectory = Path.Combine(baseDirectory, "Upload");
            var pictureDirectory = Path.Combine(fileDirectory, "Contract");
            var belongDirectory = Path.Combine(pictureDirectory, contractId.ToString("N").ToUpper());
            if (!Directory.Exists(belongDirectory))
                Directory.CreateDirectory(belongDirectory);
            var name = Path.GetFileName(file.FileName);
            var fileName = Path.Combine(belongDirectory, name);
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            file.SaveAs(fileName);
            var adjunct = new ContractAdjunct
            {
                Id = Guid.NewGuid(),
                ContractId = contractId,
                ContentType = file.ContentType,
                ContentLength = file.ContentLength,
                Name = name,
                FileName = fileName.Replace(baseDirectory, "")
            };
            var entity = await _contractAdjunctRepository.InsertAsync(adjunct);
            return Json(ObjectMapper.Map<ContractAdjunctDto>(entity));
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var adjunct = await _contractAdjunctRepository.GetAsync(id);
            var baseDirectory = Server.MapPath("~/");
            var fileName = Path.Combine(baseDirectory, adjunct.FileName);
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            await _contractAdjunctRepository.HardDeleteAsync(adjunct);
            return Json(ObjectMapper.Map<ContractAdjunctDto>(adjunct));
        }
    }
}