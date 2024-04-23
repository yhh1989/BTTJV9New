using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    public class CustomerReportQuery
    {
        public virtual string QueryText { get; set; }
        public virtual int? CustomerReportState { get; set; }

        public virtual DateTime? StartRegisterDate { get; set; }
        public virtual DateTime? EndRegisterDate { get; set; }
        public virtual DateTime? StartHandoverDate { get; set; }
        public virtual DateTime? EndHandoverDate { get; set; }
        public virtual DateTime? StartSendDate { get; set; }
        public virtual DateTime? EndSendDate { get; set; }

        public virtual string Handover { get; set; }
        public virtual string Receiptor { get; set; }
        public virtual string Sender { get; set; }
        public virtual string Receiver { get; set; }

        public virtual Guid? ClientRegId { get; set; }
    }
}
