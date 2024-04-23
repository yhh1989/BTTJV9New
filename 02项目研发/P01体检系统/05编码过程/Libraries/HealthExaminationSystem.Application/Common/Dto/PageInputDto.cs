namespace Sw.Hospital.HealthExaminationSystem.Application.Common.Dto
{
    /// <summary>
    /// 包含参数的分页输入对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInputDto<T> : PageInputDto
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        public T Input { get; set; }
    }

    /// <summary>
    /// 不带参数的分页输入对象
    /// </summary>
    public class PageInputDto
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurentPage { get; set; }
    }
}