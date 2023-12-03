using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.SickTypes
{
    /// <summary>
    /// Model for create sick type
    /// </summary>
    public class SickTypeCreateDto
    {
        /// <summary>
        /// Name zone type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}