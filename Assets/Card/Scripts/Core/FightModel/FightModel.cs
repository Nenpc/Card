using System.Collections.Generic;
using System.Linq;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;

namespace TheaCard.Core.FightModel
{
    public sealed class FightModel : IFightModel
    {
        private IHeroesFightModel _player = new HeroesFightModel();
        private IHeroesFightModel _enemy = new HeroesFightModel();
        private List<IHeroModel> _heroesBoard = new List<IHeroModel>();
        private int _round = 0;

        public FightType FightType { get; set; } = FightType.None;
        public int Round => _round;

        public void MoveToBoard(IHeroModel heroModel)
        {
            if (heroModel.OnBoard && !_heroesBoard.Contains(heroModel))
                return;

            _heroesBoard.Add(heroModel);
            heroModel.SetPlace(_heroesBoard.Count - 1);
        }

        public void NextRound()
        {
            _round++;
        }

        public IHeroesFightModel Player => _player;
        public IHeroesFightModel Enemy => _enemy;
    }
}