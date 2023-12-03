using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.BehaviorTypes
{
    /// <summary>
    /// Model for update behavior types
    /// </summary>
    public class BehaviorTypeUpdateDto
    {
        /// <summary>
        /// Id behavior types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name behavior types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}