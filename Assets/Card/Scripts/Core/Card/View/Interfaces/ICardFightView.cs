using System;
using TheaCard.Core.Heroes;
using UnityEngine;

namespace TheaCard.Core.Card
{
    public interface ICardFightView : IView
    {
        event Action<IHeroModel> OnCardClick;
        
        Transform InfoContainer { get; }
        Canvas Canvas { get; }

        void Init(IHeroModel heroModel, bool mainSide = true);
        void RotateCard(bool mainSide);
        void UpdateView();
        void SetHeroActive(bool active);
    }
}