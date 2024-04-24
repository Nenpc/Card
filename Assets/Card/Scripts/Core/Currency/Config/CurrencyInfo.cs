using System;
using UnityEngine;

namespace TheaCard.Core.Currency
{
    [Serializable]
    public sealed class CurrencyInfo
    {
        [SerializeField] private Currencies _currency;
        [SerializeField] private int _amount;

        public Currencies Currency => _currency;
        
        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
    }
}