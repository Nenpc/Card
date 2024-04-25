using System.Collections.Generic;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;

namespace TheaCard.Core.FightModel
{
    public sealed class FightModel : IFightModel
    {
        private IHeroesFightModel _player = new HeroesFightModel();
        private IHeroesFightModel _enemy = new HeroesFightModel();
        private List<IHeroModel> _heroesBoard = new List<IHeroModel>();

        public FightType FightType { get; set; } = FightType.None;

        public void MoveToBoard(IHeroModel heroModel)
        {
            if (heroModel.OnBoard && !_heroesBoard.Contains(heroModel))
                return;

            _heroesBoard.Add(heroModel);
            heroModel.SetPlace(_heroesBoard.Count - 1);
        }

        public void RemoveFromBoard(IHeroModel heroModel)
        {
            _heroesBoard.Remove(heroModel);
        }

        public void ClearAllInfo()
        {
            Enemy.ClearAllHeroModel();
            Enemy.ClearAllHeroConfig();
            
            Player.ClearAllHeroModel();
            Player.ClearAllHeroConfig();
            _heroesBoard.Clear();
        }

        public IHeroesFightModel Player => _player;
        public IHeroesFightModel Enemy => _enemy;
        public IReadOnlyList<IHeroModel> HeroesBoard => _heroesBoard;
    }
}