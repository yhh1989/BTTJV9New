using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}