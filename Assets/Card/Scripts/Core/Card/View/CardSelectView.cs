using System;
using System.Linq;
using TheaCard.Core.Buff;
using TheaCard.Core.Heroes;
using TheaCard.Core.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheaCard.Core.Card
{
    public sealed class CardSelectView : MonoBehaviour, ICardSelectView
    {
        public event Action<IHeroConfig> OnCardClick;
        [SerializeField] private Button _clickButton;
        [SerializeField] private Image _selectImage;
        
        [Header("Info")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _price;
        
        [Header("Physical")]
        [SerializeField] private TextMeshProUGUI _physicalDamage;
        [SerializeField] private TextMeshProUGUI _physicalDefinition;
        [SerializeField] private TextMeshProUGUI _physicalHealth;
        
        [Header("Magic")]
        [SerializeField] private TextMeshProUGUI _magicDamage;
        [SerializeField] private TextMeshProUGUI _magicDefinition;
        [SerializeField] private TextMeshProUGUI _magicHealth;
        
        [Header("Verbal")]
        [SerializeField] private TextMeshProUGUI _verbalDamage;
        [SerializeField] private TextMeshProUGUI _verbalDefinition;
        [SerializeField] private TextMeshProUGUI _verbalHealth;
        
        [Header("Baff")]
        [SerializeField] private TextMeshProUGUI _baffName;
        
        private IHeroConfig _heroConfig;
        
        public GameObject View => gameObject;

        private void Awake()
        {
            _clickButton.onClick.AddListener(() => OnCardClick?.Invoke(_heroConfig));
        }

        private void OnDestroy()
        {
            _clickButton.onClick.RemoveAllListeners();
        }

        public void Select()
        {
            _selectImage.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _selectImage.gameObject.SetActive(false);
        }

        public void Init(IHeroConfig heroConfig)
        {
            _heroConfig = heroConfig;
            _name.text = _heroConfig.Name;
            _icon.sprite = _heroConfig.Icon;
            _price.text = heroConfig.Price.ToString();
            
            {
                var fightInfo = _heroConfig.DamageInfos.FirstOrDefault(x => x.FightType == FightType.Physical);
                _physicalDamage.text = fightInfo.Damage.ToString();
                _physicalDefinition.text = fightInfo.Defense.ToString();
                _physicalHealth.text = fightInfo.Health.ToString();
            }
            
            {
                var fightInfo = _heroConfig.DamageInfos.FirstOrDefault(x => x.FightType == FightType.Magic);
                _magicDamage.text = fightInfo.Damage.ToString();
                _magicDefinition.text = fightInfo.Defense.ToString();
                _magicHealth.text = fightInfo.Health.ToString();
            }
            
            {
                var fightInfo = _heroConfig.DamageInfos.FirstOrDefault(x => x.FightType == FightType.Verbal);
                _verbalDamage.text = fightInfo.Damage.ToString();
                _verbalDefinition.text = fightInfo.Defense.ToString();
                _verbalHealth.text = fightInfo.Health.ToString();
            }

            if (_heroConfig.Buff != BuffType.None)
            {
                _baffName.text = _heroConfig.Buff.ToString();
            }
            else
            {
                _baffName.text = "";
            }
        }
    }
}