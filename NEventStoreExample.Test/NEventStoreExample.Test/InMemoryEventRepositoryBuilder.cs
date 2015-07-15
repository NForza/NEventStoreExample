using System;
using System.Collections.Generic;
using CommonDomain.Core;
using NEventStoreExample.Infrastructure;

namespace NEventStoreExample.Test
{
    class InMemoryEventRepositoryBuilder : TestDataBuilder<InMemoryEventRepository>
    {
        private List<AggregateBase> objects = new List<AggregateBase>();
 
        public override InMemoryEventRepository Build()
        {
            var repo = new InMemoryEventRepository(new List<IEvent>(), new AggregateFactory());
            objects.ForEach(o => { repo.Save(o, Guid.NewGuid()); });
            return repo;
        }

        public InMemoryEventRepositoryBuilder WithAggregates(params AggregateBase[] aggregates)
        {
            objects.AddRange(aggregates);
            return this;
        }

    }
}
