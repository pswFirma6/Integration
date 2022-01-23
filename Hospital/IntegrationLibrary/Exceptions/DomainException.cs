using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IntegrationLibrary.Exceptions
{
    [Serializable]
    public abstract class DomainException: Exception
    {
        public DomainException(string message): base(message)
        {
            
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    [Serializable]
    public class DomainNotFoundException: DomainException
    {
        public DomainNotFoundException(string message) : base(message)
        {

        }

        protected DomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message)
        {

        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class NotAuthenticatedException : DomainException
    {
        public NotAuthenticatedException(string message) : base(message) 
        {
        
        }

        protected NotAuthenticatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
