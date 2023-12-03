using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.SickTypes
{
    /// <summary>
    /// Model for get all sick types
    /// </summary>
    public class SickTypeGetAllDto
    {
        /// <summary>
        /// Collection sick types
        /// </summary>
        public List<SickTypeDto> SickTypes { get; set; }
    }
}