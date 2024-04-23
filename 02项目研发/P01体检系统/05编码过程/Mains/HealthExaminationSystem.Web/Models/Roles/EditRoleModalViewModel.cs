﻿using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Web.Models.Roles
{
    public class EditRoleModalViewModel
    {
        public RoleDto Role { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }

        public bool HasPermission(PermissionDto permission)
        {
            return Permissions != null && Role.Permissions.Any(p => p == permission.Name);
        }
    }
}