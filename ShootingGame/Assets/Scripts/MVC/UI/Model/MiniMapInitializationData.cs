using UnityEngine;
using System;

namespace Model.ShootingGame
{
    [Serializable]
    public struct MiniMapInitializationData
    {
        [SerializeField] private Camera _miniMapCamera;

        public Camera MiniMapCamera { get => _miniMapCamera; }
    }
}