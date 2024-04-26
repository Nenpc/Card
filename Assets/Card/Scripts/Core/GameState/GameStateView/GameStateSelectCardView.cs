using System;
using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectCardView : GameStateViewAbstract, IGameStateSelectCardView
    {
        public event Action<IHeroConfig> OnCardClick;
        
        public override GameStates States => GameStates.SelectCard;
        
        [SerializeField] private RectTransform _field;
        
        private Dictionary<IHeroConfig, ICardSelectView> _cardSelectViews = new Dictionary<IHeroConfig, ICardSelectView>();
        private ICardViewFactory<IHeroConfig, ICardSelectView> _cardViewFactory;
        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void CardClick(IHeroConfig heroConfig)
        {
            OnCardClick?.Invoke(heroConfig);
        }

        public void SelectCard(IHeroConfig heroConfig)
        {
            _cardSelectViews[heroConfig].Select();
        }
        
        public void DeselectCard(IHeroConfig heroConfig)
        {
            _cardSelectViews[heroConfig].Deselect();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            foreach (var cardView in _cardSelectViews)
            {
                cardView.Value.Deselect();
                cardView.Value.OnCardClick -= CardClick;
                _cardViewFactory.Return(cardView.Value);
            }
            _cardSelectViews.Clear();
        }

        public void Init(ICardViewFactory<IHeroConfig, ICardSelectView> cardViewFactory, IReadOnlyList<IHeroConfig> heroesConfigs)
        {
            _cardViewFactory = cardViewFactory;

            foreach (var heroConfig in heroesConfigs)
            {
                var cardSelectView = cardViewFactory.Get(heroConfig, _field);
                cardSelectView.OnCardClick += CardClick;
                _cardSelectViews.Add(heroConfig, cardSelectView);
            }
        }
    }
}