using Confluent.Kafka;
using ENS_API.Data;
using ENS_API.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ENS_API.Workers
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly string _groupId;
        public NotificationWorker(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
            _groupId = configuration["Kafka:GroupId"];
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe(_topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));
                if (consumeResult != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var message = JsonConvert.DeserializeObject<Notification>(consumeResult.Message.Value);
                        var _context = scope.ServiceProvider.GetRequiredService<ENSDbContext>();
                        var _emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                        await _emailService.SendEmailAsync(message.Email, "ENS(built by Tim)", message.Text);
                        _context.Notifications.Find(message.NotificationId).Status = true;
                        await _context.SaveChangesAsync();
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(40), stoppingToken);
            }
        }
    }
}
