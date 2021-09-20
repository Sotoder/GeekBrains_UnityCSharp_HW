using UnityEngine;

namespace Model.ShootingGame
{
    public class SwapMine : Mine
    {
        [SerializeField] private int _hpForSwap;
        protected override (DamagingMethods damagingMethod, int damage) InflictDamage(IDamageable target) => (target.SwapHP, _hpForSwap);
    }
}
