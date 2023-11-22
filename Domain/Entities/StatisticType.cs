using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class StatisticType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        public virtual ICollection<CreatureStatisticType> CreatureStatisticTypes { get; private set; }

        protected StatisticType()
        {
        }

        public StatisticType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}