using System;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Card
{
    public interface ICardFightView : IView
    {
        event Action<IHeroModel> OnCardClick;
        void Init(IHeroModel heroModel, bool mainSide = true);
        void RotateCard(bool mainSide);
        void UpdateView();
        void SetHeroActive(bool active);
    }
}