using System;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for account light info
    /// </summary>
    public class AccountLightDto
    {
        /// <summary>
        /// Id account
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name display account
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Has ban
        /// </summary>
        public bool InBanned { get; set; }

        /// <summary>
        /// Creation date account
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}