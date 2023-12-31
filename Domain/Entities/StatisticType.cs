﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class StatisticType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Key]
        [Required]
        [StringLength(255)]
        public string Name { get; private set; }

        public virtual ICollection<CreatureStatisticType> CreatureStatisticTypes { get; private set; }

        protected StatisticType()
        {
        }

        public StatisticType(
            long id, 
            string name)
            : this(name)
        {
            Id = id;
        }

        public StatisticType(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SetCreatureStatisticTypes(ICollection<CreatureStatisticType> creatureStatisticTypes)
        {
            CreatureStatisticTypes = creatureStatisticTypes;
        }
    }
}