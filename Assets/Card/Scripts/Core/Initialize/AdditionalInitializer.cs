using TheaCard.Core.Currency;
using TheaCard.Core.Progress;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class AdditionalInitializer : IInitializable
    {
        private readonly ICurrencyPresenter _currencyPresenter;
        private readonly CurrencyConfig _currencyConfig;
        
        private readonly IProgressPresenter _progressPresenter;
        
        public AdditionalInitializer(ICurrencyPresenter currencyPresenter, 
            CurrencyConfig currencyConfig,
            IProgressPresenter progressPresenter)
        {
            _currencyPresenter = currencyPresenter;
            _currencyConfig = currencyConfig;
            _progressPresenter = progressPresenter;
        }

        public void Initialize()
        {
            _currencyPresenter.Init(_currencyConfig.StartCurrencies);

            _progressPresenter.Init();
        }
    }
}