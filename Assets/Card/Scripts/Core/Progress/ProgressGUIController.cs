using System;
using System.Collections.Generic;
using TheaCard.Core.Enums;

namespace TheaCard.Core.Progress
{
    public sealed class ProgressGUIController : IProgressGUIController, IDisposable
    {
        private readonly IProgressGUI _progressGUI;

        private bool _isActive;
        
        public ProgressGUIController(IProgressGUI progressGUI)
        {
            _progressGUI = progressGUI;
            _progressGUI.OnHideButtonClick += HidePopup;
            _progressGUI.OnShowButtonClick += ShowPopup;
        }
        
        public void Dispose()
        {
            _progressGUI.OnHideButtonClick -= HidePopup;
            _progressGUI.OnShowButtonClick += ShowPopup;
        }

        public void HidePopup()
        {
            _progressGUI.HidePopup();
        }
        
        private void ShowPopup()
        {
            _progressGUI.ShowPopup();
        }

        public void UpdateProgress(IReadOnlyDictionary<ProgressTypes, int> progress)
        {
            _progressGUI.SetDamageDoneAmount(progress[ProgressTypes.DamageDone]);
            _progressGUI.SetDamageReceivedAmount(progress[ProgressTypes.DamageReceived]);
            _progressGUI.SetCardsDestroyedAmount(progress[ProgressTypes.CardsDestroyed]);
            _progressGUI.SetLostCardsAmount(progress[ProgressTypes.LostCards]);
            _progressGUI.SetGamesPlayedAmount(progress[ProgressTypes.GamesPlayed]);
        }
        
        public void ShowPanel()
        {
            if (!_isActive)
            {
                _progressGUI.ShowPanel();
                _isActive = true;
            }
        }

        public void HidePanel()
        {
            if (_isActive)
            {
                _progressGUI.HidePanel();
                _isActive = false;
            }
        }
    }
}