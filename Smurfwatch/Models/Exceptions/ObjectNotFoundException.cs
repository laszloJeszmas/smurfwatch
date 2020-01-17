using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smurfwatch.Models.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message)
        {

        }

        public ObjectNotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
