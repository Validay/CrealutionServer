using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.ItemTypes
{
    /// <summary>
    /// Model for get all item types
    /// </summary>
    public class ItemTypeGetAllDto
    {
        /// <summary>
        /// Collection item types
        /// </summary>
        public List<ItemTypeDto> ItemTypes { get; set; }
    }
}