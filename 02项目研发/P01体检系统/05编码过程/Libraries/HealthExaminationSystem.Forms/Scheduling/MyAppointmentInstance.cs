using System;
using System.Text;
using DevExpress.XtraScheduler.Internal.Implementations;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public class MyAppointmentInstance : AppointmentInstance
    {
        public SchedulingNewDto DataBinding { get; private set; }

        public void SetAppointment(SchedulingNewDto value)
        {
            DataBinding = value;
            Subject = value.IsTeam ? value.ClientInfo.ClientName : value.PersonalName;

            Location = $"{value.TotalNumber}人";
            var sb = new StringBuilder();
            if (value.ItemGroups.Count != 0)
            {
                sb.Append("项目：");
                foreach (var itemGroup in value.ItemGroups)
                {
                    sb.AppendLine();
                    sb.Append(itemGroup.ItemGroupName);
                }
            }

            if (!string.IsNullOrWhiteSpace(value.Remarks))
            {
                if (sb.Length != 0)
                {
                    sb.AppendLine();
                }

                sb.Append("备注：");
                sb.AppendLine();
                sb.Append(value.Remarks);
            }
            Description = sb.ToString();
            Start = value.ScheduleDate.Date;
            End = value.ScheduleDate.Date.AddDays(1).AddMinutes(-1);
        }
    }
}