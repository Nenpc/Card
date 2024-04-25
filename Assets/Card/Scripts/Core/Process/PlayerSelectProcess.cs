using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using TheaCard.Core.FightModel;
using TheaCard.Core.GameState;
using TheaCard.Core.Heroes;
using TheaCard.Infrastructure.Intermediary;
using TheaCard.Core.Enums;

namespace TheaCard.Core.Process
{
    public sealed class PlayerSelectProcess : IIntermediaryState<ProcessStates>, IDisposable
    {
        public event Action<ProcessStates> OnEndState;

        private readonly IGameStateFightViewController _viewController;
        private readonly IFightModel _fightModel;

        private bool _active;
        
        public ProcessStates State => ProcessStates.Player;

        public PlayerSelectProcess(IGameStateFightViewController viewController, IFightModel fightModel)
        {
            _viewController = viewController;
            _viewController.OnHeroClick += MoveToBoard;
            
            _fightModel = fightModel;
        }
        
        public void Dispose()
        {
            _viewController.OnHeroClick -= MoveToBoard;
        }

        private void MoveToBoard(IHeroModel heroModel)
        {
            if (!_active)
                return;
            
            if (heroModel.OnBoard || heroModel.Team != GameTeam.Player)
                return;
            
            _fightModel.MoveToBoard(heroModel);
            _viewController.MoveToMainField(heroModel);

            OnEndState?.Invoke(State);
        }
        
        public void Start()
        {
            _active = true;

            var activeCard = _fightModel.Player.HeroesModel.Where(x => !x.OnBoard).ToList();
            if (activeCard.Count == 0)
                OnEndState?.Invoke(State);
                
        }

        public void End()
        {
            _active = false;
        }
    }
}