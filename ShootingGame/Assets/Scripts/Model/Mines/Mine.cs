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
        
        public Player player;

        protected bool _isBombed = false;
        protected float _stayTime = 0f;

        protected void OnTriggerStay(Collider other)
        {
            if (_isBombed) return;
            if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable target) || IsPlayer(other.gameObject, out target))
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
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target) || IsPlayer(collision.gameObject, out target))
            {
                Undermining();
            }
        }

        protected void Undermining()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in colliders)
            {
                if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable target) || IsPlayer(hit.gameObject, out target))
                {
                    InflictDamage(target);
                }
            }
            Dispose();
        }

        protected bool IsPlayer(GameObject gameObject, out IDamageable target)
        {
            if (gameObject.GetInstanceID() == player.PlayerData.GameObject.GetInstanceID())
            {
                target = player;
                return true;
            } else
            {
                target = null;
                return false;
            }
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
