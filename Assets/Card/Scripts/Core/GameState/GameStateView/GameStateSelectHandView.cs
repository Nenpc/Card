using System;
using System.Collections.Generic;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateSelectHandView : MonoBehaviour, IGameStateSelectHandView
    {
        [SerializeField] private RectTransform _fightHand;
        [SerializeField] private RectTransform _supportHand;
        
        private Dictionary<IHeroConfig, ICardFightView> _cardHandSelectViews = new Dictionary<IHeroConfig, ICardFightView>();
        private ICardViewFactory<IHeroModel, ICardFightView> _cardFactory;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            foreach (var heroView in _cardHandSelectViews)
            {
                _cardFactory.Return(heroView.Value);
            }
            _cardHandSelectViews.Clear();
        }

        public void Init(ICardViewFactory<IHeroModel, ICardFightView> cardFactory, IReadOnlyList<IHeroModel> heroesModels)
        {
            _cardFactory = cardFactory;
            foreach (var heroModel in heroesModels)
            {
                var handParent = heroModel.Hand == Hands.Fight ? _fightHand : _supportHand;
                var cardFightView = _cardFactory.Get(heroModel, handParent);
                _cardHandSelectViews.Add(heroModel.Def, cardFightView);
            }
        }

        public void UpdateCardHand(IReadOnlyList<IHeroModel> heroesModels)
        {
            foreach (var heroModel in heroesModels)
            {
                var handParent = heroModel.Hand == Hands.Fight ? _fightHand : _supportHand;
                var cardFightView = _cardHandSelectViews[heroModel.Def];
                cardFightView.View.transform.SetParent(handParent);
            }
        }
    }
}