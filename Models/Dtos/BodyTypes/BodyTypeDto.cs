namespace CrealutionServer.Models.Dtos.BodyTypes
{
    /// <summary>
    /// Model for body type
    /// </summary>
    public class BodyTypeDto
    {
        /// <summary>
        /// Id body type
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name body type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Image data
        /// </summary>
        public byte[] ImageData { get; init; }
    }
}