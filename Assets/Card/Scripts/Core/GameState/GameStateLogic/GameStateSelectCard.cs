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
        private readonly IFightModel _fightModel;
        private readonly ICurrencyPresenter _currencyPresenter;
        private readonly ICardViewFactory<IHeroConfig, ICardSelectView> _cardViewFactory;
        
        public GameStateSelectCard(IGameStateSelectCardView view, 
            IGameStateSelectCardGUI gui, 
            IHeroesConfig heroesConfig,
            IFightModel fightModel,
            ICurrencyPresenter currencyPresenter,
            ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory)
        {
            _view = view;
            _view.OnCardClick += OnCardClick;
            
            _gui = gui;
            _gui.OnNextStage += NextStage;

            _heroesConfig = heroesConfig;
            _fightModel = fightModel;
            _currencyPresenter = currencyPresenter;
            _cardViewFactory = cardViewFactory;
        }
        
        private void NextStage()
        {
            if (_fightModel.Player.HeroesConfig.Count == 0)
            {
                Debug.LogWarning("You must select at least one hero to fight!");
                return;
            }

            OnEndState?.Invoke(State);
        }

        private void OnCardClick(IHeroConfig heroConfig)
        {
            if (!_fightModel.Player.HeroesConfig.Contains(heroConfig))
            {
                if (_currencyPresenter.TryTakeCurrency(Currencies.Silver, heroConfig.Price))
                {
                    _fightModel.Player.AddHeroConfig(heroConfig);
                    _view.SelectCard(heroConfig);
                }
            }
            else
            {
                _currencyPresenter.GiveCurrency(Currencies.Silver, heroConfig.Price);
                _fightModel.Player.RemoveHeroConfig(heroConfig);
                _view.DeselectCard(heroConfig);
            }
        }

        public void Start()
        {
            _view.Init(_cardViewFactory, _heroesConfig.Heroes);
            _currencyPresenter.SetDefaultCurrencies();
            
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
            _view.OnCardClick -= OnCardClick;
            _gui.OnNextStage -= NextStage;
        }
    }
}