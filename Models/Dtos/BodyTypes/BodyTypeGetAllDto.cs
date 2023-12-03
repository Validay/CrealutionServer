using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.BodyTypes
{
    /// <summary>
    /// Model for get all body types
    /// </summary>
    public class BodyTypeGetAllDto
    {
        /// <summary>
        /// Collection body types
        /// </summary>
        public List<BodyTypeDto> BodyTypes { get; set; }
    }
}