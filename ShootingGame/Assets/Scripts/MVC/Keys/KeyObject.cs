using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct KeyObject : IUniqObjectCollectionElement
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private GameObject _keyPrefab;

        public GameObject Object { get => _object; }
        public GameObject KeyPrefab { get => _keyPrefab; }
    }
}