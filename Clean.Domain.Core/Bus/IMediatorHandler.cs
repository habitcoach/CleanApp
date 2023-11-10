using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        public Task<T> SendCommand<T>(IRequest<T> commandOrQuery);
    }
}
