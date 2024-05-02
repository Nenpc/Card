using System;
using TheaCard.Core.Heroes;
using System.Collections.Generic;
using TheaCard.Core.Enums;

namespace TheaCard.Core.FightModel
{
    public sealed class HeroesFightModel : IHeroesFightModel
    {
        public event Action<IHeroModel> OnHeroModelAdd;
        public event Action<IHeroModel> OnHeroModelRemove;
        public event Action OnAllHeroModelRemove;
        
        private List<IHeroConfig> _heroesConfig { get; } = new List<IHeroConfig>();
        private List<IHeroModel> _heroesModel { get; } = new List<IHeroModel>();

        public IReadOnlyList<IHeroConfig> HeroesConfig => _heroesConfig;
        public IReadOnlyList<IHeroModel> HeroesModel => _heroesModel;

        public FightType FightType { get; set; }

        public void AddHeroConfig(IHeroConfig hero)
        {
            _heroesConfig.Add(hero);
        }

        public void RemoveHeroConfig(IHeroConfig hero)
        {
            _heroesConfig.Remove(hero);
        }
        
        public void AddHeroModel(IHeroModel hero)
        {
            _heroesModel.Add(hero);
            OnHeroModelAdd?.Invoke(hero);
        }

        public void RemoveHeroModel(IHeroModel hero)
        {
            _heroesModel.Remove(hero);
            OnHeroModelRemove?.Invoke(hero);
        }
        
        public void ClearAllHeroConfig()
        {
            _heroesConfig.Clear();
        }

        public void ClearAllHeroModel()
        {
            _heroesModel.Clear();
            OnAllHeroModelRemove?.Invoke();
        }

        public void GameComplete()
        {
            _heroesConfig.Clear();
            _heroesModel.Clear();
        }
    }
}