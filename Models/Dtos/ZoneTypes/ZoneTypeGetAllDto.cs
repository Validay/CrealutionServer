using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.ZoneTypes
{
    /// <summary>
    /// Model for get all zone types
    /// </summary>
    public class ZoneTypeGetAllDto
    {
        /// <summary>
        /// Collection zone types
        /// </summary>
        public List<ZoneTypeDto> ZoneTypes { get; set; }
    }
}