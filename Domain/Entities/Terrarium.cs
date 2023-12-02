using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class Terrarium
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long AccountId { get; private set; }

        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        [Required]
        public bool InGame { get; private set; }

        [Required]
        public virtual Account Account { get; private set; }

        protected Terrarium()
        {
        }

        public Terrarium(
            long id, 
            string name,
            bool inGame,
            Account account)
            : this(
                name, 
                inGame, 
                account)
        {
            Id = id;
        }

        public Terrarium(
            string name,
            bool inGame,
            Account account)
        {
            Name = name;
            InGame = inGame;
            Account = account;
        }

        public void Update(
            string name,
            bool inGame)
        {
            Name = name;
            InGame = inGame;
        }

        public void SetAccount(Account account)
        {
            Account = account;
        }
    }
}