using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Roles
{
    /// <summary>
    /// Model for create role
    /// </summary>
    public class RoleCreateDto
    {
        /// <summary>
        /// Name role
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}