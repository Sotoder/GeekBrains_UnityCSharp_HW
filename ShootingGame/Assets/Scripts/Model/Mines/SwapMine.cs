using UnityEngine;

namespace Model.ShootingGame
{
    public class SwapMine : Mine
    {
        [SerializeField] private int _hpForSwap;
        protected override void InflictDamage(IDamageable target)
        {
            target.SwapingHP(_hpForSwap);
        }
    }
}
