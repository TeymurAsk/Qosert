
using ENS_API.Data;

namespace ENS_API.Workers
{
    public class NotificationCleanUpWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public NotificationCleanUpWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateAsyncScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ENSDbContext>();
                    _context.Notifications.RemoveRange(_context.Notifications.Where(x => x.Status == true).ToList());
                    await _context.SaveChangesAsync();
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
    }
}
