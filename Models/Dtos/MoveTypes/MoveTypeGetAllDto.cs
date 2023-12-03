using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.MoveTypes
{
    /// <summary>
    /// Model for get all move types
    /// </summary>
    public class MoveTypeGetAllDto
    {
        /// <summary>
        /// Collection move types
        /// </summary>
        public List<MoveTypeDto> MoveTypes { get; set; }
    }
}