using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.StatisticTypes
{
    /// <summary>
    /// Model for get all statistic type
    /// </summary>
    public class StatisticTypeGetAllDto
    {
        /// <summary>
        /// Collection statistic types
        /// </summary>
        public List<StatisticTypeDto> StatisticTypeDtos { get; set; }
    }
}