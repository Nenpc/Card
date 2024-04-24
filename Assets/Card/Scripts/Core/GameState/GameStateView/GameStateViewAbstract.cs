using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public abstract class GameStateViewAbstract : MonoBehaviour
    {
        public abstract GameStates States { get; }
    }
}