using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.Terrariums
{
    /// <summary>
    /// Model for get all terrariums
    /// </summary>
    public class TerrariumGetAllDto
    {
        /// <summary>
        /// Collection terrariums
        /// </summary>
        public List<TerrariumDto> Terrariums { get; set; }
    }
}