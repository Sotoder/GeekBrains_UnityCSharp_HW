using UnityEngine;

namespace PlayerInput.ShootingGame
{
    public interface IInput
    {
        public bool IsFire { get; }
        public bool IsCameraRotate { get; }
        public Vector3 Direction { get; }
        public float MouseLookX { get; }
    }
}
