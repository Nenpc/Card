using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.GameState
{
    public interface IGameStateSelectHandView
    {
        void Init(ICardViewFactory<IHeroModel, ICardFightView> cardFactory, IReadOnlyList<IHeroModel> heroesModels);
        void UpdateCardHand(IReadOnlyList<IHeroModel> heroesModels);
        void Show();
        void Hide();
    }
}