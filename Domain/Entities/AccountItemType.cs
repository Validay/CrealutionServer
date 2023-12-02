using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class AccountItemType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long AccountId { get; private set; }

        public long ItemTypeId { get; private set; }

        [Required]
        public int Count { get; private set; }

        [Required]
        public virtual Account Account { get; private set; }

        [Required]
        public virtual ItemType ItemType { get; private set; }

        protected AccountItemType()
        {
        }

        public AccountItemType(
            int count,
            Account account,
            ItemType itemType)
        {
            Count = count;
            Account = account;
            ItemType = itemType;
        }

        public AccountItemType(
            long id, 
            int count,
            Account account,
            ItemType itemType)
            : this(
                count,
                account,
                itemType)
        {
            Id = id;
        }

        public void Update(
            int count,
            Account account,
            ItemType itemType)
        {
            Count = count;
            Account = account;
            ItemType = itemType;
        }

        public void SetAccount(Account account)
        {
            Account = account;
        }

        public void SetItemType(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}