using CrealutionServer.Models.Dtos.ItemTypes;

namespace CrealutionServer.Models.Dtos.AccountItemTypes
{
    /// <summary>
    /// Model for account item type
    /// </summary>
    public class AccountItemTypeDto
    {
        /// <summary>
        /// Id account item type
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Count item
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// item type dto
        /// </summary>
        public ItemTypeDto ItemType { get; set; }
    }
}