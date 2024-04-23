using System.Collections.Generic;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes
{
    public class ClientInfoesAppService : AppServiceApiProxyBase, IClientInfoesAppService
    {
        public ClientInfosViewDto Add(ClientInfoesDto input)
        {
            return GetResult<ClientInfoesDto, ClientInfosViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Del(ClientInfoesDto input)
        {
            GetResult<ClientInfoesDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ClientInfoesDto Edit(ClientInfoesDto input)
        {
            return GetResult<ClientInfoesDto, ClientInfoesDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ClientInfosViewDto Get(ClientInfoesListInput input)
        {
            return GetResult<ClientInfoesListInput, ClientInfosViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ClientInfosViewDto> Query(ClientInfoesListInput input)
        {
            return GetResult<ClientInfoesListInput, List<ClientInfosViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ICollection<ClientInfosViewDto> GetAll()
        {
            return GetResult<ICollection<ClientInfosViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<ClientInfosViewDto> PageFulls(PageInputDto<ClientInfoesListInput> input)
        {
            return GetResult<PageInputDto<ClientInfoesListInput>, PageResultDto<ClientInfosViewDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ClientInfosNameDto> QueryClientName(ChargeBM input)
        {
            return GetResult<ChargeBM, List<ClientInfosNameDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        /// <inheritdoc />
        public Task<List<ClientInfoCacheDto>> GetAllForCache()
        {
            var url = DynamicUriBuilder.GetAppSettingValue();
            return Task<List<ClientInfoCacheDto>>.Factory.StartNew(() =>
                GetResult<List<ClientInfoCacheDto>>(url));
        }
    }
}