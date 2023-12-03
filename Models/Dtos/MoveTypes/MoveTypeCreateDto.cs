using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.MoveTypes
{
    /// <summary>
    /// Model for create move type
    /// </summary>
    public class MoveTypeCreateDto
    {
        /// <summary>
        /// Name move type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}