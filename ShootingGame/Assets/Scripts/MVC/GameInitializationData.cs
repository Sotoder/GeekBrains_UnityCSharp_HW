using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct GameInitializationData
    {
        [SerializeField] private InputData _inputData;
        [SerializeField] private PlayerInitializationData _playerInitializationData;
        [SerializeField] private BuffInitializationData _buffObjectsInitializationData;
        [SerializeField] private UIInitializationData _uIInitializationData;
        [SerializeField] private KeyObjectsInitializationData _keyObjectsInitializationData;
        [SerializeField] private MiniMapInitializationData _miniMapInitializationData;
        [SerializeField] private RadarInitializationData _radarInitializationData;
        [SerializeField] private CameraInitializationData _cameraInitializationData;

        public BuffInitializationData BuffObjectsInitializationData { get => _buffObjectsInitializationData; }
        public UIInitializationData UIInitializationData { get => _uIInitializationData; }
        public KeyObjectsInitializationData KeyObjectsInitializationData { get => _keyObjectsInitializationData; }
        public MiniMapInitializationData MiniMapInitializationData { get => _miniMapInitializationData; }
        public RadarInitializationData RadarInitializationData { get => _radarInitializationData; }
        public InputData InputData { get => _inputData; }
        public CameraInitializationData CameraInitializationData { get => _cameraInitializationData; }
        public PlayerInitializationData PlayerInitializationData { get => _playerInitializationData; }
    }
}