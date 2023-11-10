using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Core.Events
{
    public abstract class Message<T> :IRequest<T>
    {
        public string MessageType { get; protected set; }

        
        protected Message() { 
        
            MessageType = typeof(T).Name;

        }
    }
}
