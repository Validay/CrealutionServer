using System;

namespace CrealutionServer.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity is not found
    /// </summary>
    [Serializable]
    public class CrealutionEntityNotFound: Exception
    {
        public CrealutionEntityNotFound()
        { 
        }

        public CrealutionEntityNotFound(string message) 
            : base(message)
        {
        }

        public CrealutionEntityNotFound(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}