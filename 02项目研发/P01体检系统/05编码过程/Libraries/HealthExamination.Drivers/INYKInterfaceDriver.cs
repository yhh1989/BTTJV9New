using Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers
{
  public   interface INYKInterfaceDriver : IDriver
    {
        getCardInfoDto GetNYHCardByNum(string card);
        OutCardDto NYHChargeCard(ChargCardDto input);

    }
}
