using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Caching.Memory;
using Zadanie.Models;

namespace Zadanie.Jobs
{
    public class SendDataServices : IHostedService
    {
        private Timer _timer;
        private readonly IMemoryCache _cache;

        public SendDataServices(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendData, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }

        private void SendData(object state)
        {
            Console.WriteLine("Data : \n" + _cache.Get<List<DataModelDto>>("MemoryData"));
            Thread.Sleep(2000);
        }
    }
}
