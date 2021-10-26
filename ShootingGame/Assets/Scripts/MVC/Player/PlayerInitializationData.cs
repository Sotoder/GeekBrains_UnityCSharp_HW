using UnityEngine;
using System;

namespace Model.ShootingGame
{
    [Serializable]
    public struct PlayerInitializationData
    {
        [SerializeField] private GameObject playerObject;
        [SerializeField] private int _speedForInitialization;
        [SerializeField] private float _sensetivity;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _maxStamina;
        [SerializeField] private Transform rightGunBone;
        [SerializeField] private Weapon[] _arsenal;

        public int SpeedForInitialization { get => _speedForInitialization; }
        public float Sensetivity { get => _sensetivity; }
        public Transform RightGunBone { get => rightGunBone; }
        public int MaxHP { get => _maxHP; }
        public int MaxStamina { get => _maxStamina; }
        public Weapon[] Arsenal { get => _arsenal; }
        public GameObject PlayerObject { get => playerObject; }
    }
}
