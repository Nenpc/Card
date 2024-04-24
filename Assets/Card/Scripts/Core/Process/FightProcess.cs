using System;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using UnityEngine;

namespace TheaCard.Core.Process
{
    public sealed class FightProcess : IIntermediaryState<ProcessStates>
    {
        public event Action<ProcessStates> OnEndState;

        public ProcessStates State => ProcessStates.Fight;

        public void Start()
        {
            Debug.Log("Start");
        }

        public void End()
        {
            Debug.Log("End");
        }
    }
}