using TheaCard.Core.Card;
using TheaCard.Core.GameState;
using TheaCard.Core.Heroes;
using UnityEngine;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class GameViewInstaller : MonoInstaller
    {
        [SerializeField] private GameStateSelectCardView _gameStateSelectCardView;
        [SerializeField] private GameStateEnemyCardView _gameStateEnemyCardView;
        [SerializeField] private GameStateSelectHandView _gameStateSelectHandView;
        [SerializeField] private GameStateFightView _gameStateFightView;
        
        [Space]
        [SerializeField] private CardFightViewFactory _cardFightViewFactory;
        [SerializeField] private CardSelectViewFactory _cardSelectViewFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameStateSelectCardView>().FromInstance(_gameStateSelectCardView);
            Container.Bind<GameStateViewAbstract>().FromInstance(_gameStateSelectCardView);
            
            Container.Bind<IGameStateEnemyCardView>().FromInstance(_gameStateEnemyCardView);
            Container.Bind<GameStateViewAbstract>().FromInstance(_gameStateEnemyCardView);  
            
            Container.Bind<IGameStateSelectHandView>().FromInstance(_gameStateSelectHandView);
            Container.Bind<GameStateSelectHandView>().FromInstance(_gameStateSelectHandView); 
            
            Container.Bind<IGameStateFightView>().FromInstance(_gameStateFightView);
            Container.Bind<GameStateViewAbstract>().FromInstance(_gameStateFightView);
            
            Container.Bind<ICardViewFactory<IHeroModel, ICardFightView>>().FromInstance(_cardFightViewFactory);
            Container.Bind<ICardViewFactory<IHeroConfig, ICardSelectView>>().FromInstance(_cardSelectViewFactory);
        }
    }
}