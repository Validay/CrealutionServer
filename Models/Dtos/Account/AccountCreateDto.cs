using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for create account
    /// </summary>
    public class AccountCreateDto
    {
        /// <summary>
        /// New name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// New display name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        /// <summary>
        /// New password account
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Role ids account
        /// </summary>
        [Required]
        public List<long> RoleIds { get; set; }
    }
}