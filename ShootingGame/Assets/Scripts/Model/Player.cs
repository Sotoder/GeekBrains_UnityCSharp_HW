using PlayerInput.ShootingGame;

namespace Model.ShootingGame
{
    using UnityEngine;
    public class Player : Unit
    {
        [SerializeField] private float _sensetivity;

        private float _mouseLookX;
        private float _mouseLookY;
        float _xRotation;

        private IInput _input;
        private Vector3 _moveForvard;
        private Vector3 _moveRight;
        private bool _isStandartInput = true;


        private void Awake()
        {
            _input = GetComponent<StandartInput>();
            _rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeInputType();
            }

            if (_input.IsFire)
            {
                Debug.Log("Pew");
            }
        }

        private void FixedUpdate()
        {
            _moveForvard = _input.Direction.x * _speed * transform.forward;
            _moveRight = _input.Direction.z * _speed * transform.right;

            _mouseLookX = _input.MouseLookX * _sensetivity;
            _mouseLookY = _input.MouseLookY * _sensetivity;

            MovementLogic(_moveForvard + _moveRight);
            PlayerLook();
        }

        private void MovementLogic(Vector3 moveVector)
        {
            _rb.velocity = moveVector;
            //_rb.AddForce(transform.forward * speed.z, ForceMode.VelocityChange);
            //_rb.AddForce(transform.right * speed.x, ForceMode.VelocityChange);
        }

        private void PlayerLook()
        {
            transform.Rotate(0, _mouseLookX, 0); // переделать позже - сначала поворачивается голова, на 45 градусах поворачивается тело

            _xRotation += _mouseLookY;
            _xRotation = Mathf.Clamp(_xRotation, -25f, 40f);
            _head.transform.localRotation = Quaternion.Euler(0, 0, _xRotation);
        }

        private void ChangeInputType()
        {
            if (_isStandartInput)
            {
                _input = GetComponent<AltInput>();
                _isStandartInput = false;
            }
            else
            {
                _input = GetComponent<StandartInput>();
                _isStandartInput = true;
            }
        }
    }
}
