using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common.Dto
{
    /// <summary>
    /// 分页数据传输对象
    /// </summary>
    /// <typeparam name="T">集合的类</typeparam>
    public class PageResultDto<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public int PageSize = 30;

        /// <summary>
        /// 结果
        /// </summary>
        public List<T> Result { get; set; }

        /// <summary>
        /// 计算页数
        /// </summary>
        /// <param name="number">总条数</param>
        public void Calculate(decimal number)
        {
            TotalPages = (int)Math.Ceiling(number / PageSize);
            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
                if (CurrentPage == 0)
                {
                    CurrentPage = 1;
                    TotalPages = 1;
                }
            }
        }
    }
}