using System;

namespace CrealutionServer.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity already exist
    /// </summary>
    [Serializable]
    public class CrealutionEntityValidateError: Exception
    {
        public CrealutionEntityValidateError()
        { 
        }

        public CrealutionEntityValidateError(string message) 
            : base(message)
        {
        }

        public CrealutionEntityValidateError(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}