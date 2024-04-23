using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers
{
    public interface IHisInterfaceDriver : IDriver
    {
        //GetCusInfoByNum
        /// <summary>
        /// 转换接口-按时间转换
        /// </summary>
        /// <param name="StartDatetime">开始时间</param>
        /// <returns>是否成功</returns>
        HisCusInfo GetCusInfoByNum(InCarNum input);

        List<HisPriceDto> GetHisPrice(InCarNumPrice input);
        string InsertSFCharg(TJSQ input);
        string InsertHisSFCharg(MZ_YZML_MASTER input);
        string getHisState(InCarNum input);
        List<TJSQ> searchTJR(SqlWhere input);
        bool upSfCharg(InCarNum input);
        OutHis SaveHisInfo(InCarNum input);
        string InsertYXCharg(List<HisPriceDto> input);

        List<HistoryResultDto> GetHistoryResult(SearchHisClassDto Search);

    }
}
