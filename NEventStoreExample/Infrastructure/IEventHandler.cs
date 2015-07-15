namespace NEventStoreExample.Infrastructure
{
    public interface IEventHandler { }

    public interface IEventHandler<in TEvent>: IEventHandler
    {
        void Handle(TEvent e);
    }
}