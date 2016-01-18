using System;
using CommonDomain;

namespace NEventStoreExample.Infrastructure
{
    public interface ICommand
    {
        Guid ID { get; set; } 
    }
}