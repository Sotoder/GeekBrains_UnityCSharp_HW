using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected int _maxHP;
        [SerializeField] protected float _speed;
        //[SerializeField] protected GameObject _head;

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
