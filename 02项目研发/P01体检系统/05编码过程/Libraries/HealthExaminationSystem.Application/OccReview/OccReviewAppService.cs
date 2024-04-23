using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview
{
    [AbpAuthorize]
    public class OccReviewAppService : MyProjectAppServiceBase, IOccReviewAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlCusVisit, Guid> _TjlCusVisit;
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg;
        private readonly IRepository<TjlCusVisitManage, Guid> _TjlCusVisitManage;
        private readonly IRepository<TjlCustomerSummarize, Guid> _TjlCustomerSummarize;
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        public OccReviewAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlCusVisit, Guid> TjlCusVisit,
            IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
            IRepository<TjlCusVisitManage, Guid> TjlCusVisitManage,
            IRepository<TjlCustomerSummarize, Guid> TjlCustomerSummarize,
            IRepository<TjlOccCustomerHazardSum, Guid> TjlOccCustomerHazardSum)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCusVisit = TjlCusVisit;
            _TjlCustomerReg = TjlCustomerReg;
            _TjlCusVisitManage = TjlCusVisitManage;
            _TjlCustomerSummarize = TjlCustomerSummarize;
            _TjlOccCustomerHazardSum = TjlOccCustomerHazardSum;
        }

        public List<OutOccReviewDto> GetOutOccReviewDto(OutOccReviewDto input)
        {
            var query = _TjlOccCustomerHazardSum.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ClientName))
                query = query.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);
            if (input.ClientRegId.HasValue)
                query = query.Where(i => i.CustomerRegBM.ClientRegId== input.ClientRegId);
            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate <= input.EndCheckDate.Value);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                query = query.Where(i => i.CustomerRegBM.LoginDate!=null && i.CustomerRegBM.LoginDate.Value.Year.ToString() == input.YearTime);
            }
            //var risls = query.SelectMany(g=>g.CustomerRegBM.OccHazardFactors.Select(f => new { f.Text,type=f.Parent.Text})).ToList();
            //var AAA = "";
            //foreach (var S in risls)
            //{
            //    AAA = S.type;
            //}
            var result = query.Where(p=>p.Conclusion.Contains("复查") && p.CustomerRegBM!=null).Select(o => new OutOccReviewDto
            {
                HazardFactorName = o.CustomerRegBM.RiskS,
                //HazardFactorType = o.CustomerRegBM.OccHazardFactors.FirstOrDefault().Parent.Text,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                Name = o.CustomerRegBM.Customer.Name,
                Sex = o.CustomerRegBM.Customer.Sex,
                Age = o.CustomerRegBM.Customer.Age,
                Conclusion=o.Conclusion,
                Opinions=o.Advise,
                ReviewContent=o.CustomerSummarize.ReviewContent,
                CharacterSummary=o.CustomerSummarize.CharacterSummary,
                ClientName=o.CustomerRegBM.ClientInfo.ClientName,
                 InjuryAge=o.CustomerRegBM.InjuryAge + o.CustomerRegBM.InjuryAgeUnit,
                  InjuryAgeUnit=o.CustomerRegBM.InjuryAgeUnit,
                   TotalWorkAge=o.CustomerRegBM.TotalWorkAge + o.CustomerRegBM.WorkAgeUnit,
                    WorkAgeUnit=o.CustomerRegBM.WorkAgeUnit,
                TypeWork=o.CustomerRegBM.TypeWork,
                 StartCheckDate=o.CustomerRegBM.LoginDate


            }).ToList();
            return result.OrderBy(p => p.StartCheckDate).ToList();
        }
        /// <summary>
        /// 保存回访
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CusVisitDto SaveVisit(CusVisitDto input)
        {
            TjlCusVisit outCusVisit = new TjlCusVisit();
            var cusstate = _TjlCusVisit.GetAll().FirstOrDefault(p=>p.CustomerRegID== input.CustomerRegID);
            if (cusstate == null)
            {
               var newvisit= ObjectMapper.Map<TjlCusVisit>(input);
                newvisit.Id = Guid.NewGuid();
                outCusVisit = _TjlCusVisit.Insert(newvisit);
                 
            }
            else
            {
                input.MapTo(cusstate);
                outCusVisit =   _TjlCusVisit.Update(cusstate);
            }
            
            return ObjectMapper.Map<CusVisitDto>(outCusVisit);
        }
        /// <summary>
        /// 查询体检人回访
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CusVisitDto SearchVisit(CusVisitDto input)
        {
            TjlCusVisit outCusVisit = new TjlCusVisit();
            var cusstate = _TjlCusVisit.GetAll().FirstOrDefault(p => p.CustomerRegID == input.CustomerRegID);
            
            return ObjectMapper.Map<CusVisitDto>(cusstate); //cusstate.MapTo<CusVisitDto>();
        }

        /// <summary>
        /// 查询回访信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<VisiteCusRegDto> SearchCusRegVisit(searchVisitDto input)
        {
           var que = _TjlCustomerReg.GetAll();
            que = que.Where(p =>  p.TjlCusReviews.Count > 0 ||p.TjlCusGiveUps.Count > 0);
            //var que = _TjlCustomerSummarize.GetAll().Where(p=>p.ReviewContent!=null && p.ReviewContent !="");
            //复查日期
            if (input.ReDateStar.HasValue)
            {
                var star = DateTime.Parse(input.ReDateStar.Value.ToShortDateString());
                que = que.Where(p => p.TjlCusReviews.Any(n=>n.ReviewDate>= star));
            }
            if (input.ReDateEnd.HasValue)
            {
                var dtend = DateTime.Parse(input.ReDateEnd.Value.AddDays(1).ToShortDateString());
                que = que.Where(p => p.TjlCusReviews.Any(n => n.ReviewDate < dtend));
                // que = que.Where(p => p.LoginDate < dtend);
            }
            //补检日期
            if (input.BDateStar.HasValue)
            {
                var star = DateTime.Parse(input.BDateStar.Value.ToShortDateString());
                que = que.Where(p => p.TjlCusGiveUps.Any(n => n.stayDate >= star));
            }
            if (input.BDateEnd.HasValue)
            {
                var dtend = DateTime.Parse(input.BDateEnd.Value.AddDays(1).ToShortDateString());
                que = que.Where(p => p.TjlCusGiveUps.Any(n => n.stayDate < dtend));
                // que = que.Where(p => p.LoginDate < dtend);
            }
            if (!string.IsNullOrEmpty(input.ArchivesNum))
            {
                que = que.Where(p => p.CustomerBM == input.ArchivesNum);
            }
            if (!string.IsNullOrEmpty(input.IDCardNo))
            {
                que = que.Where(p => p.Customer.IDCardNo == input.IDCardNo);
            }
            if (!string.IsNullOrEmpty(input.Mobile))
            {
                que = que.Where(p => p.Customer.Mobile == input.Mobile);
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                que = que.Where(p => p.Customer.Name == input.Name);
            }
            if (input.Sex.HasValue)
            {
                que = que.Where(p => p.Customer.Sex == input.Sex);
            }
            if (input.LoginDateStar.HasValue)
            {
                var star =DateTime.Parse( input.LoginDateStar.Value.ToShortDateString());
                que = que.Where(p => p.LoginDate >= star);
            }
            if (input.LoginDateEnd.HasValue)
            {
                var dtend = DateTime.Parse(input.LoginDateEnd.Value.AddDays(1).ToShortDateString());
                
                que = que.Where(p => p.LoginDate < dtend);
            }
            
            if (input.VisitSate.HasValue)
            {

                if (input.VisitSate == 0)
                { que = que.Where(p => p.VisitSate == input.VisitSate || p.VisitSate ==null                ); }
                else
                {
                    que = que.Where(p => p.VisitSate == input.VisitSate);
                }
            }
            if (input.MinAge.HasValue)
            {
                que = que.Where(p => p.Customer.Age >= input.MinAge);
            }
            if (input.MaxAge.HasValue)
            {
                que = que.Where(p => p.Customer.Age <= input.MaxAge);
            }
            if (input.VisitTimeStart.HasValue)
            {
                var star = DateTime.Parse(input.VisitTimeStart.Value.ToShortDateString());
                que = que.Where(p => p.VisitTime >= star);
            }
            if (input.VisitTimeEnd.HasValue)
            {
                
                var dtend = DateTime.Parse(input.VisitTimeEnd.Value.AddDays(1).ToShortDateString());
                que = que.Where(p => p.VisitTime < dtend);
            }
            var qie = que.ToList();
            return ObjectMapper.Map<List<VisiteCusRegDto>>(qie);  
        }

        /// <summary>
        /// 保存回访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SaveCusVisitManageDto SaveCusVisitManage(SaveCusVisitManageDto input)
        {
            TjlCusVisitManage CusVisitManage = new TjlCusVisitManage();
            var cusstate = _TjlCusVisitManage.GetAll().FirstOrDefault(p => p.Id == input.Id);
            if (cusstate == null)
            {
                var newvisit = ObjectMapper.Map<TjlCusVisitManage>(input);
                newvisit.Id = Guid.NewGuid();
                newvisit.VisitDate = System.DateTime.Now;
                CusVisitManage = _TjlCusVisitManage.Insert(newvisit);
            }
            else
            {
                input.MapTo(cusstate);
                //cusstate= ObjectMapper.Map<TjlCusVisitManage>(input);
                cusstate.VisitDate= System.DateTime.Now;
                CusVisitManage = _TjlCusVisitManage.Update(cusstate);
            }
            var cusReg = _TjlCustomerReg.Get(input.CustomerRegID);
            cusReg.VisitEmployeeId = input.VisitEmployeeId;
            cusReg.VisitSate = (int)VisiteState.HasVisite;
            cusReg.VisitTime = System.DateTime.Now;
            cusReg.VisitType = input.VisitType;
            _TjlCustomerReg.Update(cusReg);
            return ObjectMapper.Map<SaveCusVisitManageDto>(CusVisitManage);
        }

        /// <summary>
        /// 查询回访记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<SaveCusVisitManageDto> SearchCusVisitManage(CusVisitDto input)
        {
            TjlCusVisitManage outCusVisit = new TjlCusVisitManage();
            var cusstate = _TjlCusVisitManage.GetAll().Where(p => p.CustomerRegID == input.CustomerRegID);

            return ObjectMapper.Map<List<SaveCusVisitManageDto>>(cusstate); //cusstate.MapTo<CusVisitDto>();
        }
        /// <summary>
        /// 删除回访
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelCusVisitManage(SaveCusVisitManageDto input)
        {
            TjlCusVisitManage CusVisitManage = new TjlCusVisitManage();
            var cusstate = _TjlCusVisitManage.GetAll().FirstOrDefault(p => p.Id == input.Id);
            if (cusstate != null)
            {
                
                 _TjlCusVisitManage.Delete(cusstate);
            }           

            
        }
    }
}
