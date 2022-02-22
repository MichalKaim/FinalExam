using Notificator;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<INotificationHandler, NotificationHandler>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
