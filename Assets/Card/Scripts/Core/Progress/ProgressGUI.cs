using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.Progress
{
    public sealed class ProgressGUI : MonoBehaviour, IProgressGUI
    {
        public event Action OnHideButtonClick;
        public event Action OnShowButtonClick;

        [SerializeField] private Button _showPanelButton;
        
        [Header("Popup")]
        [SerializeField] private GameObject _progressPopup;
        [SerializeField] private TextMeshProUGUI _damageDoneText;
        [SerializeField] private TextMeshProUGUI _damageReceivedText;
        [SerializeField] private TextMeshProUGUI _cardsDestroyedText;
        [SerializeField] private TextMeshProUGUI _lostCardsText;
        [SerializeField] private TextMeshProUGUI _gamesPlayedText;
        
        [SerializeField] private Button _hidePanelButton;

        private void Awake()
        {
            _hidePanelButton.onClick.AddListener(() => OnHideButtonClick?.Invoke());
            _showPanelButton.onClick.AddListener(() => OnShowButtonClick?.Invoke());
        }

        private void OnDestroy()
        {
            _hidePanelButton.onClick.RemoveAllListeners();
            _showPanelButton.onClick.RemoveAllListeners();
        }

        public void SetDamageDoneAmount(int amount) => _damageDoneText.text = amount.ToString();
        public void SetDamageReceivedAmount(int amount) => _damageReceivedText.text = amount.ToString();
        public void SetCardsDestroyedAmount(int amount) => _cardsDestroyedText.text = amount.ToString();
        public void SetLostCardsAmount(int amount) => _lostCardsText.text = amount.ToString();
        public void SetGamesPlayedAmount(int amount) => _gamesPlayedText.text = amount.ToString();

        public void ShowPopup() => _progressPopup.SetActive(true);

        public void HidePopup() => _progressPopup.SetActive(false);

        public void ShowPanel()
        {
            _showPanelButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            _showPanelButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}