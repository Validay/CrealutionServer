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
        public long Id { get; init; }

        /// <summary>
        /// New name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }

        /// <summary>
        /// New display name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string DisplayName { get; init; }

        /// <summary>
        /// New password account
        /// </summary>
        [Required]
        public string Password { get; init; }

        /// <summary>
        /// Has ban
        /// </summary>
        [Required]
        public bool InBanned { get; init; }

        /// <summary>
        /// Role ids account
        /// </summary>
        [Required]
        public List<long> RoleIds { get; init; }
    }
}