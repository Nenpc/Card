namespace TheaCard.Core.Currency
{
    public interface ICurrencyGUIController
    {
        void UpdateCurrencyInfo(Currencies currencyType, int amount);
        void ShowPanel();
        void HidePanel();
    }
}