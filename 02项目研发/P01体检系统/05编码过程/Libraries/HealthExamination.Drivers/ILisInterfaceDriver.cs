using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExamination.Drivers.Models.LisInterface;

namespace Sw.Hospital.HealthExamination.Drivers
{
    /// <summary>
    /// LIS 驱动器
    /// </summary>
    public interface ILisInterfaceDriver : IDriver
    {
        /// <summary>
        /// 转换接口-按时间转换
        /// </summary>
        /// <param name="StartDatetime">开始时间</param>
        /// <returns>是否成功</returns>
        List<vw_TInterfaceresult> Convert(TdbInterfaceWhere tdbInterfaceWhere);

    }
}