using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport
{
    [AbpAuthorize]
    public class GroupReportAppService : MyProjectAppServiceBase, IGroupReportAppService
    {
        #region 接口和引用
        private readonly IRepository<TjlClientInfo, Guid> _tjlClientInfo; //单位查询

        private readonly IRepository<TjlClientReg, Guid> _tjlClientReg; //预约查询
        private readonly IRepository<TjlClientTeamRegitem, Guid> _Regitem; //单位分组登记项目
        private readonly IRepository<TjlCustomerReg, Guid> _cusReg; //体检预约
        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _cusSummarizeBM; //体检预约
        private readonly IRepository<TjlCustomerReg, Guid> _CustomerReg; //体检预约
        private readonly IRepository<TjlCustomerRegItem, Guid> _CustomerRegItem; //检查结果
        private  readonly IRepository<TjlCusReview, Guid> _TjlCusReview; //检查结果 
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum; //检查结果 
        public GroupReportAppService(
            IRepository<TjlClientReg, Guid> tjlClientReg,
            IRepository<TjlClientInfo, Guid> tjlClientInfo,
            IRepository<TjlClientTeamRegitem, Guid> Regitem,
             IRepository<TjlCustomerReg, Guid> CusReg,
             IRepository<TjlCustomerSummarizeBM, Guid> CusSummarizeBM,
             IRepository<TjlCustomerReg, Guid> CustomerReg,
             IRepository<TjlCustomerRegItem, Guid> CustomerRegItem,
             IRepository<TjlCusReview, Guid> TjlCusReview,
             IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum

        )
        {
            _tjlClientReg = tjlClientReg;
            _tjlClientInfo = tjlClientInfo;
            _Regitem = Regitem;
            _cusReg = CusReg;
            _cusSummarizeBM = CusSummarizeBM;
            _CustomerReg=CustomerReg;
            _CustomerRegItem = CustomerRegItem;
            _TjlCusReview = TjlCusReview;
            _TjlOccCustomerSum = TjlOccCustomerSum;
    }
        #endregion

        /// <summary>
        /// 单位查询
        /// </summary>
        public List<SelectClientRewDto> QueryCompany()
        {

            var PaDtoList = _tjlClientReg.GetAllIncluding(r => r.ClientInfo);
            var rows = PaDtoList.OrderByDescending(o=>o.CreationTime).MapTo<List<SelectClientRewDto>>();
            return rows;
        }

        /// <summary>
        /// 单位按名称查询
        /// </summary>
        public List<SelectClientRewDto> QueryCompanyName(SelectClientRewDto input)
        {
            var query = _tjlClientReg.GetAllIncluding(r => r.ClientInfo);
            //单位ID
            if (input.ClientInfo!=null)
            {
                query = query.Where(o => o.ClientInfo.Id == input.ClientInfo.Id);
            }
            if (input.ClientRegStart != null && input.ClientRegEnd != null)
            {
                query = query.Where(o => o.StartCheckDate <= input.ClientRegEnd && o.StartCheckDate > input.ClientRegStart);
            }

            return query.MapTo<List<SelectClientRewDto>>();
        }

        /// <summary>
        /// 分组/人员查询
        /// </summary>
        public List<List<GroupClientRegDto>> QueryGroup(List<GroupClientRegDto> input)
        {
            List<List<GroupClientRegDto>> regDtos = new List<List<GroupClientRegDto>>();
            //List<GroupClientRegDto> dto = new List<GroupClientRegDto>();
            
            foreach (var item in input)
            {
                var query = _tjlClientReg.GetAll();
                query =query.Where(o => o.ClientInfo.Id == item.ClientInfo.Id);
                var rows = query.MapTo<List<GroupClientRegDto>>();
                regDtos.Add(rows);
            }


            return regDtos;
        }
        /// <summary>
        /// 获取单位下组合项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        public List<GroupClientItemsDto> GRClientRegItems(ClientRegIdDto input)
        {
            List<GroupClientItemsDto> tjlClientTeamRegitems = new List<GroupClientItemsDto>();
            var query = _Regitem.GetAll();
            query = query.Where(o => input.Idlist.Contains( o.ClientRegId.Value)  && o.Department.Category !="耗材").OrderBy(o=>o.ClientTeamInfo.TeamBM).ThenBy(o=> o.DepartmentOrder ).ThenBy(o=> o.ItemGroupOrder);

            tjlClientTeamRegitems = query.MapTo<List<GroupClientItemsDto>>();
            return tjlClientTeamRegitems;
        }

        /// <summary>
        /// 获取单位下人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GroupClientCusDto> GRClientRegCus(ClientRegIdDto input)
        {
            List<GroupClientCusDto> tjlClientCus = new List<GroupClientCusDto>();
            var query = _cusReg.GetAll().AsQueryable();
            //query = query.Where(o =>o.ReviewSate!=2 && input.Idlist.Contains(o.ClientRegId.Value) );
            query = query.Where(o =>   input.Idlist.Contains(o.ClientRegId.Value));
            tjlClientCus = query.MapTo<List<GroupClientCusDto>>();
            return tjlClientCus;

        }

        /// <summary>
        /// 获取单位下人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<SimGroupClientCusDto> GRClientRegCusSmp(ClientRegIdDto input)
        {
            List<SimGroupClientCusDto> tjlClientCus = new List<SimGroupClientCusDto>();
            var query = _cusReg.GetAll().AsQueryable();
            query = query.Where(o => input.Idlist.Contains( o.ClientRegId.Value));
            tjlClientCus = query.Select(o=>new SimGroupClientCusDto {  Id=o.Id,
             Age=o.Customer.Age,
             AgeUnit=o.Customer.AgeUnit,
             BookingDate=o.BookingDate,
             CustomerBM=o.CustomerBM,
             LoginDate=o.LoginDate,
             Name=o.Customer.Name,
             PostState=o.PostState,
             Sex=o.Customer.Sex==1?"男":"女",
             TjlClientReg_Id=o.ClientRegId.Value,
             ClientTeamInfoId=o.ClientTeamInfoId,
             PhysicalType=o.PhysicalType,
             TeamName=o.ClientTeamInfo.TeamName,
             SummSate=o.SummSate}).ToList();
            return tjlClientCus;

        }
        /// <summary>
        /// 获取单位下人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<SimGroupClientCusDto> GRClientRegCusBM(CusRegBMDto input)
        {
            List<SimGroupClientCusDto> tjlClientCus = new List<SimGroupClientCusDto>();
            var query = _cusReg.GetAll().AsQueryable();
            query = query.Where(o => input.BMlist.Contains(o.CustomerBM));
            tjlClientCus = query.Select(o => new SimGroupClientCusDto
            {
                Id = o.Id,
                Age = o.Customer.Age,
                AgeUnit = o.Customer.AgeUnit,
                BookingDate = o.BookingDate,
                CustomerBM = o.CustomerBM,
                LoginDate = o.LoginDate,
                Name = o.Customer.Name,
                PostState = o.PostState,
                Sex = o.Customer.Sex == 1 ? "男" : "女",
                TjlClientReg_Id = o.ClientRegId.Value,
                ClientTeamInfoId = o.ClientTeamInfoId,
                PhysicalType = o.PhysicalType,
                TeamName = o.ClientTeamInfo.TeamName
            }).ToList();
            return tjlClientCus;

        }
        /// <summary>
        /// 获取单位下总检建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GroupClientSumDto> GRClientCusSum(ClientRegIdDto input)
        {
            List<GroupClientSumDto> tjlClientCus = new List<GroupClientSumDto>();
            var query = _cusSummarizeBM.GetAll().Where(o=>o.CustomerReg.ReviewSate!=2 && !o.SummarizeName.Contains("未见异常") &&
            !o.SummarizeName.Contains("未见明显异常") &&
            !o.SummarizeName.Contains("温馨提示"));
            query = query.Where(o => input.Idlist.Contains( o.CustomerReg.ClientRegId.Value) );
            tjlClientCus = query.MapTo<List<GroupClientSumDto>>();
            var tjlClientCus1 = tjlClientCus.Select(o => new GroupClientSumDto {
                 Advice=o.Advice,
                CustomerReg= o.CustomerReg,
                CustomerRegID=  o.CustomerRegID,
                Id=  o.Id,
                SummarizeAdvice=  o.SummarizeAdvice,
                SummarizeAdviceId=  o.SummarizeAdviceId,
                SummarizeName=  o.SummarizeName.Replace(" ","").Replace("\r\n", "").Replace("\r","").Replace("\n",""),
                SummarizeOrderNum= o.SummarizeOrderNum,
                SummarizeType = o.SummarizeType,
                IsPrivacy =o.IsPrivacy
                


            }).ToList();           
            return tjlClientCus1;

        }
        /// <summary>
        /// 获取单位历史下总检建议
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<HistoryGroupClientSumDto> GRHisClientCusSum(ClientRegIdDto input)
        {
            var clientIdlist = _tjlClientReg.GetAll().Where(o => input.Idlist.Contains(o.Id)).ToList();
            var clienIds = clientIdlist.Select(p=>p.ClientInfoId).ToList();
 


            List<HistoryGroupClientSumDto> tjlClientCus = new List<HistoryGroupClientSumDto>();
            var query = _cusSummarizeBM.GetAll().Where(o => o.CustomerReg.ReviewSate != 2 && !o.SummarizeName.Contains("未见异常") &&
            !o.SummarizeName.Contains("未见明显异常") &&
            !o.SummarizeName.Contains("温馨提示"));
            var ClientRegIds = query.Where(o => !input.Idlist.Contains(o.CustomerReg.ClientRegId.Value) &&
            clienIds.Contains(o.CustomerReg.ClientInfoId.Value)).OrderByDescending(p => p.CustomerReg.ClientReg.StartCheckDate).Select(p=>p.CustomerReg.ClientRegId).Distinct().Take(2);

            var clienretIdlist = ClientRegIds.ToList();

            query = query.Where(o => clienretIdlist.Contains(o.CustomerReg.ClientRegId.Value));
          
            var tjlClientCus1 = query.Select(o => new HistoryGroupClientSumDto
            {
                Advice = o.Advice,
                CustomerRegID = o.CustomerRegID,

                
                SummarizeAdviceId = o.SummarizeAdviceId,
                SummarizeName = o.SummarizeName.Replace(" ", "").Replace("\r\n", "").Replace("\r", "").Replace("\n", ""),
                SummarizeOrderNum = o.SummarizeOrderNum,
                SummarizeType = o.SummarizeType,
                IsPrivacy = o.IsPrivacy,
                ClientRegBM = o.CustomerReg.ClientReg.ClientRegBM,
                ClientRegDate = o.CustomerReg.ClientReg.StartCheckDate,
                 ClientRegId = o.CustomerReg.ClientRegId,
                  Sex = o.CustomerReg.Customer.Sex
            }).ToList();
            return tjlClientCus1;

        }
        public List<SelectClientRewDto> getreglist(List<EntityDto<Guid>> input)
        {
           var ids = input.Select(o => o.Id);
           var clientreg=  _tjlClientReg.GetAllIncluding(o=>o.ClientInfo).Where(r=> ids.Contains(r.ClientInfo.Id)).Select(o => new { o.Id, o.StartCheckDate, o.ClientInfo });
          return clientreg.MapTo<List<SelectClientRewDto>>();
        }
        /// <inheritdoc />
        [UnitOfWork(false)]
        public List<GroupClientRegDto> GetCompanyRegisterIncludeGroupAndPersonnel(EntityDto<Guid> input)
        {
            var query = _tjlClientReg.GetAllIncluding(r => r.ClientTeamInfo, r => r.ClientInfo).AsNoTracking();
            query = query.Where(r => r.ClientInfoId == input.Id);
            query = query.OrderByDescending(r => r.StartCheckDate);
            return query.MapTo<List<GroupClientRegDto>>();
        }
        /// <summary>
        /// 获取单位诊断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GroupSumBMDto> getClientSum(ClientRegIdDto input)
        {
            List<GroupSumBMDto> tjlClientCus = new List<GroupSumBMDto>();
            var query = _cusSummarizeBM.GetAll().Where(o => !o.SummarizeName.Contains("未见异常") &&
            !o.SummarizeName.Contains("未见明显异常") &&
            !o.SummarizeName.Contains("温馨提示"));
            query = query.Where(o => input.Idlist.Contains( o.CustomerReg.ClientRegId.Value));
            tjlClientCus = query.GroupBy(o=>o.SummarizeName).Select(o=>new GroupSumBMDto
            {
                SummarizeName = o.Key,
                cout = o.Count()
            }).OrderByDescending(o=>o.cout).ToList();
            return tjlClientCus;

        }
        public List<SumClientDto> getClientSumResult(EntityDto<Guid> input)
        {
            List<SumClientDto> sumClientDtos = new List<SumClientDto>();
            string Hstr = SymbolHelper.SymbolFormatter(Symbol.High);
            string Lstr = SymbolHelper.SymbolFormatter(Symbol.Low);
            string SHstr = SymbolHelper.SymbolFormatter(Symbol.Superhigh);
            string ULstr = SymbolHelper.SymbolFormatter(Symbol.UltraLow);

            //阳性结果
            var que = _CustomerRegItem.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id
              && o.ProcessState == (int)ProjectIState.Complete &&
              (o.Symbol == Hstr || o.Symbol == Lstr
              || o.Symbol == SHstr || o.Symbol == ULstr)).
              Select(o=>new { o.CustomerRegBM.Customer.Name ,o.ItemName,o.ItemResultChar,o.CustomerRegId}).ToList();

           
            
            var query = _cusSummarizeBM.GetAll().Where(o => !o.SummarizeName.Contains("未见异常") &&
            !o.SummarizeName.Contains("未见明显异常") &&
            !o.SummarizeName.Contains("温馨提示"));
            query = query.Where(o => o.CustomerReg.ClientRegId == input.Id);
            //异常名单
            var cuslst = query.Select(o => new { o.CustomerReg.Customer.Name, o.SummarizeName,o.Advice,o.CustomerRegID }).ToList();
        var     tjlClientCus = cuslst.GroupBy(o => o.SummarizeName).Select(o => new 
            {
                SummarizeName = o.Key,  
                advice=o.First(n=>n.SummarizeName==o.Key).Advice,
                cout = o.Count()
            }).OrderByDescending(o => o.cout).ToList();
            foreach (var sum in tjlClientCus)
            {
                SumClientDto sumClientDto = new SumClientDto();
                var SumName = sum.SummarizeName.Replace("偏高", "").Replace("偏低", "").
                    Replace("极高", "").Replace("极低", "").Replace("升高", "").Replace("降低", "");
                var cusregIds = cuslst.Where(o => o.SummarizeName == sum.SummarizeName).Select(o => o.CustomerRegID).ToList();
                var cuslist = que.Where(o => SumName == o.ItemName && cusregIds.Contains(o.CustomerRegId)).Select(o => (o.Name + " " + o.ItemName + ":" + o.ItemResultChar)).ToList();
                sumClientDto.SumName = sum.SummarizeName;
                sumClientDto.SumAdvicee = sum.advice;
                if (cuslist != null && cuslist.Count > 0)
                {
                    
                    sumClientDto.CusSum = string.Join(",", cuslist).TrimEnd(',') + "(共" + sum.cout + "人)";
                }
                else
                {
                    var sumcuslist = cuslst.Where(o => o.SummarizeName == sum.SummarizeName).Select(o=>o.Name).ToList();
                    sumClientDto.CusSum = string.Join(",", sumcuslist).TrimEnd(',') + "(共" + sum.cout + "人)";
                }
                sumClientDtos.Add(sumClientDto);

            }
            return sumClientDtos;
        }

        /// <summary>
        /// 获取单位复查信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReviewGroupDto> ReVewGroupCus(EntityDto<List<Guid>> input)
        {
            List<CusReviewGroupDto> tjlClientCus = new List<CusReviewGroupDto>();
            var query = _TjlCusReview.GetAll().AsQueryable();
            var cusSum = _TjlOccCustomerSum.GetAll().Where(p => p.CustomerRegBMId!=null&& p.CustomerRegBM!=null &&  input.Id.Contains(p.CustomerRegBMId.Value)).ToList();
            query = query.Where(o => input.Id.Contains(o.CustomerRegId.Value ));
            foreach ( var cus in query)
            {
                if (cus.ItemGroup != null)
                {
                    foreach (var Group in cus.ItemGroup)
                    {
                        CusReviewGroupDto cusReviewGroup = new CusReviewGroupDto();
                        cusReviewGroup.Age = cus.CustomerReg.Customer.Age;
                        cusReviewGroup.AgeUnit = cus.CustomerReg.Customer.AgeUnit;
                        cusReviewGroup.Conclusion = cusSum.FirstOrDefault(p => p.CustomerRegBMId == cus.CustomerRegId)?.Conclusion;
                        cusReviewGroup.CustomerBM = cus.CustomerReg.CustomerBM;
                        cusReviewGroup.Department = cus.CustomerReg.Customer.Department;
                        cusReviewGroup.InjuryAge = cus.CustomerReg.InjuryAge;
                        cusReviewGroup.ItemGroupName = Group.ItemGroupName;
                        cusReviewGroup.Name = cus.CustomerReg.Customer.Name;
                        cusReviewGroup.Remart = cus.Remart;
                        cusReviewGroup.RiskS = cus.CustomerReg.RiskS;
                        cusReviewGroup.Sex = cus.CustomerReg.Customer.Sex == 1 ? "男" : "女";
                        cusReviewGroup.TypeWork = cus.CustomerReg.TypeWork;
                        tjlClientCus.Add(cusReviewGroup);
                    }
                }
                
            }
           
            return tjlClientCus;

        }
    }
}