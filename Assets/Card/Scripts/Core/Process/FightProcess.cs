using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TheaCard.Core.Enums;
using TheaCard.Core.FightModel;
using TheaCard.Core.GameState;
using TheaCard.Core.Heroes;
using TheaCard.Infrastructure.Intermediary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheaCard.Core.Process
{
    public sealed class FightProcess : IIntermediaryState<ProcessStates>
    {
        private const int FightStepPause = 1000;
        
        public event Action<ProcessStates> OnEndState;
        
        private readonly IGameStateFightViewController _viewController;
        private readonly IFightModel _fightModel;

        public ProcessStates State => ProcessStates.Fight;

        private CancellationTokenSource _cancellationTokenSource;
        
        public FightProcess(IGameStateFightViewController viewController, IFightModel fightModel)
        {
            _viewController = viewController;
            _fightModel = fightModel;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartAttackCycle(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartAttackCycle(CancellationToken token)
        {
            await Task.Delay(FightStepPause, token);

            for (int i = 0; i < _fightModel.HeroesBoard.Count; i++)
            {
                if (!token.IsCancellationRequested)
                {
                    if (HeroAction(i))
                        i--;

                    await Task.Delay(FightStepPause, token);
                }
                else
                {
                    break;
                }
            }

            if (!token.IsCancellationRequested)
            {
                var firstModel = _fightModel.HeroesBoard.FirstOrDefault();
                if (firstModel != default)
                {
                    var hasEnemyModel = _fightModel.HeroesBoard.Any(x => x.Team != firstModel.Team);
                    if (hasEnemyModel)
                    {
                        StartAttackCycle(token).Forget();
                        return;
                    }
                }
            }

            OnEndState?.Invoke(State);
        }

        private bool HeroAction(int heroPosition)
        {
            var leftEnemyPosition = -1;
            var rightEnemyPosition = -1;
            var heroModel = _fightModel.HeroesBoard[heroPosition];

            if (!heroModel.Active)
            {
                heroModel.ActiveHero();
                _viewController.ActiveHero(heroModel);
                return false;
            }

            for (int i = heroPosition; i >= 0; i--)
            {
                if (_fightModel.HeroesBoard[i].Team != heroModel.Team)
                {
                    leftEnemyPosition = i;
                    break;
                }
            }
            
            for (int i = heroPosition; i < _fightModel.HeroesBoard.Count; i++)
            {
                if (_fightModel.HeroesBoard[i].Team != heroModel.Team)
                {
                    rightEnemyPosition = i;
                    break;
                }
            }

            if (leftEnemyPosition == -1 && rightEnemyPosition == -1)
            {
                return false;
            }

            int direction;
            IHeroModel enemyModel;
            if (leftEnemyPosition != -1 && rightEnemyPosition != -1)
            {
                direction = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
            }
            else
            {
                direction = leftEnemyPosition != -1 ? -1 : 1;
            }

            enemyModel = direction == -1 ? 
                _fightModel.HeroesBoard[leftEnemyPosition] : 
                _fightModel.HeroesBoard[rightEnemyPosition];
            
            enemyModel.TakeDamage(heroModel.Damage);
            if (enemyModel.Health == 0)
            {
                EnemyDead(enemyModel);
                if (direction == -1)
                    return true;
            }
            else
            {
                _viewController.AttackHero(heroModel, enemyModel, FightStepPause);
            }

            return false;
        }

        private void EnemyDead(IHeroModel enemyModel)
        {
            _fightModel.RemoveFromBoard(enemyModel);
            _viewController.DestroyHero(enemyModel);
        }

        public void End()
        {
            Debug.Log("End");
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}