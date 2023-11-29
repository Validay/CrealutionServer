using System;

namespace CrealutionServer.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity already exist
    /// </summary>
    [Serializable]
    public class CrealutionEntityValidateException: Exception
    {
        public CrealutionEntityValidateException()
        { 
        }

        public CrealutionEntityValidateException(string message) 
            : base(message)
        {
        }

        public CrealutionEntityValidateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}