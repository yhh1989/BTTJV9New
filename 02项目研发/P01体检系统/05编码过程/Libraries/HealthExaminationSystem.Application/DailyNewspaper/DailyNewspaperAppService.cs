using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper
{
    /// <summary>
    /// 日报月报
    /// </summary>
    [AbpAuthorize]
    public class DailyNewspaperAppService : MyProjectAppServiceBase, IDailyNewspaperAppService
    {
        private readonly IRepository<TjlMReceiptInfo, Guid> _mReceiptInfoRepository; //收费记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mReceiptInfoRepository">收费记录</param>
        public DailyNewspaperAppService(
             IRepository<TjlMReceiptInfo, Guid>  mReceiptInfoRepository
            ) {
            _mReceiptInfoRepository = mReceiptInfoRepository;
        }
        /// <summary>
        /// 查询收费记录
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public List<SearchMReceiptInfoDto> GetMReceiptInfo(QuerySunMoon Query)
        {
            var query = _mReceiptInfoRepository.GetAll().AsNoTracking();
            //收费日期
            if (Query.ChargeDateStart != null && Query.ChargeDateEnd != null)
            {
                query = query.Where(o => o.ChargeDate >= Query.ChargeDateStart && o.ChargeDate >= Query.ChargeDateEnd);
            }
            //人员类别：个人
            if (Query.PersonnelCategory == 1)
            {
                query = query.Where(o => o.ClientRegId==Guid.Empty);
            }
            //人员类别：团体
            else if (Query.PersonnelCategory == 2)
            {
                query = query.Where(o => o.ClientRegId != Guid.Empty);
            }
            //收费人
            if (Query.UserId != null)
            {
                query = query.Where(o => o.UserId == Query.UserId);
            }
            //单位
            if (Query.Client != Guid.Empty)
            {
                query = query.Where(o => o.ClientRegId == Query.Client);
            }
            var MReceip = query.MapTo<List<SearchMReceiptInfoDto>>();
            return MReceip;
        }
    }
}
