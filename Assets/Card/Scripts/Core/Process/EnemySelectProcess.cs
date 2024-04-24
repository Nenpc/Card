using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using TheaCard.Core.FightModel;
using TheaCard.Core.GameState;
using TheaCard.Infrastructure.Intermediary;
using TheaCard.Core.Enums;
using Random = UnityEngine.Random;

namespace TheaCard.Core.Process
{
    public sealed class EnemySelectProcess : IIntermediaryState<ProcessStates>
    {
        private const int EndDelayMillisecond = 2000;
        
        public event Action<ProcessStates> OnEndState;
        public ProcessStates State => ProcessStates.AI;
        
        private readonly IGameStateFightViewController _viewController;
        private readonly IFightModel _fightModel;

        public EnemySelectProcess(IGameStateFightViewController viewController, IFightModel fightModel)
        {
            _viewController = viewController;
            _fightModel = fightModel;
        }

        public void Start()
        {
            StartWithDelay().Forget();
        }

        private async UniTask StartWithDelay()
        {            
            await UniTask.Delay(EndDelayMillisecond);
            
            var heroModels = _fightModel.Enemy.HeroesModel.Where(x => !x.OnBoard).ToList();
            if (heroModels.Count > 0)
            {
                var heroModel = heroModels[Random.Range(0, heroModels.Count)];
                _fightModel.MoveToBoard(heroModel);
                _viewController.MoveToMainField(heroModel);
            }
            
            _fightModel.NextRound();
            OnEndState?.Invoke(State);
        }

        public void End()
        {
            
        }
    }
}