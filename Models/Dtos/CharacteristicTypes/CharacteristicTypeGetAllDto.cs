using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.CharacteristicTypes
{
    /// <summary>
    /// Model for get all characteristic type
    /// </summary>
    public class CharacteristicTypeGetAllDto
    {
        /// <summary>
        /// Collection characteristic types
        /// </summary>
        public List<CharacteristicTypeDto> CharacteristicTypeDtos { get; set; }
    }
}