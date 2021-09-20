using UnityEngine;
using System;

namespace Model.ShootingGame
{

    public abstract class Mine : MonoBehaviour, IDisposable
    {
        protected delegate void DamagingMethods(int damage);

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

        protected void Undermining()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in colliders)
            {
                if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    var (damagingMethod, damage) = InflictDamage(target);
                    CallDamage(damagingMethod, damage);
                }
            }
            Dispose();
        }

        protected abstract (DamagingMethods damagingMethod, int damage) InflictDamage(IDamageable target);

        protected void CallDamage(DamagingMethods damagingMethod, int damage)
        {
            damagingMethod(damage);
        }

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
