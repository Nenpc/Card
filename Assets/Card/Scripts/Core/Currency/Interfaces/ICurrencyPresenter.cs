using System.Collections.Generic;

namespace TheaCard.Core.Currency
{
    public interface ICurrencyPresenter
    {
        void Init(IReadOnlyList<CurrencyInfo> startCurrencies);
        bool TryTakeCurrency(Currencies currencyType, int amount);
        void GiveCurrency(Currencies currencyType, int amount);
        void ShowPanel();
        void HidePanel();
    }
}