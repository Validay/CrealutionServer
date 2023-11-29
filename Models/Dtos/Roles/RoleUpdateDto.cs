using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Roles
{
    /// <summary>
    /// Model for update role
    /// </summary>
    public class RoleUpdateDto
    {
        /// <summary>
        /// Id role
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// New name role
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}