namespace CrealutionServer.Models.Dtos.Accounts
{
    /// <summary>
    /// Model for authorized account
    /// </summary>
    public class AccountAuthorizedDto
    {
        /// <summary>
        /// Token authorize
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Account info
        /// </summary>
        public AccountDto AccountInfo { get; set; }
    }
}