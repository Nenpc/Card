using System;
using System.Collections.Generic;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;

namespace TheaCard.Infrastructure.GameState
{
    public sealed class GameStateIntermediary : IntermediaryAbstract<GameStates>
    {
        private readonly IEnumerable<IIntermediaryState<GameStates>> _gameStates;
        
        protected override GameStates FirstState => GameStates.Menu;
        protected override IEnumerable<IIntermediaryState<GameStates>> States => _gameStates;

        public GameStateIntermediary(IEnumerable<IIntermediaryState<GameStates>> gameStates)
        {
            _gameStates = gameStates;
        }
        
        protected override void OnEndState(GameStates gameStates)
        {
            if (_active)
            {
                _activeState.End();
                _activeState.OnEndState -= OnEndState;
                StartState(GetNextState(_activeState.State));
            }
        }

        protected override GameStates GetNextState(GameStates gameStates)
        {
            var currentState = (int) gameStates;
            var maxValue = Enum.GetValues(typeof(GameStates)).Length - 1;
            var nextState = 0;

            if (currentState == maxValue)
                nextState = 1;
            else
                nextState = currentState + 1;

            return (GameStates) nextState;
        }
    }
}
