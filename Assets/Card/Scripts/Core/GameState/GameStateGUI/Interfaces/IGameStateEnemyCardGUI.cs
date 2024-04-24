using System;
using TheaCard.Core.Enums;

namespace TheaCard.Core.GameState
{
    public interface IGameStateEnemyCardGUI
    {
        event Action OnNextStage;
        event Action<FightType> OnFightTypeChanged;
        public void Show();
        public void Hide();
    }
}