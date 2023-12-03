using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.CharacteristicTypes
{
    /// <summary>
    /// Model for update characteristic type
    /// </summary>
    public class CharacteristicTypeUpdateDto
    {
        /// <summary>
        /// Id characteristic type
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name characteristic type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}