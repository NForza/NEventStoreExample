namespace NEventStoreExample.Infrastructure
{
    public interface ICommandHandler 
    {
    }

    public interface ICommandHandler<in TCommand>: ICommandHandler
    {
        void Handle(TCommand command);
    }
}