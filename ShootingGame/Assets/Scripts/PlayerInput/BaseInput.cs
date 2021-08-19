namespace PlayerInput.ShootingGame
{
    using UnityEngine;
    public abstract class BaseInput : MonoBehaviour
    {
        protected Vector3 _direction;
        protected float mouseLookX;
        //protected float mouseLookY;
        protected bool _isFire;

        private void FixedUpdate()
        {
            _direction.z = Input.GetAxis("Horizontal");
            _direction.x = Input.GetAxis("Vertical");

            mouseLookX = Input.GetAxis("Mouse X");
            //mouseLookY = Input.GetAxis("Mouse Y");

            OpenFire();
        }

        protected abstract void OpenFire();
    }
}

