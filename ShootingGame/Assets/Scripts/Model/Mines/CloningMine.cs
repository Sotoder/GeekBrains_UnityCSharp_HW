using UnityEngine;
using System;

namespace Model.ShootingGame
{
    public class CloningMine : Mine, ICloneable
    {
        [SerializeField] private int _damage = 50;
        [SerializeField] private LayerMask _layerMask;

        private Vector3 _startRayPointUpOffset = Vector3.up * 0.2f;
        private const float MaxCopyDistanse = 2f;
        private const float MinCopyDistanse = 0.5f;
        private const float IndentOfObstacles = 0.5f;


        protected new void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target) || IsPlayer(collision.gameObject, out target))
            {
                Clone();
            }
        }

        public object Clone()
        {
            Vector3 spherePoint = transform.position + (UnityEngine.Random.insideUnitSphere * MaxCopyDistanse);
            Vector3 spawnPoint = new Vector3(spherePoint.x, transform.position.y, spherePoint.z);

            Ray ray = new Ray(transform.position + _startRayPointUpOffset, (spawnPoint - transform.position) + _startRayPointUpOffset);
            if (Physics.Raycast(ray, out var hit, MaxCopyDistanse, _layerMask))
            {
                if (Vector3.Distance(transform.position, hit.point) < MinCopyDistanse) return null; // ??????? ??? ????????? ???????????? ?????? ???, ????? ??? ???????? ? ?????
                spherePoint = transform.position + (UnityEngine.Random.insideUnitSphere * (Vector3.Distance(transform.position, hit.point) - IndentOfObstacles));
                spawnPoint = new Vector3(spherePoint.x, transform.position.y, spherePoint.z);
            }

            var clone = Instantiate(this, spawnPoint, transform.rotation);
            clone.player = player;
            return clone;
        }

        protected override void InflictDamage(IDamageable target)
        {
            target.TakingDamage(_damage);
        }
    }
}
