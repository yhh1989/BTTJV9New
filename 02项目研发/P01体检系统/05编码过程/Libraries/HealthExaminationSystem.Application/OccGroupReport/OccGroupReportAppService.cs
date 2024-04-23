using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport
{
    [AbpAuthorize]
    public class OccGroupReportAppService : MyProjectAppServiceBase, IOccGroupReportAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg;

        private readonly IRepository<TbmOccTargetDisease, Guid> _TbmOccTargetDisease;
        private readonly IRepository<TbmBasicDictionary, Guid> _TbmBasicDictionary;
        private readonly IRepository<TjlCustomerRegItem, Guid> _TjlCustomerRegItem;
        private readonly IRepository<TjlClientTeamRegitem, Guid> _TjlClientTeamRegitem;
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        public OccGroupReportAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
            IRepository<TbmOccTargetDisease, Guid> TbmOccTargetDisease,
            IRepository<TbmBasicDictionary, Guid> TbmBasicDictionary,
            IRepository<TjlCustomerRegItem, Guid> TjlCustomerRegItem,
             IRepository<TjlOccCustomerHazardSum, Guid> TjlOccCustomerHazardSum,
             IRepository<TjlClientTeamRegitem, Guid> TjlClientTeamRegitem
         )
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCustomerReg = TjlCustomerReg;
            _TbmOccTargetDisease = TbmOccTargetDisease;
            _TbmBasicDictionary = TbmBasicDictionary;
            _TjlCustomerRegItem = TjlCustomerRegItem;
            _TjlOccCustomerHazardSum = TjlOccCustomerHazardSum;
            _TjlClientTeamRegitem = TjlClientTeamRegitem;
        }
        /// <summary>
        /// 获取职业健康体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccCustomerSumDto> getOccCustomerSum(EntityDto<Guid> input)
        {
           
            var que = _TjlOccCustomerSum.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id);
            return que.MapTo<List<OccCustomerSumDto>>();

          //  return await que.AsNoTracking().ProjectToListAsync<OccCustomerSumDto>(GetConfigurationProvider<Core.Occupational.TjlOccCustomerSum, OccCustomerSumDto>());

        }
        public List<OccCustomerRegHazardSumDto> getCustomerRegHazardSum(EntityDto<Guid> input)
        {

            //var que = _TjlOccCustomerHazardSum.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id && o.CustomerRegBM.ReviewSate !=2);
            var que = _TjlOccCustomerHazardSum.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id  );
            return que.MapTo<List<OccCustomerRegHazardSumDto>>();

            //  return await que.AsNoTracking().ProjectToListAsync<OccCustomerSumDto>(GetConfigurationProvider<Core.Occupational.TjlOccCustomerSum, OccCustomerSumDto>());

        }
        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccTargetCountDto> getTargetCount(InOcccCusIDDto input)
        {

           return getItemTargetCount(  input);
            List<OccTargetCountDto> occTargetCountDtos = new List<OccTargetCountDto>();
            //获取危害因素及检查类型
            var typ1 = _TbmBasicDictionary.GetAll().Where(o => o.Type == "ExaminationType" && o.Text.Contains("职业")).ToList();
              
            var quelk = _TjlCustomerReg.GetAll().Where(o => o.ClientRegId == input.Id).ToList();
            if (typ1 != null && typ1.Count>0)
            {
                var typ = typ1.Select(p => p.Value).ToList();
                quelk = quelk.Where(o => o.PhysicalType!=null &&  typ.Contains( o.PhysicalType.Value)).ToList(); }
            if (input.CusRegIDList != null && input.CusRegIDList.Count > 0)
            {
                quelk = quelk.Where(o => input.CusRegIDList.Contains(o.Id)).ToList();
            }
            if (input.isfc == false)
            {
                quelk = quelk.Where(o => o.ReviewSate != 2).ToList();
            }
            var quel = quelk.Where(o => o.ClientRegId == input.Id && o.RiskS != null && o.PostState != null).ToList();
            // occTargetCountDtos= quel.GroupBy(o=>new { o.RiskS, o.PostState })

            var que = quel.GroupBy(o => new { o.RiskS, o.PostState }).Select(
                g => new
                {
                    risknames = g.FirstOrDefault(r => r.RiskS == g.Key.RiskS && r.PostState == g.Key.PostState).OccHazardFactors.Select(f => f.Text).ToList(),
                    checktaype = g.Key.PostState,
                     g.Key.RiskS
                     //allsum=g.Count(),
                     //checksum=g.Where(o=>o.SummSate!=1).Count()
                }).ToList();
            if (que != null && que.Count > 0)
            {
               
                foreach (var risk in que)
                {//找到目标疾病
                  
                    var risks = risk.risknames.ToList();                   
                        var cusregcount = quelk.Where(o => o.OccHazardFactors.All(p => risks.Contains(p.Text))
                    && o.OccHazardFactors.Count== risks.Count && o.PostState== risk.checktaype).ToList();
                    if (cusregcount.Count == 0)
                    {
                        continue;
                    }
                    var regIds = cusregcount.Select(o => o.Id).ToList();
                    quelk = quelk.Where(o => !regIds.Contains(o.Id)).ToList();
                    
                    var targs = _TbmOccTargetDisease.GetAll().Where(o => risks.Contains(o.OccHazardFactors.Text) && o.CheckType == risk.checktaype).ToList();
                    if (targs != null && targs.Count > 0)
                    {
                        var groups = targs.SelectMany(o => o.MustIemGroups).Select(o => o.ItemGroupName).Distinct();
                        var groupName = string.Join("、", groups).TrimEnd('、');
                        var Diss = targs.SelectMany(o => o.OccDiseases).Select(o => o.Text).Distinct().ToList();
                        var targDiss = "职业病：" + string.Join("、", Diss).TrimEnd('、') + Environment.NewLine;
                        var Cation = targs.SelectMany(o => o.Contraindications).Select(o => o.Text).ToList();
                        var argCation = "职业禁忌证：" + string.Join("、", Cation).TrimEnd('、');

                        OccTargetCountDto occTargetCountDto = new OccTargetCountDto();
                        occTargetCountDto.RiskNames = risk.RiskS;
                        occTargetCountDto.ChckType = risk.checktaype;

                        occTargetCountDto.Groups = groupName;
                        occTargetCountDto.AllCount = cusregcount.Count();//risk.allsum; ;
                        occTargetCountDto.HasCount = cusregcount.Where(o => o.SummSate != 1).Count(); //risk.checksum;
                        occTargetCountDto.Target = targDiss + argCation;
                        occTargetCountDto.InspectionCycle = targs.FirstOrDefault(p => p.InspectionCycle != null && p.InspectionCycle != "")?.InspectionCycle;
                        occTargetCountDtos.Add(occTargetCountDto);

                    }
                    else
                    {
                        

                        OccTargetCountDto occTargetCountDto = new OccTargetCountDto();
                        occTargetCountDto.RiskNames = risk.RiskS;
                        occTargetCountDto.ChckType = risk.checktaype;

                        occTargetCountDto.Groups = "";
                        occTargetCountDto.AllCount = cusregcount.Count();//risk.allsum; ;
                        occTargetCountDto.HasCount = cusregcount.Where(o => o.SummSate != 1).Count(); //risk.checksum;
                        occTargetCountDto.Target = "";
                        occTargetCountDto.InspectionCycle = "";
                        occTargetCountDtos.Add(occTargetCountDto);
                    }

                }
            }
            return occTargetCountDtos.OrderByDescending(o=>o.AllCount).ToList();
        }
        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccTargetCountDto> getItemTargetCount(InOcccCusIDDto input)
        {
            List<OccTargetCountDto> occTargetCountDtos = new List<OccTargetCountDto>();
            //获取危害因素及检查类型
            var typ1 = _TbmBasicDictionary.GetAll().Where(o => o.Type == "ExaminationType" && o.Text.Contains("职业")).ToList();

            var quelk = _TjlCustomerReg.GetAll().Where(o => o.ClientRegId == input.Id).ToList();
            if (typ1 != null && typ1.Count > 0)
            {
                var typ = typ1.Select(p => p.Value).ToList();
                quelk = quelk.Where(o => o.PhysicalType != null && typ.Contains(o.PhysicalType.Value)).ToList();
            }
            if (input.CusRegIDList != null && input.CusRegIDList.Count > 0)
            {
                quelk = quelk.Where(o => input.CusRegIDList.Contains(o.Id)).ToList();
            }
            if (input.isfc == false)
            {
                quelk = quelk.Where(o => o.ReviewSate != 2).ToList();
            }
            var quel = quelk.Where(o => o.ClientRegId == input.Id && o.RiskS != null && o.PostState != null).ToList();
         

            var que = quel.GroupBy(o => new { o.RiskS, o.PostState }).Select(
                g => new
                {
                    risknames = g.FirstOrDefault(r => r.RiskS == g.Key.RiskS && r.PostState == g.Key.PostState).OccHazardFactors.Select(f => f.Text).ToList(),
                    checktaype = g.Key.PostState,
                    g.Key.RiskS

                }).ToList();

            //查找分组项目
            var clientIitem = _TjlClientTeamRegitem.GetAll().Where(p => p.ClientRegId == input.Id).ToList();
            if (que != null && que.Count > 0)
            {
                List<string> riskNamelist = new List<string>();

                foreach (var risk in que)
                {//找到目标疾病

                    var risklist = risk.risknames.ToList();

                    foreach (var riskName in risklist)
                    {
                        if (!riskNamelist.Contains(riskName + risk.checktaype))
                        {
                            riskNamelist.Add(riskName + risk.checktaype);
                            var cusregcount = quelk.Where(o => o.OccHazardFactors.Any(p => p.Text == riskName)
                    && o.PostState == risk.checktaype).ToList();
                            //获取分组项目
                            var teamlist = cusregcount.Select(p => p.ClientTeamInfoId).Distinct();
                            var teamGroups = clientIitem.Where(p => teamlist.Contains(p.ClientTeamInfoId)).Select(
                                p => p.ItemGroupName).Distinct().ToList();
                        if (cusregcount.Count == 0)
                        {
                            continue;
                        }
                        var regIds = cusregcount.Select(o => o.Id).ToList();
                        //quelk = quelk.Where(o => !regIds.Contains(o.Id)).ToList();

                        var targs = _TbmOccTargetDisease.GetAll().Where(o => o.OccHazardFactors.Text== riskName && o.CheckType == risk.checktaype).ToList();
                        if (targs != null && targs.Count > 0)
                        {
                            var groups = targs.SelectMany(o => o.MustIemGroups).Select(o => o.ItemGroupName).Distinct().ToList();
                            var groupName = string.Join("、", groups).TrimEnd('、');
                                //查询加项目
                                var addGrouplis = teamGroups.Where(p => !groups.Contains(p)).ToList();
                                var addGroupNames= string.Join("、", addGrouplis).TrimEnd('、');
                                var Diss = targs.SelectMany(o => o.OccDiseases).Select(o => o.Text).Distinct().ToList();
                            var targDiss = "职业病：" + string.Join("、", Diss).TrimEnd('、') + Environment.NewLine;
                            var Cation = targs.SelectMany(o => o.Contraindications).Select(o => o.Text).ToList();
                            var argCation = "职业禁忌证：" + string.Join("、", Cation).TrimEnd('、');

                            OccTargetCountDto occTargetCountDto = new OccTargetCountDto();
                            occTargetCountDto.RiskNames = riskName;
                            occTargetCountDto.ChckType = risk.checktaype;
                                occTargetCountDto.AddGroups = addGroupNames;
                            occTargetCountDto.Groups = groupName;
                            occTargetCountDto.AllCount = cusregcount.Count();//risk.allsum; ;
                            occTargetCountDto.HasCount = cusregcount.Where(o => o.SummSate != 1).Count(); //risk.checksum;
                            occTargetCountDto.Target = targDiss + argCation;
                            occTargetCountDto.InspectionCycle = targs.FirstOrDefault(p => p.InspectionCycle != null && p.InspectionCycle != "")?.InspectionCycle;
                            occTargetCountDtos.Add(occTargetCountDto);

                        }
                        else
                        {


                            OccTargetCountDto occTargetCountDto = new OccTargetCountDto();
                            occTargetCountDto.RiskNames = riskName;
                            occTargetCountDto.ChckType = risk.checktaype;
                                occTargetCountDto.AddGroups = "";
                           occTargetCountDto.Groups = "";
                            occTargetCountDto.AllCount = cusregcount.Count();//risk.allsum; ;
                            occTargetCountDto.HasCount = cusregcount.Where(o => o.SummSate != 1).Count(); //risk.checksum;
                            occTargetCountDto.Target = "";
                            occTargetCountDto.InspectionCycle = "";
                            occTargetCountDtos.Add(occTargetCountDto);
                        }
                    }
                    }
                }
            }
            return occTargetCountDtos.OrderByDescending(o => o.AllCount).ToList();
        }
        /// <summary>
        /// 获取异常结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccCustomerItemDto> getCusItemResult(EntityDto<Guid> input)
        {
            var mb = _TbmOccTargetDisease.GetAll().Where(o=>o.OccHazardFactors !=null).Select(o => new { o.OccHazardFactorsId, o.CheckType, o.ItemInfo }).ToList();
            var que = _TjlCustomerRegItem.GetAll().Where(o => o.CustomerRegBM.ClientRegId == input.Id).
                Where(o => o.ProcessState == (int)ProjectIState.Complete);
            que = que.Where(o => o.Symbol == "H" || o.Symbol == "HH" || o.Symbol == "L" || o.Symbol == "LL" ||
            (o.Symbol == "P" && !o.ItemDiagnosis.Contains("未见明显异常") && !o.ItemDiagnosis.Contains("正常心电图")
             && !o.ItemDiagnosis.Contains("正常范围内心电图") && !o.ItemDiagnosis.Contains("未见异常") && o.ItemDiagnosis != ""));

            var cuslst = que.Select(o => o.CustomerRegBM).Distinct().Select(o=>new {
            o.Id,o.PostState, occ=o.OccHazardFactors.Select(n=>n.Id).ToList()});
            List<OccCustomerItemDto> outre = new List<OccCustomerItemDto>();
            foreach (var cus in cuslst)
            {
               
                var occTar = mb.Where(o => o.CheckType == cus.PostState && cus.occ.Contains(o.OccHazardFactorsId.Value)).ToList();
                if (occTar != null && occTar.SelectMany(o => o.ItemInfo).Count() > 0)
                {
                    OccCustomerItemDto occCustomerItemDto = new OccCustomerItemDto();
                    occCustomerItemDto.CustomerRegId = cus.Id;
                    var ITems = occTar.SelectMany(o => o.ItemInfo).Select(o => o.Id).Distinct().ToList();
                    var sum = que.Where(o => o.CustomerRegId == cus.Id && ITems.Contains(o.ItemId)).
                        Select(o =>    o.ItemTypeBM == (int)ItemType.Explain ? o.ItemName + ":" +  o.ItemDiagnosis:(o.ItemName+o.ItemResultChar +o.Unit)).ToList();
                    occCustomerItemDto.Sum = string.Join("\r\n", sum);
                    occCustomerItemDto.SumTypeBM = 1;
                    outre.Add(occCustomerItemDto);
                    OccCustomerItemDto occCustomerItemDto1 = new OccCustomerItemDto();
                    occCustomerItemDto1.CustomerRegId = cus.Id;
                    var sum2 = que.Where(o => o.CustomerRegId == cus.Id && !ITems.Contains(o.ItemId)).
                        Select(o =>  o.ItemTypeBM == (int)ItemType.Explain ? o.ItemName + ":"+o.ItemDiagnosis : (o.ItemName + o.ItemResultChar + o.Unit) ).ToList();
                    occCustomerItemDto1.Sum = string.Join("\r\n", sum2);
                    occCustomerItemDto1.SumTypeBM = 2;
                    outre.Add(occCustomerItemDto1);
                }
                else
                {
                    OccCustomerItemDto occCustomerItemDto = new OccCustomerItemDto();
                    occCustomerItemDto.CustomerRegId = cus.Id;
                    var sum = que.Where(o => o.CustomerRegId == cus.Id).
                        Select(o => o.ItemTypeBM == (int)ItemType.Explain ? o.ItemName +":" + o.ItemDiagnosis : (o.ItemName + o.ItemResultChar + o.Unit)).ToList();
                    occCustomerItemDto.Sum = string.Join("\r\n", sum);
                    occCustomerItemDto.SumTypeBM = 2;
                    outre.Add(occCustomerItemDto);
                }
            }



            return outre.ToList();
        }

    }   

    
}
