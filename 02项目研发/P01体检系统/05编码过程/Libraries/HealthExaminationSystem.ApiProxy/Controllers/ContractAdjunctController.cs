using System;
using System.Collections.Specialized;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers
{
    /// <summary>
    /// 合同附件管理器
    /// </summary>
    public class ContractAdjunctController : AppServiceApiProxyBase
    {
        /// <summary>
        /// 初始化合同附件管理器
        /// </summary>
        public ContractAdjunctController() : base("Url", "Controller")
        {

        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public ContractAdjunctDto Upload(Guid contractId, string file)
        {
            var value = new NameValueCollection();
            value.Set(nameof(contractId), contractId.ToString());
            return GetResult<ContractAdjunctDto>(file, value, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContractAdjunctDto Delete(Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            return GetResult<ContractAdjunctDto>(value, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}