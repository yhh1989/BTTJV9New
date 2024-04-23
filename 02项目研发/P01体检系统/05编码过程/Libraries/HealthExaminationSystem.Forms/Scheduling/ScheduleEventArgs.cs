using System;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public class ScheduleEventArgs : EventArgs
    {
        public ScheduleEventArgs(SchedulingNewDto data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public SchedulingNewDto Data { get; set; }
    }
}