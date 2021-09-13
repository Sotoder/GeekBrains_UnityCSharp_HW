using PlayerInput.ShootingGame;

namespace Model.ShootingGame
{
    using System.Collections;
    using UnityEngine;

    public class Player : Unit, IDamageable
    {
        [System.Serializable]
        private struct Inventory
        {
            [SerializeField] private int _key;

            public int Key { get => _key; set => _key = value; }
        }

        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _sensetivity = 20f;
        [SerializeField] private Transform _camRotationTarget;

        private IInput _input;
        private Vector3 _moveForvard;
        private Vector3 _moveRight;
        private bool _isStandartInput = true;
        private float _mouseLookX;
        private bool _isStay = true;

        public int MaxHP { get => _maxHP; }
        public bool IsStay { get => _isStay; }
        public IInput CurrentInput { get => _input; }


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

            if (_input.Direction.x != 0 || _input.Direction.z != 0)
            {
                _moveForvard = _input.Direction.x * _speed * transform.forward;
                _moveRight = _input.Direction.z * _speed * transform.right;
                MovementLogic(_moveForvard + _moveRight);
                _isStay = false;
            }
            else _isStay = true;         

            if (!_isStay)
            {
                _mouseLookX = _input.MouseLookX * _sensetivity;
                PlayerLook();
            }

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
