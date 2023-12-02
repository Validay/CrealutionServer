using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.ItemTypes
{
    /// <summary>
    /// Model for create item type
    /// </summary>
    public class ItemTypeCreateDto
    {
        /// <summary>
        /// Name item type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}