using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.BodyTypes
{
    /// <summary>
    /// Model for update body types
    /// </summary>
    public class BodyTypeUpdateDto
    {
        /// <summary>
        /// Id body types
        /// </summary>
        [Required]
        public long Id { get; init; }

        /// <summary>
        /// New name body types
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }

        /// <summary>
        /// Image data
        /// </summary>
        [Required]
        public byte[] ImageData { get; init; }
    }
}