using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Z.EntityFramework.Plus;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using AutoMapper.QueryableExtensions;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal
{
    [AbpAuthorize]
    public class InspectionTotalAppService : MyProjectAppServiceBase, IInspectionTotalAppService
    {
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository;

        private readonly IRepository<TjlCustomerRegItem, Guid> _customerRegItemRepository;

        private readonly IRepository<TjlCustomerSummarize, Guid> _customerSummarizeRepository;

        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _customerSummarizeBmRepository;

        private readonly IRepository<TjlCustomerSummBack, Guid> _customerSummBackRepository;

        private readonly IRepository<TjlCustomerDepSummary, Guid> _customerDepSummaryRepository;
        private readonly IRepository<TbmItemGroup, Guid> _ItemGroup;

        private readonly ICustomerAppService _customerAppService;

        private readonly ISqlExecutor _sqlExecutor;

        private readonly IRepository<TjlCusReview, Guid> _TjlCusReview;
        private readonly IRepository<TjlCusGiveUp, Guid> _TjlCusGiveUp;

        private readonly IRepository<TbmReviewItemSet, Guid> _ReviewItemSet;
        private readonly IRepository<TbmSumHide, Guid> _TbmSumHide;
        private readonly IRepository<TbmOccTargetDisease, Guid> _TbmOccTargetDisease;
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlOccCustomerOccDiseases, Guid> _TjlOccCustomerOccDiseases;
        private readonly IRepository<TjlOccCustomerContraindication, Guid> _TjlOccCustomerContraindication;
        private IIDNumberAppService _iIDNumberAppService;
        private readonly IRepository<TjlErrBM, Guid> _TjlErrBM;
        private readonly IRepository<TbmSumConflict, Guid> _SumConflictRepository;
        private readonly IIDNumberAppService _idNumberAppService;
        private readonly IRepository<User, long> _User;
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        private readonly IRepository<TbmOccDisease, Guid> _TbmOccDisease;
        private readonly IRepository<TbmOccDictionary, Guid> _bmOccDictionary;
        private readonly IRepository<TbmBasicDictionary, Guid> _basicDictionary;
        public InspectionTotalAppService(
            IRepository<TjlCustomerReg, Guid> customerRegRepository,
            IRepository<TjlCustomerSummarize, Guid> customerSummarizeRepository,
            IRepository<TjlCustomerSummarizeBM, Guid> customerSummarizeBmRepository,
            IRepository<TjlCustomerRegItem, Guid> customerRegItemRepository,
            IRepository<TjlCustomerSummBack, Guid> customerSummBackRepository,
            IRepository<TjlCustomerDepSummary, Guid> customerDepSummaryRepository,
            IRepository<TbmItemGroup, Guid> ItemGroup,
            ISqlExecutor sqlExecutor,
            IRepository<TjlCusReview, Guid> TjlCusReview,
             IRepository<TbmReviewItemSet, Guid> ReviewItemSet,
             IRepository<TbmSumHide, Guid> TbmSumHide,
             IRepository<TbmOccTargetDisease, Guid> TbmOccTargetDisease,
             IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
             IRepository<TjlOccCustomerOccDiseases, Guid> TjlOccCustomerOccDiseases,
             IRepository<TjlOccCustomerContraindication, Guid> TjlOccCustomerContraindication,
             IIDNumberAppService iIDNumberAppService,
             IRepository<TjlErrBM, Guid> TjlErrBM,
             IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
             IRepository<TbmSumConflict, Guid> SumConflictRepository,
             IIDNumberAppService idNumberAppService,
              ICustomerAppService customerAppService,
               IRepository<TjlCusGiveUp, Guid> TjlCusGiveUp,
               IRepository<User, long> User,
               IRepository<TjlOccCustomerHazardSum, Guid> TjlOccCustomerHazardSum,
              IRepository<TbmOccDisease, Guid> TbmOccDisease,
             IRepository<TbmOccDictionary, Guid> bmOccDictionary,
             IRepository<TbmBasicDictionary, Guid> basicDictionary
        )
        {
            _customerRegRepository = customerRegRepository;
            _customerSummarizeRepository = customerSummarizeRepository;
            _customerSummarizeBmRepository = customerSummarizeBmRepository;
            _customerRegItemRepository = customerRegItemRepository;
            _customerSummBackRepository = customerSummBackRepository;
            _customerDepSummaryRepository = customerDepSummaryRepository;
            _sqlExecutor = sqlExecutor;
            _ItemGroup = ItemGroup;
            _TjlCusReview = TjlCusReview;
            _ReviewItemSet = ReviewItemSet;
            _TbmSumHide = TbmSumHide;
            _TbmOccTargetDisease = TbmOccTargetDisease;
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlOccCustomerOccDiseases = TjlOccCustomerOccDiseases;
            _TjlOccCustomerContraindication = TjlOccCustomerContraindication;
            _iIDNumberAppService = iIDNumberAppService;
            _TjlErrBM = TjlErrBM;
            _customerItemGroupRepository = customerItemGroupRepository;
            _SumConflictRepository = SumConflictRepository;
            _idNumberAppService = idNumberAppService;
            _customerAppService = customerAppService;
            _TjlCusGiveUp = TjlCusGiveUp;
            _User = User;
            _TjlOccCustomerHazardSum = TjlOccCustomerHazardSum;
            _TbmOccDisease = TbmOccDisease;
            _bmOccDictionary = bmOccDictionary;
            _basicDictionary = basicDictionary;
        }

        /// <summary>
        /// 查询体检人登记信息（列表页）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<InspectionTotalListDto> GetInspectionTotalList(TjlCustomerQuery query)
        {
            var _TjlCustomerReg = _customerRegRepository.GetAllIncluding(r => r.Customer);
            _TjlCustomerReg = _TjlCustomerReg.Where(r => r.CheckSate != (int)PhysicalEState.Not);
            if (query != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Name))
                    _TjlCustomerReg = _TjlCustomerReg.Where(m =>
                        m.Customer.Name.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.ClientInfo.ClientName.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.CustomerBM.Contains(DbFunctions.AsNonUnicode(query.Name)));
                if (!string.IsNullOrWhiteSpace(query.Code))
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(m => m.CustomerBM == DbFunctions.AsNonUnicode(query.Code));
                }
                else
                {
                    if (query.BeginDate.HasValue)
                    {
                        query.BeginDate = query.BeginDate.Value.Date;
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.LoginDate >= query.BeginDate);
                    }

                    if (query.EndDate.HasValue)
                    {
                        query.EndDate = query.EndDate.Value.Date.AddDays(1);
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.LoginDate <= query.EndDate);
                    }
                }
                if (query.arrEmployeeName_Id != null && query.arrEmployeeName_Id.Length > 0)
                {
                    var DocId = query.arrEmployeeName_Id[0];
                    _TjlCustomerReg = _TjlCustomerReg.Where(m => m.CSEmployeeId == DocId);
                }
            }

            _TjlCustomerReg = _TjlCustomerReg.OrderByDescending(o => o.LoginDate);
            _TjlCustomerReg = _TjlCustomerReg.Include(r => r.Customer);
            var result = _TjlCustomerReg.MapTo<List<InspectionTotalListDto>>();
            return result;
        }

        /// <summary>
        /// 查询体检人登记信息（个人）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalSearchDto> GetCustomerRegList(TjlCustomerQuery query)
        {
            //获取职业加健康的体检类型
           // var tjtype = _basicDictionary.GetAll().FirstOrDefault(p=>p.Type== "ExaminationType" && p.Text.Contains("职业+健康") )?.Value;
            if (query != null && query.CustomerRegID != Guid.Empty && query.CustomerRegID != null)
            {
                var _TjlCustomerReg = _customerRegRepository.Get((Guid)query.CustomerRegID);

                var result = _TjlCustomerReg.MapTo<TjlCustomerRegForInspectionTotalSearchDto>();
                List<TjlCustomerRegForInspectionTotalSearchDto> list = new List<TjlCustomerRegForInspectionTotalSearchDto>();
                list.Add(result);
                return list;
            }
            else
            {
                var _TjlCustomerReg = _customerRegRepository.GetAllIncluding(r => r.Customer);
                // _TjlCustomerReg = _TjlCustomerReg.Where(r => r.CheckSate != (int)PhysicalEState.Not);
                if (query != null)
                {
                    if (query.IsGetNeiWaiKe != null && !(bool)query.IsGetNeiWaiKe)
                    {
                        //不查询内外科
                        _TjlCustomerReg = _TjlCustomerReg.Where(n => n.CustomerItemGroup.Where(o => o.DepartmentName == "内外科检查").Count() != 0);
                    }
                    if (!string.IsNullOrWhiteSpace(query.Name))
                        _TjlCustomerReg = _TjlCustomerReg.Where(m =>
                            m.Customer.Name.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.ClientInfo.ClientName.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.CustomerBM.Contains(DbFunctions.AsNonUnicode(query.Name)));
                    if (!string.IsNullOrWhiteSpace(query.Code))
                    {
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.CustomerBM == DbFunctions.AsNonUnicode(query.Code));
                    }
                    else
                    {
                        if (query.EndDate.HasValue && query.EndDate.HasValue && query.DateType.HasValue && query.DateType == 2)
                        {
                           
                                var regID = _customerSummarizeRepository.GetAll().Where(o => (o.ConclusionDate >= query.BeginDate &&
                             o.ConclusionDate < query.EndDate) || (o.occConclusionDate.HasValue &&
                             o.occConclusionDate >= query.BeginDate &&
                             o.occConclusionDate < query.EndDate)).Select(o => o.CustomerRegID).ToList();
                                _TjlCustomerReg = _TjlCustomerReg.Where(o => regID.Contains(o.Id));
                           

                            //_TjlCustomerReg = _TjlCustomerReg.Where(m => m. >= query.BeginDate);
                        }
                        else
                        {
                            if (query.BeginDate.HasValue)
                            {
                                query.BeginDate = query.BeginDate.Value.Date;
                                _TjlCustomerReg = _TjlCustomerReg.Where(m => m.LoginDate >= query.BeginDate);
                            }

                            if (query.EndDate.HasValue)
                            {
                                query.EndDate = query.EndDate.Value.Date.AddDays(1);
                                _TjlCustomerReg = _TjlCustomerReg.Where(m => m.LoginDate <= query.EndDate);
                            }
                        }
                    }
                    if (query.arrEmployeeName_Id != null && query.arrEmployeeName_Id.Length > 0)
                    {
                        var DocId = query.arrEmployeeName_Id[0];
                        if (query.EmployeeNameType == 1)
                        {
                            _TjlCustomerReg = _TjlCustomerReg.Where(m => m.FSEmployeeId == DocId);
                        }
                        else
                        {
                            _TjlCustomerReg = _TjlCustomerReg.Where(m => m.CSEmployeeId == DocId);
                        }
                    }
                    if (query.PhysicalType.HasValue)
                    {
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.PhysicalType == query.PhysicalType.Value);
                    }
                    if (query.ClientrRegID.HasValue)
                    {
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.ClientRegId == query.ClientrRegID.Value);
                    }
                    if (query.SumState.HasValue)
                    {
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.SummSate == query.SumState.Value || (m.OccSummSate.HasValue && m.OccSummSate== query.SumState.Value));
                    }
                    if (query.CheckState.HasValue)
                    {
                        _TjlCustomerReg = _TjlCustomerReg.Where(m => m.CheckSate == query.CheckState.Value);
                    }
                    if (query.UpZybState.HasValue && query.UpZybState != 2)
                    {
                        if (query.UpZybState == 0)
                        {
                            _TjlCustomerReg = _TjlCustomerReg.Where(m => m.UploadState == query.UpZybState.Value ||   m.UploadState==null);
                        }
                        else
                        {
                            _TjlCustomerReg = _TjlCustomerReg.Where(m => m.UploadState == query.UpZybState.Value);
                        }
                    }

                }
                var userBM = _User.Get(AbpSession.UserId.Value);
                if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(p => p.HospitalArea == userBM.HospitalArea
                    || p.HospitalArea == null);
                }

                _TjlCustomerReg = _TjlCustomerReg.OrderByDescending(o => o.LoginDate);
                _TjlCustomerReg = _TjlCustomerReg.Include(r => r.Customer);
                var result = _TjlCustomerReg.MapTo<List<TjlCustomerRegForInspectionTotalSearchDto>>();
                return result;
            }
        }
        /// <summary>
        /// 总检dto转换
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TjlCustomerRegForInspectionTotalDto Transformation(TjlCustomerRegForInspectionTotalSearchDto input)
        {
            return ObjectMapper.Map<TjlCustomerRegForInspectionTotalDto>(input);

        }
        /// <summary>
        /// 查询下一位已完成的体检人
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetReCustomerReg(TjlCustomerQuery query)
        {
            if (query != null && query.CustomerRegID != Guid.Empty && query.CustomerRegID != null)
            {
                var _TjlCustomerReg = _customerRegRepository.Get((Guid)query.CustomerRegID);
                var result = _TjlCustomerReg.MapTo<TjlCustomerRegForInspectionTotalDto>();
                List<TjlCustomerRegForInspectionTotalDto> list = new List<TjlCustomerRegForInspectionTotalDto>();
                list.Add(result);
                return list;
            }
            else
            {
                //获取一个月内体检已完成未总检或未审核的体检人
                var _TjlCustomerReg = _customerRegRepository.GetAllIncluding(r => r.Customer);
                var star = System.DateTime.Now.AddMonths(-1);
                var end = System.DateTime.Now;
                if (query.SumState.HasValue && query.SumState == (int)SummSate.NotAlwaysCheck)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(r => r.CheckSate == (int)PhysicalEState.Complete &&
             r.LoginDate >= star && r.LoginDate <= end && r.SummSate == (int)SummSate.NotAlwaysCheck);
                }
                else if (query.SumState.HasValue && query.SumState == (int)SummSate.HasInitialReview)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(r => r.CheckSate == (int)PhysicalEState.Complete &&
            r.LoginDate >= star && r.LoginDate <= end && r.SummSate == (int)SummSate.HasInitialReview);
                }
                else
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(r => r.CheckSate == (int)PhysicalEState.Complete &&
                 r.LoginDate >= star && r.LoginDate <= end &&
                (r.SummSate == (int)SummSate.NotAlwaysCheck || r.SummSate == (int)SummSate.HasInitialReview));
                }
                if (query.ClientrRegID.HasValue)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(o => o.ClientRegId == query.ClientrRegID.Value);
                }
                if (query.PhysicalType.HasValue)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(o => o.PhysicalType == query.PhysicalType.Value);
                }
                if (query.PhysicalTypelist != null && query.PhysicalTypelist.Count > 0)
                {
                    _TjlCustomerReg = _TjlCustomerReg.Where(p => p.PhysicalType.HasValue && query.PhysicalTypelist.Contains(p.PhysicalType.Value));
                }
                _TjlCustomerReg = _TjlCustomerReg.OrderByDescending(o => o.LoginDate);
                _TjlCustomerReg = _TjlCustomerReg.Include(r => r.Customer);
                var result = _TjlCustomerReg.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
                return result;
            }
        }
        /// <summary>
        /// 生成总检结论表
        /// </summary>
        /// <param name="input"></param>
        public TjlCustomerSummarizeDto CreateSummarize(TjlCustomerSummarizeDto dto)
        {
            CreateCustomerSummarizeDto input = dto.MapTo<CreateCustomerSummarizeDto>();
            //判断是新增或修改
            if (input.Id == Guid.Empty)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    //查询之前是否有删除过得记录，如果有取之前的，没有则新增
                    var _TjlCustomerSummarize = _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == input.CustomerRegID && n.IsDeleted == false);
                    if (_TjlCustomerSummarize == null)
                    {
                          _TjlCustomerSummarize = _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == input.CustomerRegID && n.IsDeleted == true);
                    }
                        if (_TjlCustomerSummarize == null)
                    {
                        input.Id = Guid.NewGuid();
                        var entity = input.MapTo<TjlCustomerSummarize>();
                        entity.CustomerReg = null;
                        entity.ShEmployeeBM = null;
                        entity.EmployeeBM = null;
                        entity.ReviewContentDate = DateTime.Now;
                        var result = _customerSummarizeRepository.Insert(entity);
                        return result.MapTo<TjlCustomerSummarizeDto>();
                    }
                    else
                    {
                        var oldid = _TjlCustomerSummarize.Id;
                        input.MapTo(_TjlCustomerSummarize);
                        _TjlCustomerSummarize.EmployeeBM = null;
                        _TjlCustomerSummarize.ShEmployeeBM = null;
                        _TjlCustomerSummarize.IsDeleted = false;
                        _TjlCustomerSummarize.Id = oldid;
                        var result = _customerSummarizeRepository.Update(_TjlCustomerSummarize);
                        return result.MapTo<TjlCustomerSummarizeDto>();
                    }
                }
            }
            else
            {
                var Info = _customerSummarizeRepository.Get(input.Id);
                if (Info == null)
                    return null;
                input.MapTo(Info);
                //Info.CustomerReg = _customerRegRepository.Get(input.CustomerRegID);
                Info.EmployeeBM = null;
                Info.ShEmployeeBM = null;
                var result = _customerSummarizeRepository.Update(Info);
                // CurrentUnitOfWork.SaveChanges();
                return result.MapTo<TjlCustomerSummarizeDto>();

            }
        }

        /// <summary>
        /// 保存总检审核
        /// </summary>
        /// <param name="input"></param>
        public CustomerRegisterSummarizeDto SaveSummarize(CustomerRegisterSummarizeDto input)
        {
            TjlCustomerSummarize result = new TjlCustomerSummarize();

            //判断是新增或修改
            if (input.Id == Guid.Empty)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    //查询之前是否有删除过得记录，如果有取之前的，没有则新增
                    var _TjlCustomerSummarize = _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == input.CustomerRegID && n.IsDeleted == false);
                    if (_TjlCustomerSummarize == null)
                    {
                          _TjlCustomerSummarize = _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == input.CustomerRegID && n.IsDeleted == true);
                    }
                        if (_TjlCustomerSummarize == null)
                    {
                        input.Id = Guid.NewGuid();
                        var entity = ObjectMapper.Map<TjlCustomerSummarize>(input); //input.MapTo<TjlCustomerSummarize>();
                        entity.CustomerReg = null;
                        entity.ShEmployeeBM = null;
                        entity.EmployeeBM = null;
                        entity.ReviewContentDate = DateTime.Now;
                        result = _customerSummarizeRepository.Insert(entity);


                        // return result.MapTo<CustomerRegisterSummarizeDto>();

                    }
                    else
                    {
                        var oldid = _TjlCustomerSummarize.Id;
                        input.MapTo(_TjlCustomerSummarize);
                        _TjlCustomerSummarize.EmployeeBM = null;
                        _TjlCustomerSummarize.ShEmployeeBM = null;
                        _TjlCustomerSummarize.IsDeleted = false;
                        _TjlCustomerSummarize.Id = oldid;
                        result = _customerSummarizeRepository.Update(_TjlCustomerSummarize);
                        //try
                        //{
                        //    CurrentUnitOfWork.SaveChanges();
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw;
                        //}


                    }

                }
            }
            else
            {
                var Info = _customerSummarizeRepository.Get(input.Id);
                if (Info == null)
                    return null;
                input.MapTo(Info);
                //Info.CustomerReg = _customerRegRepository.Get(input.CustomerRegID);
                Info.EmployeeBM = null;
                Info.ShEmployeeBM = null;
                result = _customerSummarizeRepository.Update(Info);
                //try
                //{
                //    CurrentUnitOfWork.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}

                // result.MapTo<CustomerRegisterSummarizeDto>();

            }
            if (result.Qualified != null && result.Qualified == "合格")
            {

                var cusreg = _customerRegRepository.Get(input.CustomerRegID);
                if (string.IsNullOrWhiteSpace(cusreg.JKZBM))
                {
                    //有未使用的编码则使用
                    var ErrBM = _TjlErrBM.GetAll().ToList();
                    if (ErrBM.Count > 0)
                    {
                        cusreg.JKZBM = ErrBM[0].BM;
                        _TjlErrBM.Delete(ErrBM[0]);
                    }
                    else
                    {
                        cusreg.JKZBM = _iIDNumberAppService.CreateJKZBM();
                    }
                    _customerRegRepository.Update(cusreg);
                }
            }
            else if (result.Qualified != null && result.Qualified == "不合格")
            {
                // _TjlErrBM
                var cusreg = _customerRegRepository.Get(input.CustomerRegID);
                if (!string.IsNullOrWhiteSpace(cusreg.JKZBM))
                {//不合格把健康证编码记录起来
                    TjlErrBM tjlErrBM = new TjlErrBM();
                    tjlErrBM.Id = Guid.NewGuid();
                    tjlErrBM.IDType = "15";
                    tjlErrBM.BM = cusreg.JKZBM.ToString().Trim();
                    _TjlErrBM.Insert(tjlErrBM);
                    cusreg.JKZBM = "";
                    _customerRegRepository.Update(cusreg);
                }
            }

            return result.MapTo<CustomerRegisterSummarizeDto>();
        }

        /// <summary>
        /// 生成总检建议表
        /// </summary>
        /// <param name="input"></param>
        public void CreateSummarizeBM(List<TjlCustomerSummarizeBMDto> input)
        {
            foreach (var item in input)
            {
                var entity = item.MapTo<TjlCustomerSummarizeBM>();
                entity.CustomerReg = null;
                var result = _customerSummarizeBmRepository.Insert(entity);
            }
        }

        /// <summary>
        /// 分页获取体检人登记信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageResultDto<TjlCustomerRegForInspectionTotalDto> PageFulls(PageInputDto<TjlCustomerQuery> input)
        {
            var result = PageHelper.Paging<TjlCustomerQuery, TjlCustomerReg, TjlCustomerRegForInspectionTotalDto>(input,
                query =>
                {
                    var tjlCustomerReg = _customerRegRepository.GetAll().AsNoTracking();
                    tjlCustomerReg = tjlCustomerReg.Where(r => r.CheckSate != (int)PhysicalEState.Not);
                    if (query != null)
                    {
                        if (!string.IsNullOrWhiteSpace(query.Name))
                            tjlCustomerReg = tjlCustomerReg.Where(m =>
                                m.Customer.Name.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.ClientInfo.ClientName.Contains(DbFunctions.AsNonUnicode(query.Name)) || m.CustomerBM.Contains(DbFunctions.AsNonUnicode(query.Name)));
                        if (!string.IsNullOrWhiteSpace(query.Code))
                        {
                            tjlCustomerReg = tjlCustomerReg.Where(m => m.CustomerBM == DbFunctions.AsNonUnicode(query.Code));
                        }
                        else
                        {
                            if (query.BeginDate.HasValue)
                            {
                                query.BeginDate = query.BeginDate.Value.Date;
                                tjlCustomerReg = tjlCustomerReg.Where(m => m.LoginDate >= query.BeginDate);
                            }

                            if (query.EndDate.HasValue)
                            {
                                query.EndDate = query.EndDate.Value.Date.AddDays(1);
                                tjlCustomerReg = tjlCustomerReg.Where(m => m.LoginDate <= query.EndDate);
                            }
                        }
                    }

                    tjlCustomerReg = tjlCustomerReg.OrderByDescending(o => o.LoginDate);
                    tjlCustomerReg = tjlCustomerReg.Include(r => r.Customer);
                    return tjlCustomerReg;
                });
            return result;
        }

        public List<TjlCustomerRegItemDto> GetCustomerRegItemList(QueryClass query)
        {

            var data = _customerRegItemRepository.GetAll().AsNoTracking();
            data = data.Include(r => r.DepartmentBM);
            data = data.Include(r => r.CustomerItemGroupBM);
            if (query.CustomerRegId != Guid.Empty)
            {
                //于慧ChargeAppService中保存抹零项InsertMLGroup是根据一个固定的Guid来获取的，这里也这样获取，把抹零项目排除
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                var bmGrop = _ItemGroup.FirstOrDefault(o => o.Id == guid);
                if (bmGrop != null)
                    data = data.Where(o => o.ItemGroupBMId != bmGrop.Id);
                data = data.Where(r => r.CustomerRegId == query.CustomerRegId);

            }
            return data.MapTo<List<TjlCustomerRegItemDto>>();
        }

        /// <summary>
        /// 修改体检人预约信息表
        /// </summary>
        /// <param name="dto"></param>
        public void UpdateTjlCustomerRegDto(TjlCustomerRegForInspectionTotalDto dto)
        {
            var data = _customerRegRepository.Get(dto.Id);
            //这里修改否则会修改体检人信息
            // dto.MapTo(data);
            data.FSEmployeeId = dto.FSEmployeeId;
            data.CSEmployeeId = dto.CSEmployeeId;
            data.SummLocked = dto.SummLocked;
            data.SummSate = dto.SummSate;
            data.OccSummSate = dto.OccSummSate;
            if (dto.SummSate != (int)SummSate.Audited)
            {
                data.WXExportSate = 1;
            }
            _customerRegRepository.Update(data);

            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /// <summary>
        /// 修改体检人总检状态
        /// </summary>
        /// <param name="dto"></param>
        public void UpdateCusSumSate(INCusSumSateDto dto)
        {
            var data = _customerRegRepository.Get(dto.Id);
            dto.MapTo(data);
            //如果不是审核状态微信报告导出状态重置,网报上传状态重置
            if (dto.SummSate != (int)SummSate.Audited)
            {
                data.WXExportSate = 1;
                data.UploadState = 0;
            }
            _customerRegRepository.Update(data);

            try
            {
                CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 修改体检人锁定状态
        /// </summary>
        /// <param name="dto"></param>
        public void UpdateCuslockSate(LockStateDto dto)
        {
            //锁定前先解除所有其他锁定
            if (dto.SummLocked != 2)
            {
                _customerRegRepository.GetAll().Where(p => p.SummLockEmployeeBMId
                == dto.SummLockEmployeeBMId).Update(o => new TjlCustomerReg
                {
                    SummLockEmployeeBMId = null,
                    SummLocked = 2

                });
            }
            
            if (dto.SummLocked != 3)
            {
                var data = _customerRegRepository.Get(dto.Id);
                data.SummLockEmployeeBMId = dto.SummLockEmployeeBMId;
                data.SummLocked = dto.SummLocked;
                data.SummLockTime = System.DateTime.Now;
                _customerRegRepository.Update(data);
            }
            try
            {
                //CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /// <summary>
        /// 修改体检人总检状态
        /// </summary>
        /// <param name="dto"></param>
        public void UpdateTjlCustomerRegState(EditCustomerRegStateDto dto)
        {
            var data = _customerRegRepository.Get(dto.Id);
            dto.MapTo(data);
            CurrentUnitOfWork.SaveChanges();
            _customerRegRepository.Update(data);

            //CurrentUnitOfWork.SaveChanges();

        }
        /// <summary>
        /// 修改职业体检人总检状态
        /// </summary>
        /// <param name="dto"></param>
        public void UpdateOccTjlCustomerRegState(OccEditCustomerRegStateDto dto)
        {
            var data = _customerRegRepository.Get(dto.Id);
            dto.MapTo(data);
            CurrentUnitOfWork.SaveChanges();
            _customerRegRepository.Update(data);

            //CurrentUnitOfWork.SaveChanges();

        }
        /// <summary>
        /// 获取总检结论
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public TjlCustomerSummarizeDto GetSummarize(TjlCustomerQuery query)
        {
            var _TjlCustomerSummarize =
                _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == query.CustomerRegID);
            return _TjlCustomerSummarize.MapTo<TjlCustomerSummarizeDto>();
        }

        /// <summary>
        /// 获取总检建议
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerSummarizeBMDto> GetSummarizeBM(TjlCustomerQuery query)
        {
            //var _TjlCustomerSummarizeBM = _customerSummarizeBmRepository.GetAllIncluding(r => r.CustomerReg);
            //if (query != null)
            //    if (query.CustomerRegID != Guid.Empty)
            //        _TjlCustomerSummarizeBM =
            //            _TjlCustomerSummarizeBM.Where(m => m.CustomerRegID == query.CustomerRegID);
            //return _TjlCustomerSummarizeBM.MapTo<List<TjlCustomerSummarizeBMDto>>();

            var _TjlCustomerSummarizeBM = _customerSummarizeBmRepository.GetAll().AsNoTracking();
            if (query != null)
                if (query.CustomerRegID != Guid.Empty)
                    _TjlCustomerSummarizeBM =
                        _TjlCustomerSummarizeBM.Where(m => m.CustomerRegID == query.CustomerRegID);
            var result = _TjlCustomerSummarizeBM.Select(r => new { r.Id, r.SummarizeOrderNum, r.SummarizeName, r.Advice, r.CustomerRegID, r.SummarizeAdviceId, r.ParentAdviceId, r.IsPrivacy, r.IsZYB });
            var result1 = result.Select(r => new TjlCustomerSummarizeBMDto
            {
                Id = r.Id,
                SummarizeOrderNum = r.SummarizeOrderNum,
                SummarizeName = r.SummarizeName,
                Advice = r.Advice,
                CustomerRegID = r.CustomerRegID,
                SummarizeAdviceId = r.SummarizeAdviceId,
                ParentAdviceId = r.ParentAdviceId,
                IsPrivacy = r.IsPrivacy,
                IsZYB = r.IsZYB
            }).ToList();
            return result1;
        }

        /// <summary>
        /// 获取总检建议列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerSummarizeBMViewDto> GetlstSummarizeBM(QueryClass query)
        {
            var _TjlCustomerSummarizeBM =
                _customerSummarizeBmRepository.GetAll().Where(r => r.CustomerRegID == query.CustomerRegId);
            return _TjlCustomerSummarizeBM.MapTo<List<TjlCustomerSummarizeBMViewDto>>();
        }

        /// <summary>
        /// 总检医生工作量查询
        /// </summary>
        /// <returns></returns>
        public List<InspectionTotalStatisticsDto> InspectionTotalStatistics(TjlCustomerQuery query)
        {
            //TjlCustomerSummarize表    查出总检人 
            var data = _customerSummarizeRepository.GetAllIncluding(r => r.EmployeeBM);
            if (query.EmployeeNameType == 1)
            {
                data = data.Where(p => p.CustomerReg != null && p.CustomerReg.SummSate == 4 && p.ShEmployeeBMId.HasValue);
            }
            else
            {
                data = data.Where(p => p.CustomerReg != null && p.CustomerReg.SummSate != 1 && p.EmployeeBMId.HasValue);

            }
            if (query != null)
            {
                if (query.EmployeeNameType == 1)
                {
                    if (query.arrEmployeeName_Id != null && query.arrEmployeeName_Id.Length > 0)
                        data = data.Where(r => query.arrEmployeeName_Id.Contains(r.ShEmployeeBMId.Value));
                }
                else
                {
                    if (query.arrEmployeeName_Id != null && query.arrEmployeeName_Id.Length > 0)
                        data = data.Where(r => query.arrEmployeeName_Id.Contains(r.EmployeeBMId.Value));
                }
                if (query.EmployeeNameType == 1)
                {
                    if (query.BeginDate != null)
                        data = data.Where(r => r.ConclusionDate >= query.BeginDate);
                    if (query.EndDate != null)
                        data = data.Where(r => r.ConclusionDate <= query.EndDate);
                }
                else
                {
                    if (query.BeginDate != null)
                        data = data.Where(r => r.ExamineDate >= query.BeginDate);
                    if (query.EndDate != null)
                        data = data.Where(r => r.ExamineDate <= query.EndDate);
                }
            }
            List<InspectionTotalStatisticsDto> statisticsList = new List<InspectionTotalStatisticsDto>();
            if (query.EmployeeNameType == 1)
            {
                statisticsList = data.GroupBy(r => new { r.ShEmployeeBMId, r.ShEmployeeBM.Name }).Select(g =>
              new InspectionTotalStatisticsDto { EmployeeName = g.Key.Name, Count = g.Count() }).ToList();
            }
            else
            {
                statisticsList = data.GroupBy(r => new { r.EmployeeBMId, r.EmployeeBM.Name }).Select(g =>
new InspectionTotalStatisticsDto { EmployeeName = g.Key.Name, Count = g.Select(o=>o.CustomerRegID).Distinct().Count() }).ToList();
            }
            return statisticsList;
        }

        /// <summary>
        /// 新增总检退回
        /// </summary>
        /// <param name="input"></param>
        public void CreateCustomerSummBack(List<TjlCustomerSummBackDto> input)
        {
            foreach (var item in input)
                if (item.Id == Guid.Empty)
                {
                    var entity = item.MapTo<TjlCustomerSummBack>();
                    entity.Id = Guid.NewGuid();
                    entity.CustomerRegBM = null;
                    entity.Department = null;
                    entity.ReEmp1 = null;
                    entity.ReEmp2 = null;
                    entity.Bakemp1 = null;
                    entity.Bakemp2 = null;
                    entity.Opemp = null;
                    var result = _customerSummBackRepository.Insert(entity);
                }
                else
                {
                    var Info = _customerSummBackRepository.Get(item.Id);
                    item.MapTo(Info);
                    _customerSummBackRepository.Update(Info);
                }
        }

        /// <summary>
        /// 获取总检退回
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TjlCustomerSummBackDto> GetCustomerSummBack(TjlCustomerQuery query)
        {
            var _TjlCustomerSummBack =
                _customerSummBackRepository.GetAllList().Where(r => r.CustomerRegBmId == query.CustomerRegID);
            return _TjlCustomerSummBack.MapTo<List<TjlCustomerSummBackDto>>();
        }

        /// <summary>
        /// 删除建议表（对应多条）
        /// </summary>
        public void DelTjlCustomerSummarizeBM(TjlCustomerQuery query)
        {
            var Info = _customerSummarizeBmRepository.GetAll().Where(n => n.CustomerRegID == query.CustomerRegID);
            if (query.IsZYB == 1)
            {
                Info = Info.Where(p => p.IsZYB == 1 || p.IsZYB == null);
            }
            else if (query.IsZYB == 2)
            { Info = Info.Where(p => p.IsZYB == 2 || p.IsZYB == null); }
            var ss = Info.ToList();
            foreach (var item in Info)
            {
                _customerSummarizeBmRepository.Delete(item);
            }
        }

        /// <summary>
        /// 删除建议汇总表（对应一条）
        /// </summary>
        public void DelTjlCustomerSummarize(TjlCustomerQuery query)
        {
            var Info = _customerSummarizeRepository.FirstOrDefault(n => n.CustomerRegID == query.CustomerRegID);
            if (Info != null)
            {
                if (query.IsZYB.HasValue && query.IsZYB == 1)
                {
                    Info.occAdvice = "";
                    Info.occCharacterSummary = "";
                    Info.occConclusionDate = null;
                    Info.occEmployeeBMId = null;
                    Info.occExamineDate = null;
                    Info.occShEmployeeBMId = null;
                    Info.occCheckState = null;
                }
                else if (query.IsZYB.HasValue && query.IsZYB == 2)
                {
                    Info.Advice = "";
                    Info.CharacterSummary = "";
                    Info.ConclusionDate = null;
                    Info.EmployeeBMId = null;
                    Info.ExamineDate = null;
                    Info.ShEmployeeBMId = null;
                    Info.CheckState = null;
                }
                else
                {
                    _customerSummarizeRepository.Delete(Info);
                }
            }
        }

        /// <summary>
        /// 查询体检人详细登记信息
        /// </summary>
        /// <returns></returns>
        public ATjlCustomerRegDto GetCustomerRegInfo(EntityDto<Guid> Id)
        {
            var info = _customerRegRepository.Get(Id.Id);
            _sqlExecutor.DbContext.Entry(info).Collection(r => r.CustomerDepSummary).Load();
            return info.MapTo<ATjlCustomerRegDto>();
        }

        /// <summary>
        /// 查询体检人科室小结信息
        /// </summary>
        /// <returns>预约记录id</returns>
        public List<ATjlCustomerDepSummaryDto> GetCustomerDepSummaryList(EntityDto<Guid> Id)
        {
            var ListInfo = _customerDepSummaryRepository.GetAll().AsNoTracking().Select(o => new ATjlCustomerDepSummaryDto
            {
                CustomerRegId = o.CustomerRegId,
                DepartmentBMId = o.DepartmentBMId,
                CharacterSummary = o.CharacterSummary,
                DagnosisSummary = o.DagnosisSummary,
                CheckDate = o.CheckDate,
                SystemCharacter = o.SystemCharacter,
                DepartmentOrder = o.DepartmentOrder,
                DepartmentName = o.DepartmentName,
                ExamineEmployeeBMId = o.ExamineEmployeeBMId,
                PrivacyCharacterSummary = o.PrivacyCharacterSummary
            }).Where(n => n.CustomerRegId == Id.Id);
            return ListInfo.ToList();
        }

        /// <summary>
        /// 根据体检人身份证查询预约记录
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public TjlCustomerRegForInspectionTotalDto GetCustomerRegForIDCode(SearchCustomerRegDto Input)
        {
            var query = _customerRegRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(Input.IDCardNo))
                query = query.Where(o => o.Customer.IDCardNo == Input.IDCardNo);

            var result = query.OrderByDescending(o => o.CreationTime).FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var retdatas = result.MapTo<TjlCustomerRegForInspectionTotalDto>();
            return retdatas;
        }
        /// <summary>
        /// 根据体检人身份证查询所有记录
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public List<TjlCustomerRegForInspectionTotalDto> GetAllCustomerRegForIDCode(SearchCustomerRegDto Input)
        {
            var query = _customerRegRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(Input.IDCardNo))
                query = query.Where(o => o.Customer.IDCardNo == Input.IDCardNo);

            var result = query.OrderByDescending(o => o.LoginDate).ToList();
            if (result == null)
            {
                return null;
            }

            var retdatas = result.MapTo<List<TjlCustomerRegForInspectionTotalDto>>();
            return retdatas;
        }
        /// <summary>
        /// 历年项目结果
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<HistoryComparison.Dto.SearchCustomerRegItemDto> GetTjlCustomerHistoryItemReoprtDtos(EntityDto<Guid> input)
        {
            var cusreg = _customerRegRepository.Get(input.Id);
            var customerId = cusreg.CustomerId;
            var regs = _customerRegRepository.GetAll().Where(o => o.CustomerId == customerId && o.LoginDate <= cusreg.LoginDate).OrderByDescending(p => p.LoginDate).Take(5).
                Select(r => new { r.Id, r.CustomerBM, r.LoginDate }).ToList();
            var reglis = regs.Select(p => p.Id).ToList();
            var RegItemDto = _customerRegItemRepository.GetAllIncluding(r => r.CustomerRegBM).Where(o => reglis.Contains(o.CustomerRegId));
            RegItemDto = RegItemDto.OrderBy(
                p => p.DepartmentBM.OrderNum).ThenBy(p => p.ItemGroupBM.OrderNum).ThenBy(
                p => p.ItemOrder).ThenByDescending(p => p.CustomerRegBM.LoginDate);
            return RegItemDto.MapTo<List<HistoryComparison.Dto.SearchCustomerRegItemDto>>();
        }
        /// <summary>
        /// 获取历年建议结论
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        public List<TjlCustomerSummarizeBMViewDto> GetHistorySummarizeBM(EntityDto<Guid> input)
        {
            var customerId = _customerRegRepository.Get(input.Id).CustomerId;
            var reglis = _customerRegRepository.GetAll().Where(o => o.CustomerId == customerId).Take(5).Select(r => r.Id).ToList();
            var _TjlCustomerAdVice = _customerSummarizeBmRepository.GetAll().Where(o => reglis.Contains(o.CustomerRegID)).OrderBy(o => o.CustomerRegID).ThenByDescending(o => o.CreationTime);
            return _TjlCustomerAdVice.MapTo<List<TjlCustomerSummarizeBMViewDto>>();

        }

        /// <summary>
        /// 总检展示项目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerRegInspectItemDto> GetCusInspectItemList(QueryClass query)
        {
            var data = _customerRegItemRepository.GetAll().AsNoTracking();
            if (query.CustomerRegId != Guid.Empty)
            {
                //于慧ChargeAppService中保存抹零项InsertMLGroup是根据一个固定的Guid来获取的，这里也这样获取，把抹零项目排除
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                data = data.Where(o => o.ItemId != guid);
                data = data.Where(r => r.CustomerRegId == query.CustomerRegId);

            }
            data = data.Where(o => o.DepartmentBM.Category != "耗材");
            data = data.Where(o => o.DepartmentBM.Name != null);
            data = data.Where(n => n.CustomerItemGroupBM.PayerCat != (int)PayerCatType.NoCharge && n.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus);

            var relist = data.Select(o => new CustomerRegInspectItemDto
            {

                DepartmentName = o.DepartmentBM.Name,
                DepartmentOrderNum = o.DepartmentBM.OrderNum,
                ItemGroupName = o.CustomerItemGroupBM.ItemGroupName,
                GroupCheckState = o.CustomerItemGroupBM.CheckState,
                ItemGroupOrder = o.CustomerItemGroupBM.ItemGroupOrder,
                ItemName = o.ItemName,
                ItemResultChar = o.ItemResultChar,
                ItemId = o.ItemId,
                CustomerRegId = o.CustomerRegId,
                ItemOrder = o.ItemOrder,
                CheckEmployeeBMName = o.CheckEmployeeBM.Name,
                InspectEmployeeBMName = o.InspectEmployeeBM.Name,
                TotalEmployeeBMName = o.TotalEmployeeBM.Name,
                ItemGroupSum = o.CustomerItemGroupBM.ItemGroupSum,
                Symbol = o.Symbol,
                CrisisSate = o.CrisisSate,
                Id = o.Id,
                ProcessState = o.ProcessState,
                DepartmentId = o.DepartmentId,
                ItemSum = o.ItemSum,
                ItemDiagnosis = o.ItemDiagnosis,
                Stand = o.Stand,
                PrivacyState = o.ItemGroupBM.PrivacyState,
                ItemGroupDiagnosis = o.CustomerItemGroupBM.ItemGroupDiagnosis,
                IsZYB = o.CustomerItemGroupBM.IsZYB,
                CrisisLever = o.CrisisLever,
                 ReportBM=o.ReportBM
            }).ToList();

            return relist;
            // return  ObjectMapper.Map<List<CustomerRegInspectItemDto>>(data);
            // return data.ProjectTo<CustomerRegInspectItemDto>(GetConfigurationProvider<Core.Examination.TjlCustomerRegItem, CustomerRegInspectItemDto>()).ToList(); 
        }
        /// <summary>
        /// 总检组合小结
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<CustomerRegInspectGroupDto> GetCusGroupList(QueryClass query)
        {
            var data = _customerRegItemRepository.GetAll().AsNoTracking();

            if (query.CustomerRegId != Guid.Empty)
            {
                //于慧ChargeAppService中保存抹零项InsertMLGroup是根据一个固定的Guid来获取的，这里也这样获取，把抹零项目排除
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                data = data.Where(o => o.ItemId != guid);
                data = data.Where(r => r.CustomerRegId == query.CustomerRegId);

            }
            data = data.Where(o => o.DepartmentBM.Category != "耗材");
            data = data.Where(o => o.ItemGroupBM.ItemGroupName != null);
            data = data.Where(n => n.CustomerItemGroupBM.PayerCat != (int)PayerCatType.NoCharge
            && n.CustomerItemGroupBM.IsAddMinus != (int)AddMinusType.Minus);
            var result = data.Select(o => new CustomerRegInspectGroupDto
            {
                CustomerRegId = o.CustomerRegId,
                DepartmentName = o.DepartmentBM.Name,
                ItemGroupName = o.CustomerItemGroupBM.ItemGroupName,
                ItemGroupOrder = o.CustomerItemGroupBM.ItemGroupOrder,
                DepartmentOrderNum = o.DepartmentBM.OrderNum,
                ItemGroupSum = o.CustomerItemGroupBM.ItemGroupSum,
                ItemGroupDiagnosis = o.CustomerItemGroupBM.ItemGroupDiagnosis,
                IsZYB = o.CustomerItemGroupBM.IsZYB,
                ItemGroupBM_Id = o.ItemGroupBMId,                 
                  Id=o.CustomerItemGroupBM.Id

            });


            return result.ToList();
        }
        /// <summary>
        /// 获取复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> GetCusReViewDto(EntityDto<Guid> input)
        {
            var cusreviewlist = _TjlCusReview.GetAll().AsNoTracking().Where(o => o.CustomerRegId == input.Id);

            return cusreviewlist.MapTo<List<CusReViewDto>>();
        }
        /// <summary>
        /// 获取补检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusGiveUpShowDto> GetCusGiveDto(EntityDto<Guid> input)
        {
            var cusreviewlist = _TjlCusGiveUp.GetAll().AsNoTracking().Where(o => o.CustomerRegId == input.Id);
            var quelist = cusreviewlist.Select(p => new CusGiveUpShowDto
            {
                GroupName = p.CustomerItemGroup.ItemGroupName,
                remart = p.remart,
                stayDate = p.stayDate,
                stayType = p.stayType
            }).ToList();
            return quelist;
        }
        /// <summary>
        /// 报告获取复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<reportCusReViewDto> GetCusReView(EntityDto<Guid> input)
        {
            var cusreviewlist = _TjlCusReview.GetAll().AsNoTracking().Where(o => o.CustomerRegId == input.Id).ToList();
            List<reportCusReViewDto> relist = new List<reportCusReViewDto>();
            foreach (var re in cusreviewlist)
            {
                reportCusReViewDto reportCusReViewDto = new reportCusReViewDto();
                reportCusReViewDto.SumName = re.SummarizeAdvice?.AdviceName;
                reportCusReViewDto.ReviewDate = re.ReviewDate;
                reportCusReViewDto.Remart = re.Remart;
                reportCusReViewDto.ItemGroups = string.Join(",", re.ItemGroup.Select(o => o.ItemGroupName).ToList()).TrimEnd(',');
                relist.Add(reportCusReViewDto);
            }
            return relist.ToList();
        }
        /// <summary>
        /// 报告获取复查总检
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public reportCusReSumDto GetCusReSum(EntityDto<Guid> input)
        {
          //复查总检
            var reg = _TjlOccCustomerHazardSum.GetAll().Where(p=>p.CustomerRegBM.ReviewRegID== input.Id);

            //原总检
            //var regSum = _customerSummarizeRepository.GetAll().FirstOrDefault(p => p.CustomerRegID == input.Id);
            reportCusReSumDto creatOccCustomerSum = new reportCusReSumDto();
            if (reg.Count() > 0)
            {
                creatOccCustomerSum.Description = string.Join(",", reg.Select(p => p.Description).ToList());
                creatOccCustomerSum.Opinions = string.Join(",", reg.Select(p => p.Advise).ToList());

                creatOccCustomerSum.sumEmp = reg.FirstOrDefault().CustomerSummarize?.ShEmployeeBM?.Name;
                creatOccCustomerSum.sumTime = reg.FirstOrDefault().CustomerSummarize?.ConclusionDate;
               creatOccCustomerSum.ReviewDate = reg.FirstOrDefault().CustomerRegBM?.LoginDate;
            }
     
           // creatOccCustomerSum.ItemGroups = regSum.ReviewContent;
         
            return creatOccCustomerSum;
        }

        /// <summary>
        /// 根据阳性匹配复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> GetIllReViewDto(List<TjlCustomerSummarizeBMDto> input)
        {
            var advicelist = input.Select(o => o.SummarizeAdviceId).ToList();
            var reviwsets = _ReviewItemSet.GetAll().AsNoTracking().Where(n => advicelist.Contains(n.SummarizeAdviceId)).ToList();
            List<CusReViewDto> cusReViews = new List<CusReViewDto>();
            foreach (var reviwset in reviwsets)
            {
                CusReViewDto cusriew = new CusReViewDto();
                cusriew.CustomerRegId = input.First().CustomerRegID;
                cusriew.SummarizeAdviceId = reviwset.SummarizeAdviceId;
                cusriew.ItemGroup = reviwset.ItemGroupBM.MapTo<List<GroupCusGroupDto>>();
                cusriew.Remart = reviwset.Remark;
                //cusriew.ReviewDate = reviwset.Checkday;
                cusriew.ReviewDay = reviwset.Checkday.Value;
                cusriew.ReviewDate = DateTime.Now.AddDays(reviwset.Checkday.Value);
                cusReViews.Add(cusriew);
            }

            return cusReViews;
        }

        /// <summary>
        /// 根据阳性匹配复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> GetIllReViewNewDto(List<CustomerRegisterSummarizeSuggestDto> input)
        {
            var advicelist = input.Select(o => o.SummarizeAdviceId).ToList();
            var reviwsets = _ReviewItemSet.GetAll().AsNoTracking().Where(n => advicelist.Contains(n.SummarizeAdviceId)).ToList();
            List<CusReViewDto> cusReViews = new List<CusReViewDto>();
            foreach (var reviwset in reviwsets)
            {
                CusReViewDto cusriew = new CusReViewDto();
                cusriew.CustomerRegId = input.First().CustomerRegID;
                cusriew.SummarizeAdviceId = reviwset.SummarizeAdviceId;
                cusriew.ItemGroup = reviwset.ItemGroupBM.MapTo<List<GroupCusGroupDto>>();
                cusriew.Remart = reviwset.Remark;
                //cusriew.ReviewDate = reviwset.Checkday;
                cusriew.ReviewDay = reviwset.Checkday.Value;
                cusriew.ReviewDate = DateTime.Now.AddDays(reviwset.Checkday.Value);
                cusReViews.Add(cusriew);
            }

            return cusReViews;
        }
        /// <summary>
        /// 保存复查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CusReViewDto> SaveCusReViewDto(List<CusReViewDto> input)
        {
            input = input.Distinct().ToList();
            List<CusReViewDto> recusreViewlis = new List<CusReViewDto>();
            Guid cusregid = input.First().CustomerRegId.Value;
            var cusolds = _TjlCusReview.GetAll().Where(o => o.CustomerRegId == cusregid);

            foreach (var cusold in cusolds)
            {
                var cureview = input.Where(o => o.Id == cusold.Id).ToList();
                if (cureview.Count == 0)
                {
                    _TjlCusReview.Delete(cusold);
                }

            }
            CurrentUnitOfWork.SaveChanges();
            var currcusvie = new CusReViewDto();
            List<string> GroupName = new List<string>();

            foreach (var cusreview in input)
            {
                var gID = string.Join(",", cusreview.ItemGroup.Select(p => p.Id));
                if (!GroupName.Contains(gID))
                {
                    if (cusreview.Id != Guid.Empty)
                    {
                        var cusre = _TjlCusReview.Get(cusreview.Id);
                        cusreview.MapTo(cusre);
                        cusre.ItemGroup = new List<TbmItemGroup>();
                        foreach (var group in cusreview.ItemGroup)
                        {
                            var tbmgroup = _ItemGroup.Get(group.Id);
                            cusre.ItemGroup.Add(tbmgroup);
                        }
                        cusre.ReviewDate = System.DateTime.Now.AddDays(cusreview.ReviewDay);

                        var cusrvie = _TjlCusReview.Update(cusre);

                        GroupName.Add(gID);
                        var cusviedto = cusrvie.MapTo<CusReViewDto>();
                        currcusvie = cusviedto;
                        recusreViewlis.Add(cusviedto);
                    }
                    else
                    {
                        var cusre = cusreview.MapTo<TjlCusReview>();
                        cusre.Id = Guid.NewGuid();
                        cusre.ItemGroup = new List<TbmItemGroup>();
                        foreach (var group in cusreview.ItemGroup)
                        {
                            var tbmgroup = _ItemGroup.Get(group.Id);
                            cusre.ItemGroup.Add(tbmgroup);
                        }
                        cusre.ReviewDate = System.DateTime.Now.AddDays(cusreview.ReviewDay);
                        var cusrvie = _TjlCusReview.Insert(cusre);
                        GroupName.Add(gID);
                        var cusviedto = cusrvie.MapTo<CusReViewDto>();
                        recusreViewlis.Add(cusviedto);
                        currcusvie = cusviedto;
                        CurrentUnitOfWork.SaveChanges();

                    }
                }

            }
            var cusreg = _customerRegRepository.GetAll().FirstOrDefault(p => p.ReviewRegID == currcusvie.CustomerRegId);
            #region 个人预约
            var currReg = _customerRegRepository.Get(currcusvie.CustomerRegId.Value);
            QueryCustomerRegDto data = new QueryCustomerRegDto();
            if (cusreg != null && cusreg.RegisterState != (int)RegisterState.No)
            {
                return recusreViewlis;
            }
            else if (cusreg != null && cusreg.RegisterState == (int)RegisterState.No)
            {
                data = cusreg.MapTo<QueryCustomerRegDto>();
                data.BookingDate = currReg.LoginDate.Value.AddDays(currcusvie.ReviewDay);
                data.Remarks = "复查";
                data.CustomerItemGroup = null;
                data.CustomerItemGroup = new List<CusReg.Dto.TjlCustomerItemGroupDto>();
            }
            else
            {

                //检索是否有预约
                //本次体检
                data.ReviewRegID = currcusvie.CustomerRegId.Value;
                data.ReviewSate = 2;
                data.BarState = 1;
                data.BlindSate = 1;
                data.BookingDate = currReg.LoginDate.Value.AddDays(currcusvie.ReviewDay);
                data.Remarks = "复查";
                data.CheckSate = 1;
                data.ClientType = "";//体检类别
                data.CostState = 1;
                data.CustomerBM = _idNumberAppService.CreateArchivesNumBM();
                data.CustomerType = currReg.CustomerType;
                data.EmailReport = currReg.EmailReport;
                data.ExamPlace = currReg.ExamPlace;
                data.FamilyState = currReg.FamilyState;
                data.GuidanceSate = 1;
                data.HaveBreakfast = 1;
                data.InfoSource = currReg.InfoSource;

                data.IsFree = false;
                data.MailingReport = currReg.MailingReport;
                data.MarriageStatus = currReg.MarriageStatus;
                data.Message = 2;
                data.PhysicalType = currReg.PhysicalType;
                data.PrintSate = 1;
                data.ReceiveSate = 1;
                data.RegAge = currReg.RegAge;
                data.RegisterState = 1;
                data.ReplaceSate = 1;
                data.ReportBySelf = 1;
                data.RequestState = 1;
                data.SendToConfirm = 1;
                data.SummLocked = 2;
                data.SummSate = 1;
                data.UrgentState = 1;
                data.OrderNum = "";
                //data. 生成查询码
                data.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();

                //体检人基本信息
                data.Customer = currReg.Customer.MapTo<QueryCustomerDto>();
               
                data.CustomerItemGroup = new List<CusReg.Dto.TjlCustomerItemGroupDto>();
            }
            // 职业检信息
            data.RiskS = currReg.RiskS;
            data.PostState = currReg.PostState;
            data.TotalWorkAge = currReg.TotalWorkAge;
            data.WorkAgeUnit = currReg.WorkAgeUnit;
            data.TypeWork = currReg.TypeWork;
            data.InjuryAgeUnit = currReg.InjuryAgeUnit;
            data.InjuryAge = currReg.InjuryAge;
            data.WorkName = currReg.WorkName;
            data.ClientRegId = currReg.ClientRegId;
            data.ClientTeamInfoId = currReg.ClientTeamInfoId;
            data.ClientInfoId = currReg.ClientInfoId;
            data.ClientTeamInfo_Id = currReg.ClientTeamInfoId;
            //危害因素过滤结论是复查的危害因素
            if (!string.IsNullOrEmpty(currReg.RiskS))
            {
                var risksumlist = _TjlOccCustomerHazardSum.GetAll().Where(p => p.CustomerRegBMId == currReg.Id
                 && p.Conclusion != null && p.Conclusion.Contains("复查")).Select(p => p.OccHazardFactorsId).ToList();
                if (risksumlist.Count > 0)
                {
                    data.OccHazardFactors = currReg.OccHazardFactors.Where(p => risksumlist.Contains(p.Id)).MapTo<List<ShowOccHazardFactorDto>>();
                    data.RiskS = string.Join(",", data.OccHazardFactors.Select(p=>p.Text).ToList());
                }
            }
           //组合信息

           var oldcusGroup = currReg?.CustomerItemGroup;
            foreach (GroupCusGroupDto cusGroup in recusreViewlis.SelectMany(p => p.ItemGroup).ToList())
            {

                CusReg.Dto.TjlCustomerItemGroupDto tjlCustomerItemGroup = new CusReg.Dto.TjlCustomerItemGroupDto();
                tjlCustomerItemGroup.BarState = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.CheckState = (int)ProjectIState.Not;

                //传来的是bm  ItemGroupBM_Id
                TbmItemGroup tbmItemGroup = _ItemGroup.Get(cusGroup.Id);

                tjlCustomerItemGroup.DepartmentId = tbmItemGroup.DepartmentId;
                TbmDepartment tbmDepartment = tbmItemGroup.Department;
                tjlCustomerItemGroup.DepartmentName = tbmDepartment.Name;
                tjlCustomerItemGroup.DepartmentOrder = tbmDepartment.OrderNum;
                if (oldcusGroup != null)
                {
                    var oldGrou = oldcusGroup.FirstOrDefault(p => p.ItemGroupBM_Id == cusGroup.Id);
                    if (oldGrou != null && oldGrou.PayerCat == (int)PayerCatType.ClientCharge)
                    { tjlCustomerItemGroup.PayerCat = oldGrou.PayerCat; }
                    else
                    {
                        tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                    }
                    if (oldGrou != null)
                    {
                        tjlCustomerItemGroup.DiscountRate = oldGrou.DiscountRate;
                        tjlCustomerItemGroup.GRmoney = oldGrou.GRmoney;
                        tjlCustomerItemGroup.PriceAfterDis = oldGrou.PriceAfterDis;
                        tjlCustomerItemGroup.TTmoney = oldGrou.TTmoney;
                    }
                    else
                    {
                        tjlCustomerItemGroup.DiscountRate = 1;
                        tjlCustomerItemGroup.GRmoney = tbmItemGroup.Price.Value;
                        tjlCustomerItemGroup.PriceAfterDis = tbmItemGroup.Price.Value;
                        tjlCustomerItemGroup.TTmoney = 0;
                    }
                }
                else
                {
                    tjlCustomerItemGroup.DiscountRate = 1;
                    tjlCustomerItemGroup.GRmoney = tbmItemGroup.Price.Value;
                    tjlCustomerItemGroup.PayerCat = (int)PayerCatType.NoCharge;
                    tjlCustomerItemGroup.PriceAfterDis = tbmItemGroup.Price.Value;
                    tjlCustomerItemGroup.TTmoney = 0;
                }
                tjlCustomerItemGroup.DrawSate = 1;
                tjlCustomerItemGroup.BillingEmployeeBMId = AbpSession.UserId.Value;            
                tjlCustomerItemGroup.GuidanceSate = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目

                tjlCustomerItemGroup.ItemGroupBM_Id = tbmItemGroup.Id;
                tjlCustomerItemGroup.ItemGroupName = tbmItemGroup.ItemGroupName;
                tjlCustomerItemGroup.ItemGroupOrder = tbmItemGroup.OrderNum;
                tjlCustomerItemGroup.ItemGroupCodeBM = tbmItemGroup.ItemGroupBM;
                tjlCustomerItemGroup.ItemPrice = tbmItemGroup.Price.Value;

                tjlCustomerItemGroup.ItemSuitName = data.ItemSuitName;
            
                tjlCustomerItemGroup.RefundState = (int)PayerCatType.NotRefund;
                tjlCustomerItemGroup.RequestState = (int)PrintSate.NotToPrint;
                tjlCustomerItemGroup.SFType = Convert.ToInt32(tbmItemGroup.ChartCode);
                tjlCustomerItemGroup.SummBackSate = (int)SummSate.NotAlwaysCheck;
                tjlCustomerItemGroup.SuspendState = 1;
                //tjlCustomerItemGroup.TTmoney = 0;
                data.CustomerItemGroup.Add(tjlCustomerItemGroup);
            }

            QueryCustomerRegDto curCustomRegInfo = _customerAppService.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();

            #endregion
            return recusreViewlis;
        }
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelCusReViewDto(EntityDto<Guid> input)
        {
            List<CusReViewDto> recusreViewlis = new List<CusReViewDto>();
            Guid cusregid = input.Id;
            var cusolds = _TjlCusReview.GetAll().Where(o => o.CustomerRegId == cusregid);
            foreach (var cusold in cusolds)
            {
                foreach (var group in cusold.ItemGroup)
                {
                    var cusgroup = _ItemGroup.Get(group.Id);
                    cusgroup.ReviewItemSet = null;
                    _ItemGroup.Update(cusgroup);
                }
                _TjlCusReview.Delete(cusold);

            }
        }
        /// <summary>
        /// 根据单位预约id查询此预约下所有人员总检信息
        /// </summary>
        /// <returns>单位预约id</returns>
        public List<TjlCustomerSummarizeDto> GetCustomerSummary(EntityDto<Guid> Id)
        {
            var _TjlCustomerSummarize = _customerSummarizeRepository.GetAll();
            if (Id != null && Id.Id != Guid.Empty)
            {
                _TjlCustomerSummarize = _TjlCustomerSummarize.Where(n => n.CustomerReg.ClientRegId == Id.Id);
            }
            return _TjlCustomerSummarize.MapTo<List<TjlCustomerSummarizeDto>>();
        }
        /// <summary>
        /// 添加屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TbmSumHideDto AddSumHide(TbmSumHideDto input)
        {

            if (_TbmSumHide.GetAll().Any(a => a.SumWord == input.SumWord))
                throw new FieldVerifyException("该诊断词已存在无需重复维护！", "");
            var entity = input.MapTo<TbmSumHide>();
            entity.Id = Guid.NewGuid();
            entity.HelpChar = ChineseHelper.GetBriefCode(entity.SumWord);

            var result = _TbmSumHide.Insert(entity);
            return result.MapTo<TbmSumHideDto>();
        }
        /// <summary>
        /// 修改屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TbmSumHideDto EditSumHide(TbmSumHideDto input)
        {

            if (input.Id == null || input.Id == Guid.Empty)
                throw new FieldVerifyException("请选择需要修改的诊断词！", "");
            var entity = _TbmSumHide.Get(input.Id);
            input.MapTo(entity);
            entity.HelpChar = ChineseHelper.GetBriefCode(entity.SumWord);
            var result = _TbmSumHide.Update(entity);
            return result.MapTo<TbmSumHideDto>();
        }
        /// <summary>
        /// 删除屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void delSumHide(EntityDto<Guid> input)
        {

            var entity = _TbmSumHide.Get(input.Id);
            _TbmSumHide.Delete(entity);

        }
        /// <summary>
        /// 查询屏蔽诊断词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<TbmSumHideDto> SearchSumHide()
        {

            var entity = _TbmSumHide.GetAll();
            return entity.MapTo<List<TbmSumHideDto>>();
        }
        /// <summary>
        /// 根据体检人危害因素获取职业健康等
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccTargetDiseaseByRisk GetOccSumByCusReg(EntityDto<Guid> input)
        {
            var cusreginfo = _customerRegRepository.Get(input.Id);
            var risks = cusreginfo.OccHazardFactors.Select(o => o.Id).ToList();

            var Tarlist = _TbmOccTargetDisease.GetAll().Where(o => o.CheckType == cusreginfo.PostState && risks.Contains(o.OccHazardFactorsId.Value)).ToList();
            var outresult = new OccTargetDiseaseByRisk();
            outresult.OccDiseases = new List<OccCusDiseaseDto>();
            outresult.Contraindications = new List<OccCusContraindicationDto>();
            foreach (var Tar in Tarlist)
            {

                if (Tar != null)
                {

                    outresult.Opinions = outresult.Opinions + ";" + Tar.Opinions;

                    var OccDiseaseslist = Tar.OccDiseases?.Select(o => new OccCusDiseaseDto
                    {
                        OccHazardFactorsId = Tar.OccHazardFactorsId,
                        HelpChar = o.HelpChar,
                        OrderNum = o.OrderNum,
                        Remarks = o.Remarks,
                        Standards = o.TbmOccStandards?.FirstOrDefault(r => r.IsShow == 1)?.StandardName ?? "",
                        Text = o.Text,
                        ParentName = o.Parent?.Text,
                        Id = o.Id

                    }).ToList();

                    outresult.OccDiseases.AddRange(OccDiseaseslist);
                    var Contraindications = ObjectMapper.Map<List<OccCusContraindicationDto>>(Tar.Contraindications);
                    //计算禁忌症ID
                    if (Contraindications != null)
                    {
                        var occsis = _bmOccDictionary.GetAll().Where(p => p.Type == "Contraindication").ToList();
                        for (int i = 0; i < Contraindications.Count; i++)
                        {

                            var contrain = Contraindications[i];
                            contrain.OccHazardFactorsId = Tar.OccHazardFactorsId;
                            var occ = occsis.FirstOrDefault(p => p.Text == contrain.Text);
                            if (occ != null)
                            {
                                contrain.Id = occ.Id;
                                outresult.Contraindications.Add(contrain);
                            }
                        }
                    }
                  //  outresult.Contraindications.AddRange(Contraindications);// = ObjectMapper.Map<List<OccCusContraindicationDto>>(Tar.Contraindications);// Tar.Contraindications?.MapTo<List<OccCusContraindicationDto>>();
                }
            }
            return outresult;
        }
        /// <summary>
        /// 保存职业健康总检
        /// </summary>
        /// <param name="inputOccCusSumDto"></param>
        /// <returns></returns>
        public InputOccCusSumDto SaveOccSum(InputOccCusSumDto inputOccCusSumDto)
        {
            InputOccCusSumDto outresult = new InputOccCusSumDto();
            outresult.CraetOccCustomerOccDiseasesDto = new List<CraetOccCustomerOccDiseasesDto>();
            outresult.CreatOccCustomerContraindicationDto = new List<CreatOccCustomerContraindicationDto>();


            //危害因素总检
            //危害因素总检
            if (inputOccCusSumDto.OccCustomerHazardSumDto != null && inputOccCusSumDto.OccCustomerHazardSumDto.Count > 0)
            {

                var contrls = inputOccCusSumDto.OccCustomerHazardSumDto.Select(o => o.OccHazardFactorsId).ToList();
                _TjlOccCustomerHazardSum.GetAll().Where(o => o.CustomerRegBMId == inputOccCusSumDto.CreatOccCustomerSumDto.CustomerRegBMId && !contrls.Contains(o.OccHazardFactorsId)).Delete();

                foreach (var cont in inputOccCusSumDto.OccCustomerHazardSumDto)
                {

                    var cusdisOld = _TjlOccCustomerHazardSum.GetAll().FirstOrDefault(o => o.CustomerRegBMId == inputOccCusSumDto.CreatOccCustomerSumDto.CustomerRegBMId && o.OccHazardFactorsId == cont.OccHazardFactorsId);
                    //新增禁忌证
                    if (cusdisOld == null)
                    {

                        var newoccdis = cont.MapTo<TjlOccCustomerHazardSum>();
                        if (newoccdis.OccCustomerOccDiseases == null)
                        {
                            newoccdis.OccCustomerOccDiseases = new List<TbmOccDisease>();
                        }
                        if (newoccdis.OccDictionarys == null)
                        {
                            newoccdis.OccDictionarys = new List<TbmOccDictionary>();
                        }
                        newoccdis.Id = Guid.NewGuid();
                        newoccdis.CustomerRegBMId = inputOccCusSumDto.CreatOccCustomerSumDto.CustomerRegBMId;
                        newoccdis.CustomerSummarizeId = inputOccCusSumDto.CreatOccCustomerSumDto.CustomerSummarizeId;
                        //职业病
                        if (!string.IsNullOrEmpty(cont.OccCustomerOccDiseasesIds))
                        {
                            var list = cont.OccCustomerOccDiseasesIds.Split(',').ToList();
                            foreach (var li in list)
                            {
                                var gid = Guid.Parse(li);
                                var dise = _TbmOccDisease.Get(gid);
                                newoccdis.OccCustomerOccDiseases.Add(dise);
                            }
                        }
                        //禁忌症
                        if (!string.IsNullOrEmpty(cont.OccDictionarysIDs))
                        {
                            var list = cont.OccDictionarysIDs.Split(',').ToList();
                            foreach (var li in list)
                            {
                                var gid = Guid.Parse(li);
                                var dise = _bmOccDictionary.FirstOrDefault(gid);
                                if (dise != null)
                                {
                                    newoccdis.OccDictionarys.Add(dise);
                                }
                            }
                        }
                        newoccdis = _TjlOccCustomerHazardSum.Insert(newoccdis);
                        if (outresult.OccCustomerHazardSumDto == null)
                        {
                            outresult.OccCustomerHazardSumDto = new List<OccCustomerHazardSumDto>();
                        }
                        outresult.OccCustomerHazardSumDto.Add(newoccdis.MapTo<OccCustomerHazardSumDto>());
                    }
                    //修改禁忌证
                    else
                    {
                        cont.Id = cusdisOld.Id;
                        cont.CustomerRegBMId = cusdisOld.CustomerRegBMId;
                        cont.MapTo(cusdisOld);
                        cusdisOld.CustomerSummarizeId = inputOccCusSumDto.CreatOccCustomerSumDto.CustomerSummarizeId;
                        cusdisOld.OccCustomerOccDiseases = new List<TbmOccDisease>();
                        cusdisOld.OccDictionarys = new List<TbmOccDictionary>();
                        //职业病
                        if (!string.IsNullOrEmpty(cont.OccCustomerOccDiseasesIds))
                        {
                            var list = cont.OccCustomerOccDiseasesIds.Split(',').ToList();
                            foreach (var li in list)
                            {
                                var gid = Guid.Parse(li);
                                var dise = _TbmOccDisease.Get(gid);
                                cusdisOld.OccCustomerOccDiseases.Add(dise);
                            }
                        }
                        //禁忌症
                        if (!string.IsNullOrEmpty(cont.OccDictionarysIDs))
                        {
                            var list = cont.OccDictionarysIDs.Split(',').ToList();
                            foreach (var li in list)
                            {
                                var gid = Guid.Parse(li);
                                var dise = _bmOccDictionary.FirstOrDefault(gid);
                                if (dise != null)
                                {
                                    cusdisOld.OccDictionarys.Add(dise);
                                }
                            }
                        }
                        cusdisOld = _TjlOccCustomerHazardSum.Update(cusdisOld);
                        if (outresult.OccCustomerHazardSumDto == null)
                        {
                            outresult.OccCustomerHazardSumDto = new List<OccCustomerHazardSumDto>();
                        }
                        outresult.OccCustomerHazardSumDto.Add(cusdisOld.MapTo<OccCustomerHazardSumDto>());
                    }

                }
            }
            else
            {
                _TjlOccCustomerHazardSum.GetAll().Where(o => o.CustomerRegBMId == inputOccCusSumDto.CreatOccCustomerSumDto.CustomerRegBMId).Delete();

            }

            // CurrentUnitOfWork.SaveChanges();
            return outresult;
        }
        /// <summary>
        /// 删除职业健康总检
        /// </summary>
        public void DelOccSum(TjlCustomerQuery query)
        {
            //总检
            
            var occsumold = _TjlOccCustomerHazardSum.GetAll().Where(o => o.CustomerRegBMId == query.CustomerRegID).Delete();
            //if (occsumold != null)
            //{
            //    //_TjlOccCustomerOccDiseases.GetAll().Where(o => o.CustomerRegBMId == query.CustomerRegID).Delete();
            //    //_TjlOccCustomerContraindication.GetAll().Where(o => o.CustomerRegBMId == query.CustomerRegID).Delete();
            //    _TjlOccCustomerHazardSum.Delete(occsumold);
            //}
        }

        /// <summary>
        /// 根据体检人获取职业健康总检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InputOccCusSumDto GetCusOccSumByRegId(EntityDto<Guid> input)
        {
            InputOccCusSumDto inputOccCusSumDto = new InputOccCusSumDto();
            //var que = _TjlOccCustomerSum.GetAll().FirstOrDefault(o => o.CustomerRegBMId == input.Id);
            //if (que != null)
            //{
            //    inputOccCusSumDto.CreatOccCustomerSumDto = que.MapTo<CreatOccCustomerSumDto>();
            //    if (que.OccCustomerOccDiseases != null)
            //    {
            //        inputOccCusSumDto.CraetOccCustomerOccDiseasesDto = que.OccCustomerOccDiseases.MapTo<List<CraetOccCustomerOccDiseasesDto>>();
            //    }
            //    if (que.OccCustomerContraindications != null)
            //    {
            //        inputOccCusSumDto.CreatOccCustomerContraindicationDto = que.OccCustomerContraindications.MapTo<List<CreatOccCustomerContraindicationDto>>();
            //    }
            //    if (que.OccCustomerHazardSum != null)
            //    {
            //        inputOccCusSumDto.OccCustomerHazardSumDto = que.OccCustomerHazardSum.MapTo<List<OccCustomerHazardSumDto>>();
            //    }             

            //}
            var que = _TjlOccCustomerHazardSum.GetAll().Where(p => p.CustomerRegBMId == input.Id);
            inputOccCusSumDto.OccCustomerHazardSumDto = que.MapTo<List<OccCustomerHazardSumDto>>();
            if (inputOccCusSumDto.OccCustomerHazardSumDto == null || inputOccCusSumDto.OccCustomerHazardSumDto?.Count == 0)
            {

                inputOccCusSumDto.OccCustomerHazardSumDto = new List<OccCustomerHazardSumDto>();
                var cusreg = _customerRegRepository.Get(input.Id);
                foreach (var risk in cusreg.OccHazardFactors)
                {
                    OccCustomerHazardSumDto occCustomerHazardSumDto = new OccCustomerHazardSumDto();
                    occCustomerHazardSumDto.CustomerRegBMId = cusreg.Id;
                    occCustomerHazardSumDto.OccHazardFactorsId = risk.Id;
                    inputOccCusSumDto.OccCustomerHazardSumDto.Add(occCustomerHazardSumDto);
                }
            }
            return inputOccCusSumDto;
        }
        /// <summary>
        /// 获取总检列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutCusListDto> GetOutCus(InSearchCusDto input)
        {
            var que = _customerRegRepository.GetAll().Where(o => o.RegisterState != (int)RegisterState.No);
            if (input.LoginStar.HasValue && input.DateType.HasValue)
            {
                if (input.DateType == 1)
                { que = que.Where(o => o.BookingDate >= input.LoginStar.Value); }
                else
                {
                    que = que.Where(o => o.LoginDate >= input.LoginStar.Value);
                }
            }
            if (input.LoginEnd.HasValue && input.DateType.HasValue)
            {
                if (input.DateType == 1)
                {
                    que = que.Where(o => o.BookingDate < input.LoginEnd.Value);
                }
                else if (input.DateType == 2)
                {
                    var regID = _customerSummarizeRepository.GetAll().Where(o => o.ConclusionDate >= input.LoginStar &&
                 o.ConclusionDate < input.LoginEnd).Select(o => o.CustomerRegID).ToList();
                    que = que.Where(o => regID.Contains(o.Id));
                }
                else
                {
                    que = que.Where(o => o.LoginDate < input.LoginEnd.Value);
                }
            }
            if (!string.IsNullOrEmpty(input.Code))
            {
                que = que.Where(o => o.CustomerBM== input.Code);
            }
            if (!string.IsNullOrEmpty(input.CusNameBM))
            {
                que = que.Where(o => o.CustomerBM.Contains(input.CusNameBM) || o.Customer.Name.Contains(input.CusNameBM) ||
                o.Customer.IDCardNo.Contains(input.CusNameBM)
                || o.FPNo.Contains(input.CusNameBM) || o.Customer.NameAB.ToUpper().Contains(input.CusNameBM.ToUpper()));
            }
            if (input.CheckType.HasValue)
            {
                que = que.Where(o => o.PhysicalType == input.CheckType.Value);
            }
            if (input.Sate.HasValue)
            {
                que = que.Where(o => o.CheckSate == input.Sate.Value);
            }
            if (input.SumSate.HasValue)
            {
                que = que.Where(o => o.SummSate == input.SumSate.Value);
            }
            if (input.SumStar.HasValue && input.SumEnd.HasValue)
            {
                var regID = _customerSummarizeRepository.GetAll().Where(o => o.ConclusionDate >= input.SumStar &&
                   o.ConclusionDate < input.SumEnd).Select(o => o.CustomerRegID).ToList();
                que = que.Where(o => regID.Contains(o.Id));

            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(o => o.ClientRegId == input.ClientRegId.Value);
            }
            if (input.arrEmployeeName_Id != null && input.arrEmployeeName_Id.Length > 0)
            {
                var DocId = input.arrEmployeeName_Id[0];
                if (input.EmployeeNameType == 1)
                {
                    que = que.Where(m => m.FSEmployeeId == DocId);
                }
                else
                {
                    que = que.Where(m => m.CSEmployeeId == DocId);
                }
            }
            var userBM = _User.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                que = que.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }
            var cuslist = que.Select(o => new OutCusListDto
            {
                Age = o.Customer.Age,
                Id = o.Id,
                CheckSate = o.CheckSate,
                CustomerBM = o.CustomerBM,
                Name = o.Customer.Name,
                Sex = o.Customer.Sex,
                SummSate = o.SummSate,
                SummLocked = o.SummLocked,
                IDCardNo = o.Customer.IDCardNo,
                CSEmployeeId = o.CSEmployeeId,
                FSEmployeeId = o.FSEmployeeId,
                ClientName = o.ClientInfo.ClientName,
                LoginDate = o.LoginDate,
                PhysicalType = o.PhysicalType,
                CustomerId = o.CustomerId,
                FPNo = o.FPNo,
                PrintSate = o.PrintSate,
                InfoSource = o.InfoSource,
                forQuestionState = o.QuestionState==1?"已问诊":"未问诊",
                  QuestionTime=o.QuestionTime,
                   SummLockEmployeeBMId=o.SummLockEmployeeBMId,
                ISIll = o.CustomerRegItems.Where(p => p.Symbol == "H"
                 || p.Symbol == "L" || p.Symbol == "LL" || p.Symbol == "HH" || p.Symbol == "P").Count() > 0 ? "异常" : "正常",






            }).OrderBy(o => o.LoginDate).ToList();
            if (!string.IsNullOrEmpty(input.Qualified))
            {
                var oldIds = cuslist.Select(p => p.Id).ToList();
                var cusIds = _customerSummarizeRepository.GetAll().Where(o => oldIds.Contains(o.CustomerRegID) && o.Qualified == input.Qualified).Select(o => o.CustomerRegID).ToList();
                cuslist = cuslist.Where(p => cusIds.Contains(p.Id)).ToList();
            }
            return cuslist;

        }
        /// <summary>
        /// 获取已总检人员合格不合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<JKZCusDto> GetJKZOutCus(InSearchCusDto input)
        {

            var que = _customerSummarizeRepository.GetAll();
            if (input.LoginStar.HasValue)
            {
                que = que.Where(o => o.CustomerReg.LoginDate >= input.LoginStar.Value);
            }
            if (input.LoginEnd.HasValue)
            {
                que = que.Where(o => o.CustomerReg.LoginDate < input.LoginEnd.Value);
            }
            if (!string.IsNullOrEmpty(input.CusNameBM))
            {
                que = que.Where(o => o.CustomerReg.CustomerBM == input.CusNameBM || o.CustomerReg.Customer.Name == input.CusNameBM
                || o.CustomerReg.Customer.IDCardNo == input.CusNameBM);
            }
            if (input.CheckType.HasValue)
            {
                que = que.Where(o => o.CustomerReg.PhysicalType == input.CheckType.Value);
            }
            if (input.Sate.HasValue)
            {
                que = que.Where(o => o.CustomerReg.CheckSate == input.Sate.Value);
            }
            if (input.SumSate.HasValue)
            {
                que = que.Where(o => o.CustomerReg.SummSate == input.SumSate.Value);
            }
            if (!string.IsNullOrEmpty(input.Qualified))
            {
                que = que.Where(o => o.Qualified == input.Qualified);
            }
            var cuslist = que.Select(o => new JKZCusDto
            {
                Age = o.CustomerReg.Customer.Age,
                Id = o.CustomerReg.Id,
                CheckSate = o.CustomerReg.CheckSate,
                CustomerBM = o.CustomerReg.CustomerBM,
                Name = o.CustomerReg.Customer.Name,
                Sex = o.CustomerReg.Customer.Sex,
                SummSate = o.CustomerReg.SummSate,
                ClientName = o.CustomerReg.ClientInfo.ClientName,
                LoginDate = o.CustomerReg.LoginDate,
                PhysicalType = o.CustomerReg.PhysicalType,
                Qualified = o.Qualified,
                Mobile = o.CustomerReg.Customer.Mobile,
                SumDate = o.CheckDate,
                Opinion = o.Opinion

            }).OrderBy(o => o.LoginDate).ToList();

            return cuslist;

        }

        /// <summary>
        /// 获取已总检人员合格不合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<JKZCusDto> GetJKZOutCuslist(InSearchCusDto input)
        {

            var que = _customerSummarizeRepository.GetAll().Where(o => o.Qualified == "合格" || o.Qualified == "不合格");
            if (input.LoginStar.HasValue)
            {
                que = que.Where(o => o.CustomerReg.LoginDate >= input.LoginStar.Value);
            }
            if (input.LoginEnd.HasValue)
            {
                que = que.Where(o => o.CustomerReg.LoginDate < input.LoginEnd.Value);
            }
            if (!string.IsNullOrEmpty(input.CusNameBM))
            {
                que = que.Where(o => o.CustomerReg.CustomerBM == input.CusNameBM || o.CustomerReg.Customer.Name == input.CusNameBM
                || o.CustomerReg.Customer.IDCardNo == input.CusNameBM);
            }
            if (input.CheckType.HasValue)
            {
                que = que.Where(o => o.CustomerReg.PhysicalType == input.CheckType.Value);
            }
            if (input.Sate.HasValue)
            {
                que = que.Where(o => o.CustomerReg.CheckSate == input.Sate.Value);
            }
            if (input.SumSate.HasValue)
            {
                que = que.Where(o => o.CustomerReg.SummSate == input.SumSate.Value);
            }
            if (!string.IsNullOrEmpty(input.Qualified))
            {
                que = que.Where(o => o.Qualified == input.Qualified);
            }
            var cuslist = que.Select(o => new JKZCusDto
            {
                Age = o.CustomerReg.Customer.Age,
                Id = o.CustomerReg.Id,
                CheckSate = o.CustomerReg.CheckSate,
                CustomerBM = o.CustomerReg.CustomerBM,
                Name = o.CustomerReg.Customer.Name,
                Sex = o.CustomerReg.Customer.Sex,
                SummSate = o.CustomerReg.SummSate,
                ClientName = o.CustomerReg.ClientInfo.ClientName,
                LoginDate = o.CustomerReg.LoginDate,
                PhysicalType = o.CustomerReg.PhysicalType,
                Qualified = o.Qualified,
                Mobile = o.CustomerReg.Customer.Mobile,
                SumDate = o.ConclusionDate,
                Opinion = o.Opinion

            }).OrderBy(o => o.LoginDate).ToList();

            return cuslist;

        }
        /// <summary>
        /// 判断健康证是否合格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ISHG(EntityDto<Guid> input)
        {
            var cusSum = _customerSummarizeRepository.GetAll().FirstOrDefault(p => p.CustomerRegID == input.Id);
            if (cusSum != null && cusSum.Qualified == "合格")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取体检人组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CustomerGroupSumDto> GetCustomerRegGroupSum(ChargeBM input)
        {
            var query = _customerItemGroupRepository.GetAll().AsNoTracking();
            query = query.Where(o => o.IsAddMinus != (int)AddMinusType.Minus);
            query = query.Where(o => o.DepartmentBM.Category != "耗材");
            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            query = query.Where(o => o.ItemGroupBM_Id != guid);
            if (!string.IsNullOrEmpty(input.Name))
            {
                query = query.Where(r => r.CustomerRegBM.CustomerBM == input.Name);
            }
            if (input.Id != null && input.Id != Guid.Empty)
            {
                query = query.Where(r => r.CustomerRegBM.Id == input.Id);
            }
            query = query.OrderBy(r => r.DepartmentBM.OrderNum).ThenBy(r => r.ItemGroupBM.OrderNum);
            //去掉科室是其他和抹零项目


            return ObjectMapper.Map<List<CustomerGroupSumDto>>(query);
        }

        /// <summary>
        /// 获取体检人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public BeforSumCustomerRegDto GetCustomerReg(ChargeBM input)
        {

            var query = _customerRegRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrEmpty(input.Name))
            {
                query = query.Where(r => r.CustomerBM == input.Name);
            }
            if (input.Id != null && input.Id != Guid.Empty)
            {
                query = query.Where(r => r.Id == input.Id);
            }
            var cus = query.FirstOrDefault();
            //去掉科室是其他和抹零项目
            return ObjectMapper.Map<BeforSumCustomerRegDto>(cus);
        }
        /// <summary>
        /// 保存初审
        /// </summary>
        /// <param name="input"></param>
        public void SavePerSum(BeforSaveSumDto input)
        {
            var cusReg = _customerRegRepository.Get(input.Id);
            cusReg.SummSate = input.SummSate;
            if (input.SummSate == (int)SummSate.HasBeenEvaluated)
            {
                cusReg.PreEmployeeId = AbpSession.UserId.Value;
                cusReg.PreSumDate = System.DateTime.Now;
            }
            else
            {
                cusReg.PreEmployeeId = null;
                cusReg.PreSumDate = null;
            }           
            if (input.cusGroup != null && input.cusGroup.Count > 0)
            {
                foreach (var cusgroup in input.cusGroup)
                {

                    var cusREggroup = _customerItemGroupRepository.Get(cusgroup.Id);

                    if (cusgroup.CheckState == (int)ProjectIState.GiveUp && (cusREggroup.CheckState == 2 ||
                    cusREggroup.CheckState == 3))
                    {
                        throw new FieldVerifyException("项目已检查不能放弃！", "");
                    }
                    if (cusREggroup.CheckState == (int)ProjectIState.Complete
                  || cusREggroup.CheckState == (int)ProjectIState.Part)
                    {
                        continue;
                    }
                    if (cusgroup.CheckState != (int)ProjectIState.GiveUp &&
                       cusgroup.CheckState != (int)ProjectIState.Not
                       && cusgroup.CheckState != null)
                    {
                        continue;
                    }
                    //if (cusREggroup.CheckState == (int)ProjectIState.GiveUp && cusgroup.CheckState == 4)
                    //{
                    //    //throw new FieldVerifyException("项目已放弃不能重复放弃！", "");
                    //    continue;
                    //}
                    cusREggroup.SumRemark = cusgroup.SumRemark;
                    if (cusgroup.CheckState == (int)ProjectIState.GiveUp)
                    {
                        cusREggroup.CheckState = (int)ProjectIState.GiveUp;
                    }
                    else
                    {
                        cusREggroup.CheckState = 1;
                    }
                    var cusItems = _customerRegItemRepository.GetAll().Where(p => p.CustomerItemGroupBMid == cusgroup.Id).ToList();
                    foreach (var cusItem in cusItems)
                    {
                        cusItem.ProcessState = cusREggroup.CheckState;
                        _customerRegItemRepository.Update(cusItem);
                    }
                    _customerItemGroupRepository.Update(cusREggroup);

                }
            }
            _customerRegRepository.Update(cusReg);
        }

        /// <summary>
        /// 匹配冲突
        /// </summary>
        public OutMesDto MatchSumConflict(SumAdviseDto input)
        {
            OutMesDto outMesDto = new OutMesDto();
            var Sumconfctlist = _SumConflictRepository.GetAll().ToList();
            string outmess = "";
            foreach (var sumcon in Sumconfctlist)
            {
                if (input.CharacterSummary == null)
                {
                    continue;
                }
                //汇总匹配
                if (input.CharacterSummary.Contains(sumcon.SumWord))
                {
                    if (sumcon.IsSex == 1 && input.Sex == sumcon.Sex)
                    {
                        string sex = "男";
                        if (input.Sex == 1)
                        {
                            sex = "女";

                        }
                        outmess += "体检汇总：" + sumcon.SumWord + "，体检人性别：" + sex + "，性别冲突\r\n";
                    }
                    if (sumcon.IsAge == 1 && input.Age >= sumcon.MinAge && input.Age <= sumcon.MaxAge)
                    {

                        outmess += "体检汇总：" + sumcon.SumWord + "，体检人年龄：" + input.Age + "，年龄段冲突\r\n";
                    }
                }
                if (input.Advice != null && input.Advice.Count > 0)
                {
                    var adnum = input.Advice.Where(p => p.SummarizeName.Contains(sumcon.SumWord)).Count();
                    if (adnum > 0)
                    {
                        if (sumcon.IsSex == 1 && input.Sex == sumcon.Sex)
                        {
                            string sex = "男";
                            if (input.Sex == 1)
                            {
                                sex = "女";

                            }
                            outmess += "建议名称：" + sumcon.SumWord + "，体检人性别：" + sex + "，性别冲突\r\n";
                        }
                        if (sumcon.IsAge == 1 && input.Age >= sumcon.MinAge && input.Age <= sumcon.MaxAge)
                        {

                            outmess += "建议名称：" + sumcon.SumWord + "，体检人年龄：" + input.Age + "，年龄段冲突\r\n";
                        }
                    }

                    var adVice = input.Advice.Where(p => p.Advice.Contains(sumcon.SumWord)).Count();
                    if (adVice > 0)
                    {
                        if (sumcon.IsSex == 1 && input.Sex == sumcon.Sex)
                        {
                            string sex = "男";
                            if (input.Sex == 1)
                            {
                                sex = "女";

                            }
                            outmess += "建议内容：" + sumcon.SumWord + "，体检人性别：" + sex + "，性别冲突\r\n";
                        }
                        if (sumcon.IsAge == 1 && input.Age >= sumcon.MinAge && input.Age <= sumcon.MaxAge)
                        {

                            outmess += "建议内容：" + sumcon.SumWord + "，体检人年龄：" + input.Age + "，年龄段冲突\r\n";
                        }
                    }
                }

            }
            outMesDto.OutMess = outmess;
            return outMesDto;
        }

        /// <summary>
        /// 获取体检组合
        /// </summary>
        /// <returns></returns>
        public List<CusGroupShowDto> getCusGroup(EntityDto<Guid> regID)
        {
            var que = _customerItemGroupRepository.GetAll().Where(p=>p.CustomerRegBMId== regID.Id).Where(p=>p.IsAddMinus!=3);
            var outgroup = que.Select(p => new CusGroupShowDto
            {
                CheckEmployeeName = p.CheckEmployeeBM == null ? "" : p.CheckEmployeeBM.Name,
                CheckState = p.CheckState,
                FirstDateTime = p.FirstDateTime,
                Id = p.Id,
                InspectEmployeeName = p.InspectEmployeeBM == null ? "" : p.InspectEmployeeBM.Name,
                ItemGroupBM_Id = p.ItemGroupBM_Id,
                ItemGroupCodeBM = p.ItemGroupCodeBM,
                ItemGroupName = p.ItemGroupName,
                DepartOrder = p.DepartmentBM == null ? 0 : p.DepartmentBM.OrderNum,
                GroupOrder = p.ItemGroupBM == null ? 0 : p.ItemGroupBM.OrderNum
            }).OrderBy(p=>p.DepartOrder).ThenBy(p=>p.GroupOrder).ToList();
            return outgroup;

        }
    }
}