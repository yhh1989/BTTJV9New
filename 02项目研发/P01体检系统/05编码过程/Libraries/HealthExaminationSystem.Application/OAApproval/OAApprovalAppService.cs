using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval
{
    public class OAApprovalAppService : MyProjectAppServiceBase, IOAApprovalAppService
    {
        private readonly IRepository<TjlOAApproValcs, Guid> _OAApproValcs;

        private readonly IRepository<TbmClientZKSet, Guid> _TbmClientZKSet;

        public OAApprovalAppService(IRepository<TjlOAApproValcs, Guid> itemSuitRepository,
            IRepository<TbmClientZKSet, Guid> TbmClientZKSet
            )
        {
            _OAApproValcs = itemSuitRepository;
            _TbmClientZKSet = TbmClientZKSet;
        }
        /// <summary>
        /// 添加修改审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreatOAApproValcsDto creatOAApproValcs(CreatOAApproValcsDto input )
        {
            if (input.Id != Guid.Empty)
            {
                var entity = _OAApproValcs.Get(input.Id);
               
                input.MapTo(entity); // 赋值
                _OAApproValcs.Update(entity);
                return entity.MapTo<CreatOAApproValcsDto>();
            }
            else
            {
                var entity = input.MapTo<TjlOAApproValcs>();
                entity.Id = Guid.NewGuid();
                entity.AppliTime = System.DateTime.Now;
                _OAApproValcs.Insert(entity);
                return entity.MapTo<CreatOAApproValcsDto>();
            }

        }
        /// <summary>
        /// 删除审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelOAApproValcs(EntityDto<Guid> input)
        {
            
                var entity = _OAApproValcs.Get(input.Id);
            if (entity.AppliState == 0)
            {
                _OAApproValcs.Delete(entity);
            }         

        }
        /// <summary>
        ///查询审批
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CreatOAApproValcsDto >SearchOAApproValcs(SearchOAApproValcsDto input)
        {
            if (input.Id != Guid.Empty)
            {
                List<CreatOAApproValcsDto> creatOAApproValcsDtos = new List<CreatOAApproValcsDto>();
               var entity = _OAApproValcs.Get(input.Id);

                creatOAApproValcsDtos.Add(entity.MapTo<CreatOAApproValcsDto>());
                return creatOAApproValcsDtos;
            }
            else
            {
                var que = _OAApproValcs.GetAll();
                if (input.AppliState.HasValue)
                {
                    que = que.Where(o=>o.AppliState==input.AppliState);
                }
                if (input.ClientInfoId.HasValue)
                {
                    que = que.Where(o=>o.ClientInfoId == input.ClientInfoId);
                }
                if (input.StarAppliTime.HasValue)
                {
                    que = que.Where(o => o.AppliTime >= input.StarAppliTime.Value);
                }
                if (input.EndAppliTime.HasValue)
                {
                    que = que.Where(o => o.AppliTime <input.EndAppliTime.Value.AddDays(1));
                }
                if (input.StarCreateTime.HasValue)
                {
                    que = que.Where(o => o.AppliTime >= input.StarCreateTime.Value);
                }
                if (input.EndCreateTime.HasValue)
                {
                    que = que.Where(o => o.AppliTime < input.EndCreateTime.Value);
                }
                var OAlist = que.MapTo<List<CreatOAApproValcsDto>>();
                return OAlist.OrderByDescending(o=>o.AppliTime).ToList();

            }

        }

        /// <summary>
        /// 添加修改审批设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateClientXKSetDto creatCreateClientXKSet(CreateClientXKSetDto input)
        {
            if (input.Id != Guid.Empty)
            {
                var entity = _TbmClientZKSet.Get(input.Id);

                input.MapTo(entity); // 赋值
                _TbmClientZKSet.Update(entity);
                return entity.MapTo<CreateClientXKSetDto>();
            }
            else
            {
                var entity = input.MapTo<TbmClientZKSet>();
                entity.Id = Guid.NewGuid();
                _TbmClientZKSet.Insert(entity);
                return entity.MapTo<CreateClientXKSetDto>();
            }

        }
        /// <summary>
        ///查询审批设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateClientXKSetDto getCreateClientXKSet()
        {
          
                var entity = _TbmClientZKSet.GetAll().ToList();
            if (entity.Count > 0)
            {
              
                return entity.FirstOrDefault().MapTo<CreateClientXKSetDto>();
            }
            else
                return null;
          

        }

        public CreatOAApproValcsDto upOAApproValcsDto(UpOAApproValcsDto input  )
        {
            var entity = _OAApproValcs.Get(input.Id);
            input.MapTo(entity);

            _OAApproValcs.Update(entity);
            return entity.MapTo<CreatOAApproValcsDto>();

        }



    }
}
