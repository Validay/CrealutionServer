using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class BehaviorType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        //public virtual ICollection<CreatureBehaviorType> CreatureBehaviorTypes { get; private set; }

        protected BehaviorType()
        {
        }

        public BehaviorType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public BehaviorType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //public void SetCreatureBehaviorTypes(ICollection<CreatureBehaviorType> creatureBehaviorTypeTypes)
        //{
        //    CreatureBehaviorTypes = creatureBehaviorTypeTypes;
        //}
    }
}