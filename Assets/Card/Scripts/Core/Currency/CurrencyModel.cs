using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Core.Currency
{
    public sealed class CurrencyModel : ICurrencyModel
    {
        private Dictionary<Currencies, int> _currencyInfos = new Dictionary<Currencies, int>();

        public IReadOnlyDictionary<Currencies, int> CurrencyInfos => _currencyInfos;

        public void AddNewCurrency(Currencies currencyType, int amount = 0)
        {
            if (!_currencyInfos.TryAdd(currencyType, amount))
                Debug.LogWarning($"Currency {currencyType} already added!");
        }

        public void GiveCurrency(Currencies currencyType, int amount)
        {
            if (amount < 0)
                Debug.LogWarning($"Can't add negative values!");

            AddCurrency(currencyType, amount);
        }

        public bool TryTakeCurrency(Currencies currencyType, int amount)
        {
            if (amount < 0)
                Debug.LogWarning($"Can't subtract negative values!");

            if (!_currencyInfos.ContainsKey(currencyType) || _currencyInfos[currencyType] < amount)
                return false;
                
            AddCurrency(currencyType, -amount);
            return true;
        }
        
        public void SetCurrencyValue(Currencies currencyType, int amount)
        {
            if (!_currencyInfos.ContainsKey(currencyType))
                _currencyInfos.Add(currencyType, amount);
            else
                _currencyInfos[currencyType] = amount;
        }

        private void AddCurrency(Currencies currencyType, int amount)
        {
            if (!_currencyInfos.ContainsKey(currencyType))
                _currencyInfos.Add(currencyType, 0);

            _currencyInfos[currencyType] += amount;
        }
    }
}