using Notificator;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        Console.WriteLine("START NOTIFICATOR");
        services.AddSingleton<INotificationHandler, NotificationHandler>();
        services.AddHostedService<Worker>();
        services.AddSingleton<RabbitMq>();
    })
    .Build();

await host.RunAsync();
