using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.AccountItemTypes
{
    /// <summary>
    /// Model for create account item type
    /// </summary>
    public class AccountItemTypeCreateDto
    {
        /// <summary>
        /// Account id
        /// </summary>
        [Required]
        public long AccountId { get; init; }

        /// <summary>
        /// Item type id
        /// </summary>
        [Required]
        public long ItemTypeId { get; init; }

        /// <summary>
        /// Count item
        /// </summary>
        [Required]
        public int Count { get; init; }
    }
}