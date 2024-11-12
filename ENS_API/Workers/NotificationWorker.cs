using ENS_API.Data;
using ENS_API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ENS_API.Workers
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public NotificationWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ENSDbContext>();
                    var _emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                    var notifications = _context.Notifications.Where(x => x.Status == false).ToList();
                    foreach (var notification in notifications)
                    {
                        await _emailService.SendEmailAsync(notification.Email, "ENS(built by Tim)", notification.Text);
                        _context.Notifications.Find(notification.NotificationId).Status = true;
                        _context.SaveChanges();
                    }
                    _context.Notifications.RemoveRange(notifications);
                    _context.SaveChanges();
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }
    }
}
