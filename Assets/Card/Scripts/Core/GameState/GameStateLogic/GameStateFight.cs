using System;
using TheaCard.Code.ScreenDealer;
using TheaCard.Core.Buff;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
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
        private readonly IAdditionalScreenDealer _screenDealer;
        private readonly IFightModel _fightModel;
        private readonly IIntermediary<ProcessStates> _processIntermediary;
        private readonly IBuffContainer _buffContainer;
        
        public GameStateFight(IGameStateFightViewController viewController, 
            IGameStateFightGUI gui,
            IAdditionalScreenDealer screenDealer,
            IFightModel fightModel,
            IIntermediary<ProcessStates> processIntermediary,
            IBuffContainer buffContainer)
        {
            _viewController = viewController;
            _processIntermediary = processIntermediary;
            _processIntermediary.OnEndState += CheckEndState;

            _gui = gui;
            _gui.GiveUp.onClick.AddListener(GiveUp);

            _screenDealer = screenDealer;
            _fightModel = fightModel;
            _buffContainer = buffContainer;
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
                var heroModel = new HeroModel(heroConfig, _buffContainer.GetBuff(heroConfig.Buff), _fightModel.FightType, GameTeam.Enemy, hand);
                _fightModel.Enemy.AddHeroModel(heroModel);
            }
        }

        private void CheckEndState(ProcessStates states)
        {
            if (states == ProcessStates.Fight)
            {
                _fightModel.EndFight();
                _viewController.ClearAllCards();
                OnEndState?.Invoke(State);
            }
        }

        public void Dispose()
        {
            _processIntermediary.OnEndState -= CheckEndState;
            _gui?.EndMove.onClick.RemoveAllListeners();
            _gui?.GiveUp.onClick.RemoveAllListeners();
        }
        
        public void Start()
        {
            CreateEnemyFightModel();
            InitView();
            _processIntermediary.StartIntermediaryStates();
            
            _gui.Show();
            _viewController.Show();
            _screenDealer.ShowScreen(AdditionalScreens.Progress);
        }

        public void End()
        {
            _gui.Hide();
            _viewController.Hide();
            _screenDealer.HideScreen(AdditionalScreens.Progress);
        }

        private void GiveUp()
        {
            _fightModel.EndFight();
            _viewController.ClearAllCards();
            OnEndState?.Invoke(State);
        }
    }
}