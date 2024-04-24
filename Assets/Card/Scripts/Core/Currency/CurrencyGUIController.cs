namespace TheaCard.Core.Currency
{
    public sealed class CurrencyGUIController : ICurrencyGUIController
    {
        private readonly ICurrencyGUI _currencyGUI;

        private bool _isActive;
        
        public CurrencyGUIController(ICurrencyGUI currencyGUI)
        {
            _currencyGUI = currencyGUI;
        }

        public void UpdateCurrencyInfo(Currencies currencyType, int amount)
        {
            switch (currencyType)
            {
                case Currencies.Silver:
                    _currencyGUI.SetSilverAmount(amount);
                    break;
                case Currencies.Gold:
                    _currencyGUI.SetGoldAmount(amount);
                    break;
                case Currencies.Cristal:
                    _currencyGUI.SetGoldAmount(amount);
                    break;
                case Currencies.None:
                    break;
            }
        }

        public void ShowPanel()
        {
            if (!_isActive)
            {
                _currencyGUI.ShowPanel();
                _isActive = true;
            }
        }

        public void HidePanel()
        {
            if (_isActive)
            {
                _currencyGUI.HidePanel();
                _isActive = false;
            }
        }
    }
}