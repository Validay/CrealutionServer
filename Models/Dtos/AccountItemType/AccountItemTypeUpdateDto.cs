using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.AccountItemTypes
{
    /// <summary>
    /// Model for update account item types
    /// </summary>
    public class AccountItemTypeUpdateDto
    {
        /// <summary>
        /// Id account item types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// Count item
        /// </summary>
        [Required]
        public int Count { get; init; }
    }
}