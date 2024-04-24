using TheaCard.Core.Heroes;
using System.Collections.Generic;

namespace TheaCard.Core.FightModel
{
    public interface IHeroesFightModel
    {
        IReadOnlyList<IHeroConfig> HeroesConfig { get; }
        void AddHeroConfig(IHeroConfig hero);
        void RemoveHeroConfig(IHeroConfig hero);
        
        IReadOnlyList<IHeroModel> HeroesModel { get; }
        void AddHeroModel(IHeroModel hero);
        void RemoveHeroModel(IHeroModel hero);
        void ClearAllHeroModel();
    }
}