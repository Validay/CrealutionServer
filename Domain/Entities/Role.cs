using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class Role
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        public virtual ICollection<Account> Accounts { get; private set; }

        protected Role()
        {
        }

        public Role(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public Role(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SetAccounts(ICollection<Account> accounts)
        {
            Accounts = accounts;
        }
    }
}