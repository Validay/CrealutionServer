using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.MoveTypes
{
    /// <summary>
    /// Model for update move types
    /// </summary>
    public class MoveTypeUpdateDto
    {
        /// <summary>
        /// Id move types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name move types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}