﻿using Abp.Application.Navigation;

namespace Sw.Hospital.HealthExaminationSystem.Web.Models.Layout
{
    public class SideBarNavViewModel
    {
        public UserMenu MainMenu { get; set; }

        public string ActiveMenuItemName { get; set; }
    }
}