using PlayerInput.ShootingGame;
using UI.ShootingGame;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Debug;

namespace Model.ShootingGame
{
    public sealed class Player : Unit, IDamageable
    {
        public UnityAction<int> takeDamage;
        public UnityAction<int> swapHP;
        public UnityAction getKey;

        [SerializeField] private Keystorege _keystorege;
        [SerializeField] private float _sensetivity = 20f;
        [SerializeField] private Transform rightGunBone;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _maxStamina;

        private BaseInput _input;
        private Vector3 _moveForvard;
        private Vector3 _moveRight;
        private float _mouseLookX;
        private bool _isStay = true;
        public Arsenal[] _arsenal;
        private Animator _animator;

        public bool IsStay { get => _isStay; }
        public BaseInput CurrentInput { get => _input; }

        public Parameters Parameters { get => _parameters; }
        public int MaxHP { get => _maxHP; }
        public int MaxStamina { get => _maxStamina; }
        public Keystorege Keystorege { get => _keystorege; }

        //private const int DEFAULT_HP = 100;
        //private const int DEFAULT_STAMINA = 100;


        private void Awake()
        {
            //try // Ну вот просто за ради играний с обработкой эксепшена написано, а так знаю что атата, лид не разрешал!
            //{
            //    if (_maxHP < 0) throw new ParameterException("Некорректно указано здоровье персонажа", _maxHP);
            //    Debug.Log("ok");
            //}
            //catch (ParameterException e)
            //{
            //    Debug.LogWarning($"{e.Message} {e.Parameter}");
            //    Debug.LogWarning($"Здоровье будет установлено по умолчанию {DefaultHP}");
            //}
            //finally
            //{
            //    _maxHP = DefaultHP;
            //}

            if (_maxHP < 0) throw new PlayerHPExeption("Некорректно указано здоровье персонажа", _maxHP);


            _input = GetComponent<BaseInput>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();

            _keystorege = new Keystorege(this);
            _parameters = new Parameters(this, _maxHP, _maxStamina);

            Cursor.lockState = CursorLockMode.Locked;

            if (_arsenal.Length > 0)
                SetArsenal(_arsenal[0].name);

        }

        private void Update()
        {

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

            if (!_input.IsCameraRotate)
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

        public void TakeDamage(int damage)
        {
            Log("Auch!");
            takeDamage?.Invoke(damage);
        }

        public void SwapHP(int hpForSwap)
        {
            swapHP?.Invoke(hpForSwap);
        }

        public void GetBuffOrDebuff(BuffTypes bonusType, int value, int bonusTime)
        {
            switch (bonusType)
            {
                case BuffTypes.Speed:
                    _speed = _speed * value;
                    Log(_speed);
                    StartCoroutine(ReturnSpeedBack(bonusTime, value));
                    break;
                case BuffTypes.Regeneration:
                    break;
                case BuffTypes.Rage:
                    break;
                case BuffTypes.AttackSpeed:
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
            getKey?.Invoke();
            Log(_keystorege.Key);
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
