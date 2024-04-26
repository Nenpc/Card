using System;
using TheaCard.Core.Card;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
using TheaCard.Infrastructure.GameState;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using Random = UnityEngine.Random;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectHand : IIntermediaryState<GameStates>, IDisposable
    {
        private const int MaxShuffleCards = 3;
        
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.SelectHand;

        private readonly IGameStateSelectHandView _view;
        private readonly IGameStateSelectHandGUI _gui;
        private readonly ICardViewFactory<IHeroModel, ICardFightView> _cardFightViewFactory;
        private readonly IFightModel _fightModel;

        private bool _isInitialize = false;
        private int _shuffleCards = 0;
        
        public GameStateSelectHand(IGameStateSelectHandView view, 
            IGameStateSelectHandGUI gui,
            ICardViewFactory<IHeroModel, ICardFightView> cardFightViewFactory,
            IFightModel fightModel)
        {
            _view = view;
            
            _gui = gui;
            _gui.OnNextStage += NextStage;
            _gui.OnShuffleCards += ShuffleCards;

            _cardFightViewFactory = cardFightViewFactory;
            _fightModel = fightModel;
        }
        
        public void Dispose()
        {
            _gui.OnNextStage -= NextStage;
        }
        
        private void NextStage()
        {
            OnEndState?.Invoke(State);
        }

        private void ShuffleCards()
        {
            if (_shuffleCards < MaxShuffleCards)
            {
                UpdateCardHand();
                _view.UpdateCardHand(_fightModel.Player.HeroesModel);
                _shuffleCards++;
            }
        }

        private void UpdateCardHand()
        {
            _fightModel.Player.ClearAllHeroModel();
            foreach (var heroConfig in _fightModel.Player.HeroesConfig)
            {
                var hand = Random.Range(0f, 1f) > 0.5f ? Hands.Fight : Hands.Support;
                _fightModel.Player.AddHeroModel(new HeroModel(heroConfig, _fightModel.FightType, GameTeam.Player, hand));
            }
        }

        public void Start()
        {
            UpdateCardHand();
            _view.Init(_cardFightViewFactory, _fightModel.Player.HeroesModel);
            _isInitialize = true;
            
            _gui.Show();
            _view.Show();
        }

        public void End()
        {
            _isInitialize = false;
            
            _gui.Hide();
            _view.Hide();
        }
    }
}