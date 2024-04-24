using System;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
using TheaCard.Core.Progress;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using Random = UnityEngine.Random;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateFight : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.Fight;

        private readonly IGameStateFightViewController _viewController;
        private readonly IGameStateFightGUI _gui;
        private readonly IProgressPresenter _progressPresenter;
        private readonly IFightModel _fightModel;
        private readonly IntermediaryAbstract<ProcessStates> _processIntermediary;

        private bool _isInitialize = false;
        
        public GameStateFight(IGameStateFightViewController viewController, 
            IGameStateFightGUI gui,
            IProgressPresenter progressPresenter,
            IFightModel fightModel,
            IntermediaryAbstract<ProcessStates> processIntermediary)
        {
            _viewController = viewController;
            _processIntermediary = processIntermediary;

            _gui = gui;
            _gui.EndMove.onClick.AddListener(ChangePlayer);
            _gui.GiveUp.onClick.AddListener(GiveUp);

            _progressPresenter = progressPresenter;
            _fightModel = fightModel;
        }
        
        private void InitView()
        {
            _viewController.Init(_fightModel.Player.HeroesModel ,_fightModel.Enemy.HeroesModel);
        }

        private void CreateEnemyFightModel()
        {
            foreach (var heroConfig in _fightModel.Enemy.HeroesConfig)
            {
                var hand = Random.Range(0f, 1f) > 0.5f ? Hands.Fight : Hands.Support;
                var heroModel = new HeroModel(heroConfig, _fightModel.FightType, GameTeam.Enemy, hand);
                _fightModel.Enemy.AddHeroModel(heroModel);
            }
        }

        public void Dispose()
        {
            _gui?.EndMove.onClick.RemoveAllListeners();
            _gui?.GiveUp.onClick.RemoveAllListeners();
        }
        
        public void Start()
        {
            if (!_isInitialize)
            {
                CreateEnemyFightModel();
                InitView();
                _processIntermediary.StartIntermediaryStates();
                _isInitialize = true;
            }

            _gui.Show();
            _viewController.Show();
            _progressPresenter.ShowPanel();
        }

        public void End()
        {
            _gui.Hide();
            _viewController.Hide();
            _progressPresenter.HidePanel();
        }

        private void GiveUp()
        {
            OnEndState?.Invoke(State);
        }

        private void ChangePlayer()
        {
            
        }
    }
}