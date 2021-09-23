using UnityEngine;
using System;

namespace Model.ShootingGame
{

    public abstract class Mine : MonoBehaviour, IDisposable
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
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
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
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
            {
                Undermining();
            }
        }

        protected void Undermining()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in colliders)
            {
                if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    InflictDamage(target);
                }
            }
            Dispose();
        }

        protected abstract void InflictDamage(IDamageable target);

        public void Dispose()
        {
            _particleObject.SetActive(true);
            GetComponent<AudioSource>().Play();
            Destroy(_mineBody);
            _isBombed = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }
}
