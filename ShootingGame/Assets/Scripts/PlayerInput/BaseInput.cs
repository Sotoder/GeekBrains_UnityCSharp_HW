using UnityEngine;

namespace PlayerInput.ShootingGame
{
    public class BaseInput : MonoBehaviour, IInput
    {
        private Vector3 _direction;
        private float _mouseLookX;
        private bool _isFire;
        private bool _isCameraRotate;

        public bool IsFire { get => _isFire; }
        public Vector3 Direction { get => _direction; }
        public float MouseLookX { get => _mouseLookX; }
        public bool IsCameraRotate { get => _isCameraRotate; }

        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const string MOUSE_X = "Mouse X";
        private const string FIRE1 = "Fire1";
        private const string CAMERA_ROTATE = "CameraRotate";

        private void FixedUpdate()
        {
            _direction.z = Input.GetAxis(HORIZONTAL);
            _direction.x = Input.GetAxis(VERTICAL);

            _mouseLookX = Input.GetAxis(MOUSE_X);

            OpenFire();
        }

        private void OpenFire()
        {
            if (Input.GetAxis(FIRE1) == 1f)
            {
                _isFire = true;
            }
            else if (Input.GetAxis(FIRE1) < 1f)
            {
                _isFire = false;
            }

            if (Input.GetAxis(CAMERA_ROTATE) == 1f)
            {
                _isCameraRotate = true;
            }
            else if (Input.GetAxis(CAMERA_ROTATE) < 1f)
            {
                _isCameraRotate = false;
            }
        }
    }
}

