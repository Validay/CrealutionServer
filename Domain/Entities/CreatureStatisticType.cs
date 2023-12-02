using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrealutionServer.Domain.Entities
{
    public class CreatureStatisticType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long StatisticTypeId { get; private set; }

        public string Value { get; private set; }

        [Required]
        public virtual StatisticType StatisticType { get; private set; }

        protected CreatureStatisticType()
        { 
        }

        public CreatureStatisticType(
            long id,
            string value,
            StatisticType statisticType)
            : this(
                value,
                statisticType)
        {
            Id = id;
        }

        public CreatureStatisticType(
            string value, 
            StatisticType statisticType)
        {
            Value = value;
            StatisticType = statisticType;
        }

        public void Update(
            string value,
            StatisticType statisticType)
        {
            Value = value;
            StatisticType = statisticType;
        }

        public void SetStatisticType(StatisticType statisticType)
        {
            StatisticType = statisticType;
        }
    }
}