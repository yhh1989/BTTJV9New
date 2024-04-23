using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Abp.Authorization;
using AutoMapper;
using AutoMapper.Configuration;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Core;
using System.Data.SqlClient;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice
{
    [AbpAuthorize]
    public class SummarizeAdviceAppService : MyProjectAppServiceBase, ISummarizeAdviceAppService
    {
        private readonly IRepository<TbmSummarizeAdvice, Guid> _summarizeAdviceRepository;
        private readonly IRepository<TbmSumConflict, Guid> _SumConflictRepository;
        private readonly IRepository<TbmSummHB, Guid> _TbmSummHBRepository;
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private readonly ISqlExecutor _sqlExecutor;
        public SummarizeAdviceAppService(IRepository<TbmSummarizeAdvice, Guid> summarizeAdviceRepository, ISqlExecutor sqlExecutor,
            IRepository<TbmSumConflict, Guid> SumConflictRepository,
            IRepository<TbmSummHB, Guid> TbmSummHBRepository)
        {
            _summarizeAdviceRepository = summarizeAdviceRepository;
            _sqlExecutor = sqlExecutor;
            _SumConflictRepository = SumConflictRepository;
            _TbmSummHBRepository = TbmSummHBRepository;
        }

        public FullSummarizeAdviceDto Add(SummarizeAdviceInput input)
        {
            if (string.IsNullOrEmpty(input.SummarizeAdvice.Uid))
            {
                throw new FieldVerifyException("编码不可为空！", "编码不可为空！");
            }
            if (string.IsNullOrEmpty(input.SummarizeAdvice.AdviceName))
            {
                throw new FieldVerifyException("建议名称不可为空！", "建议名称不可为空！");
            }
            //if (_summarizeAdviceRepository.GetAll().Any(r => r.Uid == input.SummarizeAdvice.Uid))
            //{
            //    throw new FieldVerifyException("编码重复！", "编码重复！");
            //}
            //if (_summarizeAdviceRepository.GetAll().Any(r => r.AdviceName == input.SummarizeAdvice.AdviceName))
            //{
            //    throw new FieldVerifyException("建议名称重复！", "建议名称重复！");
            //}
            if (input.SummarizeAdvice.Id == Guid.Empty) input.SummarizeAdvice.Id = Guid.NewGuid();
            var entity = input.SummarizeAdvice.MapTo<TbmSummarizeAdvice>();
            entity = _summarizeAdviceRepository.Insert(entity);
            var dto = entity.MapTo<FullSummarizeAdviceDto>();
            return dto;
        }

        public void Del(EntityDto<Guid> input)
        {
            _summarizeAdviceRepository.Delete(input.Id);
        }

        public FullSummarizeAdviceDto Edit(SummarizeAdviceInput input)
        {
            if (_summarizeAdviceRepository.GetAll().Any(r => r.Uid == input.SummarizeAdvice.Uid && r.Id != input.SummarizeAdvice.Id))
            {
                throw new FieldVerifyException("编码重复！", "编码重复！");
            }
            if (_summarizeAdviceRepository.GetAll().Any(r => r.AdviceName == input.SummarizeAdvice.AdviceName && r.Id != input.SummarizeAdvice.Id))
            {
                throw new FieldVerifyException("建议名称重复！", "建议名称重复！");
            }
            var entity = _summarizeAdviceRepository.Get(input.SummarizeAdvice.Id);
            input.SummarizeAdvice.MapTo(entity); // 赋值
            entity = _summarizeAdviceRepository.Update(entity);
            var dto = entity.MapTo<FullSummarizeAdviceDto>();
            return dto;
        }

        public FullSummarizeAdviceDto Get(SearchSummarizeAdvice input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<FullSummarizeAdviceDto>();
            return dto;
        }
        public List<SimpleSummarizeAdviceDto> QuerySimples(SearchSummarizeAdvice input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<SimpleSummarizeAdviceDto>>();
        }

        public List<SummarizeAdviceDto> QueryNatives(SearchSummarizeAdvice input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<SummarizeAdviceDto>>();
        }

        public List<FullSummarizeAdviceDto> QueryFulls(SearchSummarizeAdvice input)
        {
            var query = BuildQuery(input);
            return query.MapTo<List<FullSummarizeAdviceDto>>();
        }

        public PageResultDto<SummarizeAdviceDto> PageNatives(PageInputDto<SearchSummarizeAdvice> input)
        {
            return Common.PageHelper.Paging<SearchSummarizeAdvice, TbmSummarizeAdvice, SummarizeAdviceDto>(input, BuildQuery);
        }

        public PageResultDto<FullSummarizeAdviceDto> PageFulls(PageInputDto<SearchSummarizeAdvice> input)
        {
            return Common.PageHelper.Paging<SearchSummarizeAdvice, TbmSummarizeAdvice, FullSummarizeAdviceDto>(input, BuildQuery);
        }

        private IQueryable<TbmSummarizeAdvice> BuildQuery(SearchSummarizeAdvice input)
        {
            var query = _summarizeAdviceRepository.GetAll();
            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (input.DepartmentId != Guid.Empty)
                        query = query.Where(m => m.DepartmentId == input.DepartmentId);
                    if (!string.IsNullOrEmpty(input.QueryText))
                        query = query.Where(m => m.Uid.Contains(input.QueryText)
                                || m.AdviceName.Contains(input.QueryText)
                                || m.HelpChar.Contains(input.QueryText));
                    if (!string.IsNullOrEmpty(input.Advicevalue))
                        query = query.Where(m => m.Advicevalue.Contains(input.Advicevalue));
                    if (!string.IsNullOrEmpty(input.DiagnosisExpain))
                        query = query.Where(m => m.DiagnosisExpain.Contains(input.DiagnosisExpain));
                    if (input.DiagnosisSate != null)
                        query = query.Where(m => m.DiagnosisSate == input.DiagnosisSate);
                    if (input.CrisisSate != null)
                        query = query.Where(m => m.CrisisSate == input.CrisisSate);
                    if (input.SexState != null)
                        query = query.Where(m => m.SexState == input.SexState);
                    if (input.MarrySate != null)
                        query = query.Where(m => m.MarrySate == input.MarrySate);
                    if (input.MinAge != null)
                        query = query.Where(m => m.MinAge == input.MinAge);
                    if (input.MaxAge != null)
                        query = query.Where(m => m.MaxAge == input.MaxAge);
                    if (input.DiagnosisAType != null)
                        query = query.Where(m => m.DiagnosisAType == input.DiagnosisAType);
                    if (input.HideOnGroupReport != null)
                        query = query.Where(m => m.HideOnGroupReport == input.HideOnGroupReport);
                }
            }
            query = query.OrderBy(m => m.Department.OrderNum).ThenBy(m => m.Department.Name)
                .OrderBy(m => m.OrderNum).ThenByDescending(m => m.CreationTime);
            return query;
        }

        /// <summary>
        /// 建议字典数据，缓存使用
        /// </summary>
        /// <returns></returns>
        public List<SummarizeAdviceDto> GetSummAll(InputSearchSumm Input)
        {
            var data = _summarizeAdviceRepository.GetAll().AsNoTracking();
            if (string.IsNullOrWhiteSpace(Input.Name))
            {
                data = _summarizeAdviceRepository.GetAll().AsNoTracking().Where(n =>
                   n.MaxAge > Input.Age &&
                   n.MinAge < Input.Age).Where(n => n.SexState == Input.SexState || n.SexState == 9);
                //data = data.Where(n => Input.DepartmentId.Contains(n.DepartmentId));
            }
            else
            {
                //根据建议名称检索
                data = data.Where(n => n.AdviceName.Contains(Input.Name));
            }

            var result = data.Select(r => new { r.Id, r.AdviceName, r.OrderNum, r.SummAdvice });
            var result1 = result.Select(r => new SummarizeAdviceDto
            { Id = r.Id, AdviceName = r.AdviceName, OrderNum = r.OrderNum, SummAdvice = r.SummAdvice }).ToList();

            return result1;
        }

        /// <summary>
        /// 根据Id集合查询数据
        /// </summary>
        /// <param name="GuidList"></param>
        /// <returns></returns>
        public List<SummarizeAdviceDto> GetSummForGuidList(List<Guid> GuidList)
        {
            List<TbmSummarizeAdvice> list = new List<TbmSummarizeAdvice>();
            foreach (var Id in GuidList)
            {
                var info = _summarizeAdviceRepository.Get(Id);
                list.Add(info);
            }
            return list.MapTo<List<SummarizeAdviceDto>>();
        }

        public List<SearchSummarizeAdviceDto> QueryAll()
        {
            string strsql = "select Name ,AdviceName,cast( DiagnosisAType as  nvarchar(200))  DiagnosisAType from TbmSummarizeAdvices " +
         " inner join TbmDepartments on TbmDepartments.id=TbmSummarizeAdvices.DepartmentId " +
        "order by TbmDepartments.OrderNum ";
            SqlParameter[] parameters = {
                        new SqlParameter("@id",new Guid())

                        };

            List<SearchSummarizeAdviceDto> lstregNumberDatas = _sqlExecutor.SqlQuery<SearchSummarizeAdviceDto>
                 (strsql, parameters).ToList();
            return lstregNumberDatas;
        }
        /// <summary>
        /// 建议字典数据，缓存使用
        /// </summary>
        /// <returns></returns>
        public async Task<List<SummarizeAdviceDto>> GetAllSummAll()
        {

            return await _summarizeAdviceRepository.GetAll().Where(p=>p.Department!=null).AsNoTracking().ProjectToListAsync<SummarizeAdviceDto>(GetConfigurationProvider<Core.Coding.TbmSummarizeAdvice, SummarizeAdviceDto>());

            //var data = _summarizeAdviceRepository.GetAll().AsNoTracking().MapTo<List<SummarizeAdviceDto>>();

            // return data;
        }
        /// <summary>
        /// 冲突关键词
        /// </summary>
        public SumConflictDto SaveSumConflict(SumConflictDto input)
        {
            if (input.Id != null && input.Id != Guid.Empty)
            {
                var sumConflit = _SumConflictRepository.Get(input.Id);
                //sumConflit = ObjectMapper.Map<TbmSumConflict>(input);
                input.MapTo(sumConflit);
                sumConflit = _SumConflictRepository.Update(sumConflit);
                return ObjectMapper.Map<SumConflictDto>(sumConflit);
            }
            else
            {
              var  sumConflit = ObjectMapper.Map<TbmSumConflict>(input);
                sumConflit.Id = Guid.NewGuid();
                sumConflit = _SumConflictRepository.Insert(sumConflit);
                return ObjectMapper.Map<SumConflictDto>(sumConflit);
            }

        }
        /// <summary>
        /// 查询冲突关键词
        /// </summary>
        public List<SumConflictDto> SearchSumConflict(ChargeBM input)
        {
            var que = _SumConflictRepository.GetAll();
            if (!string.IsNullOrEmpty(input.Name))
            {
                que = que.Where(p=>input.Name.Contains(p.SumWord));
            }
            if (input.Id!=null && input.Id !=Guid.Empty)
            {
                que = que.Where(p => p.Id== input.Id);
            }
            return ObjectMapper.Map<List<SumConflictDto>>(que);
        }
        /// <summary>
        /// 删除冲突关键词
        /// </summary>
        public void DelSumConflict(ChargeBM input)
        {
            var que = _SumConflictRepository.Get(input.Id);
            _SumConflictRepository.Delete(que);            
        }
        /// <summary>
        /// 保存复合诊断
        /// </summary>
        /// <param name="input"></param>
        public TbmSummHBDto SaveSumHB(TbmSummHBDto input)
        {
            //修改
            if (input.Id != Guid.Empty)
            {
                var tbmsummHB = _TbmSummHBRepository.Get(input.Id);
                tbmsummHB.AdviceName = input.AdviceName;
                tbmsummHB.SummarizeAdviceId = input.SummarizeAdviceId;
                if (input.Advices != null && input.Advices.Count > 0)
                {

                     tbmsummHB.Advices = new List<TbmSummarizeAdvice>();
                  
                    foreach (var adInfo in input.Advices)
                    {
                        tbmsummHB.Advices.Add(_summarizeAdviceRepository.Get(adInfo.Id));
                    }

                }
                var SummHB = _TbmSummHBRepository.Update(tbmsummHB);
                return ObjectMapper.Map<TbmSummHBDto>(SummHB);
            }
            else
            {
                TbmSummHB tbmsummHB = new TbmSummHB();
                tbmsummHB.Id = Guid.NewGuid();
                tbmsummHB.AdviceName = input.AdviceName;
                tbmsummHB.SummarizeAdviceId = input.SummarizeAdviceId;
                if (input.Advices != null && input.Advices.Count > 0)
                {
              
                   if (tbmsummHB.Advices == null) tbmsummHB.Advices = new List<TbmSummarizeAdvice>();
                    foreach (var adInfo in input.Advices)
                    {
                        tbmsummHB.Advices.Add(_summarizeAdviceRepository.Get(adInfo.Id));
                    }

                }

                var SummHB = _TbmSummHBRepository.Insert(tbmsummHB);
                CurrentUnitOfWork.SaveChanges();
                return ObjectMapper.Map<TbmSummHBDto>(SummHB);
            }
        }

        /// <summary>
        /// 查询复合诊断
        /// </summary>
        /// <param name="input"></param>
        public List<TbmSummHBDto> SearchSumHB()
        {            
               
                var SummHB = _TbmSummHBRepository.GetAll();
                return ObjectMapper.Map<List<TbmSummHBDto>>(SummHB);
            
        }
        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="input"></param>
        public void DelSumHB(EntityDto<Guid> input)
        {

            var SummHB = _TbmSummHBRepository.Get(input.Id);
            _TbmSummHBRepository.Delete(SummHB);

        }
    }
}
