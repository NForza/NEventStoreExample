using Autofac;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using NEventStore;
using NEventStore.Dispatcher;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStoreExample.Command;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace NEventStoreExample.UI
{
    public partial class App : Application
    {
        IStoreEvents store;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bus = BusSetup.StartWith<Conservative>()
                           .Apply<FlexibleSubscribeAdapter>(a =>
                           {
                               a.ByInterface(typeof(IEventHandler<>));
                               a.ByInterface(typeof(ICommandHandler<>));
                           }).Construct();

            store = WireupEventStore(bus);
                IContainer container = BuildContainer(bus, store);

            container.Resolve<IEnumerable<ICommandHandler>>().ToList().ForEach(h => bus.Subscribe(h));
            container.Resolve<IEnumerable<IEventHandler>>().ToList().ForEach(h => bus.Subscribe(h));

            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();

        }

        private static IContainer BuildContainer(IBus bus, IStoreEvents store)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder
                .RegisterAssemblyTypes(typeof(CreateAccountCommand).Assembly)
                .Where(t => t.IsAssignableTo<ICommandHandler>())
                .As<ICommandHandler>();

            builder
                .RegisterAssemblyTypes(typeof(AccountCreatedEvent).Assembly)
                .Where(t => t.IsAssignableTo<IEventHandler>())
                .As<IEventHandler>();

            builder.RegisterInstance<IRepository>(new EventStoreRepository(store, new AggregateFactory(), new ConflictDetector()));
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterInstance<IBus>(bus);

            IContainer container = builder.Build();
            return container;
        }

        private static IStoreEvents WireupEventStore(IBus bus)
        {
            return Wireup.Init()
                .UsingSqlPersistence("EventStoreExampleConnection")
                    .WithDialect(new MsSqlDialect())
                    .EnlistInAmbientTransaction()
                    .InitializeStorageEngine()
                    .UsingJsonSerialization()
                        .Compress()
                .UsingSynchronousDispatchScheduler()
                .DispatchTo(new DelegateMessageDispatcher(c => DelegateDispatcher.DispatchCommit(bus, c)))
                .Build();
        }
    }
}
