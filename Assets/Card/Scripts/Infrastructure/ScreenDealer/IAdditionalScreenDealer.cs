using System;

namespace TheaCard.Infrastructure.ScreenDealer
{
    public interface IAdditionalScreenDealer<TScreenType> where TScreenType : Enum
    {
        void ShowScreen(TScreenType screenType);
        void HideScreen(TScreenType screenType);
    }
}