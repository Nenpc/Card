using System;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.Intermediary;
using UnityEngine;

namespace TheaCard.Core.GameState
{
    public sealed class GameStateMenu : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.Menu;

        private readonly IGameStateMenuGUI _gui;
        
        public GameStateMenu(IGameStateMenuGUI gui)
        {
            _gui = gui;
            _gui.StartGame.onClick.AddListener(() => OnEndState?.Invoke(State));
            _gui.Quite.onClick.AddListener(EndGame);
        }
        
        public void Dispose()
        {
            _gui?.StartGame.onClick.RemoveAllListeners();
            _gui?.Quite.onClick.RemoveAllListeners();
        }

        public void Start()
        {
            _gui.Show();
        }
        
        private void EndGame()
        {
            Application.Quit();
        }

        public void End()
        {
            _gui.Hide();
        }
    }
}