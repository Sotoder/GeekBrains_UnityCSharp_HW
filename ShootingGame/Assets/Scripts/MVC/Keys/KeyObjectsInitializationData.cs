using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    [Serializable]
    public struct KeyObjectsInitializationData
    {
        [SerializeField] private List<KeyObject> _keyObjectsCollection;
        [SerializeField] private Image _icoForRadarObject;

        public List<KeyObject> KeyObjectsCollection { get => _keyObjectsCollection; }
        public Image IcoForRadarObject { get => _icoForRadarObject; }
    }
}
