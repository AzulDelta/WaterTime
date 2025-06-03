using Microsoft.Extensions.Hosting;

namespace WaterTimeServer.Application.Services
{
    public class ServicoNotificacaoWorker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServicoNotificacaoWorker(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpClient http = _httpClientFactory.CreateClient("interno");

            while (!stoppingToken.IsCancellationRequested)
            {
                //_ = await http.PostAsync("api/push/send", null, stoppingToken);
                //Console.WriteLine("Enviando notificações...");

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
