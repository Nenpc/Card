using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public abstract class GameStateGUIAbstract : MonoBehaviour
    {
        public abstract GameStates States { get; }
    }
}