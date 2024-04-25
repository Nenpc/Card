using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using Zenject;

namespace TheaCard.Core.Initialize
{
    public sealed class GameInitializer : IInitializable
    {
        private readonly IIntermediary<GameStates> _gameStateIntermediary;

        public GameInitializer(IIntermediary<GameStates> gameStateIntermediary)
        {
            _gameStateIntermediary = gameStateIntermediary;
        }

        public void Initialize()
        {
            _gameStateIntermediary.StartIntermediaryStates();
        }
    }
}