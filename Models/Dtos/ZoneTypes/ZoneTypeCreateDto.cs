using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.ZoneTypes
{
    /// <summary>
    /// Model for create zone type
    /// </summary>
    public class ZoneTypeCreateDto
    {
        /// <summary>
        /// Name zone type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}