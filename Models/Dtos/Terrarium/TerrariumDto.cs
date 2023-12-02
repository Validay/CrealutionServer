using CrealutionServer.Models.Dtos.Accounts;

namespace CrealutionServer.Models.Dtos.Terrariums
{
    /// <summary>
    /// Model for terrarium
    /// </summary>
    public class TerrariumDto
    {
        /// <summary>
        /// Id terrarium
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name terrarium
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account info
        /// </summary>
        public AccountLightDto AccountLightInfo { get; set; }
    }
}