using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.StatisticTypes
{
    /// <summary>
    /// Model for create statistic type
    /// </summary>
    public class StatisticTypeCreateDto
    {
        /// <summary>
        /// Name statistic type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; init; }
    }
}