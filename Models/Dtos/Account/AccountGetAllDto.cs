using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for get all account
    /// </summary>
    public class AccountGetAllDto
    {
        /// <summary>
        /// Collection accounts
        /// </summary>
        public List<AccountDto> Accounts { get; set; }
    }
}