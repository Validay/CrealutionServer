using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.BodyTypes
{
    /// <summary>
    /// Model for create body type
    /// </summary>
    public class BodyTypeCreateDto
    {
        /// <summary>
        /// Name body type
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