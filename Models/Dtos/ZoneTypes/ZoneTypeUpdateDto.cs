using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.ZoneTypes
{
    /// <summary>
    /// Model for update zone types
    /// </summary>
    public class ZoneTypeUpdateDto
    {
        /// <summary>
        /// Id zone types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name zone types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}