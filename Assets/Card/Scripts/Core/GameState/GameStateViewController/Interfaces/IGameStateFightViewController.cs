using System;
using System.Collections.Generic;
using TheaCard.Core.Heroes;

namespace TheaCard.Core.GameState
{
    public interface IGameStateFightViewController
    {
        event Action<IHeroModel> OnHeroClick; 
        
        void Init(IReadOnlyList<IHeroModel> playerHeroes, IReadOnlyList<IHeroModel> enemyHeroes);
        void MoveToMainField(IHeroModel hero);
        void AttackHero(IHeroModel hero, IHeroModel goal);
        void DestroyHero(IHeroModel hero);
        void ClearAllCards();
        void Show();
        void Hide();
    }
}