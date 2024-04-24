using UnityEngine.UI;

namespace TheaCard.Core.GameState
{
    public interface IGameStateFightView : IGameStateBase
    {
        HorizontalLayoutGroup PlayerFightHand { get; }
        HorizontalLayoutGroup PlayerSupportHand { get; }
        HorizontalLayoutGroup EnemyFightHand { get; }
        HorizontalLayoutGroup EnemySupportHand { get; }
        HorizontalLayoutGroup MainField { get; }
        void Show();
        void Hide();
    }
}