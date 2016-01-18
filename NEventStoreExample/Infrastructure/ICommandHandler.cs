using System.Collections.Generic;
using CommonDomain;

namespace NEventStoreExample.Infrastructure
{
    public interface ICommandHandler 
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
    {
        IEnumerable<IEvent> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, in TAggregateType> : ICommandHandler where TAggregateType: IAggregate
    {
        IEnumerable<IEvent> Handle(TCommand command, TAggregateType aggregateRoot);
    }
}