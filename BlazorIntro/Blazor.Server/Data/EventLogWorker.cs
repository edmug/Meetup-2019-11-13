using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blazor.Server.Data
{
    public class EventLogWorker : BackgroundService
    {
        private readonly ILogger<EventLogWorker> _logger;
        private readonly IMemoryCache _cache;
        private readonly EventLogService _service;

        public EventLogWorker(ILogger<EventLogWorker> logger, IMemoryCache cache, EventLogService service)
        {
            _logger = logger;
            _cache = cache;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _cache.TryGetValue<int>(EventLogWorkerConstants.EventLogCacheSize, out var cacheSize);
                    if (cacheSize < 1) cacheSize = 10;
                    if (cacheSize > 100) cacheSize = 100;

                    _cache.TryGetValue<int>(EventLogWorkerConstants.EventLogCacheDelay, out var cacheDelay);
                    if (cacheDelay == 0) cacheDelay = 3;
                    if (cacheDelay < 1) cacheDelay = 1;
                    if (cacheDelay > 60) cacheDelay = 60;

                    var items = await _service.GetEventLogsAsync(cacheSize);
                    _cache.Set(EventLogWorkerConstants.EventLogCache, items);

                    await Task.Delay(TimeSpan.FromSeconds(cacheDelay), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unexpected error fetching event log data: {ex}", ex);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
    public static class EventLogWorkerConstants
    {
        public const string EventLogCacheSize = "EventLogCacheSize";
        public const string EventLogCacheDelay = "EventLogCacheDelay";
        public const string EventLogCache = "EventLogCache";
    }
}
