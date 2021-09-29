using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected Parameters _parameters;
        [SerializeField] protected int _speedForInitialization;

        protected Rigidbody _rb;
    }
}
