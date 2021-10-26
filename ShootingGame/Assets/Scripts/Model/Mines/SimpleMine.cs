using UnityEngine;

namespace Model.ShootingGame
{
    public class SimpleMine : Mine
    {
        [SerializeField] private int _damage = 50;

        protected override void InflictDamage(IDamageable target)
        {
            target.TakingDamage(_damage);
        }
    }
}
