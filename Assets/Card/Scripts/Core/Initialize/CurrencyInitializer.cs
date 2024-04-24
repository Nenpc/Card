using TheaCard.Core.Currency;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class CurrencyInitializer : IInitializable
    {
        private ICurrencyPresenter _currencyPresenter;
        private CurrencyConfig _currencyConfig;
        
        public CurrencyInitializer(ICurrencyPresenter currencyPresenter, CurrencyConfig currencyConfig)
        {
            _currencyPresenter = currencyPresenter;
            _currencyConfig = currencyConfig;
        }

        public void Initialize()
        {
            _currencyPresenter.Init(_currencyConfig.StartCurrencies);
        }
    }
}