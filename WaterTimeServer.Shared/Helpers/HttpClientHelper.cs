using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WaterTimeServer.Shared.Helpers
{
    /// <summary>
    /// Serviço HTTP estático centralizado para todas as requisições da aplicação.
    /// </summary>
    public static class HttpHelper
    {
        private static readonly HttpClient _clienteHttp;

        static HttpHelper()
        {
            _clienteHttp = new HttpClient
            {
            };

            _clienteHttp.DefaultRequestHeaders.Accept.Clear();
            _clienteHttp.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Caso precise de autenticação:
            // _clienteHttp.DefaultRequestHeaders.Authorization =
            //     new AuthenticationHeaderValue("Bearer", "<token>");
        }

        /* ---------- Métodos públicos de conveniência ---------- */

        public static Task<T?> ObterJsonAsync<T>(
            string caminho,
            CancellationToken tokenCancelamento = default)
        {
            return EnviarAsync<T>(HttpMethod.Get, caminho, null, tokenCancelamento);
        }

        public static Task<HttpResponseMessage> ObterAsync(
            string caminho,
            CancellationToken tokenCancelamento = default)
        {
            return _clienteHttp.GetAsync(caminho, tokenCancelamento);
        }

        public static Task<T?> PostarJsonAsync<T>(
        string caminho,
        T conteudo,
       CancellationToken tokenCancelamento = default)
        {
            return EnviarAsync<T>(HttpMethod.Post, caminho, conteudo, tokenCancelamento);
        }

        public static Task<TResposta?> PostarJsonAsync<TEnvio, TResposta>(
            string caminho,
            TEnvio conteudo,
            CancellationToken tokenCancelamento = default)
            where TResposta : class
        {
            return EnviarAsync<TResposta>(HttpMethod.Post, caminho, conteudo, tokenCancelamento);
        }

        private static async Task<T?> EnviarAsync<T>(
            HttpMethod metodo,
            string caminho,
            object? corpo,
            CancellationToken tokenCancelamento)
        {
            using var requisicao = new HttpRequestMessage(metodo, caminho);

            if (corpo is not null)
            {
                var json = JsonSerializer.Serialize(
                    corpo,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                requisicao.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var resposta = await _clienteHttp.SendAsync(requisicao, tokenCancelamento)
                                             .ConfigureAwait(false);

            resposta.EnsureSuccessStatusCode(); // lança exceção em 4xx/5xx

            // HEAD ou 204 não têm corpo; evita desserialização vazia
            if (resposta.Content.Headers.ContentLength == 0)
                return default;

            await using var fluxo = await resposta.Content.ReadAsStreamAsync(tokenCancelamento)
                                                          .ConfigureAwait(false);

            return await JsonSerializer.DeserializeAsync<T>(
                       fluxo,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
                       tokenCancelamento)
                   .ConfigureAwait(false);
        }

        /* ---------- Ajustes externos opcionais ---------- */

        /// <summary>
        /// Permite configurar o HttpClient singleton no startup,
        /// como por exemplo inserir um token de autenticação vigente.
        /// </summary>
        public static void Configurar(Action<HttpClient> configuracao) => configuracao(_clienteHttp);
    }
}
