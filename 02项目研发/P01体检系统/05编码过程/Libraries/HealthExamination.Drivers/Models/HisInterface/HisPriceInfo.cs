using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{
   public class HisPriceInfo
    {
        //编号
        public string BH { get; set; }

        public string MC { get; set; }

        public string FYTJBH { get; set; }
        //助记码
        public string SRM1 { get; set; }
        //单价
        public int DJ { get; set; }
        //单位
        public string DW { get; set; }
        public string YJHD { get; set; }
        


    }
}
