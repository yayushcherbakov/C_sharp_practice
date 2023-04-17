using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Entities
{
    public class Letter
    {
        /// <summary>
        /// Letter subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Letter message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Letter sender id.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Letter receiver id.
        /// </summary>
        public string ReceiverId { get; set; }
    }
}
