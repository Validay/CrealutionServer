using System.ComponentModel.DataAnnotations;

namespace CrealutionServer.Models.Dtos.StatisticTypes
{
    /// <summary>
    /// Model for update statistic type
    /// </summary>
    public class StatisticTypeUpdateDto
    {
        /// <summary>
        /// Id statistic type
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Name statistic type
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}