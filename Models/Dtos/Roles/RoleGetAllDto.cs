using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.Roles
{
    /// <summary>
    /// Model for get all roles
    /// </summary>
    public class RoleGetAllDto
    {
        /// <summary>
        /// Collection roles
        /// </summary>
        public List<RoleDto> Roles { get; set; }
    }
}