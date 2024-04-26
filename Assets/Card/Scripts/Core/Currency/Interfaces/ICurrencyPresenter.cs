using System.Collections.Generic;

namespace TheaCard.Core.Currency
{
    public interface ICurrencyPresenter
    {
        void Init(IReadOnlyList<CurrencyInfo> startCurrencies);
        void SetDefaultCurrencies();
        bool TryTakeCurrency(Currencies currencyType, int amount);
        void GiveCurrency(Currencies currencyType, int amount);
        void ShowPanel();
        void HidePanel();
    }
}