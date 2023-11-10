using Clean.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Core.Commands
{
    public abstract class Command:Message<bool>
    {
        public DateTime Timestamp { get; set; }

        public Command()
        {
                Timestamp = DateTime.Now;
        }
    }
}
