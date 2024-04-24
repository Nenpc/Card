using System;

namespace TheaCard.Core.Progress
{
    public interface IProgressGUI
    {
        event Action OnHideButtonClick;
        event Action OnShowButtonClick;
        
        void SetDamageDoneAmount(int amount);
        void SetDamageReceivedAmount(int amount);
        void SetCardsDestroyedAmount(int amount);
        void SetLostCardsAmount(int amount);
        void SetGamesPlayedAmount(int amount);

        void ShowPopup();
        void HidePopup();
        
        void ShowPanel();
        void HidePanel();
    }
}