using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.ItemTypes
{
    /// <summary>
    /// Model for update item types
    /// </summary>
    public class ItemTypeUpdateDto
    {
        /// <summary>
        /// Id item types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name item types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}