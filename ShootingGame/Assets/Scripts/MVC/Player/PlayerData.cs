using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public class PlayerData
    {
        private readonly float _sensetivity;
        private readonly GameObject _gameObject;

        public bool isStay = true;
        public Rigidbody unitRigidBody;

        private Parameters _parameters;
        private Keystorege _keystorege;
        private Weapon[] _arsenal;
        private InputController _inputController;
        private Animator _animator;
        private Transform _rightGunBone;

        public InputController InputController { get => _inputController; }
        public Parameters Parameters { get => _parameters; }
        public Keystorege Keystorege { get => _keystorege; }
        public Weapon[] Arsenal { get => _arsenal; }
        public Transform RightGunBone { get => _rightGunBone; }
        public Animator Animator { get => _animator; }
        public float Sensetivity { get => _sensetivity; }
        public GameObject GameObject { get => _gameObject; }

        public PlayerData(Player player, InputController inputController, PlayerInitializationData playerInitializationData)
        {
            _animator = playerInitializationData.PlayerObject.GetComponent<Animator>();
            unitRigidBody = playerInitializationData.PlayerObject.GetComponent<Rigidbody>();
            _gameObject = playerInitializationData.PlayerObject;
            _inputController = inputController;
            _rightGunBone = playerInitializationData.RightGunBone;
            _sensetivity = playerInitializationData.Sensetivity;

            _keystorege = new Keystorege(player);
            _parameters = new Parameters(player, playerInitializationData.MaxHP, playerInitializationData.MaxStamina, playerInitializationData.SpeedForInitialization);
            _arsenal = playerInitializationData.Arsenal;
        }
    }
}
