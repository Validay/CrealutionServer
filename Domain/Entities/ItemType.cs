using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class ItemType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        public virtual ICollection<AccountItemType> AccountItemTypes { get; private set; }

        protected ItemType()
        {
        }

        public ItemType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public ItemType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SetAccountItemTypes(ICollection<AccountItemType> accountItemTypes)
        {
            AccountItemTypes = accountItemTypes;
        }
    }
}