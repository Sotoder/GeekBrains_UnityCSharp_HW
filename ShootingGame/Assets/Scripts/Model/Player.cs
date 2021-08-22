using PlayerInput.ShootingGame;

namespace Model.ShootingGame
{
    using System.Collections;
    using UnityEngine;

    [System.Serializable]

    public class Player : Unit, IDamageable
    {
        private struct Inventory
        {
            [SerializeField] private int _key;

            public int Key { get => _key; set => _key = value; }
        }


        [SerializeField] private float _sensetivity;
        [SerializeField] private Inventory _inventory;

        private float _mouseLookX;
        //private float _mouseLookY;
        //float _xRotation;


        private IInput _input;
        private Vector3 _moveForvard;
        private Vector3 _moveRight;
        private bool _isStandartInput = true;

        public int MaxHP { get => _maxHP; }


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
            //_mouseLookY = _input.MouseLookX * _sensetivity;

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
            transform.Rotate(0, _mouseLookX, 0);

            //_xRotation += _mouseLookY; // пока что убрал движение головой, возможно понадобится в будущем.
            //_xRotation = Mathf.Clamp(_xRotation, -25f, 40f);
            //_head.transform.localRotation = Quaternion.Euler(0, 0, _xRotation);
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

        public void TakeDamage(int damage)
        {
            Debug.Log("Auch!");
        }

        public void GetBuffOrDebuff(BuffsAndDebuffs bonusType, int value, int bonusTime)
        {
            switch (bonusType)
            {
                case BuffsAndDebuffs.Speed:
                    _speed = _speed * value;
                    Debug.Log(_speed);
                    StartCoroutine(ReturnSpeedBack(bonusTime, value));
                    break;
                case BuffsAndDebuffs.Heal:
                    break;
                case BuffsAndDebuffs.Rage:
                    break;
                case BuffsAndDebuffs.Ammo:
                    break;
            }
        }

        private IEnumerator ReturnSpeedBack(int buffTime, int value)
        {
            int timeOut = 0;

            while (timeOut != buffTime)
            {
                timeOut++;
                if (timeOut == buffTime)
                {
                    _speed = _speed / value;
                    Debug.Log(_speed);
                }
                yield return new WaitForSeconds(1f);
            }
        }

        public void GetKey()
        {
            _inventory.Key++;

            Debug.Log(_inventory.Key);
        }
    }
}
