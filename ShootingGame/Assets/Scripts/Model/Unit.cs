using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected int _maxHP;
        [SerializeField] protected float _speed;

        protected int _curentHP;
        protected Rigidbody _rb;
        public enum BuffsAndDebuffs
        {
            Speed,
            Heal,
            Rage,
            Ammo
        }
    }
}
