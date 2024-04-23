using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime
{
    [AbpAuthorize]
    public class ReVisitTimeAppService : MyProjectAppServiceBase, IReVisitTimeAppService
    {
        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository; 
        private readonly IRepository<Core.Illness.ReVisitTime, Guid> _reVisitTimeRepository;
        /// <summary>
        /// 复诊时间维护
        /// </summary>
        public ReVisitTimeAppService(IRepository<TbmItemGroup, Guid> itemGroupRepository,
            IRepository<Core.Illness.ReVisitTime, Guid> reVisitTimeRepository)
        {
            _itemGroupRepository = itemGroupRepository;
            _reVisitTimeRepository = reVisitTimeRepository;
        }

        /// <summary>
        /// 根据预约id删除所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool DelReVisitTimeForRegId(InsertReVisitTimeDto dto)
        {
            var list = _reVisitTimeRepository.GetAll().Where(n => n.CustomerRegisterId == dto.CustomerRegisterId);
            foreach (var item in list)
            {
                _reVisitTimeRepository.Delete(item);
            }
            return true;
        }

        /// <summary>
        /// 获取所有项目组合
        /// </summary>
        /// <returns></returns>
        public List<SearchItemGroupDto> GetALLItemGroup(SearchItemGroupDto dto)
        {
           var itemGroup= _itemGroupRepository.GetAll().Where(o=>o.IsActive).OrderBy(o=>o.Department.OrderNum);
            return itemGroup.MapTo<List<SearchItemGroupDto>>();
        }
        /// <summary>
        /// 按项目查询数据
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetALLReVisitTime(QueryData queryData)
        {

            var data = _reVisitTimeRepository.GetAll();
            if (queryData.ItemGroupBM_Id.HasValue)
            {
                data = data.Where(o => o.ItemGroupId == queryData.ItemGroupBM_Id);
            }
            if (queryData.Id.HasValue)
            {
                data = data.Where(o => o.Id == queryData.Id);
            }
            return data.MapTo<List<SearchReVisitTimeDto>>();
        }

        /// <summary>
        /// 根据单位预约记录id查询此单位下的所有复诊信息
        /// </summary>
        /// <param name="dto">单位预约id</param>
        /// <returns></returns>
        public List<QueryCheckReview> GetReVisitTimeForClientRegId(QueryCheckReview dto)
        {
            var data = _reVisitTimeRepository.GetAll().Where(n => n.CompanyRegisterId == dto.CompanyRegisterId);
            return data.MapTo<List<QueryCheckReview>>();
        }

        /// <summary>
        /// 根据预约id查询所有
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<InsertReVisitTimeDto> GetReVisitTimeForRegId(InsertReVisitTimeDto dto)
        {
            var data = _reVisitTimeRepository.GetAll().Where(n => n.CustomerRegisterId == dto.CustomerRegisterId);
            return data.MapTo<List<InsertReVisitTimeDto>>();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool InsertReVisitTime(List<InsertReVisitTimeDto> dto)
        {
            foreach (var item in dto)
            {
                Core.Illness.ReVisitTime time = new Core.Illness.ReVisitTime();
                time.Id = Guid.NewGuid();
                time.Content = item.Content;
                time.ItemGroupId = item.ItemGroupId;
                time.IsActive = true;
                time.IsInspect = item.IsInspect;
                time.CustomerRegisterId = item.CustomerRegisterId;
                time.CompanyRegisterId = item.CompanyRegisterId;
                time.CustomerRegId = item.CustomerRegId;
                _reVisitTimeRepository.Insert(time);
            }
            return true;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public bool UpdateIsActive(QueryData queryData)
        {
            var data = _reVisitTimeRepository.FirstOrDefault(queryData.Id.Value);
            data.IsActive = false;
            _reVisitTimeRepository.Update(data);
            return true;
        }
        /// <summary>
        /// 修改/新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool UpdateReVisitTime(SearchReVisitTimeDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                Core.Illness.ReVisitTime time = new Core.Illness.ReVisitTime();
                time.Id = Guid.NewGuid();
                time.IsActive = dto.IsActive;
                time.ItemGroupId = dto.ItemGroupId;
                time.Content = dto.Content;
                time.ItemGroup = _itemGroupRepository.FirstOrDefault(dto.ItemGroupId.Value);
                _reVisitTimeRepository.Insert(time);
            }
            else
            {
                var data = _reVisitTimeRepository.FirstOrDefault(dto.Id);
                data.IsActive = dto.IsActive;
                data.ItemGroupId = dto.ItemGroupId;
                data.Content = dto.Content;
                data.ItemGroup = _itemGroupRepository.FirstOrDefault(dto.ItemGroupId.Value);
                _reVisitTimeRepository.Update(data);
            }
            return true;
        }
        /// <summary>
        /// 根据患者id获取未复诊过的项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetReVisitTimeForCustomerId(QueryData id)
        {
            var rt = _reVisitTimeRepository.GetAll().Where(o => o.CustomerRegId == id.Id && o.IsActive == true && o.IsInspect == false);
            return rt.MapTo<List<SearchReVisitTimeDto>>();
        }
        /// <summary>
        /// 根据单位和项目组合编码获取复诊对应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SearchReVisitTimeDto> GetReVisitTimeContentList(QueryData data)
        {
           var rt= _reVisitTimeRepository.GetAll().Where(o => o.CompanyRegisterId == data.ClientRegId && o.ItemGroupId == data.ItemGroupBM_Id);
            return rt.MapTo<List<SearchReVisitTimeDto>>();
        }
        /// <summary>
        ///  查询项目金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TbmItemGroupDto GetItemGroupPrice(QueryData data)
        {
          var query=  _itemGroupRepository.FirstOrDefault(o => o.Id == data.ItemGroupBM_Id);
            return query.MapTo<TbmItemGroupDto>();
        }
    }
}
