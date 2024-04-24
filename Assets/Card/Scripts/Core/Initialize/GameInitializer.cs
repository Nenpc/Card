using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class GameInitializer : IInitializable
    {
        private readonly IntermediaryAbstract<GameStates> _gameStateIntermediary;

        public GameInitializer(IntermediaryAbstract<GameStates> gameStateIntermediary)
        {
            _gameStateIntermediary = gameStateIntermediary;
        }

        public void Initialize()
        {
            _gameStateIntermediary.StartIntermediaryStates();
        }
    }
}