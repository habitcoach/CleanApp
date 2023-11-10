using Clean.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Core.Queries
{
    public abstract class Query<T>:Message<T>
    {
        public DateTime Timestamp { get; protected set; }

        public Query()
        {
            Timestamp = DateTime.Now;
        }
    }
}
