using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Exceptions
{
    public abstract class DomainException: Exception
    {
        public DomainException(string message): base(message)
        {

        }
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
