using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Helpers.CurrencyExchange
{
    public class CurrencyExchangeService
    {
        private readonly HttpClient _httpClient;

        public CurrencyExchangeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            var url = $"https://api.frankfurter.app/latest?amount={amount}&from={fromCurrency}&to={toCurrency}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var rates = doc.RootElement.GetProperty("rates");

            if (!rates.TryGetProperty(toCurrency.ToUpper(), out var rate))
                throw new KeyNotFoundException($"Currency '{toCurrency}' not found in response.");

            return rate.GetDecimal();
        }
    }
}
