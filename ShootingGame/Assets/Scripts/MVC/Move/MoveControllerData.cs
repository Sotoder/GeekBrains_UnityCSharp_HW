using UnityEngine;

namespace Model.ShootingGame
{
    public class MoveControllerData
    {
        private readonly PlayerData _player;
        private readonly InputController _inputController;
        private readonly GameObject _playerObject;

        public float horizontal;
        public float vertical;
        public Vector3 direction;
        public float mouseAxisX;
        public bool isCameraRotate;

        public PlayerData Player => _player;

        public InputController InputController => _inputController;

        public GameObject PlayerObject => _playerObject;

        public MoveControllerData(Player player, InputController inputController)
        {
            _player = player.PlayerData;
            _inputController = inputController;
            _playerObject = player.PlayerData.GameObject;
        }
    }
}
