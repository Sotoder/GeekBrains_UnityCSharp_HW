using UnityEngine;

namespace Model.ShootingGame
{
    public class DeathMine : Mine
    {
        protected override (DamagingMethods damagingMethod, int damage) InflictDamage(IDamageable target)
        {
            int damage = target.MaxHP;
            return (target.TakeDamage, damage);
        }
    }
}
