using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes
{
    /// <summary>
    /// 单位建档
    /// <para>http://{host}:{port}/api/services/app/ClientInfoes/Get</para>
    /// <para>http://{host}:{port}/api/services/app/{appService}/{function}</para>
    /// by yjh 2018-08-09
    /// </summary>
    [AbpAuthorize]
    public class ClientInfoesAppService : MyProjectAppServiceBase, IClientInfoesAppService
    {
        private readonly IRepository<TjlClientInfo, Guid> _clientInfoesRepository;
        /// <summary>
        /// 用户表仓储
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        public ClientInfoesAppService(IRepository<TjlClientInfo, Guid> clientInfoesRepository,
             IRepository<User, long> userRepository)
        {
            _clientInfoesRepository = clientInfoesRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位类</returns>
        public ClientInfosViewDto Add(ClientInfoesDto input)
        {
            if (_clientInfoesRepository.GetAll().Any(r => r.ClientName == input.ClientName))
                throw new FieldVerifyException("单位名称重复！", "单位名称重复！");

            if (_clientInfoesRepository.GetAll().Any(r => r.ClientBM == input.ClientBM))
                throw new FieldVerifyException("单位编码重复！", "单位编码重复！");

            var entity = input.MapTo<TjlClientInfo>();
            entity.Id = Guid.NewGuid();
            if (input.Parent != null)
                entity.Parent = _clientInfoesRepository.Get(input.Parent.Id);
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM!=null && userBM.HospitalArea.HasValue)
            {
                entity.HospitalArea = userBM.HospitalArea;
            }
            entity = _clientInfoesRepository.Insert(entity);
            var dto = entity.MapTo<ClientInfosViewDto>();
            return dto;
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="input">单位类</param>
        public void Del(ClientInfoesDto input)
        {
            _clientInfoesRepository.Delete(input.Id);
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位类</returns>
        public ClientInfoesDto Edit(ClientInfoesDto input)
        {
            if (_clientInfoesRepository.GetAll().Any(r => r.ClientName == input.ClientName && r.Id != input.Id))
                throw new FieldVerifyException("单位名称重复！", "单位名称重复！");

            if (_clientInfoesRepository.GetAll().Any(r => r.ClientBM == input.ClientBM && r.Id != input.Id))
                throw new FieldVerifyException("单位编码重复！", "单位编码重复！");

            var id = input.Parent == null ? Guid.Empty : input.Parent.Id;
            input.Parent = null;
            var entity = _clientInfoesRepository.Get(input.Id);
            input.MapTo(entity); // 赋值
            if (id != null && id != Guid.Empty)
                entity.ParentId = id;
            entity = _clientInfoesRepository.Update(entity);
            var dto = entity.MapTo<ClientInfoesDto>();
            CurrentUnitOfWork.SaveChanges();
            return dto;
        }

        /// <summary>
        /// 获取单条单位
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位类</returns>
        public ClientInfosViewDto Get(ClientInfoesListInput input)
        {
            var query = BuildQuery(input);
            var entity = query.FirstOrDefault();
            var dto = entity.MapTo<ClientInfosViewDto>();
            return dto;
        }

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位列表</returns>
        [UnitOfWork(false)]
        public List<ClientInfosViewDto> Query(ClientInfoesListInput input)
        {
            var query = BuildQuery(input);

            query = query.Include(r => r.Parent);
            query = query.OrderByDescending(o => o.CreationTime);            
            var dtos = query.MapTo<List<ClientInfosViewDto>>();
            return dtos;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ClientInfosNameDto>> QueryClientName(ChargeBM input)
        {
            int num = -1;
            if (input.Name == "已预约")
            {
                num = 0;
            }
          

            var query = _clientInfoesRepository.GetAll();
            if (input.Id != Guid.Empty)
            {
                query = query.Where(o=>o.Id==input.Id);
            }
             query = query.Where(o => o.ClientReg.Count > num);
            // query = query.OrderByDescending(o => o.CreationTime).ToList();

            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            return await query.OrderByDescending(o => o.CreationTime).ProjectToListAsync<ClientInfosNameDto>(GetConfigurationProvider<Core.Company.TjlClientInfo, ClientInfosNameDto>());
        }

        public ICollection<ClientInfosViewDto> GetAll()
        {
            var result = _clientInfoesRepository.GetAll();
            return result.OrderByDescending(o=>o.CreationTime).MapTo<ICollection<ClientInfosViewDto>>();
        }

        /// <summary>
        /// 分页查询单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageResultDto<ClientInfosViewDto> PageFulls(PageInputDto<ClientInfoesListInput> input)
        {
            var query = _clientInfoesRepository.GetAll();

            if (input.Input.Id != Guid.Empty)
            {
                query = query.Where(m => m.Id == input.Input.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty( input.Input.ClientBM)) //编码
                    query = query.Where(m => m.ClientBM == input.Input.ClientBM || m.Parent.ClientBM == input.Input.ClientBM);

                if (!string.IsNullOrWhiteSpace(input.Input.ClientName)) //单位名称
                    query = query.Where(m => m.ClientName.Contains(input.Input.ClientName) 
                    || m.Parent.ClientName.Contains(input.Input.ClientName) ||
                    m.HelpCode.ToUpper().Contains(input.Input.ClientName.ToUpper()));

                if (!string.IsNullOrWhiteSpace(input.Input.HelpCode)) //助记码
                    query = query.Where(m => m.HelpCode == input.Input.HelpCode || m.Parent.HelpCode == input.Input.HelpCode);

                if (!string.IsNullOrWhiteSpace(input.Input.WubiCode)) //五笔码
                    query = query.Where(m => m.WubiCode == input.Input.WubiCode || m.Parent.WubiCode == input.Input.WubiCode);

                if (!string.IsNullOrWhiteSpace(input.Input.ClientSource)) //来源
                {
                    var clientSources = input.Input.ClientSource.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    query = query.Where(m => clientSources.Contains(m.ClientSource));
                }

                if (!string.IsNullOrWhiteSpace(input.Input.LinkMan)) //联系人
                    query = query.Where(m => m.LinkMan.Contains(input.Input.LinkMan));

                if (input.Input.UserId.HasValue) //专属客服
                    query = query.Where(m => m.UserId == input.Input.UserId);

                if (input.Input.StartTime.HasValue)
                    query = query.Where(o => o.CreationTime >= input.Input.StartTime.Value);

                if (input.Input.EndTime.HasValue)
                    query = query.Where(o => o.CreationTime <= input.Input.EndTime.Value);
            }

            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea!=999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
               || p.HospitalArea == null);
            }

            query = query.OrderByDescending(o => o.CreationTime);
            var result = new PageResultDto<ClientInfosViewDto> { CurrentPage = input.CurentPage };
            result.Calculate(query.Count());
            query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
            result.Result = query.MapTo<List<ClientInfosViewDto>>();
            return result;
        }

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <param name="input">单位类</param>
        /// <returns>单位列表</returns>
        private IQueryable<TjlClientInfo> BuildQuery(ClientInfoesListInput input)
        {
            var query = _clientInfoesRepository.GetAll().AsNoTracking();

            if (input != null)
            {
                if (input.Id != Guid.Empty)
                {
                    query = query.Where(m => m.Id == input.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty( input.ClientBM)) //编码
                        query = query.Where(m => m.ClientBM == input.ClientBM);
                    if (!string.IsNullOrEmpty(input.ClientName)) //单位名称
                        query = query.Where(m => m.ClientName.Contains(DbFunctions.AsNonUnicode(input.ClientName)));
                    if (!string.IsNullOrEmpty(input.HelpCode)) //助记码
                        query = query.Where(m => m.HelpCode == input.HelpCode);
                    if (!string.IsNullOrEmpty(input.WubiCode)) //五笔码
                        query = query.Where(m => m.WubiCode == input.WubiCode);
                    if (!string.IsNullOrEmpty(input.ClientSource)) //来源
                        query = query.Where(m => input.ClientSource.Contains(m.ClientSource));
                    if (!string.IsNullOrEmpty(input.LinkMan)) //联系人
                        query = query.Where(m => m.LinkMan.Contains(input.LinkMan));
                    //if (!string.IsNullOrEmpty(input.ClientDegree.ToString())) //专属客服
                        //query = query.Where(m => m.ClientDegree.ToString() == input.ClientDegree.ToString());
                    if (!string.IsNullOrEmpty(input.UserId.ToString())) //专属客服
                        query = query.Where(m => m.UserId == input.UserId);
                    if (input.StartTime != null)
                        query = query.Where(o => o.CreationTime >= input.StartTime);
                    if (input.EndTime != null)
                        query = query.Where(o => o.CreationTime <= input.EndTime);
                    if (input.ParentId != null && input.ParentId != Guid.Empty)
                        query = query.Where(o => o.ParentId == input.ParentId);
                }
            }

            return query.OrderByDescending(o => o.CreationTime);
        }

        /// <inheritdoc />
        public async Task<List<ClientInfoCacheDto>> GetAllForCache()
        {
            var result = _clientInfoesRepository.GetAll().AsNoTracking().OrderByDescending(r => r.CreationTime);
            return await result.ProjectToListAsync<ClientInfoCacheDto>(GetConfigurationProvider<ClientInfoCacheDto>());
        }
    }
}