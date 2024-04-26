using TheaCard.Core.Buff;
using TheaCard.Core.Enums;
using UnityEngine;

namespace TheaCard.Core.Heroes
{
    public interface IHeroModel
    {
        public IHeroConfig Def { get; }
        public string Name { get; }
        public Sprite Icon { get; }
        public int BaseDamage { get; }
        public int Damage { get; }
        public int BaseHealth { get; }
        public int Defense { get; }
        public int BaseDefense { get; }
        public int Health { get; }
        public IBuff Buff { get; }
        public GameTeam Team { get; }
        public Hands Hand { get; }
        public int Place { get; }
        public bool OnBoard { get; }
        public bool Active { get; }

        public void SetPlace(int place);
        public void TakeDamage(int damage);
        public void TakeHeal(int heal);
        public void UpdateDamage(int amount);
        public void UpdateDefense(int amount);
        public void ActiveHero();
    }
}