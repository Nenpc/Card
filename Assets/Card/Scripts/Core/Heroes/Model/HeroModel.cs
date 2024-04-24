using System;
using System.Linq;
using TheaCard.Core.Buff;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    public sealed class HeroModel : IHeroModel
    {
        public event Action<IHeroModel> OnDeath;
        
        private FightType FightType;
        private IHeroConfig _def;
        private GameTeam _team;
        private Hands _hand;
        private int _damage;
        private int _baseDamage;
        private int _health;
        private int _baseHealth;
        private int _defense;
        private int _baseDefense;
        private int _place;
        private IBuff _buff;
        
        public IHeroConfig Def => _def;
        public string Name => Def.Name;
        public Sprite Icon => Def.Icon;
        public int BaseDamage => _baseDamage;
        public int Damage => _damage;
        public int BaseHealth => _baseHealth;
        public int Defense => _defense;
        public int BaseDefense => _baseDefense;
        public int Health => _health;
        public IBuff Buff => _buff;
        public GameTeam Team => _team;
        public Hands Hand => _hand;
        public int Place => _place;
        public bool OnBoard => _place > -1;
        
        public HeroModel(IHeroConfig def, FightType fightType, GameTeam team, Hands hand)
        {
            _place = -1;
            _def = def;
            _team = team;
            _hand = hand;

            var damageInfo = _def.DamageInfos.FirstOrDefault(x => x.FightType == fightType);
            
            _baseHealth = damageInfo.Health;
            _health = _baseHealth;
            _baseDamage = damageInfo.Damage;
            _damage = _baseDamage;
            _baseDefense = damageInfo.Defense;
            _defense = _baseDefense;
        }

        public void SetPlace(int place)
        {
            if (place > -1)
                _place = place;
        }

        public void TakeDamage(int damage)
        {
            if (_health <= 0) 
                return;
            
            _health = Mathf.Clamp(_health - damage, 0, _baseHealth);
            
            if (_health == 0)
                OnDeath?.Invoke(this);
        }
        
        public void TakeHeal(int heal)
        {
            if (_health <= 0) 
                return;
            
            _health = Mathf.Clamp(_health + heal, 0, _baseHealth);
        }

        public void UpdateDamage(int amount)
        {
            _damage = Mathf.Clamp(_damage + amount, 0, int.MaxValue);
        }
        
        public void UpdateDefense(int amount)
        {
            _defense = Mathf.Clamp(_defense + amount, 0, int.MaxValue);
        }

        public override string ToString() => $"Figure, def:{Def}, team:{Team}";
    }
}