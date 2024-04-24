using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheaCard.Infrastructure.Intermediary
{
    public abstract class IntermediaryAbstract<TState> : IIntermediary<TState> where TState : Enum
    {
        public event Action<TState> OnStateChanged;

        protected abstract TState FirstState { get; }
        protected abstract IEnumerable<IIntermediaryState<TState>> States { get; }
        
        protected IIntermediaryState<TState> _activeState;
        protected bool _active;

        public virtual void StartIntermediaryStates()
        {
            _active = true;
            StartState(FirstState);
        }

        public virtual void EndIntermediaryStates()
        {
            _active = false;
            _activeState?.End();
        }

        protected virtual void StartState(TState stateType)
        {
            var state = States.FirstOrDefault(x => x.State.Equals(stateType));

            if (state == default)
            {
                Debug.LogError($"Logic for state {stateType} doesn't exist!");
                return;
            }
            state.OnEndState += OnEndState;
            OnStateChanged?.Invoke(stateType);
            _activeState = state;
            state.Start();
        }

        protected virtual void OnEndState(TState states)
        {
            if (_active)
            {
                _activeState.OnEndState -= OnEndState;
                _activeState.End();
                StartState(GetNextState(_activeState.State));
            }
        }

        protected abstract TState GetNextState(TState states);
    }
}