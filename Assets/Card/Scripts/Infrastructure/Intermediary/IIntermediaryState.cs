using System;

namespace TheaCard.Infrastructure.Intermediary
{
    public interface IIntermediaryState<TState> where TState : Enum
    {
        event Action<TState> OnEndState;
        TState State { get; }
        void Start();
        void End();
    }
}