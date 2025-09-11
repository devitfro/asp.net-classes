using System;
using System.Collections.Generic;
using System.Linq;

namespace app_hw_exchange.Services
{
    public enum Currency
    {
        USD,
        EUR,
        UAH
    }

    public class ExchangeRateService
    {
        private readonly Dictionary<Currency, float> _rates = new();

        public ExchangeRateService()
        {
            _rates[Currency.USD] = 36.9f;
            _rates[Currency.EUR] = 40.2f;
            _rates[Currency.UAH] = 1f;
        }

        public float GetRate(Currency from, Currency to)
        {
            if (!_rates.ContainsKey(from) || !_rates.ContainsKey(to))
                return 0;

            float rateFromUAH = _rates[from];
            float rateToUAH = _rates[to];

            return rateFromUAH / rateToUAH;
        }

        public List<Currency> GetAvailableCurrencies()
        {
            return _rates.Keys.ToList();
        }
    }
}
