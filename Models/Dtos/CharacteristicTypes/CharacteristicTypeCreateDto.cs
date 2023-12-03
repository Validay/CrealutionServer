using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.CharacteristicTypes
{
    /// <summary>
    /// Model for create characteristic type
    /// </summary>
    public class CharacteristicTypeCreateDto
    {
        /// <summary>
        /// Name characteristic type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}