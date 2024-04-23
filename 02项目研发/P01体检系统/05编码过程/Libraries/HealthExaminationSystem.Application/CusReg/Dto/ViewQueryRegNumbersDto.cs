using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 登记窗体统计登记数dto
    /// </summary>
    public class ViewQueryRegNumbersDto
    {
        /// <summary>
        /// 按单位统计
        /// </summary>
        public List<RegNumberData> datas { get; set; }
        /// <summary>
        /// 今日总登记人数
        /// </summary>
        public int SumReg { get; set; }
        ///// <summary>
        ///// 男性登记
        ///// </summary>
        //public int MaleReg { get; set; }
        ///// <summary>
        ///// 女性登记
        ///// </summary>
        //public int FemaleReg { get; set; }
        ///// <summary>
        ///// 未登记
        ///// </summary>
        //public int NotReg { get; set; }
        ///// <summary>
        ///// 男未登记
        ///// </summary>
        //public int MaleNotReg { get; set; }
        ///// <summary>
        ///// 女未登记
        ///// </summary>
        //public int FemaleNotReg { get; set; }
        ///// <summary>
        ///// 体检中
        ///// </summary>
        //public int Tijianzhong { get; set; }
        ///// <summary>
        ///// 体检中男性
        ///// </summary>
        //public int TijianzhongMale { get; set; }
        ///// <summary>
        ///// 体检中女性
        ///// </summary>
        //public int TijianzhongFemale { get; set; }
        ///// <summary>
        ///// 已体检
        ///// </summary>
        //public int WanchengTijian { get; set; }
        ///// <summary>
        ///// 已体检男
        ///// </summary>
        //public int WanchengTijianMale { get; set; }
        ///// <summary>
        ///// 已体检女
        ///// </summary>
        //public int WanchengTijianFemale { get; set; }
        ///// <summary>
        /////已总检
        ///// </summary>
        //public int Yizongjian { get; set; }
        ///// <summary>
        /////已总检男
        ///// </summary>
        //public int YizongjianMale { get; set; }
        ///// <summary>
        /////已总检女
        ///// </summary>
        //public int YizongjianFemale { get; set; }
        ///// <summary>
        ///// 已审核
        ///// </summary>
        //public int Yishenhe { get; set; }
        ///// <summary>
        ///// 已审核男
        ///// </summary>
        //public int YishenheMale { get; set; }
        ///// <summary>
        ///// 已审核女
        ///// </summary>
        //public int YishenheFemale { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class RegNumberData
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Sum { get; set; }
        /// <summary>
        /// 男
        /// </summary>
        public int Male { get; set; }
        /// <summary>
        /// 女
        /// </summary>
        public int Famale { get; set; }
        /// <summary>
        /// 未说明
        /// </summary>
        //public int NoSex { get; set; }
    }
}
