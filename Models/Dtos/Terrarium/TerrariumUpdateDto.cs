using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Terrariums
{
    /// <summary>
    /// Model for update terrarium
    /// </summary>
    public class TerrariumUpdateDto
    {
        /// <summary>
        /// Id terrarium
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New account id
        /// </summary>
        [Required]
        public long AccountId { get; init; }

        /// <summary>
        /// New name terrarium
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}