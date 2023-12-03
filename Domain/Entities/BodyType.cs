using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class BodyType
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
        public byte[] ImageData { get; private set; }

        //public virtual ICollection<CreatureBodyType> CreatureBodyTypes { get; private set; }

        protected BodyType()
        {
        }

        public BodyType(
            long id, 
            string name,
            byte[] imageData)
            : this(
                  name, 
                  imageData)
        {
            Id = id;
        }

        public BodyType(
            string name, 
            byte[] imageData)
        {
            Name = name;
            ImageData = imageData;
        }

        public void Update(
            string name,
            byte[] imageData)
        {
            Name = name;
            ImageData = imageData;
        }

        //public void SetCreatureBodyTypes(ICollection<CreatureBodyType> creatureBodyTypeTypes)
        //{
        //    CreatureBodyTypes = creatureBodyTypeTypes;
        //}
    }
}