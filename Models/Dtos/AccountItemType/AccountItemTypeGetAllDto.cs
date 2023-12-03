using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.AccountItemTypes
{
    /// <summary>
    /// Model for get all account item types
    /// </summary>
    public class AccountItemTypeGetAllDto
    {
        /// <summary>
        /// Collection account item types
        /// </summary>
        public List<AccountItemTypeDto> AccountItemTypes { get; set; }
    }
}