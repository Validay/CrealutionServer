﻿using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for update info account
    /// </summary>
    public class AccountUpdateInfoDto
    {
        /// <summary>
        /// Name account
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
    }
}