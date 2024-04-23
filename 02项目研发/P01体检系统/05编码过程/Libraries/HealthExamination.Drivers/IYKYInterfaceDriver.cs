using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExamination.Drivers.Models.YKYInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers
{
    public interface  IYKYInterfaceDriver : IDriver
    {
        YHCustomer GetYHCusInfoByNum(InCarNum input);

        YHCustomer ChargeByYHNum(InYKCarNum input);
    }
}
