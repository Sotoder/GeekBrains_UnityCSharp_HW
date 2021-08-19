using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected float _hp;
        [SerializeField] protected float _speed;
        //[SerializeField] protected GameObject _head;

        protected Rigidbody _rb;
    }
}
