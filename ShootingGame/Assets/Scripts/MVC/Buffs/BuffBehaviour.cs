using System;
using UnityEngine;
using UnityEngine.Events;

namespace Model.ShootingGame {
    public sealed class BuffBehaviour : MonoBehaviour, IRotateble, IBuff, ITwitching
    {
        public UnityAction<BuffData> buffCollected;

        [SerializeField] private BuffData _buffData;

        private float _baseY;
        private GameObject _buffObject;

        public BuffData BuffData => _buffData;

        private const int Z_ANGLE = 1;
        private const float TWITCH_SPEED = 3f;
        private const float TWITCH_AMPLITUDE = 0.2f;

        private void Awake()
        {
            var pathsCollection = new BuffPrefabPath();
            var prefabPath = pathsCollection.prefabsPaths[_buffData.buffStructure.BuffType];
            var buffPrefab = Resources.Load(prefabPath) as GameObject;

            _buffObject = Instantiate(buffPrefab, transform.position, new Quaternion(x: -0.7f, transform.rotation.y, transform.rotation.z, w: 0.7f));
            _buffObject.transform.SetParent(transform);

            _baseY = _buffObject.transform.position.y;
        }

        private void FixedUpdate()
        {
            RotateBuff();
            TwitchBuf();
        }

        public void TwitchBuf()
        {
            _buffObject.transform.position = new Vector3(_buffObject.transform.position.x, _baseY + Mathf.Sin(Time.fixedTime * TWITCH_SPEED) * TWITCH_AMPLITUDE, _buffObject.transform.position.z);
        }

        public void RotateBuff()
        {
            _buffObject.transform.Rotate(0, 0, Z_ANGLE);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                buffCollected?.Invoke(_buffData);
                Destroy(gameObject);
            }
        }
    }
}
