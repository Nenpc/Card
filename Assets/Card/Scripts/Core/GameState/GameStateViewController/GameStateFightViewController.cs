using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TheaCard.Core.Card;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateFightViewController : IGameStateFightViewController, IDisposable
    {
        private static int SortingOrder = 1;
        
        public event Action<IHeroModel> OnHeroClick;
        
        private readonly IGameStateFightView _view;
        private readonly ICardViewFactory<IHeroModel, ICardFightView> _cardViewFactory;

        private Dictionary<GameTeam, Dictionary<IHeroModel, ICardFightView>> _teamCardViews = 
            new Dictionary<GameTeam, Dictionary<IHeroModel, ICardFightView>>();

        public GameStateFightViewController(IGameStateFightView view, 
            ICardViewFactory<IHeroModel, ICardFightView> cardViewFactory)
        {
            _view = view;
            _cardViewFactory = cardViewFactory;
        }
        
        public void Init(IReadOnlyList<IHeroModel> playerHeroes, IReadOnlyList<IHeroModel> enemyHeroes)
        {
            _teamCardViews.Add(GameTeam.Enemy, new Dictionary<IHeroModel, ICardFightView>());
            foreach (var enemyModel in enemyHeroes)
            {
                var parent = enemyModel.Hand == Hands.Fight
                    ? _view.EnemyFightHand
                    : _view.EnemySupportHand;
                var enemyView = _cardViewFactory.Get(enemyModel, parent.transform);
                enemyView.RotateCard(false);
                _teamCardViews[GameTeam.Enemy].Add(enemyModel, enemyView);
            }
            
            _teamCardViews.Add(GameTeam.Player, new Dictionary<IHeroModel, ICardFightView>());
            foreach (var playerModel in playerHeroes)
            {
                var parent = playerModel.Hand == Hands.Fight
                    ? _view.PlayerFightHand
                    : _view.PlayerSupportHand;
                var playerView = _cardViewFactory.Get(playerModel, parent.transform);
                playerView.OnCardClick += SelectCard;
                _teamCardViews[GameTeam.Player].Add(playerModel, playerView);
            }
        }
        
        public void Dispose()
        {
            if (_teamCardViews.ContainsKey(GameTeam.Player))
            {
                foreach (var card in _teamCardViews[GameTeam.Player])
                {
                    card.Value.OnCardClick -= SelectCard;
                }
            }
        }

        private void SelectCard(IHeroModel heroModel)
        {
            OnHeroClick?.Invoke(heroModel);
        }

        public void MoveToMainField(IHeroModel hero)
        {
            _teamCardViews[hero.Team][hero].View.transform.SetParent(_view.MainField.transform);
            _teamCardViews[hero.Team][hero].SetHeroActive(hero.Active);
        }

        public void ActiveHero(IHeroModel hero)
        {
            _teamCardViews[hero.Team][hero].SetHeroActive(hero.Active);
        }

        public void AttackHero(IHeroModel hero, IHeroModel goal, int animationTimeMili = 0)
        {
            AttackAnimation(hero, goal, animationTimeMili).Forget();
        }

        private async UniTask AttackAnimation(IHeroModel heroModel, IHeroModel goalModel, int animationTimeMili = 0)
        {
            if (animationTimeMili != 0)
            {
                var animationDuration = animationTimeMili / 2f;
                var heroView = _teamCardViews[heroModel.Team][heroModel];
                heroView.Canvas.overrideSorting = true;
                heroView.Canvas.sortingOrder = SortingOrder;
                
                var heroContainer = heroView.InfoContainer;
                var basePosition = heroContainer.position;
                var goalContainer = _teamCardViews[goalModel.Team][goalModel].InfoContainer;

                heroContainer.DOMove(goalContainer.position, animationDuration * 0.001f, true);
                await UniTask.Delay((int) (animationDuration));

                _teamCardViews[goalModel.Team][goalModel].UpdateView();

                heroContainer.DOMove(basePosition, animationDuration * 0.001f, true);
                await UniTask.Delay((int) (animationDuration));
                
                heroView.Canvas.overrideSorting = false;
            }
            else
            {
                _teamCardViews[goalModel.Team][goalModel].UpdateView();
            }
        }

        public void DestroyHero(IHeroModel hero)
        {
            var cardView = _teamCardViews[hero.Team][hero].View.GetComponent<ICardFightView>();
            _teamCardViews[hero.Team].Remove(hero);
            
            _cardViewFactory.Return(cardView);
        }

        public void ClearAllCards()
        {
            foreach (var teamCardViews in _teamCardViews)
            {
                foreach (var card in teamCardViews.Value)
                {
                    var cardView = card.Value.View.GetComponent<ICardFightView>();
                    _cardViewFactory.Return(cardView);
                }
            }
            
            foreach (var teamCardViews in _teamCardViews)
            {
                _teamCardViews[teamCardViews.Key].Clear();
            }
            _teamCardViews.Clear();
        }

        public void UpdateHeroView(IHeroModel hero)
        {
            _teamCardViews[hero.Team][hero].UpdateView();
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}