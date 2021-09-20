using UnityEngine;

namespace Model.ShootingGame
{
    public class SimpleMine : Mine
    {
        [SerializeField] private int _damage = 50;

        protected override (DamagingMethods damagingMethod, int damage) InflictDamage(IDamageable target) => (target.TakeDamage, _damage);
    }
}
