using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected Parameters _parameters;
        [SerializeField] protected float _speed;

        protected Rigidbody _rb;
    }
}
