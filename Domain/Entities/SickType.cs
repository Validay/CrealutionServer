using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class SickType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        //public virtual ICollection<CreatureSickType> CreatureSickTypes { get; private set; }

        protected SickType()
        {
        }

        public SickType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public SickType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //public void SetCreatureSickTypes(ICollection<CreatureSickType> creatureSickTypeTypes)
        //{
        //    CreatureSickTypes = creatureSickTypeTypes;
        //}
    }
}