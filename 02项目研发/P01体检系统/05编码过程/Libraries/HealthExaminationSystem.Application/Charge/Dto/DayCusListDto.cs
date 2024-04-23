using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
 public    class DayCusListDto
    {
        public virtual Guid? CustomerRegId { get; set; }
        public virtual string ClientName { get; set; }
        public virtual int? RSCount { get; set; }

        public virtual decimal? Allmoney { get; set; }

        public virtual decimal? Actualmoney { get; set; }
        public virtual decimal? MPayment { get; set; }
        public virtual string PaymentName { get; set; }
    }
}
