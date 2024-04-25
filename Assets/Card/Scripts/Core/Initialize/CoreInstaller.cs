using TheaCard.Core.Currency;
using TheaCard.Core.Enums;
using TheaCard.Core.GameState;
using TheaCard.Core.Progress;
using TheaCard.Infrastructure.GameState;
using TheaCard.Core.FightModel;
using TheaCard.Core.Process;
using TheaCard.Infrastructure.Intermediary;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindModel();
            BindGameSateViewController();
            BindGameSate();
            BindCurrency();
            BindProgress();
            BindProcess();
            BindCard();
            
            Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IInitializable>().To<CurrencyInitializer>().AsSingle().NonLazy();
        }

        private void BindGameSate()
        {
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateMenu>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateSelectCard>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateEnemyCard>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateSelectHand>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateFight>().AsSingle().NonLazy();
            
            Container.Bind<IIntermediary<GameStates>>().To<GameStateIntermediary>().AsSingle().NonLazy();
        }

        private void BindModel()
        {
            Container.Bind<IFightModel>().To<FightModel.FightModel>().AsSingle().NonLazy();
        }

        private void BindGameSateViewController()
        {
            Container.Bind<IGameStateFightViewController>().To<GameStateFightViewController>().AsSingle().NonLazy();
        }
        
        private void BindCurrency()
        {
            Container.Bind<ICurrencyModel>().To<CurrencyModel>().AsSingle().NonLazy();
            Container.Bind<ICurrencyPresenter>().To<CurrencyPresenter>().AsSingle().NonLazy();
            Container.Bind<ICurrencyGUIController>().To<CurrencyGUIController>().AsSingle().NonLazy();
        }
        
        private void BindProgress()
        {
            Container.Bind<IProgressModel>().To<ProgressModel>().AsSingle().NonLazy();
            Container.Bind<IProgressPresenter>().To<ProgressPresenter>().AsSingle().NonLazy();
            Container.Bind<IProgressGUIController>().To<ProgressGUIController>().AsSingle().NonLazy();
        }

        private void BindProcess()
        {
            Container.Bind<IIntermediaryState<ProcessStates>>().To<EnemySelectProcess>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<ProcessStates>>().To<PlayerSelectProcess>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<ProcessStates>>().To<FightProcess>().AsSingle().NonLazy();
            
            Container.Bind<IIntermediary<ProcessStates>>().To<ProcessIntermediary>().AsSingle().NonLazy();
        }

        private void BindCard()
        {

        }
    }
}