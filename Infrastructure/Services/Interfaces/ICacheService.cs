using System.Threading.Tasks;

namespace CrealutionServer.Infrastructure.Services.Interfaces
{
    public interface ICacheService
    {
        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Object T</returns>
        Task<T> GetData<T>(string key)
            where T : class;

        /// <summary>
        /// Set Data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        Task SetData<T>(
            string key, 
            T value, 
            double expirationTime)
            where T : class;

        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveData(string key);
    }
}