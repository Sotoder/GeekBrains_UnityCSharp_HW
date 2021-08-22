using UnityEngine;

namespace Model.ShootingGame
{
    public class SimpleMine : Mine
    {
        [SerializeField] private int _damage = 50;

        protected override void Undermining()
        {

            var colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in colliders)
            {
                if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    target.TakeDamage(_damage);
                }
            }

            _particleObject.SetActive(true);
            GetComponent<AudioSource>().Play();
            Destroy(_mineBody);
            _isBombed = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }
}
