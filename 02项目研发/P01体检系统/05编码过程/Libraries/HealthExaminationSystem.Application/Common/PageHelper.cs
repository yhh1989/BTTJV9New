using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common
{
    /// <summary>
    /// 分页帮助
    /// </summary>
    public class PageHelper
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="input">输入</param>
        /// <param name="query">查询</param>
        /// <returns></returns>
        public static PageResultDto<TResult> Paging<TEntity, TResult>(PageInputDto input, IQueryable<TEntity> query)
            where TEntity : IEntity<Guid>
        {
            var result = new PageResultDto<TResult> { CurrentPage = input.CurentPage };
            result.Calculate(query.Count());
            query = query.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
            var result1 = query.ToList();
            result.Result = result1.MapTo<List<TResult>>();
            return result;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="input">输入</param>
        /// <param name="method">查询</param>
        /// <returns></returns>
        public static PageResultDto<TResult> Paging<TEntity, TResult>(PageInputDto input,
            Func<IQueryable<TEntity>> method)
            where TEntity : IEntity<Guid>
        {
            return Paging<TEntity, TResult>(input, method.Invoke());
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TInput">输入类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="input">输入</param>
        /// <param name="method">查询</param>
        /// <returns></returns>
        public static PageResultDto<TResult> Paging<TInput, TEntity, TResult>(PageInputDto<TInput> input,
            Func<TInput, IQueryable<TEntity>> method)
            where TEntity : IEntity<Guid>
        {
            return Paging<TEntity, TResult>(input, method.Invoke(input.Input));
        }
    }
}