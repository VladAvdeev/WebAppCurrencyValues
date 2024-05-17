using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text.Json;
using WebAppCurrencyValues.Models;

namespace WebAppCurrencyValues.Services
{
    public class CurrencyService : ICurrencyService
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ValuteModel> GetValuteById(string id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("CurrencyUrl").ToString());
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(client.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                var dailyCurrency = await JsonSerializer.DeserializeAsync<CurrencyModel>(responseStream);

                var currencyByCountry = dailyCurrency.Valute
                    .GetType()
                    .GetProperties()
                    .Select(property => dailyCurrency.Valute
                        .GetType()
                        .GetProperty(property.Name)
                        .GetValue(dailyCurrency.Valute, null) as ValuteModel)
                        .FirstOrDefault(x => x.ID == id);

                return currencyByCountry;
            }
            else
                return null;
           
        }

        public async Task<IEnumerable<ValuteModel>> GetValuteModels()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetConnectionString("CurrencyUrl").ToString());
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(client.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                var dailyCurrency = await JsonSerializer.DeserializeAsync<CurrencyModel>(responseStream);

                var currencies = dailyCurrency.Valute
                    .GetType()
                    .GetProperties()
                    .Select(property => 
                        dailyCurrency.Valute
                        .GetType()
                        .GetProperty(property.Name)
                        .GetValue(dailyCurrency.Valute, null) as ValuteModel);

                return currencies;
            }
            else
            {
                return null;
            }
        }
    }
}
