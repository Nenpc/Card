using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using TheaCard.Core.Buff;
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
            ApplyBuff(heroModel);

            OnEndState?.Invoke(State);
        }

        private void ApplyBuff(IHeroModel heroModel)
        {
            if (heroModel.Buff.Buff != BuffType.None)
            {
                IHeroModel leftHero = null;
                IHeroModel rightHero = null;
                
                if (_fightModel.HeroesBoard.Count > 1)
                {
                    for (int i = 0; i < _fightModel.HeroesBoard.Count; i++)
                    {
                        if (_fightModel.HeroesBoard[i] == heroModel)
                        {
                            if (i > 0)
                                leftHero = _fightModel.HeroesBoard[i - 1];
                            
                            if (i < _fightModel.HeroesBoard.Count - 2)
                                rightHero = _fightModel.HeroesBoard[i + 1];
                        }
                    }
                }

                heroModel.Buff.Use(_fightModel.FightType, heroModel, leftHero, rightHero);
                
                if (leftHero != null)
                    _viewController.UpdateHeroView(leftHero);
                if (rightHero != null)
                    _viewController.UpdateHeroView(rightHero);
            }
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