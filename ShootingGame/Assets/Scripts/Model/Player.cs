using PlayerInput.ShootingGame;

namespace Model.ShootingGame
{
    using System.Collections;
    using UnityEngine;
    using static UnityEngine.Debug;

    public class Player : Unit, IDamageable
    {
        [System.Serializable]
        private struct Inventory
        {
            [SerializeField] private int _key;

            public int Key { get => _key; set => _key = value; }
        }

        [System.Serializable]
        public struct Arsenal
        {
            public string name;
            public GameObject rightGun;
            public RuntimeAnimatorController controller;
        }

        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _sensetivity = 20f;
        [SerializeField] private Transform rightGunBone;

        private IInput _input;
        private Vector3 _moveForvard;
        private Vector3 _moveRight;
        private bool _isStandartInput = true;
        private float _mouseLookX;
        private bool _isStay = true;
        public Arsenal[] _arsenal;
        private Animator _animator;

        public int MaxHP { get => _maxHP; }
        public int CurentHP { get => _curentHP; }
        public bool IsStay { get => _isStay; }
        public IInput CurrentInput { get => _input; }


        private void Awake()
        {
            _input = GetComponent<StandartInput>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;

            if (_arsenal.Length > 0)
                SetArsenal(_arsenal[0].name);
            _curentHP = _maxHP;
    }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeInputType();
            }

            if (_input.IsFire)
            {
                Log("Pew");
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
            Log("Auch!");
        }

        public void SwapHP(int hpForSwap)
        {
            _curentHP = _curentHP + hpForSwap;
            hpForSwap = _curentHP - hpForSwap;
            _curentHP = _curentHP - hpForSwap;
            Log(_curentHP);
        }

        public void GetBuffOrDebuff(BuffsAndDebuffs bonusType, int value, int bonusTime)
        {
            switch (bonusType)
            {
                case BuffsAndDebuffs.Speed:
                    _speed = _speed * value;
                    Log(_speed);
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
                    Log(_speed);
                }
                yield return new WaitForSeconds(1f);
            }
        }

        public void GetKey()
        {
            _inventory.Key++;

            Log(_inventory.Key);
        }

        public void SetArsenal(string name)
        {
            foreach (Arsenal hand in _arsenal)
            {
                if (hand.name == name)
                {
                    if (rightGunBone.childCount > 0)
                        Destroy(rightGunBone.GetChild(0).gameObject);
                    if (hand.rightGun != null)
                    {
                        GameObject newRightGun = (GameObject)Instantiate(hand.rightGun);
                        newRightGun.transform.parent = rightGunBone;
                        newRightGun.transform.localPosition = Vector3.zero;
                        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    }
                    _animator.runtimeAnimatorController = hand.controller;
                    return;
                }
            }
        }
    }
}
