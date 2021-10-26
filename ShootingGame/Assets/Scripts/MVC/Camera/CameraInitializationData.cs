using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct CameraInitializationData
    {
        [SerializeField] private LayerMask _noPlayer;
        [SerializeField] private LayerMask _environtment;
        [SerializeField] private Transform _target;

        public LayerMask NoPlayer { get => _noPlayer; }
        public LayerMask Environtment { get => _environtment; }
        public Transform Target { get => _target; }
    }
}
