using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class ZoneType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        //public virtual ICollection<CreatureZoneType> CreatureZoneTypes { get; private set; }

        protected ZoneType()
        {
        }

        public ZoneType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public ZoneType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //public void SetCreatureZoneTypes(ICollection<CreatureZoneType> creatureZoneTypeTypes)
        //{
        //    CreatureZoneTypes = creatureZoneTypeTypes;
        //}
    }
}