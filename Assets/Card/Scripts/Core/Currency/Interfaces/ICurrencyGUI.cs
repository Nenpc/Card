namespace TheaCard.Core.Currency
{
    public interface ICurrencyGUI
    {
        void SetSilverAmount(int amount);
        void SetGoldAmount(int amount);
        void SetCristalAmount(int amount);
        
        void ShowPanel();
        void HidePanel();
    }
}