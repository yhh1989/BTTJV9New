namespace Sw.Hospital.HealthExaminationSystem.Common.Interfaces
{
    public interface IPage
    {
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int CurentPage { get; set; }
    }
}