using TMPro;
using UnityEngine;

namespace TheaCard.Core.Currency
{
    public sealed class CurrencyGUI : MonoBehaviour, ICurrencyGUI
    {
        [SerializeField] private TextMeshProUGUI _silverText;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _cristalText;

        public void SetSilverAmount(int amount) => _silverText.text = amount.ToString();
        public void SetGoldAmount(int amount) => _goldText.text = amount.ToString();
        public void SetCristalAmount(int amount) => _cristalText.text = amount.ToString();
        public void ShowPanel() => gameObject.SetActive(true);
        public void HidePanel() => gameObject.SetActive(false);
    }
}