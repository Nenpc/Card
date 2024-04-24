using System;
using System.Linq;
using TheaCard.Core.Card;
using TheaCard.Core.Currency;
using TheaCard.Core.FightModel;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectCard : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.SelectCard;
        
        private readonly IGameStateSelectCardView _view;
        private readonly IGameStateSelectCardGUI _gui;
        private readonly IHeroesConfig _heroesConfig;
        private readonly IFightModel _FightModel;
        private readonly ICurrencyPresenter _currencyPresenter;
        private readonly ICardViewFactory<IHeroConfig, ICardSelectView> _cardViewFactory;

        private bool _isInitialize = false;
        
        public GameStateSelectCard(IGameStateSelectCardView view, 
            IGameStateSelectCardGUI gui, 
            IHeroesConfig heroesConfig,
            IFightModel FightModel,
            ICurrencyPresenter currencyPresenter,
            ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory)
        {
            _view = view;
            
            _gui = gui;
            _gui.OnNextStage += NextStage;

            _heroesConfig = heroesConfig;
            _FightModel = FightModel;
            _currencyPresenter = currencyPresenter;
            _cardViewFactory = cardViewFactory;
        }
        
        private void NextStage()
        {
            if (_FightModel.Player.HeroesConfig.Count == 0)
            {
                Debug.LogWarning("You must select at least one hero to fight!");
                return;
            }

            OnEndState?.Invoke(State);
        }

        private void OnCardClick(IHeroConfig heroConfig)
        {
            if (!_FightModel.Player.HeroesConfig.Contains(heroConfig))
            {
                if (_currencyPresenter.TryTakeCurrency(Currencies.Silver, heroConfig.Price))
                {
                    _FightModel.Player.AddHeroConfig(heroConfig);
                    _view.SelectCard(heroConfig);
                }
            }
            else
            {
                _currencyPresenter.GiveCurrency(Currencies.Silver, heroConfig.Price);
                _FightModel.Player.RemoveHeroConfig(heroConfig);
                _view.DeselectCard(heroConfig);
            }
        }

        public void Start()
        {
            if (!_isInitialize)
            {
                _view.Init(_cardViewFactory, _heroesConfig.Heroes);
                _view.OnCardClick += OnCardClick;
                _isInitialize = true;
            }

            _gui.Show();
            _view.Show();
            _currencyPresenter.ShowPanel();
        }

        public void End()
        {
            _gui.Hide();
            _view.Hide();
            _currencyPresenter.HidePanel();
        }

        public void Dispose()
        {
            if (_isInitialize)
                _view.OnCardClick -= OnCardClick;

            _gui.OnNextStage -= NextStage;
        }
    }
}