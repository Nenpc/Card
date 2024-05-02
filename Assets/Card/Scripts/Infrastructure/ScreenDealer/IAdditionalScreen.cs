using System;

namespace TheaCard.Infrastructure.ScreenDealer
{
    public interface IAdditionalScreen<TScreenType> where TScreenType : Enum
    {
        TScreenType ScreenType { get; }
        void Show();
        void Hide();
    }
}