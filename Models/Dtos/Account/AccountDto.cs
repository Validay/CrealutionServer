using CrealutionServer.Models.Dtos.AccountItemTypes;
using CrealutionServer.Models.Dtos.Roles;
using CrealutionServer.Models.Dtos.Terrariums;
using System;
using System.Collections.Generic;

namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for account
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Id account
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name account
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// Account roles
        /// </summary>
        public List<RoleDto> Roles { get; set; }

        /// <summary>
        /// Account terrariums
        /// </summary>
        public List<TerrariumLightDto> Terrariums { get; set; }

        /// <summary>
        /// Account items and count
        /// </summary>
        public List<AccountItemTypeDto> AccountItemTypes { get; set; }
    }
}