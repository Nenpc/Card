using System;

namespace TheaCard.Infrastructure.Intermediary
{
    public interface IIntermediary<TStates> where TStates : Enum
    {
        event Action<TStates> OnStateChanged;
        event Action<TStates> OnEndState;
        void StartIntermediaryStates();
        void EndIntermediaryStates();
    }
}