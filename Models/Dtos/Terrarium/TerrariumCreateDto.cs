using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.Terrariums
{
    /// <summary>
    /// Model for create terrarium
    /// </summary>
    public class TerrariumCreateDto
    {
        /// <summary>
        /// Account id
        /// </summary>
        [Required]
        public long AccountId { get; init; }

        /// <summary>
        /// Name terrarium
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}