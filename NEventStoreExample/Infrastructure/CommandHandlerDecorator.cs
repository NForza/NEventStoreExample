using CommonDomain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonDomain;
using NEventStore;

namespace NEventStoreExample.Infrastructure
{
    public class CommandHandlerDecorator<TCommand, TAggregateRoot> : ICommandHandler<TCommand, TAggregateRoot>
        where TAggregateRoot : IAggregate
        where TCommand : ICommand
    {
        ICommandHandler<TCommand, TAggregateRoot> commandHandler;
        IRepository repository;

        public CommandHandlerDecorator(ICommandHandler<TCommand, TAggregateRoot> commandHandler, IRepository repository)
        {
            this.repository = repository;
            this.commandHandler = commandHandler;
        }

        public IEnumerable<IEvent> Handle(TCommand command, TAggregateRoot aggregateRoot)
        {
            TAggregateRoot root = GetAggregateRoot(typeof(TAggregateRoot), command.ID);
            IEnumerable<IEvent> events = commandHandler.Handle(command, root);
            aggregateRoot.ApplyEvents(events);
            return events;
        }

        private TAggregateRoot GetAggregateRoot(Type type, Guid ID)
        {
            Type repoType = repository.GetType();
            MethodInfo method = repoType.GetMethods().First(mi => mi.IsGenericMethod && mi.GetParameters().Length == 1);
            MethodInfo specificMethod = method.MakeGenericMethod(type);
            return (TAggregateRoot)specificMethod.Invoke(repository, new object[] { ID });
        }
    }

    public class CommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
            where TCommand : ICommand
    {
        IStoreEvents storeEvents;
        ICommandHandler<TCommand> commandHandler;

        public CommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IStoreEvents storeEvents)
        {
            this.storeEvents = storeEvents;
            this.commandHandler = commandHandler;
        }

        public IEnumerable<IEvent> Handle(TCommand command)
        {
            IEnumerable<IEvent> events = commandHandler.Handle(command);
            using (var stream = storeEvents.OpenStream(command.ID, 0))
            {
                foreach( var someEvent in events)
                    stream.Add(new EventMessage() { Body = someEvent });
                stream.CommitChanges(command.ID);
            }
            return events;
        }
    }
}
