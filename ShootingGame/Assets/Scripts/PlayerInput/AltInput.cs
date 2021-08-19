namespace PlayerInput.ShootingGame
{
    using UnityEngine;
    public class AltInput : BaseInput, IInput
    {
        public bool IsFire { get => _isFire; }
        public Vector3 Direction { get => _direction; }
        public float MouseLookX { get => mouseLookX; }
        //public float MouseLookY { get => mouseLookY; }

        protected override void OpenFire()
        {
            if (Input.GetAxis("Fire2") == 1f)
            {
                _isFire = true;
            }
            else if (Input.GetAxis("Fire2") < 1f)
            {
                _isFire = false;
            }
        }
    }
}