﻿using System.Collections.Generic;
using UnityEngine;

namespace TheaCard.Core.Currency
{
    [CreateAssetMenu(menuName = "TheaCard/CurrencyConfig")]
    public sealed class CurrencyConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyInfo> _startCurrencies;

        public IReadOnlyList<CurrencyInfo> StartCurrencies => _startCurrencies;
    }
}