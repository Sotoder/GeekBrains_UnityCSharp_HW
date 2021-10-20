using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Debug;

namespace Model.ShootingGame
{
    public class Player: IController, ITriggerableOnEnter, IDamageable
    {
        public UnityAction<GameObject> buffObjectCollected;
        public UnityAction<GameObject> keyObjectCollected;
        public UnityAction<int> takeDamage;
        public UnityAction<int> swapHP;
        public UnityAction getKey;

        private PlayerData _playerData;
        public PlayerData PlayerData { get => _playerData; }

        public Player (PlayerInitializationData playerInitializationData, InputController inputController)
        {
            _playerData = new PlayerData(this, inputController, playerInitializationData);

            if (playerInitializationData.MaxHP < 0) throw new PlayerHPExeption("Некорректно указано здоровье персонажа", playerInitializationData.MaxHP);

            Cursor.lockState = CursorLockMode.Locked;

            if (_playerData.Arsenal.Length > 0)
                SetWeapon(_playerData.Arsenal[0].name);
        }

        public void SetWeapon(string name)
        {
            foreach (Weapon weapon in _playerData.Arsenal)
            {
                if (weapon.name == name)
                {
                    if (_playerData.RightGunBone.childCount > 0)
                        Object.Destroy(_playerData.RightGunBone.GetChild(0).gameObject);
                    if (weapon.rightGun != null)
                    {
                        GameObject newRightGun = (GameObject)Object.Instantiate(weapon.rightGun);
                        newRightGun.transform.parent = _playerData.RightGunBone;
                        newRightGun.transform.localPosition = Vector3.zero;
                        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    }
                    _playerData.Animator.runtimeAnimatorController = weapon.controller;
                    return;
                }
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
            Log(_playerData.Keystorege.Key);
        }

        public void TriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == 7)
            {
                buffObjectCollected.Invoke(collider.gameObject);
            }
            if (collider.gameObject.layer == 8)
            {
                keyObjectCollected.Invoke(collider.gameObject);
                GetKey();
            }
        }
    }
}
