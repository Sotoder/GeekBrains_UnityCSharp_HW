using System;
using UnityEngine;
using UnityEngine.Events;

namespace Model.ShootingGame {
    public class BuffBehaviour : MonoBehaviour, IRotate, IBuff
    {
        public UnityAction<BuffBehaviour, BuffData> buffCollected;

        [SerializeField] private BuffData _buffData;
        [SerializeField] private GameObject _buffPrefab;

        private float _baseY;

        public BuffData BuffData => _buffData;

        protected void Awake()
        {
            var pathsCollection = new BuffPrefabPath();
            var prefabPath = pathsCollection.prefabsPaths[_buffData.buffStructure.BuffType];
            _buffPrefab = Resources.Load(prefabPath) as GameObject;

            var buffObject = Instantiate(_buffPrefab, transform.position, transform.rotation);
            buffObject.transform.SetParent(transform, false);

            _baseY = _buffPrefab.transform.position.y;
        }

        private void Update()
        {
            RotateBuff();
        }

        public void RotateBuff()
        {
            _buffPrefab.transform.Rotate(0, 0, 1);
            _buffPrefab.transform.position = new Vector3(_buffPrefab.transform.position.x, _baseY + Mathf.Sin(Time.deltaTime * 3f) * 0.2f, _buffPrefab.transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            buffCollected?.Invoke(this, _buffData);
        }
    }
}
