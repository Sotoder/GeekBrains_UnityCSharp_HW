using UnityEngine;

namespace Model.ShootingGame
{
    public class CameraControllerData
    {
        private readonly Camera _camera;
        private readonly IPlayer _player;
        private readonly InputController _inputController;
        private readonly Transform _target;

        private readonly LayerMask _noPlayer;
        private readonly LayerMask _environtment;
        public LayerMask camBaseMask;

        public float camDistance;
        public float mouseAxisX;
        public float stayTime;

        public Vector3 localPosition;
        public bool isCameraRotated;
        public bool isCameraRotate;

        public CountdownTimer timer;
        public bool isTimerWorking;

        public const float SPEED_X = 360f;
        public const float HIDE_DISTANSE = 1f;
        public const float CAMERA_FOLLOW_SPEED = 0.05f;
        public const float OFFSET = 0.1f;
        public const float MOTION_WAITING_TIME = 1f;
        public const float CAMERA_LERP_SPEED = 5f;
        public const float CAMERA_LERP_TIME = 1.2f;

        public Camera Camera => _camera;
        public IPlayer Player => _player;
        public LayerMask NoPlayer => _noPlayer;
        public LayerMask Environtment => _environtment;
        public Transform Target => _target;
        public GameObject PlayerObject => _player.PlayerData.GameObject;
        public InputController InputController => _inputController;

        public CameraControllerData(Camera camera, IPlayer player, CameraInitializationData cameraInitializationData, InputController inputController)
        {
            _camera = camera;
            _player = player;
            _target = cameraInitializationData.Target;
            _noPlayer = cameraInitializationData.NoPlayer;
            _environtment = cameraInitializationData.Environtment;
            _inputController = inputController;
        }
    }
}
