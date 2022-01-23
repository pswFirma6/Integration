using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IntegrationLibrary.Exceptions
{
    [Serializable]
    public abstract class DomainException: Exception
    {
        protected DomainException(string message): base(message)
        {
            
        }

        //protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    [Serializable]
    public class DomainNotFoundException: DomainException
    {
        protected DomainNotFoundException(string message): base(message)
        {

        }
    }

    [Serializable]
    public class ValidationException : DomainException
    {
        protected ValidationException(string message) : base(message)
        {

        }
    }

    [Serializable]
    public class NotAuthenticatedException : DomainException
    {
        protected NotAuthenticatedException(string message) : base(message) 
        {
        
        }
    }

}
