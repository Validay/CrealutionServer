using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for update account
    /// </summary>
    public class AccountUpdateDto
    {
        /// <summary>
        ///Id account
        /// </summary>
        [Required]
        public long Id { get; set; }

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
        /// Has ban
        /// </summary>
        [Required]
        public bool InBanned { get; set; }

        /// <summary>
        /// Role ids account
        /// </summary>
        [Required]
        public List<long> RoleIds { get; set; }
    }
}