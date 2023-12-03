using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.BehaviorTypes
{
    /// <summary>
    /// Model for create behavior type
    /// </summary>
    public class BehaviorTypeCreateDto
    {
        /// <summary>
        /// Name zone type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}