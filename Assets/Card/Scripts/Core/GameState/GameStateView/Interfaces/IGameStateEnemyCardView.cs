using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.GameState
{
    public interface IGameStateEnemyCardView
    {
        void Init(ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory, IReadOnlyList<IHeroConfig> enemyConfigs, IReadOnlyList<IHeroConfig> playerConfigs);
        void Show();
        void Hide();
    }
}