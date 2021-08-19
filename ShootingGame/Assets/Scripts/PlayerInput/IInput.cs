using UnityEngine;

namespace PlayerInput.ShootingGame
{
    public interface IInput
    {
        public bool IsFire { get; }
        public Vector3 Direction { get; }
        public float MouseLookX { get; }
        public float MouseLookY { get; }
    }
}
