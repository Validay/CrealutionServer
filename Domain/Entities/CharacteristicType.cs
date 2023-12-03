using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class CharacteristicType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        //public virtual ICollection<CreatureCharacteristicType> CreatureCharacteristicTypes { get; private set; }

        protected CharacteristicType()
        {
        }

        public CharacteristicType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public CharacteristicType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //public void SetCreatureCharacteristicTypes(ICollection<CreatureCharacteristicType> creatureCharacteristicTypes)
        //{
        //    CreatureCharacteristicTypes = creatureCharacteristicTypes;
        //}
    }
}