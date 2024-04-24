using System;
using TheaCard.Core.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.Card
{
    public sealed class CardFightView : MonoBehaviour, ICardFightView
    {
        public event Action<IHeroModel> OnCardClick;
        
        [SerializeField] private Button _clickButton;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _backImage;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _damageIcon;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _defense;
        [SerializeField] private TextMeshProUGUI _health;
        
        private IHeroModel _heroModel;

        public GameObject View => gameObject;
        
        private void Awake()
        {
            _clickButton.onClick.AddListener(() => OnCardClick?.Invoke(_heroModel));
        }
        
        private void OnDestroy()
        {
            _clickButton.onClick.RemoveAllListeners();
        }
        
        public void Init(IHeroModel heroModel, bool mainSide = true)
        {
            _heroModel = heroModel;
            _name.text = _heroModel.Name;
            _icon.sprite = _heroModel.Icon;
            _backImage.gameObject.SetActive(!mainSide);
            UpdateView();
        }

        public void RotateCard(bool mainSide)
        {
            _backImage.gameObject.SetActive(mainSide);
        }

        public void UpdateView()
        {
            var damageText = _heroModel.Damage + "/" + _heroModel.BaseDamage;
            _damage.text = damageText;
            _damage.color = GetColor(_heroModel.Damage, _heroModel.BaseDamage);
            
            var defenseText = _heroModel.Defense + "/" + _heroModel.BaseDefense;
            _defense.text = defenseText;
            _defense.color = GetColor(_heroModel.Defense, _heroModel.BaseDefense);
            
            var healthText = _heroModel.Health + "/" + _heroModel.BaseHealth;
            _health.text = healthText;
            _health.color = GetColor(_heroModel.Health, _heroModel.BaseHealth);
        }
        
        private Color GetColor(int currentValue, int baseValue)
        {
            if (currentValue > baseValue)
                return Color.green;
            else if (currentValue < baseValue)
                return Color.red;
            else
                return Color.white;
        }
    }
}