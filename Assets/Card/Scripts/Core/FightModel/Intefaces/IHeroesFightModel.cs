using System;
using TheaCard.Core.Heroes;
using System.Collections.Generic;

namespace TheaCard.Core.FightModel
{
    public interface IHeroesFightModel
    {
        event Action<IHeroModel> OnHeroModelAdd;
        event Action<IHeroModel> OnHeroModelRemove;
        event Action OnAllHeroModelRemove;
        
        IReadOnlyList<IHeroConfig> HeroesConfig { get; }
        void AddHeroConfig(IHeroConfig hero);
        void RemoveHeroConfig(IHeroConfig hero);
        void ClearAllHeroConfig();
        
        IReadOnlyList<IHeroModel> HeroesModel { get; }
        void AddHeroModel(IHeroModel hero);
        void RemoveHeroModel(IHeroModel hero);
        void ClearAllHeroModel();
    }
}