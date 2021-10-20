using PlayerInput.ShootingGame;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Debug;

namespace Model.ShootingGame
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        public UnityAction<GameObject> buffObjectCollected;
        public UnityAction<GameObject> keyObjectCollected;
        public UnityAction<int> takeDamage;
        public UnityAction<int> swapHP;
        public UnityAction getKey;

        [SerializeField] private int _speedForInitialization;        
        [SerializeField] private float _sensetivity = 20f;
        [SerializeField] private Transform rightGunBone;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _maxStamina;

        public bool isStay = true;
        public Rigidbody unitRigidBody;
        public Weapon[] _arsenal;

        private Keystorege _keystorege;
        private Parameters _parameters;
        private BaseInput _input;
        private Animator _animator;

        public BaseInput CurrentInput { get => _input; }

        public Parameters Parameters { get => _parameters; }
        public int MaxHP { get => _maxHP; }
        public int MaxStamina { get => _maxStamina; }
        public Keystorege Keystorege { get => _keystorege; }
        public float Sensetivity { get => _sensetivity; }

        //private const int DEFAULT_HP = 100;
        //private const int DEFAULT_STAMINA = 100;


        private void Awake()
        {
            if (_maxHP < 0) throw new PlayerHPExeption("Некорректно указано здоровье персонажа", _maxHP);


            _input = GetComponent<BaseInput>();
            _animator = GetComponent<Animator>();
            unitRigidBody = GetComponent<Rigidbody>();

            _keystorege = new Keystorege(this);
            _parameters = new Parameters(this, _maxHP, _maxStamina, _speedForInitialization);

            Cursor.lockState = CursorLockMode.Locked;

            if (_arsenal.Length > 0)
                SetWeapon(_arsenal[0].name);

        }

        private void Update()
        {

            if (_input.IsFire)
            {
                Log("Pew");
            }
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

        public void GetKey()
        {
            getKey?.Invoke();
            Log(_keystorege.Key);
        }

        public void SetWeapon(string name)
        {
            foreach (Weapon weapon in _arsenal)
            {
                if (weapon.name == name)
                {
                    if (rightGunBone.childCount > 0)
                        Destroy(rightGunBone.GetChild(0).gameObject);
                    if (weapon.rightGun != null)
                    {
                        GameObject newRightGun = (GameObject)Instantiate(weapon.rightGun);
                        newRightGun.transform.parent = rightGunBone;
                        newRightGun.transform.localPosition = Vector3.zero;
                        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    }
                    _animator.runtimeAnimatorController = weapon.controller;
                    return;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                buffObjectCollected.Invoke(other.gameObject);
            }
            if ( other.gameObject.layer == 8)
            {
                keyObjectCollected.Invoke(other.gameObject);
                GetKey();
            }
        }
    }
}
