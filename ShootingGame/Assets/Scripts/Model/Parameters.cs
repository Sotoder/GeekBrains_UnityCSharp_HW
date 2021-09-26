using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [System.Serializable]
    public class Parameters : IDisposable
    {
        [SerializeField] private int _currentHP;
        [SerializeField] private int _currentStamina;

        private Player _player;

        public int CurrentHP { get => _currentHP; }

        public Parameters(Player player, int maxHP, int maxStamina)
        {
            _player = player;
            _player.takeDamage += GetDamage;
            _player.swapHP += SwapHP;

            _currentHP = maxHP;
            _currentStamina = maxStamina;
        }

        public void GetDamage(int damage)
        {
            _currentHP -= damage;
        }

        public void SwapHP(int hp)
        {
            _currentHP = hp;
        }
        public void Dispose()
        {
            _player.takeDamage -= GetDamage;
            _player.swapHP -= SwapHP;
        }
    }
}
