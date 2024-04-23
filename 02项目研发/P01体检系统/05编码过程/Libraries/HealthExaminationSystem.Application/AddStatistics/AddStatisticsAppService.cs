using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.AddStatistics
{
    /// <summary>
    /// 加项统计
    /// </summary>
  public class AddStatisticsAppService : MyProjectAppServiceBase,IAddStatisticsAppService
    {
        private readonly IRepository<TjlCustomerItemGroup, Guid> _TjlCustomerItemGroup;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TjlCustomerItemGroup"></param>
        public AddStatisticsAppService(IRepository<TjlCustomerItemGroup, Guid> TjlCustomerItemGroup)
        {
            _TjlCustomerItemGroup = TjlCustomerItemGroup;
        }
        /// <summary>
        /// 加项统计   显示
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AddStatisticsDto> AddStatisticsList(SearchStatisticsDto input)
        {
            var result = _TjlCustomerItemGroup.GetAll();
            if (input.GroupName.HasValue)
            {
                result = result.Where(o => o.ItemGroupBM_Id == input.GroupName);
            }
            if (input.CheckName.HasValue)
            {
                result = result.Where(o => o.BillingEmployeeBMId == input.CheckName);
            }
            result = result.Where(o => o.IsAddMinus == (int)AddMinusType.Add && o.CreationTime >= input.DataTimeStart && o.CreationTime <= input.DataTimeEnd);

            var resultList = result.Select(o => new AddStatisticsDto
            { ItemGroupName = o.ItemGroupName,
                Name = o.BillingEmployeeBM.Name,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                CustomerName = o.CustomerRegBM.Customer.Name,
                ItemPrice = o.ItemPrice,
                PriceAfterDis = o.PriceAfterDis,
                CreationTime = o.CreationTime,
            });

            return resultList.ToList();
        }
    }
}
