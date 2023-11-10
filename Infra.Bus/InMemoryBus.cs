using Clean.Domain.Core.Bus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {

            _mediator = mediator;
        }
        public Task<T> SendCommandOrQuery<T>(IRequest<T> commandOrQuery)
        {
            return _mediator.Send(commandOrQuery);
        }
    }
}
