using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct RadarInitializationData
    {
        [SerializeField] private GameObject _radar;

        public GameObject Radar { get => _radar; }
    }
}