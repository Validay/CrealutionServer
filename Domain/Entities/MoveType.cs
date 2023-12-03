using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class MoveType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        //public virtual ICollection<CreatureMoveType> CreatureMoveTypes { get; private set; }

        protected MoveType()
        {
        }

        public MoveType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public MoveType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //public void SetCreatureMoveTypes(ICollection<CreatureMoveType> creatureMoveTypeTypes)
        //{
        //    CreatureMoveTypes = creatureMoveTypeTypes;
        //}
    }
}