using System.Collections.Generic;

namespace TheaCard.Core.Currency
{
    public sealed class CurrencyPresenter : ICurrencyPresenter
    {
        private readonly ICurrencyModel _currencyModel;
        private readonly ICurrencyGUIController _currencyGUIController;
        
        public CurrencyPresenter(ICurrencyModel currencyModel, 
            ICurrencyGUIController currencyGUIController)
        {
            _currencyModel = currencyModel;
            _currencyGUIController = currencyGUIController;
        }

        public void Init(IReadOnlyList<CurrencyInfo> startCurrencies)
        {
            foreach (var startCurrency in startCurrencies)
            {
                _currencyModel.AddNewCurrency(startCurrency.Currency ,startCurrency.Amount);
            }

            foreach (var currencyInfo in _currencyModel.CurrencyInfos)
            {
                _currencyGUIController.UpdateCurrencyInfo(currencyInfo.Key, currencyInfo.Value);
            }
        }

        public bool TryTakeCurrency(Currencies currencyType, int amount)
        {
            var result = _currencyModel.TryTakeCurrency(currencyType, amount);
            if (result)
            {
                _currencyGUIController.UpdateCurrencyInfo(currencyType, _currencyModel.CurrencyInfos[currencyType]);
                return true;
            }

            return false;
        }

        public void GiveCurrency(Currencies currencyType, int amount)
        {
            _currencyModel.GiveCurrency(currencyType, amount);
            _currencyGUIController.UpdateCurrencyInfo(currencyType, _currencyModel.CurrencyInfos[currencyType]);
        }
        
        public void ShowPanel()
        {
            _currencyGUIController.ShowPanel();
        }

        public void HidePanel()
        {
            _currencyGUIController.HidePanel(); 
        }
    }
}