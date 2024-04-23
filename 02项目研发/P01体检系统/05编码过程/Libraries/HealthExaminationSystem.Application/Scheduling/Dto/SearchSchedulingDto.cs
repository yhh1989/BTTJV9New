﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
    public class SearchSchedulingDto
    {
        public virtual bool? IsTeam { get; set; }
        public virtual string QueryText { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
    }
}
