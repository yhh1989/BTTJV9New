using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection
{
    /// <summary>
    /// 导引单领取
    /// </summary>
    public class GuideListCollectionAppService : MyProjectAppServiceBase, IGuideListCollectionAppService
    {
        #region 接口和引用
        private readonly IRepository<TjlCustomerReg, Guid> _tjlCustomerReg; //体检人预约登记信息表
        public GuideListCollectionAppService(
            IRepository<TjlCustomerReg, Guid> tjlCustomerReg
        )
        {
            _tjlCustomerReg = tjlCustomerReg;
        }
        #endregion
        /// <summary>
        /// 获取预约单位导引单信息
        /// </summary>
        public PageResultDto<CustomerRegQuery> QueryCompanyName(PageInputDto<CustomerRegQuery> input)
        {

            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _tjlCustomerReg.GetAllIncluding(r => r.Customer);
                //单位ID
                if(input.Input != null)
                {
                    if (input.Input.ClientInfoId != null)
                    {
                        query = query.Where(o => o.ClientInfo.Id == input.Input.ClientInfoId);
                    }
                    if (input.Input.Id != Guid.Empty)
                    {
                        query = query.Where(o => o.ClientRegId == input.Input.Id);
                    }
                }
                
                if (query.Count() != 0)
                {
                    query = query.OrderBy(o => o.CustomerRegNum);
                    var result = new PageResultDto<CustomerRegQuery>();
                    result.CurrentPage = input.CurentPage;
                    result.Calculate(query.Count());
                    query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
                    result.Result = query.MapTo<List<CustomerRegQuery>>();
                    return result;
                }


                else
                    return null;

            }

           
        }

        /// <summary>
        /// 获取预约单位导引单信息
        /// </summary>
        public List<CustomerRegQuery> PrintCompanyName(CustomerRegQuery input)
        {

            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _tjlCustomerReg.GetAllIncluding(r => r.Customer);
                //单位ID
                if (input.ClientInfoId != null)
                {
                    query = query.Where(o => o.ClientInfo.Id == input.ClientInfoId);
                }

                var result = query.MapTo<List<CustomerRegQuery>>();
                return result;


            }


        }
    }

}