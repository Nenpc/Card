using System;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.Card
{
    public interface ICardSelectView : IView
    {
        event Action<IHeroConfig> OnCardClick;

        void Init(IHeroConfig heroConfig);
        
        void Select();
        void Deselect();
    }
}