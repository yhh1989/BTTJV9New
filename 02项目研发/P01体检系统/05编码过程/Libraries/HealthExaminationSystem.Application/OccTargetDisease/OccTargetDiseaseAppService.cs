using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease
{
    [AbpAuthorize]
    public class OccTargetDiseaseAppService : MyProjectAppServiceBase, IOccTargetDiseaseAppService
    {

        private readonly IRepository<TbmOccTargetDisease, Guid> _TbmOccTargetDisease;
        private readonly IRepository<TbmOccTargetDiseaseContraindication, Guid> _TbmOccTargetDiseaseContraindication;
        private readonly IRepository<TbmOccDisease, Guid> _TbmOccDisease;
        private readonly IRepository<TbmOccTargetDiseaseSymptoms, Guid> _TbmOccTargetDiseaseSymptoms;
        private readonly IRepository<TbmItemGroup, Guid> _TbmItemGroup;
        private readonly IRepository<TbmOccHazardFactor, Guid> _TbmOccHazardFactor; //危害因素 
        private readonly IRepository<TbmItemInfo, Guid> _TbmItemInfo;
        public OccTargetDiseaseAppService(IRepository<TbmOccTargetDisease, Guid> TbmOccTargetDisease,
            IRepository<TbmOccTargetDiseaseContraindication, Guid> TbmOccTargetDiseaseContraindication,
            IRepository<TbmOccDisease, Guid> TbmOccDisease,
            IRepository<TbmOccTargetDiseaseSymptoms, Guid> TbmOccTargetDiseaseSymptoms,
            IRepository<TbmItemGroup, Guid> TbmItemGroup,
            IRepository<TbmOccHazardFactor, Guid> TbmOccHazardFactor,
            IRepository<TbmItemInfo, Guid> TbmItemInfo
         )
        {
            _TbmOccTargetDisease = TbmOccTargetDisease;
            _TbmOccTargetDiseaseContraindication = TbmOccTargetDiseaseContraindication;
            _TbmOccDisease = TbmOccDisease;
            _TbmOccTargetDiseaseSymptoms = TbmOccTargetDiseaseSymptoms;
            _TbmItemGroup = TbmItemGroup;
            _TbmOccHazardFactor = TbmOccHazardFactor;
            _TbmItemInfo = TbmItemInfo;
        }

        /// <summary>
        /// 添加 目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutTbmOccTargetDiseaseDto Add(FullTargetDiseaseDto input)
        {
            //目标疾病
            input.OneTargetDisease.Id = Guid.NewGuid();
            var entity = input.OneTargetDisease.MapTo<TbmOccTargetDisease>();
            entity.OccDiseases = null;
            entity.OccDiseases = new List<TbmOccDisease>();
            if (input.OneDisease != null && input.OneDisease.Count > 0)
            {
                foreach (var occdis in input.OneDisease)
                {
                    var tbmoccdic = _TbmOccDisease.Get(occdis);
                    entity.OccDiseases.Add(tbmoccdic);

                }
            }
            entity.MustIemGroups = null;
            entity.MustIemGroups = new List<TbmItemGroup>();
            if (input.MustGroups != null && input.MustGroups.Count > 0)
            {
                foreach (var occdis in input.MustGroups)
                {
                    var tbmoccdic = _TbmItemGroup.Get(occdis);
                    entity.MustIemGroups.Add(tbmoccdic);

                }
            }
            entity.MayIemGroups = null;
            entity.MayIemGroups = new List<TbmItemGroup>();
            if (input.mayGroups != null && input.mayGroups.Count > 0)
            {
                foreach (var occdis in input.mayGroups)
                {
                    var tbmoccdic = _TbmItemGroup.Get(occdis);
                    entity.MayIemGroups.Add(tbmoccdic);

                }
            }
            entity.ItemInfo = null;
            entity.ItemInfo=new List<TbmItemInfo>();
            if (input.ItemInfo != null && input.ItemInfo.Count > 0)
            {
                foreach (var occdis in input.ItemInfo)
                {
                    var tbmoccdic = _TbmItemInfo.Get(occdis);
                    entity.ItemInfo.Add(tbmoccdic);

                }
            }
            entity = _TbmOccTargetDisease.Insert(entity);

            entity.Contraindications = null;
            entity.Contraindications = new List<TbmOccTargetDiseaseContraindication>();
            if (input.ManyTargetDiseaseContraindication != null && input.ManyTargetDiseaseContraindication.Count > 0)
            {
                foreach (var factors in input.ManyTargetDiseaseContraindication)
                {
                    var tbmfactors = factors.MapTo<TbmOccTargetDiseaseContraindication>();
                    tbmfactors.Id = Guid.NewGuid();
                    tbmfactors.OccTargetDiseaseId = entity.Id;
                    tbmfactors = _TbmOccTargetDiseaseContraindication.Insert(tbmfactors);
                    entity.Contraindications.Add(tbmfactors);
                }
            }
            //症状 问诊
            entity.Symptoms = null;
            entity.Symptoms = new List<TbmOccTargetDiseaseSymptoms>();
            if (input.ManyTargetDiseaseSymptoms != null && input.ManyTargetDiseaseSymptoms.Count > 0)
            {
                foreach (var stand in input.ManyTargetDiseaseSymptoms)
                {
                    var tbmstand = stand.MapTo<TbmOccTargetDiseaseSymptoms>();
                    tbmstand.Id = Guid.NewGuid();
                    tbmstand.OccTargetDiseaseId = entity.Id;
                    tbmstand = _TbmOccTargetDiseaseSymptoms.Insert(tbmstand);
                    entity.Symptoms.Add(tbmstand);
                }

            }
            CurrentUnitOfWork.SaveChanges();
            var dto = entity.MapTo<OutTbmOccTargetDiseaseDto>();
            return dto;
        }

        /// <summary>
        /// 显示 目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutTbmOccTargetDiseaseDto> ShowOccHazardFactor(SeachOccTargetDiseaseDto input)
        {
            var OccDictionary = _TbmOccTargetDisease.GetAll();
            if (!string.IsNullOrWhiteSpace(input.CheckType))
                OccDictionary = OccDictionary.Where(i => i.CheckType == input.CheckType);
            //if (!string.IsNullOrWhiteSpace(input.OccHazardFactors.Text))
            //    OccDictionary = OccDictionary.Where(i => i.OccHazardFactorsId == input.OccHazardFactorsId);
            if (input.IsOk == 3)
            {
                OccDictionary = OccDictionary;
            }
            else if (input.IsOk == 0)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsOk);
            }
            else if (input.IsOk == 1)
            {
                OccDictionary = OccDictionary.Where(i => i.IsActive == input.IsOk);
            }
            if (!string.IsNullOrEmpty(input.OccDisName))
            {
                OccDictionary = OccDictionary.Where(o => o.OccDiseases.Any(r => input.OccDisName.Contains(r.Text)));
            }
            if (!string.IsNullOrEmpty(input.ConTrName))
            {
                OccDictionary = OccDictionary.Where(o => o.Contraindications.Any(r => input.ConTrName.Contains(r.Text)));
            }
            if (input.OccHazardFactorsId != null)
            {
                OccDictionary = OccDictionary.Where(i => i.OccHazardFactorsId == input.OccHazardFactorsId);
            }
            var s= OccDictionary.MapTo<List<OutTbmOccTargetDiseaseDto>>();
            return s;

        }

        /// <summary>
        /// 删除 目标疾病
        /// </summary>
        /// <param name="input"></param>
        public void Del(EntityDto<Guid> input)
        {
            _TbmOccTargetDisease.Delete(input.Id);
        }
        /// <summary>
        /// 修改 目标疾病
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutTbmOccTargetDiseaseDto Edit(FullTargetDiseaseDto input)
        {
            OutTbmOccTargetDiseaseDto entity = new OutTbmOccTargetDiseaseDto();
            entity.Contraindications = new List<TbmOccTargetDiseaseContraindicationDto>();
            if (input.OneTargetDisease != null && input.OneTargetDisease.Id != null)
            {
                var occtarget = _TbmOccTargetDisease.Get(input.OneTargetDisease.Id);

                if (input.ManyTargetDiseaseContraindication != null && input.ManyTargetDiseaseContraindication.Count > 0)
                {
                    //先删除
                    _TbmOccTargetDiseaseContraindication.GetAll().Where(o => o.OccTargetDiseaseId == occtarget.Id).Delete();
                    foreach (var occConTar in input.ManyTargetDiseaseContraindication)
                    {
                        var tbmfactors = occConTar.MapTo<TbmOccTargetDiseaseContraindication>();
                        tbmfactors.Id = Guid.NewGuid();
                        tbmfactors.OccTargetDiseaseId = occtarget.Id;
                        tbmfactors = _TbmOccTargetDiseaseContraindication.Insert(tbmfactors);
                        //entity.Contraindications.Add(tbmfactors);
                    }

                }
                else
                {
                    _TbmOccTargetDiseaseContraindication.GetAll().Where(o => o.OccTargetDiseaseId == occtarget.Id).Delete();
                }
                //症状
                if (input.ManyTargetDiseaseSymptoms != null && input.ManyTargetDiseaseSymptoms.Count > 0)
                {
                    //先删除

                    _TbmOccTargetDiseaseSymptoms.GetAll().Where(o => o.OccTargetDiseaseId == occtarget.Id).Delete();
                    foreach (var occConTar in input.ManyTargetDiseaseSymptoms)
                    {
                        var tbmfactors = occConTar.MapTo<TbmOccTargetDiseaseSymptoms>();
                        tbmfactors.Id = Guid.NewGuid();
                        tbmfactors.OccTargetDiseaseId = occtarget.Id;
                        tbmfactors = _TbmOccTargetDiseaseSymptoms.Insert(tbmfactors);
                        //entity.Symptoms.Add(tbmfactors);
                    }

                }
                else
                {
                    _TbmOccTargetDiseaseSymptoms.GetAll().Where(o => o.OccTargetDiseaseId == occtarget.Id).Delete();
                }

                //修改禁忌证
                input.OneTargetDisease.MapTo(occtarget);
                occtarget.OccDiseases.Clear();
                //occtarget.OccDiseases = new List<TbmOccDisease>();
                if (input.OneDisease != null && input.OneDisease.Count() > 0)
                {
                    foreach (var occdis in input.OneDisease)
                    {
                        var tbmoccdic = _TbmOccDisease.Get(occdis);
                        occtarget.OccDiseases.Add(tbmoccdic);

                    }

                }
                else
                {
                    _TbmOccTargetDiseaseSymptoms.GetAll().Where(o => o.OccTargetDiseaseId == occtarget.Id).Delete();
                }
                occtarget.MustIemGroups.Clear();
                //occtarget.MustIemGroups = new List<TbmItemGroup>();
                //_TbmItemGroup.GetAll().Where(o => o.Id == occtarget.Id).Delete();
                if (input.MustGroups != null && input.MustGroups.Count > 0)
                {
                    foreach (var occdis in input.MustGroups)
                    {
                        var tbmoccdic = _TbmItemGroup.Get(occdis);
                        occtarget.MustIemGroups.Add(tbmoccdic);

                    }
                }
                occtarget.MayIemGroups.Clear();
                //occtarget.MayIemGroups = new List<TbmItemGroup>();
                if (input.mayGroups != null && input.mayGroups.Count > 0)
                {
                    foreach (var occdis in input.mayGroups)
                    {
                        var tbmoccdic = _TbmItemGroup.Get(occdis);
                        occtarget.MayIemGroups.Add(tbmoccdic);

                    }
                }
                occtarget.ItemInfo.Clear();
                //occtarget.MayIemGroups = new List<TbmItemGroup>();
                if (input.ItemInfo != null && input.ItemInfo.Count > 0)
                {
                    foreach (var occdis in input.ItemInfo)
                    {
                        var tbmoccdic = _TbmItemInfo.Get(occdis);
                        occtarget.ItemInfo.Add(tbmoccdic);

                    }
                }
                var entitys = _TbmOccTargetDisease.Update(occtarget);
                var dto = entitys.MapTo<OutTbmOccTargetDiseaseDto>();
                return dto;

            }
            else
            {
                return new OutTbmOccTargetDiseaseDto();
            }
        }
        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public OutTbmOccTargetDiseaseDto GetOccTargetDisease(EntityDto<Guid> input)
        {
            var data = _TbmOccTargetDisease.Get(input.Id);
            return data.MapTo<OutTbmOccTargetDiseaseDto>();
        }
        /// <summary>
        /// 根据目标疾病和岗位类别获取可选项目和必选项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutIItemGroupsDto getOccHazardFactors(InputRisksDto input)
        {
            OutIItemGroupsDto outResult = new OutIItemGroupsDto();
            var que = _TbmOccTargetDisease.GetAll().Where(o => input.Risks.Contains(o.OccHazardFactorsId.Value) && o.CheckType == input.ChekType);
            var outkmust = que.SelectMany(o => o.MustIemGroups).Distinct().MapTo<List<SimpleItemGroupDto>>();
            var outMay = que.SelectMany(o => o.MayIemGroups).Distinct().MapTo<List<SimpleItemGroupDto>>();
            outResult.MustItemGroups = outkmust.Where(p=>p.Department!=null).ToList();
            var mustGrouplist= que.SelectMany(o => o.MustIemGroups).Where(p => p.Department != null).Distinct().ToList();
            List<Guid> removegroup = new List<Guid>();
            foreach (var mustgroup in mustGrouplist)
            {
                var ItemIdlist = mustgroup.ItemInfos.Select(p => p.Id).ToList();
                var bhgroups = mustGrouplist.Where(p => p.Id != mustgroup.Id && p.ItemInfos.All(o => ItemIdlist.Contains(o.Id))).ToList();
                if (bhgroups.Count > 0)
                {
                    removegroup.AddRange(bhgroups.Select(p => p.Id).ToList());
                    //var removelist = bhgroups.MapTo<List<SimpleItemGroupDto>>();
                    //foreach (var remove in removelist)
                    //{
                    //    removegroup.AddRange(remove.)
                    //    outResult.MustItemGroups.Remove(remove);
                    //}

                }
            }
            if (removegroup.Count>0)
            {
                outResult.MustItemGroups = outResult.MustItemGroups.Where(p=> 
                !removegroup.Contains(p.Id)).ToList();
            }

            outResult.MayItemGroups = outMay.Where(p => p.Department != null).ToList();
            return outResult;
        }

        /// <summary>
        /// 获取防护措施
        /// </summary>
        /// <returns></returns>
        public List<TbmOccDiseaseDto> GetTbmOccDisease()
        {
            var data = _TbmOccDisease.GetAll();
            return data.MapTo<List<TbmOccDiseaseDto>>();
        }

        public List<OutTbmOccTargetDiseaseDto> AddExcel(List<OutOccTargetDiseaseExcel> input)
        {
            var dto = new List<OutTbmOccTargetDiseaseDto>();
            var ret = new TbmOccTargetDisease();
            List<string> norisk = new List<string>();
            List<string> group = new List<string>();
            List<string> Occls = new List<string>();
            var OccDictionary = _TbmOccTargetDisease.GetAll();
            foreach (var data in input)
            {
                if (_TbmOccTargetDisease.GetAll().Any(r => r.OccHazardFactors.Text == data.OccHazardFactorsName && r.CheckType == data.CheckType))
                {
                    continue;
                }
                else
                {
                    if (data.Id == null || data.Id == Guid.Empty)
                    {
                        var entity = input.MapTo<TbmOccTargetDisease>();
                        entity.Id = Guid.NewGuid();
                        //危害因素
                        if (!string.IsNullOrEmpty(data.OccHazardFactorsName))
                        {
                            var riskls = data.OccHazardFactorsName;
                            var tbmHazar = _TbmOccHazardFactor.GetAll().FirstOrDefault(o => o.Text == data.OccHazardFactorsName);
                            if (tbmHazar != null)
                            {
                                entity.OccHazardFactorsId = tbmHazar.Id;
                            }
                            else
                            {
                                //没有危害因素
                                if (!norisk.Contains(data.OccHazardFactorsName))
                                {
                                    norisk.Add(data.OccHazardFactorsName);
                                    continue;
                                }
                            }

                        }
                        //职业健康
                        entity.OccDiseases = null;
                        entity.OccDiseases = new List<TbmOccDisease>();
                        if (!string.IsNullOrEmpty(data.OccDiseases))
                        {
                            if (data.OccDiseases != "/")
                            {
                                var riskls = data.OccDiseases.Split('、');
                                foreach (var risk in riskls)
                                {
                                    //var tbmfactors = risk.MapTo<TbmOccDisease>();
                                    var tbmHazar = _TbmOccDisease.GetAll().FirstOrDefault(o => o.Text == risk);
                                    //entity.OccDiseases.Add(tbmHazar);
                                    if (tbmHazar != null)
                                    {
                                        entity.OccDiseases.Add(tbmHazar);
                                    }
                                    else {
                                        continue;
                                    }
                                    //else
                                    //{
                                    //    //没有职业健康
                                    //    if (!Occls.Contains(data.OccDiseases))
                                    //    {
                                    //        Occls.Add(data.OccDiseases);
                                    //    }
                                    //    continue;
                                    //}
                                }
                            }

                        }
                        //必检项目
                        entity.MustIemGroups = null;
                        entity.MustIemGroups = new List<TbmItemGroup>();
                        if (!string.IsNullOrEmpty(data.MustIemGroups))
                        {
                            var riskls = data.MustIemGroups.Split('、');
                            foreach (var risk in riskls)
                            {
                                if (data.MustIemGroups != "/")
                                {
                                    var tbmHazar = _TbmItemGroup.GetAll().FirstOrDefault(o => o.ItemGroupName == risk);
                                    entity.MustIemGroups.Add(tbmHazar);
                                    //if (tbmHazar != null)
                                    //{
                                    //    entity.MustIemGroups.Add(tbmHazar);
                                    //}
                                    //else
                                    //{
                                    //    //没有组合
                                    //    if (!group.Contains(risk))
                                    //    {
                                    //        group.Add(risk);
                                    //    }
                                    //    continue;
                                    //}
                                }
                            }
                        }
                        //可选项目
                        entity.MayIemGroups = null;
                        entity.MayIemGroups = new List<TbmItemGroup>();
                        if (!string.IsNullOrEmpty(data.MayIemGroups))
                        {
                            var riskls = data.MayIemGroups.Split('、');
                            foreach (var risk in riskls)
                            {
                                if (data.MayIemGroups != "/")
                                {
                                    var tbmHazar = _TbmItemGroup.GetAll().FirstOrDefault(o => o.ItemGroupName == risk);
                                    entity.MayIemGroups.Add(tbmHazar);
                                    //if (tbmHazar != null)
                                    //{
                                    //    entity.MayIemGroups.Add(tbmHazar);
                                    //}
                                    //else
                                    //{
                                    //    if (!group.Contains(risk))
                                    //    {
                                    //        group.Add(risk);
                                    //        continue;
                                    //    }
                                    //}
                                }
                            }
                        }
                        //if (norisk != null && group != null && Occls != null)
                        //{
                        //    continue;
                        //} 
                        entity.CheckType = data.CheckType;
                        entity.Crowd = data.Crowd;
                        entity.InquiryTips = data.InquiryTips;
                            entity = _TbmOccTargetDisease.Insert(entity);
                            //症状大类
                            entity.Symptoms = null;
                            entity.Symptoms = new List<TbmOccTargetDiseaseSymptoms>();
                            if (!string.IsNullOrEmpty(data.Symptoms))
                            {
                                if (data.Symptoms != "/")
                                {
                                    var riskls = data.Symptoms.Split('、');
                                    foreach (var risk in riskls)
                                    {
                                        var tbmfactors = risk.MapTo<TbmOccTargetDiseaseSymptoms>();
                                        tbmfactors.Id = Guid.NewGuid();
                                        tbmfactors.OccTargetDiseaseId = entity.Id;
                                        tbmfactors.Text = risk;
                                        var allData = _TbmOccTargetDiseaseSymptoms.GetAll();
                                        int orderNum = 0;
                                        tbmfactors.OrderNum = orderNum + 1;
                                        tbmfactors = _TbmOccTargetDiseaseSymptoms.Insert(tbmfactors);
                                        if (tbmfactors.Id != null)
                                        {
                                            entity.Symptoms.Add(tbmfactors);
                                        }
                                    }
                                }
                            }
                            //职业禁忌证
                            entity.Contraindications = null;
                            entity.Contraindications = new List<TbmOccTargetDiseaseContraindication>();
                            if (!string.IsNullOrEmpty(data.Contraindications))
                            {
                                if (data.Contraindications.Contains("/"))
                                {
                                    continue;
                                }
                                else
                                {
                                    var riskls = data.Contraindications.Split('、');
                                    foreach (var risk in riskls)
                                    {
                                        var tbmfactors = risk.MapTo<TbmOccTargetDiseaseContraindication>();
                                        tbmfactors.Id = Guid.NewGuid();
                                        tbmfactors.OccTargetDiseaseId = entity.Id;
                                        tbmfactors.Text = risk;
                                        int orderNum = 0;
                                        tbmfactors.OrderNum = orderNum + 1;
                                        tbmfactors = _TbmOccTargetDiseaseContraindication.Insert(tbmfactors);
                                        entity.Contraindications.Add(tbmfactors);
                                    }
                                }
                            }
                        
                        CurrentUnitOfWork.SaveChanges();
                        ret = entity;
                        dto.Add(ret.MapTo<OutTbmOccTargetDiseaseDto>());                                                
                    }
                }                
            }
            //if (norisk != null)
            //{
            //    throw new FieldVerifyException(string.Join(",", norisk) ,"不存在");
            //}
            //if (group != null)
            //{
            //    XtraMessageBox.Show(string.Join(",", group) + "不存在").ToString();
            //}
            //if (Occls != null)
            //{
            //    XtraMessageBox.Show(string.Join(",", Occls) + "不存在");
            //}
            return dto;
        }
    }
}
