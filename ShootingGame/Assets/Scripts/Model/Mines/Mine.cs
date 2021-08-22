using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class Mine : MonoBehaviour
    {
        [SerializeField] protected float _activationTime = 3f;
        [SerializeField] protected float _radius = 3f;
        [SerializeField] protected GameObject _particleObject;
        [SerializeField] protected GameObject _mineBody;
        protected bool _isBombed = false;
        protected float _stayTime = 0f;

        protected void OnTriggerStay(Collider other)
        {
            if (_isBombed) return;
            if (other.tag.Equals("Player") || other.tag.Equals("Enemy"))
            {

                _stayTime += Time.deltaTime;

                if (_stayTime >= _activationTime)
                {
                    _stayTime = 0f;
                    Undermining();
                }
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (_isBombed) return;
            _stayTime = 0f;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
            {
                Undermining();
            }
        }

        protected abstract void Undermining();
    }
}
