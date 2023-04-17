using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        /// <summary>
        /// Not found exception constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NotFoundException(string message) : base(message) { }
    }
}
