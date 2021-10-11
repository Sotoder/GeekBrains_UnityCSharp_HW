using System;
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    [Serializable]
    public struct BuffObject : IUniqObjectCollectionElement
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private BuffData _buffData;
        [SerializeField] private Image _icoForRadarObject;

        public GameObject Object { get => _object; }
        public BuffData BuffData { get => _buffData; }
        public Image IcoForRadarObject { get => _icoForRadarObject; }
    }
}
