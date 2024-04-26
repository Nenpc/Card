using System.Collections.Generic;

namespace TheaCard.Core.Currency
{
    public interface ICurrencyModel
    {
        IReadOnlyDictionary<Currencies, int> CurrencyInfos { get; }
        void AddNewCurrency(Currencies currencyType, int amount);
        void GiveCurrency(Currencies currencyType, int amount);
        bool TryTakeCurrency(Currencies currencyType, int amount);
        void SetCurrencyValue(Currencies currencyType, int amount);
    }
}