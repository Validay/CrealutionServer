using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for registration account
    /// </summary>
    public class AccountRegistrationDto
    {
        /// <summary>
        /// Name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }

        /// <summary>
        /// Name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string DisplayName { get; init; }

        /// <summary>
        /// Password account
        /// </summary>
        [Required]
        public string Password { get; init; }
    }
}