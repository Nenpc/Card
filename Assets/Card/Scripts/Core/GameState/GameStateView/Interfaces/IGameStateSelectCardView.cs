using System;
using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.GameState
{
    public interface IGameStateSelectCardView : IGameStateBase
    {
        event Action<IHeroConfig> OnCardClick;
        void Init(ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory, IReadOnlyList<IHeroConfig> heroesConfigs);
        void SelectCard(IHeroConfig heroConfig);
        void DeselectCard(IHeroConfig heroConfig);
    }
}