using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus
{
    [AbpAuthorize]
    public class CusReSultStatusAppService : MyProjectAppServiceBase, ICusReSultStatusAppService
    {
        private readonly IRepository<TbmReSultDepart, Guid> _TbmReSultDepart; //科室设置
        private readonly IRepository<TbmReSultSet, Guid> _TbmReSultSet; //项目设置
        private readonly IRepository<TbmReSultCusGroup, Guid> _TbmReSultCusGroup; //组合设置
        private readonly IRepository<TbmReSultCusItem, Guid> _TbmReSultCusItem; //项目设置
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg; //预约
        private readonly IRepository<TjlCustomerSummarize, Guid> _TjlCustomerSummarize; //总检
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum; //职业健康总检
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum; //职业健康总检
        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _TjlCustomerSummarizeBM; //总检编码
        public CusReSultStatusAppService(
         IRepository<TbmReSultDepart, Guid> TbmReSultDepart,
         IRepository<TbmReSultSet, Guid> TbmReSultSet,
         IRepository<TjlCustomerSummarize, Guid> TjlCustomerSummarize,
         IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
         IRepository<TbmReSultCusGroup, Guid> TbmReSultCusGroup,
         IRepository<TbmReSultCusItem, Guid> TbmReSultCusItem,
        IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
        IRepository<TjlCustomerSummarizeBM, Guid> TjlCustomerSummarizeBM,
        IRepository<TjlOccCustomerHazardSum, Guid> TjlOccCustomerHazardSum
         )
        {
            _TbmReSultDepart = TbmReSultDepart;
            _TbmReSultSet = TbmReSultSet;
            _TjlCustomerSummarize = TjlCustomerSummarize;
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TbmReSultCusGroup = TbmReSultCusGroup;
            _TbmReSultCusItem = TbmReSultCusItem;
            _TjlCustomerReg = TjlCustomerReg;
            _TjlCustomerSummarizeBM = TjlCustomerSummarizeBM;
            _TjlOccCustomerHazardSum = TjlOccCustomerHazardSum;
        }
        public List<ReSultSetDto> GetReSultDepart()
        {
          var result= _TbmReSultSet.GetAll();
          var s= result.MapTo<List<ReSultSetDto>>();
            return s;
        }
        public ReSultSetDto Add(FullReSultSetDto input)
        {
            if (input.ReSultSets.Id == Guid.Empty)
            {
                 
                var entity = new TbmReSultSet();
                entity.Id = Guid.NewGuid();
                entity.isAdVice = input.ReSultSets.isAdVice;
                entity.isOccupational = input.ReSultSets.isOccupational;                 
                entity = _TbmReSultSet.Insert(entity);
                entity.Departs = null;
                entity.Departs = new List<TbmReSultDepart>();
                if (input.Departs != null && input.Departs.Count > 0)
                {
                    foreach (var occdis in input.Departs)
                    {
                       // var tbmfactors = occdis.MapTo<TbmReSultDepart>();
                        var tbmfactors = new TbmReSultDepart();
                        tbmfactors.Id = Guid.NewGuid();
                        tbmfactors.DepartmentId = occdis;
                        tbmfactors.ReSultSetId = entity.Id;
                        tbmfactors = _TbmReSultDepart.Insert(tbmfactors);
                        entity.Departs.Add(tbmfactors);

                    }
                }
                entity.Groups = null;
                entity.Groups = new List<TbmReSultCusGroup>();
                if (input.Groups != null && input.Groups.Count > 0)
                {
                    foreach (var occdis in input.Groups)
                    {
                        //var tbmfactors = occdis.MapTo<TbmReSultCusGroup>();
                        var tbmfactors = new TbmReSultCusGroup();
                        tbmfactors.Id = Guid.NewGuid();
                        tbmfactors.ItemGroupId = occdis;
                        tbmfactors.ReSultSetId = entity.Id;
                        tbmfactors = _TbmReSultCusGroup.Insert(tbmfactors);
                        entity.Groups.Add(tbmfactors);
                    }
                }
                entity.Items = null;
                entity.Items = new List<TbmReSultCusItem>();
                if (input.Items != null && input.Items.Count > 0)
                {
                    foreach (var occdis in input.Items)
                    {
                        //var tbmfactors = occdis.MapTo<TbmReSultCusItem>();
                        var tbmfactors = new TbmReSultCusItem();
                        tbmfactors.Id = Guid.NewGuid();
                        tbmfactors.ItemInfoId = occdis;
                        tbmfactors.ReSultSetId = entity.Id;
                        tbmfactors = _TbmReSultCusItem.Insert(tbmfactors);
                        entity.Items.Add(tbmfactors);
                    }
                }
                CurrentUnitOfWork.SaveChanges();
                var dto = entity.MapTo<ReSultSetDto>();
                return dto;
            }
            else
            {
                ReSultSetDto entity = new ReSultSetDto();
                entity.Items = new List<ReSultCusItemDto>();
                entity.Groups = new List<ReSultCusGroupDto>();
                if (input.ReSultSets != null && input.ReSultSets.Id != null)
                {
                    var occtarget = _TbmReSultSet.Get(input.ReSultSets.Id);            
                    if (input.Items != null && input.Items.Count > 0)
                    {
                        //先删除
                        _TbmReSultCusItem.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                        foreach (var occConTar in input.Items)
                        {
                            // var tbmfactors = occConTar.MapTo<TbmReSultCusItem>();
                            var tbmfactors = new TbmReSultCusItem();
                            tbmfactors.Id = Guid.NewGuid();
                            tbmfactors.ItemInfoId = occConTar;
                            tbmfactors.ReSultSetId = occtarget.Id;
                            tbmfactors = _TbmReSultCusItem.Insert(tbmfactors);
                        }
                    }
                    else
                    {
                        _TbmReSultCusItem.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                    }
                    if (input.Groups != null && input.Groups.Count > 0)
                    {
                        //先删除
                        _TbmReSultCusGroup.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                        foreach (var occConTar in input.Groups)
                        {
                            //var tbmfactors = occConTar.MapTo<TbmReSultCusGroup>();
                            var tbmfactors = new TbmReSultCusGroup();
                            tbmfactors.Id = Guid.NewGuid();
                            tbmfactors.ItemGroupId = occConTar;
                            tbmfactors.ReSultSetId = occtarget.Id;
                            tbmfactors = _TbmReSultCusGroup.Insert(tbmfactors);
                        }
                    }
                    else
                    {
                        _TbmReSultCusGroup.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                    }
                    if (input.Departs != null && input.Departs.Count > 0)
                    {
                        //先删除
                        _TbmReSultDepart.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                        foreach (var occConTar in input.Departs)
                        {
                            //var tbmfactors = occConTar.MapTo<TbmReSultDepart>();
                            var tbmfactors = new TbmReSultDepart();
                            tbmfactors.Id = Guid.NewGuid();
                            tbmfactors.DepartmentId = occConTar;
                            tbmfactors.ReSultSetId = occtarget.Id;
                            tbmfactors = _TbmReSultDepart.Insert(tbmfactors);
                        }
                    }
                    else
                    {
                        _TbmReSultDepart.GetAll().Where(o => o.ReSultSetId == occtarget.Id).Delete();
                    }
                    entity.isAdVice = input.ReSultSets.isAdVice;
                    entity.isOccupational= input.ReSultSets.isOccupational;
                    //input.ReSultSets.MapTo(occtarget);
                 
                    CurrentUnitOfWork.SaveChanges();
                    var occtargetnow = _TbmReSultSet.Get(input.ReSultSets.Id);
                    occtargetnow.isAdVice = input.ReSultSets.isAdVice;
                    occtargetnow.isOccupational = input.ReSultSets.isOccupational;
                    var entitys = _TbmReSultSet.Update(occtargetnow);
                    var dto = entitys.MapTo<ReSultSetDto>();
                    return dto;
                }
                else
                {
                    return new ReSultSetDto();
                }

            }
            

        }
        /// <summary>
        /// 获取检查结果
        /// </summary>
        /// <returns></returns>
        public List<ReSultCusInfoDto> getCusResult(ShowResultSetDto input)
        {
            var cuslst = new List<ReSultCusInfoDto>();
            //获取设置
            var cus = _TjlCustomerReg.GetAll();
            var adNameSumque = _TjlCustomerSummarizeBM.GetAll();
            var que = _TjlCustomerSummarize.GetAll();
            var occque = _TjlOccCustomerHazardSum.GetAll();
            if (input.ClientrRegID != null)
            {
                cus = cus.Where(o => o.ClientRegId == input.ClientrRegID);
                adNameSumque = adNameSumque.Where(o => o.CustomerReg.ClientRegId == input.ClientrRegID);
                que = que.Where(o => o.CustomerReg.ClientRegId == input.ClientrRegID);
                occque = occque.Where(o => o.CustomerRegBM.ClientRegId == input.ClientrRegID);
            }
            if (input.StartDataTime != null)
            {
                if (input.TimeType == 1)
                {
                    cus = cus.Where(o => o.LoginDate >= input.StartDataTime);
                    adNameSumque = adNameSumque.Where(o => o.CustomerReg.LoginDate >= input.StartDataTime);
                    que = que.Where(o => o.CustomerReg.LoginDate >= input.StartDataTime);
                    occque = occque.Where(o => o.CustomerRegBM.LoginDate >= input.StartDataTime);
                }
                else
                {
                    cus = cus.Where(o => o.BookingDate >= input.StartDataTime);
                    adNameSumque = adNameSumque.Where(o => o.CustomerReg.BookingDate >= input.StartDataTime);
                    que = que.Where(o => o.CustomerReg.BookingDate >= input.StartDataTime);
                    occque = occque.Where(o => o.CustomerRegBM.BookingDate >= input.StartDataTime);
                }
            }
            if (input.EndDataTime != null)
            {
                if (input.TimeType == 1)
                {
                    cus = cus.Where(o => o.LoginDate < input.EndDataTime);
                    adNameSumque = adNameSumque.Where(o => o.CustomerReg.LoginDate < input.EndDataTime);
                    que = que.Where(o => o.CustomerReg.LoginDate < input.EndDataTime);
                    occque = occque.Where(o => o.CustomerRegBM.LoginDate < input.EndDataTime);
                }
                else
                { cus = cus.Where(o => o.BookingDate < input.EndDataTime);
                    adNameSumque = adNameSumque.Where(o => o.CustomerReg.BookingDate < input.EndDataTime);
                    que = que.Where(o => o.CustomerReg.BookingDate < input.EndDataTime);
                    occque = occque.Where(o => o.CustomerRegBM.BookingDate < input.EndDataTime);
                }
            }
           
            if (input.CheckSate != null)
            {
                cus = cus.Where(o => o.CheckSate == input.CheckSate);
                adNameSumque = adNameSumque.Where(o => o.CustomerReg.CheckSate == input.CheckSate);
                que = que.Where(o => o.CustomerReg.CheckSate == input.CheckSate);
                occque = occque.Where(o => o.CustomerRegBM.CheckSate == input.CheckSate);
            }
            if (input.PhysicalType != null)
            {
                cus = cus.Where(o => o.PhysicalType == input.PhysicalType);
                adNameSumque = adNameSumque.Where(o => o.CustomerReg.PhysicalType == input.PhysicalType);
                que = que.Where(o => o.CustomerReg.PhysicalType == input.PhysicalType);
                occque = occque.Where(o => o.CustomerRegBM.PhysicalType == input.PhysicalType);
            }
            if (input.SummSate != null)
            {
                cus = cus.Where(o => o.SummSate == input.SummSate);
                adNameSumque = adNameSumque.Where(o => o.CustomerReg.SummSate == input.SummSate);
                que = que.Where(o => o.CustomerReg.SummSate == input.SummSate);
                occque = occque.Where(o => o.CustomerRegBM.SummSate == input.SummSate);
            }
            if (input.RegisterState.HasValue)
            {
                cus = cus.Where(o => o.RegisterState == input.RegisterState);
                adNameSumque = adNameSumque.Where(o => o.CustomerReg.RegisterState == input.RegisterState);
                que = que.Where(o => o.CustomerReg.RegisterState == input.RegisterState);
                occque = occque.Where(o => o.CustomerRegBM.RegisterState == input.RegisterState);
            }
            var set = _TbmReSultSet.GetAll().FirstOrDefault();

            if (set == null)
            {
              

                var sums = que.Select(o => new ReSultCusSumDto
                {
                    Advice = o.Advice,
                    CharacterSummary = o.CharacterSummary,
                    CustomerRegid = o.CustomerRegID,
                     occCharacterSummary=o.occCharacterSummary
                     
                });
                var   adNameSums = adNameSumque.Select(o => new ReSultCusSumDto
                {
                    Advice = o.SummarizeName,
                    CustomerRegid = o.CustomerRegID,
                   SummarizeOrderNum=  o.SummarizeOrderNum,
                     AdviceContent= o.Advice,
                      IsZYB=o.IsZYB

                });
                //职业健康
                var occsums = occque.Select(o => new ReSultCusOccSumDto
                {
                    CustomerregId = o.CustomerRegBMId,
                    Advise = o.Advise,
                    Conclusion = o.Conclusion,
                    Description = o.Description,
                    MedicalAdvice = o.MedicalAdvice,
                    Opinions=o.Opinions,
                     RiskName=o.OccHazardFactors.Text
                     
                });
               
                cuslst = cus.Select(o => new ReSultCusInfoDto
                {
                    Id=o.Id,
                    PhysicalType = o.PhysicalType,
                    BookingDate = o.BookingDate,
                    Age = o.Customer.Age,
                    ArchivesNum = o.Customer.ArchivesNum,
                    CheckSate = o.CheckSate,
                    ClientName = o.ClientInfo.ClientName,
                    CustomerBM = o.CustomerBM,
                    IDCardNo = o.Customer.IDCardNo,
                    InjuryAge = o.InjuryAge + o.InjuryAgeUnit,
                    LoginDate = o.LoginDate,
                    Mobile = o.Customer.Mobile,
                    Name = o.Customer.Name,
                    PostState = o.PostState,
                    PrintSate = o.PrintSate,
                    RiskS = o.RiskS,
                    Sex = o.Customer.Sex == 1 ? "男" : "女",
                    SummSate = o.SummSate,
                    TotalWorkAge = o.TotalWorkAge + o.WorkAgeUnit,
                    TypeWork = o.TypeWork,
                    WorkName = o.WorkName,
                    ReSultCusDeparts = o.CustomerDepSummary.Select(n => new ReSultCusDepartDto
                    {

                        DepartName = n.DepartmentName,
                        DepartOrder = n.DepartmentOrder,
                        DepartSum = n.DagnosisSummary,
                         DepartId=n.DepartmentBMId.Value
                    }).ToList(),
                    ReSultCusGrous = o.CustomerItemGroup.Select(r => new ReSultCusGroupDto
                    {
                        CustomerItemGroupBMid = r.Id,
                        DepartmentId = r.DepartmentId,
                        DepartOrder = r.DepartmentOrder,
                        GroupName = r.ItemGroupName,
                        GroupOrder = r.ItemGroupOrder,
                        GroupSum = r.ItemGroupSum,
                         ItemGroupId=r.ItemGroupBM_Id.Value
                    }).ToList(),
                    ReSultCusItems = o.CustomerRegItems.Select(r => new ReSultCusItemDto
                    {
                        ItemCodeBM = r.ItemCodeBM,
                        ItemName = r.ItemGroupBM.ItemGroupName,
                        ItemResultChar = r.ItemResultChar,
                        xmbs = r.Symbol,
                         ItemInfoId=r.ItemId,
                          DepartOrder=r.DepartmentBM.OrderNum,
                           GroupOrder=r.ItemGroupBM.OrderNum,
                            ItemOrder=r.ItemOrder
                    }).ToList(),
                    ReSultCusSum = sums.FirstOrDefault(r => r.CustomerRegid == o.Id) ,
                     //ReSultCusSums = sums.FirstOrDefault(r => r.CustomerRegid == o.Id)==null?"" : sums.FirstOrDefault(r => r.CustomerRegid == o.Id).Advice,
                     ReSultCusOccSum = occsums.FirstOrDefault(r => r.CustomerregId == o.Id )==null?"": occsums.FirstOrDefault(r => r.CustomerregId == o.Id).Conclusion,
                      Opinions= occsums.FirstOrDefault(r => r.CustomerregId == o.Id) == null ? "" : occsums.FirstOrDefault(r => r.CustomerregId == o.Id).Advise

                }).ToList();
                //总检结论
                foreach (var s in cuslst)
                {
                    s.ReSultCusOccSum =string.Join(";", occsums.Where(r => r.CustomerregId == s.Id).Select(p=>p.RiskName +":" + p.Conclusion ).ToList());
                    s.Opinions = string.Join(";", occsums.Where(r => r.CustomerregId == s.Id).Select(p => p.RiskName + ":" + p.Advise).ToList());

                    var adNames = adNameSums.Where(n => n.CustomerRegid == s.Id).Select(a =>new 
                    { a.Advice,a.SummarizeOrderNum,a.AdviceContent}).OrderBy(
                        p=>p.SummarizeOrderNum).ToList();
                    var advice = adNames.Select(P => P.SummarizeOrderNum + "." + P.Advice  ).ToList();
                    s.AdViceNames = string.Join(" ", advice);
                    var advicecon = adNames.Select(P => P.SummarizeOrderNum + "." + P.Advice+":" +P.AdviceContent ).ToList();
                    s.ReSultCusSums = string.Join(" ", advicecon);
                    //职业
                    var occadNames = adNameSums.Where(n => n.CustomerRegid == s.Id 
                    && n.IsZYB==1).Select(a => new    { a.Advice, a.SummarizeOrderNum, a.AdviceContent }).OrderBy(
                       p => p.SummarizeOrderNum).ToList();
                    var occadvice = occadNames.Select(P => P.SummarizeOrderNum + "." + P.Advice).ToList();
                    s.OCCAdViceNames= string.Join(" ", occadvice);

                    var OCCadvicecon = occadNames.Select(P => P.SummarizeOrderNum + "." + P.Advice + ":" + P.AdviceContent).ToList();
                  
                    s.OCCReSultCusSums = string.Join(" ", OCCadvicecon);
                }
                //foreach (var s in sums)
                //{
                //    var cusrr = cuslst.FirstOrDefault(p => p.CustomerRegId == s.CustomerRegid);
                //    cusrr.ReSultCusSums = s.Advice;
                //}
                //foreach (var s in occsums)
                //{
                //    var cusrr = cuslst.FirstOrDefault(p => p.CustomerRegId == s.CustomerregId);
                //    cusrr.ReSultCusOccSum = s.Conclusion;
                //}
                //foreach (var s in cuslst)
                //{
                //    s.ReSultCusSums = sums.Where(n => n.CustomerRegid == s.CustomerRegId).Select(a => a.Advice).FirstOrDefault();
                //    s.ReSultCusOccSum = occsums.Where(n => n.CustomerregId == s.CustomerRegId).Select(a => a.Conclusion).FirstOrDefault();

                //}
            }
            else
            {
                var ItemIDs = set.Items?.Select(o => o.ItemInfoId).ToList();
                var GroupIDs = set.Groups?.Select(o => o.ItemGroupId).ToList();
                var DepartIDs = set.Departs?.Select(o => o.DepartmentId).ToList();
                cus = cus.Where(o=>o.CustomerItemGroup.Any(p=>GroupIDs.Contains(p.ItemGroupBM_Id)) || 
                o.CustomerItemGroup.Any(p=>DepartIDs.Contains(p.DepartmentId)) || 
                o.CustomerRegItems.Any(p=>ItemIDs.Contains(p.ItemId)));
                //var sums = new List<ReSultCusSumDto>();
                var adNameSums = new List<ReSultCusSumDto>();
                var sums = que.Select(o => new ReSultCusSumDto
                {
                    Advice = o.Advice,
                    CharacterSummary = o.CharacterSummary,
                    CustomerRegid = o.CustomerRegID,
                     occCharacterSummary=o.occCharacterSummary
                     
                });
                //总检
                if (set.isAdVice.HasValue && set.isAdVice == true)
                {
                    
                    adNameSums = adNameSumque.Select(o => new ReSultCusSumDto
                    {
                        Advice = o.SummarizeName,
                        CustomerRegid = o.CustomerRegID,
                        SummarizeOrderNum = o.SummarizeOrderNum,
                         AdviceContent=o.Advice,
                          IsZYB=o.IsZYB

                    }).ToList();


                }
                //职业健康
                var occsums = new List<ReSultCusOccSumDto>();
             

                    occsums = occque.Select(o => new ReSultCusOccSumDto
                    {
                        CustomerregId=o.CustomerRegBMId,
                        Advise =o.Advise,
                        Conclusion=o.Conclusion,
                        Description=o.Description,
                        MedicalAdvice=o.MedicalAdvice,
                         RiskName=o.OccHazardFactors.Text
                         
                    }).ToList();
                


                cuslst = cus.Select(o => new ReSultCusInfoDto
                {
                    Id=o.Id,
                    CustomerRegId=o.Id,
                    PhysicalType=o.PhysicalType,
                    BookingDate=o.BookingDate,
                    Age = o.Customer.Age,
                    ArchivesNum = o.Customer.ArchivesNum,
                    CheckSate = o.CheckSate,
                    ClientName = o.ClientInfo.ClientName,
                    CustomerBM = o.CustomerBM,
                    IDCardNo = o.Customer.IDCardNo,
                    InjuryAge = o.InjuryAge + o.InjuryAgeUnit,
                    LoginDate = o.LoginDate,
                    Mobile = o.Customer.Mobile,
                    Name = o.Customer.Name,
                    PostState = o.PostState,
                    PrintSate = o.PrintSate,
                    RiskS = o.RiskS,
                    Sex = o.Customer.Sex == 1 ? "男" : "女",
                    SummSate = o.SummSate,
                    TotalWorkAge = o.TotalWorkAge + o.WorkAgeUnit,
                    TypeWork = o.TypeWork,
                    WorkName = o.WorkName,
                     Department=o.Customer.Department,
                      RegisterState=o.RegisterState,
                       Remarks=o.Remarks,
                    ReSultCusSum = sums.FirstOrDefault(r => r.CustomerRegid == o.Id),
                    ReSultCusDeparts = o.CustomerDepSummary.Where(n => DepartIDs.Contains(n.DepartmentBMId.Value)).Select(r => new ReSultCusDepartDto {
                         DepartId=r.DepartmentBMId.Value,
                        DepartName = r.DepartmentName,
                        DepartOrder = r.DepartmentOrder,
                        DepartSum = r.DagnosisSummary }).ToList(),
                    ReSultCusGrous = o.CustomerItemGroup.Where(n => GroupIDs.Contains(n.ItemGroupBM_Id)).Select(r => new ReSultCusGroupDto {
                         ItemGroupId=r.ItemGroupBM_Id.Value,
                        CustomerItemGroupBMid = r.Id,
                        DepartmentId = r.DepartmentId,
                        DepartOrder = r.DepartmentOrder,
                        GroupName =  r.ItemGroupName,
                        GroupOrder = r.ItemGroupOrder,
                        GroupSum = r.ItemGroupSum }).ToList(),
                    ReSultCusItems = o.CustomerRegItems.Where(n => ItemIDs.Contains(n.ItemId)).Select(r => new ReSultCusItemDto {
                         ItemInfoId=r.ItemId,
                        ItemCodeBM = r.ItemCodeBM,
                        ItemName = r.ItemName,
                        ItemResultChar = r.ItemResultChar,
                        xmbs = r.Symbol ,
                        DepartOrder = r.DepartmentBM.OrderNum,
                        GroupOrder = r.ItemGroupBM.OrderNum,
                        ItemOrder = r.ItemOrder
                    }).ToList(),
                    //ReSultCusOccSum = occsums.Where(n => n.CustomerregId == o.Id).Select(r => new ReSultCusOccSumDto
                    //{

                    //    Advise = r.Advise,
                    //    Conclusion = r.Conclusion,
                    //    CustomerregId = r.CustomerregId,
                    //    Description = r.Description,
                    //    MedicalAdvice = r.MedicalAdvice
                    //}).First(),
                 
            }).ToList();
                //总检结论
                foreach (var s in cuslst)
                {
                    //s.ReSultCusSums = sums.Where(n => n.CustomerRegid == s.CustomerRegId).Select(a => a.Advice).FirstOrDefault();
                    //s.ReSultCusOccSum = occsums.Where(n => n.CustomerregId == s.CustomerRegId).Select(a => a.Conclusion).FirstOrDefault();
                    //s.Opinions = occsums.FirstOrDefault(r => r.CustomerregId == s.Id) == null ? "" : occsums.FirstOrDefault(r => r.CustomerregId == s.Id).Advise;
                    //var adNames = adNameSums.Where(n => n.CustomerRegid == s.CustomerRegId).Select(a => a.Advice).ToList();

                    s.ReSultCusOccSum = string.Join(";", occsums.Where(r => r.CustomerregId == s.Id).Select(p => p.RiskName + ":" + p.Conclusion).ToList());
                    s.Opinions = string.Join(";", occsums.Where(r => r.CustomerregId == s.Id).Select(p => p.RiskName + ":" + p.Advise).ToList());


                    var adNames = adNameSums.Where(n => n.CustomerRegid == s.CustomerRegId).Select(a => new
                    { a.Advice, a.SummarizeOrderNum,a.CharacterSummary ,a.AdviceContent}).OrderBy(
                      p => p.SummarizeOrderNum).ToList();
                    var advice = adNames.Select(P => P.SummarizeOrderNum + "." + P.Advice  ).ToList();
                    s.AdViceNames = string.Join(" ", advice);

                    var advicecon = adNames.Select(P => P.SummarizeOrderNum + "." + P.Advice + ":" + P.AdviceContent  ).ToList();
                    s.ReSultCusSums = string.Join(" ", advicecon);

                    //职业
                    var occadNames = adNameSums.Where(n => n.CustomerRegid == s.Id
                    && n.IsZYB == 1).Select(a => new { a.Advice, a.SummarizeOrderNum, a.AdviceContent }).OrderBy(
                       p => p.SummarizeOrderNum).ToList();
                    var occadvice = occadNames.Select(P => P.SummarizeOrderNum + "." + P.Advice).ToList();
                    s.OCCAdViceNames = string.Join(" ", occadvice);

                    var OCCadvicecon = occadNames.Select(P => P.SummarizeOrderNum + "." + P.Advice + ":" + P.AdviceContent).ToList();

                    s.OCCReSultCusSums = string.Join(" ", OCCadvicecon);
                }
                //  //职业健康结论
                //  foreach (var s in cuslst)
                //{
                //      s.ReSultCusOccSum = occsums.Where(n => n.CustomerregId == s.CustomerRegId).Select(a => a.Conclusion).FirstOrDefault();
                //}
            }
            return cuslst;

        }
    }
}
