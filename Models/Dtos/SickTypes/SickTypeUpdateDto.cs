using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.SickTypes
{
    /// <summary>
    /// Model for update sick types
    /// </summary>
    public class SickTypeUpdateDto
    {
        /// <summary>
        /// Id sick types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name sick types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}