using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.BehaviorTypes
{
    /// <summary>
    /// Model for get all behavior types
    /// </summary>
    public class BehaviorTypeGetAllDto
    {
        /// <summary>
        /// Collection behavior types
        /// </summary>
        public List<BehaviorTypeDto> BehaviorTypes { get; set; }
    }
}