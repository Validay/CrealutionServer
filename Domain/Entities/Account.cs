using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class Account
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        [Required]
        [StringLength(255)]
        public string DisplayName { get; private set; }

        [Required]
        [StringLength(255)]
        public string Password { get; private set; }

        [Required]
        public bool InGame { get; private set; }

        [Required]
        public bool InBanned { get; private set; }

        [Required]
        public DateTime CreateDate { get; private set; }

        public DateTime? LastLoginDate { get; private set; }

        [Required]
        public virtual ICollection<Role> Roles { get; private set; }

        [Required]
        public virtual ICollection<Terrarium> Terrariums { get; private set; }

        [Required]
        public virtual ICollection<AccountItemType> AccountItemTypes { get; private set; }

        protected Account()
        {
            CreateDate = DateTime.UtcNow;
        }

        public Account(
            long id,
            string name,
            string displayName,
            string password,
            bool inGame,
            bool inBanned)
            : this(
                name,
                displayName,
                password,
                inGame,
                inBanned)
        {
            Id = id;
        }

        public Account(
            string name,
            string displayName,
            string password,
            bool inGame,
            bool inBanned)
        {
            Name = name;
            DisplayName = displayName;
            Password = password;
            InGame = inGame;
            InBanned = inBanned;
            CreateDate = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string displayName,
            string password,
            bool inGame,
            bool inBanned,
            DateTime lastLoginDate)
        {
            Name = name;
            DisplayName = displayName;
            Password = password;
            InGame = inGame;
            InBanned = inBanned;
            LastLoginDate = lastLoginDate;
        }

        public void SetRoles(ICollection<Role> roles)
        {
            Roles = roles;
        }

        public void SetTerrariums(ICollection<Terrarium> terrariums)
        {
            Terrariums = terrariums;
        }

        public void SetAccountItemTypes(ICollection<AccountItemType> accountItemTypes)
        {
            AccountItemTypes = accountItemTypes;
        }
    }
}