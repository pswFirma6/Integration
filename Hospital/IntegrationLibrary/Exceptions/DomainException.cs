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

    public class DomainNotFoundException: DomainException
    {
        public DomainNotFoundException(string message): base(message)
        {

        }
    }

    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message)
        {

        }
    }

}
