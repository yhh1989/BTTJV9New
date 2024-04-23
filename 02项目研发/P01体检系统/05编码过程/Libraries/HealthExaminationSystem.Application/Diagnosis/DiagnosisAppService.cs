using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis
{
    [AbpAuthorize]
    public class DiagnosisAppService : MyProjectAppServiceBase, IDiagnosisAppService
    {
        #region 接口和引用
        private readonly IRepository<TbmItemGroup, Guid> _tbmItemGroup;//科室组合项目查询
        private readonly IRepository<TbmDiagnosis, Guid> _tbmDiagnosis;//复合判断设置
        private readonly IRepository<TbmDiagnosisData, Guid> _tbmDiagnosisData;//复合判断设置明细
        private readonly IRepository<TbmItemInfo, Guid> _itemInfoRepository;
        private readonly IRepository<TjlCustomerRegItem, Guid> _CustomerRegItemRepository;
        private readonly IRepository<TjlCustomerReg, Guid> _CustomerRegRepository;
        public DiagnosisAppService(
               IRepository<TbmItemGroup, Guid> tbmItemGroup,
               IRepository<TbmDiagnosis, Guid> tbmDiagnosis,
               IRepository<TbmDiagnosisData, Guid> tbmDiagnosisData,
               IRepository<TbmItemInfo, Guid> itemInfoRepository,
               IRepository<TjlCustomerRegItem, Guid> CustomerRegItemRepository,
               IRepository<TjlCustomerReg, Guid> CustomerRegRepository
            )
        {
            _tbmItemGroup = tbmItemGroup;
            _tbmDiagnosis = tbmDiagnosis;
            _tbmDiagnosisData = tbmDiagnosisData;
            _itemInfoRepository = itemInfoRepository;
            _CustomerRegItemRepository = CustomerRegItemRepository;
            _CustomerRegRepository = CustomerRegRepository;
        }
        #endregion

        #region 查询科室组合项目
        /// <summary>
        /// 科室组合项目查询
        /// </summary>
        [UnitOfWork(isTransactional: false)]
        public List<ItemInfoGroupDto> QueryInfoGroup(ItemInfoGroupDto input)
        {
            var paDtoList = _tbmItemGroup.GetAllIncluding(r => r.Department, r => r.ItemInfos);

            if (input.ItemGroupName != null&& input.ItemGroupName != "")
            { //组合项目名称
                paDtoList = paDtoList.Where(o => o.ItemGroupName.Contains(input.ItemGroupName));
            }

            var rows = paDtoList.MapTo<List<ItemInfoGroupDto>>();
            return rows;
        }



        #endregion

        #region 复合判断
        /// <summary>
        /// 查询
        /// </summary>
        public PageResultDto<TbmDiagnosisDto> QueryDiagnosis(PageInputDto<TbmDiagnosisDto> input)
        {
            var query = _tbmDiagnosis.GetAll();
            if (input.Input.RuleName != null)
            { //复合判断名称
                query = query.Where(o => o.RuleName.Contains(input.Input.RuleName));
            }


            if (query.Count() != 0)
            {
                query = query.OrderBy(o => o.OrderNum).ThenByDescending(o=>o.CreationTime);

                var result = new PageResultDto<TbmDiagnosisDto>();
                result.CurrentPage = input.CurentPage;
                result.Calculate(query.Count());
                query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
                result.Result = query.MapTo<List<TbmDiagnosisDto>>();
                return result;
            }


            else
                return null;


        }
        /// <summary>
        /// 用主键查找
        /// </summary>
        /// <param name="input">主键</param>
        public TbmDiagnosisDto GetById(EntityDto<Guid> input)
        {
            var Diagnosis = _tbmDiagnosis.Get(input.Id);
            return Diagnosis.MapTo<TbmDiagnosisDto>();
        }


        /// <summary>
        /// 添加
        /// </summary>
        public void InsertDiagnosis(TbmDiagnosisDto input)
        {
            if (input.Id == Guid.Empty)
            {
                if (_tbmDiagnosis.GetAll().Any(r => r.RuleName == input.RuleName))
                {
                    throw new FieldVerifyException("名称重复！", "名称重复！");
                }

                input.Id = Guid.NewGuid();
                var diagnosisDatals = input.DiagnosisDatals;
                input.DiagnosisDatals = null;
                var diagnosisEntity = _tbmDiagnosis.Insert(input.MapTo<TbmDiagnosis>());
                foreach (var item in diagnosisDatals)
                {
                    // inputs[item].Id = Guid.NewGuid();
                    var itemInfoId = item.ItemInfo.Id;
                    item.ItemInfo = null;
                    var entity = item.MapTo<TbmDiagnosisData>();
                    entity.Id = Guid.NewGuid();
                    entity.Diagnosis = diagnosisEntity;
                    entity.ItemInfo = _itemInfoRepository.Get(itemInfoId);
                    //entity.Diagnosis = dto;
                    _tbmDiagnosisData.Insert(entity);
                }
            }
            else
            {
                if (_tbmDiagnosis.GetAll().Any(r => r.RuleName == input.RuleName && r.Id != input.Id))
                {
                    throw new FieldVerifyException("名称重复！", "名称重复！");
                }

                TbmDiagnosis Cl = _tbmDiagnosis.FirstOrDefault(o => o.Id == input.Id);
                //input.MapTo(Cl);
                Cl.RuleName = input.RuleName;
                Cl.Conclusion = input.Conclusion;
                Cl.OrderNum = input.OrderNum;
                Cl.Remarks = input.Remarks;

                var diagnosisDatals = input.DiagnosisDatals;
                input.DiagnosisDatals = null;
                var dto = _tbmDiagnosis.Update(Cl);


                //先删除子项
                //List<TbmDiagnosisData> dtos = _tbmDiagnosisData.GetAll().Where(o => o.Diagnosis.Id == dto.Id).ToList();
                ICollection<TbmDiagnosisData> tbm = new List<TbmDiagnosisData>();
                tbm = dto.DiagnosisDatals;

                for (var i = 0; i < tbm.Count;)
                    //foreach (var item in dto.DiagnosisDatals)
                    //{
                    _tbmDiagnosisData.Delete(tbm.First());
                //}
                //再添加子项
                foreach (var item in diagnosisDatals)
                {
                    // inputs[item].Id = Guid.NewGuid();
                    var itemInfoId = item.ItemInfo.Id;
                    item.ItemInfo = null;
                    var entity = item.MapTo<TbmDiagnosisData>();
                    entity.Id = Guid.NewGuid();
                    entity.Diagnosis = dto;
                    entity.ItemInfo = _itemInfoRepository.Get(itemInfoId);
                    //entity.Diagnosis = dto;
                    _tbmDiagnosisData.Insert(entity);
                }


            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteDiagnosis(EntityDto<Guid> input)
        {

            var pa = _tbmDiagnosis.FirstOrDefault(o => o.Id == input.Id);

            //TbmDiagnosis Pa = _tbmDiagnosis.GetAll().Where(o => o.Id == input.Id).FirstOrDefault();
            var diagnosisDatals = pa.DiagnosisDatals;
            pa.DiagnosisDatals = null;
            _tbmDiagnosis.Delete(pa);

            ICollection<TbmDiagnosisData> tbm = new List<TbmDiagnosisData>();
            tbm = diagnosisDatals;

            for (var i = 0; i < tbm.Count; i++)
                //foreach (var item in dto.DiagnosisDatals)
                //{
                _tbmDiagnosisData.Delete(tbm.First());



        }
        /// <summary>
        /// 获取阳性结果
        /// </summary>
        /// <param name="intput"></param>
        /// <returns></returns>
        public List<CusRegInfoDto> getIllCount(SearchItem intput)
        {
            var que = _CustomerRegItemRepository.GetAll().Where(p=>p.CustomerItemGroupBM.IsAddMinus
            !=3 && p.ProcessState==2);
            if (intput.ClientRegId.HasValue)
            {
                que = que.Where(p => p.CustomerRegBM.ClientRegId == intput.ClientRegId);
            }
            if (intput.StartDateTime.HasValue)
            {
                que = que.Where(p => p.CustomerRegBM.LoginDate >= intput.StartDateTime);

            }
            if (intput.EndDateTime.HasValue)
            {
                que = que.Where(p => p.CustomerRegBM.LoginDate < intput.EndDateTime);

            }
            if (!string.IsNullOrEmpty(intput.IllStr))
            {
                que = que.Where(p => p.ItemResultChar.Contains(intput.IllStr) || p.ItemDiagnosis.Contains(intput.IllStr));
            }
            if (intput.ItemID != null && intput.ItemID.Count > 0)
            {
                que = que.Where(p => intput.ItemID.Contains(p.ItemId));
            }
            if (intput.MinValue.HasValue)
            {
                var min = intput.MinValue.ToString();
                que = que.Where(p => (p.ItemTypeBM == (int)ItemType.Calculation || p.ItemTypeBM == (int)ItemType.Number) && p.ItemResultChar.CompareTo(min) > 0);
            }
            if (intput.MaxValue.HasValue)
            {
                var max = intput.MaxValue.ToString();
                que = que.Where(p => (p.ItemTypeBM == (int)ItemType.Calculation || p.ItemTypeBM == (int)ItemType.Number) &&   p.ItemResultChar.CompareTo(max) <0);
            }
          
            if (intput.ISIll==true)
            {
                que = que.Where(p => p.Symbol=="H" || p.Symbol == "P" || p.Symbol == "L") ;
            }
            var reglist = que.Select(p => new CusRegInfoDto
            {
                Age = p.CustomerRegBM.Customer.Age,
                CustomerBM = p.CustomerRegBM.CustomerBM,
                ItemDiag = p.ItemDiagnosis,
                ItemName = p.ItemName,
                ItemValue =(p.ItemBM!=null && p.ItemBM.moneyType == (int)ItemType.Explain && 
                p.ItemDiagnosis !=null && p.ItemDiagnosis!="") ? p.ItemDiagnosis: p.ItemResultChar,
                LoginDate = p.CustomerRegBM.LoginDate,
                Name = p.CustomerRegBM.Customer.Name,
                Sex = p.CustomerRegBM.Customer.Sex,
                Stand = p.Stand,
                Symbol = p.Symbol,
                ClientName = p.CustomerRegBM.ClientInfo.ClientName,
                DepartOrderNum = p.DepartmentBM.OrderNum,
                GrouptOrderNum = p.ItemGroupBM.OrderNum,
                ItemOrderNum = p.ItemBM.OrderNum
            }).OrderByDescending(p => p.LoginDate).ToList();
            return reglist;
        }
        #endregion

    }

}