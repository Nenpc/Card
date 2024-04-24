using TheaCard.Core.Currency;
using TheaCard.Core.GameState;
using TheaCard.Core.Progress;
using UnityEngine;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class GameGUIInstaller : MonoInstaller
    {
        [SerializeField] private GameStateMenuGUI _gameStateMenuGUI;
        [SerializeField] private GameStateSelectCardGUI _gameStateSelectCardGUI;
        [SerializeField] private GameStateEnemyCardGUI _gameStateEnemyCardGUI;
        [SerializeField] private GameStateSelectHandGUI _gameStateSelectHandGUI;
        [SerializeField] private GameStateFightGUI _gameStateFightGUI;
        
        [Space]
        [SerializeField] private CurrencyGUI _currencyGUI;
        [SerializeField] private ProgressGUI _progressGUI;

        public override void InstallBindings()
        {
            Container.Bind<IGameStateMenuGUI>().FromInstance(_gameStateMenuGUI);
            Container.Bind<GameStateGUIAbstract>().FromInstance(_gameStateMenuGUI);
            
            Container.Bind<IGameStateSelectCardGUI>().FromInstance(_gameStateSelectCardGUI);
            Container.Bind<GameStateGUIAbstract>().FromInstance(_gameStateSelectCardGUI);
            
            Container.Bind<IGameStateEnemyCardGUI>().FromInstance(_gameStateEnemyCardGUI);
            Container.Bind<GameStateGUIAbstract>().FromInstance(_gameStateEnemyCardGUI);
            
            Container.Bind<IGameStateSelectHandGUI>().FromInstance(_gameStateSelectHandGUI);
            Container.Bind<GameStateGUIAbstract>().FromInstance(_gameStateSelectHandGUI);
            
            Container.Bind<IGameStateFightGUI>().FromInstance(_gameStateFightGUI);
            Container.Bind<GameStateGUIAbstract>().FromInstance(_gameStateFightGUI);
            
            Container.Bind<ICurrencyGUI>().FromInstance(_currencyGUI);
            Container.Bind<IProgressGUI>().FromInstance(_progressGUI);
        }
    }
}