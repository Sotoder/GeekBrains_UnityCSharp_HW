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
            if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Enemy"))
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
                if (Vector3.Distance(transform.position, hit.point) < MinCopyDistanse) return null; // костыль для остановки бесконечного спавна мин, когда они уперлись в стену
                spherePoint = transform.position + (UnityEngine.Random.insideUnitSphere * (Vector3.Distance(transform.position, hit.point) - IndentOfObstacles));
                spawnPoint = new Vector3(spherePoint.x, transform.position.y, spherePoint.z);
            }

            return Instantiate(this, spawnPoint, transform.rotation);
        }

        protected override (DamagingMethods damagingMethod, int damage) InflictDamage(IDamageable target) => (target.TakeDamage, _damage);
    }
}
