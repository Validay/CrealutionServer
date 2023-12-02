using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for login account
    /// </summary>
    public class AccountLoginDto
    {
        /// <summary>
        /// Name account
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }

        /// <summary>
        /// Password account
        /// </summary>
        [Required]
        public string Password { get; init; }
    }
}