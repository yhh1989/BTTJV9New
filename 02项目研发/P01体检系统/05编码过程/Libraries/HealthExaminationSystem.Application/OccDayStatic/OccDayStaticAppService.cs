using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic
{
    [AbpAuthorize]
    public class OccDayStaticAppService : MyProjectAppServiceBase, IOccDayStaticAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;

        public OccDayStaticAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
        }

        public List<OutOccDayDto> GetOutOccDays(OutOccDayDto input)
        {
            //input.startDate = '2020-06-12'.;
            DateTime start = Convert.ToDateTime(input.startDate);
            DateTime end = Convert.ToDateTime(input.EndDate);
            var query = _TjlOccCustomerSum.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ClientName))
                query = query.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                query = query.Where(i => i.CreationTime.Year.ToString() == input.YearTime);
            }

            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CreationTime >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CreationTime <= input.EndCheckDate.Value);

            var querys = query.GroupBy(o => new { o.CustomerRegBM.ClientInfo.ClientName,o.CustomerRegBM.LoginDate})
               .Select(group => new { Name = group.Key.ClientName,LoginDates=group.Key.LoginDate.ToString()
               }).ToList();
            List<OutOccDayDto> List = new List<OutOccDayDto>();
            for (int i = 1; i <= 31; i++)
            {
                string da = start.AddDays(i).Date.ToString("dd");
                OutOccDayDto list = new OutOccDayDto();
                list.LoginDate = da;
                list.OneDay = querys.Where(o=>o.Name==o.Name.ToString()).ToList().Count();
                list.TwoDay = querys.Where(o => o.LoginDates == da).ToList().Count;
                list.ThreeDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.FourDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.FiveDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.SixDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.SevenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.EightDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.NineDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.ElevenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TelveDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.ThirteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.FourteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.FifteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.SixteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.SeventeenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.EighteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.NineteenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyOneDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyTwoDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentythreeDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyFourDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyFiveDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentySixDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentySevenDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyEightDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.TwentyNineDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.ThirtyDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                list.ThirtyOneDay = querys.Where(o => o.LoginDates == da).ToList().Count();
                List.Add(list);
            }
            return List;
           

        }

        /// <summary>
        /// 月份统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutOccMothDto> GetOutOccMothDays(INOccMothDto input)
        {
            List<OutOccMothDto> List = new List<OutOccMothDto>();
            var query = _TjlOccCustomerSum.GetAll();
            if (input.ClienRegId.HasValue)
                query = query.Where(i => i.CustomerRegBM.ClientRegId == input.ClienRegId);

            if (input.YearTime.HasValue)
            {
                query = query.Where(i => i.CustomerRegBM.LoginDate.Value.Year == input.YearTime);
            }
            if (input.MothTime.HasValue)
            {
                query = query.Where(i => i.CustomerRegBM.LoginDate.Value.Month == input.MothTime);

                var querys = query.GroupBy(o => new { o.CustomerRegBM.ClientInfo.ClientName, o.CustomerRegBM.LoginDate.Value.Day })
                  .Select(p => new OutOccMothDto
                  {
                      ClientName = p.Key.ClientName,
                      ConName = p.Key.Day,
                      ConCount = p.Count()
                  }).ToList();
                List = querys;
            }
            else
            {

                //var ss = query.ToList();
                var querys = query.GroupBy(o => new { o.CustomerRegBM.ClientInfo.ClientName, o.CustomerRegBM.LoginDate.Value.Month })
                   .Select(p => new OutOccMothDto 
                   {
                    ClientName=p.Key.ClientName,
                     ConName=p.Key.Month,
                      ConCount=p.Count()
                   }).ToList();
                List = querys;
            }
           
            return List;


        }
    }
}
