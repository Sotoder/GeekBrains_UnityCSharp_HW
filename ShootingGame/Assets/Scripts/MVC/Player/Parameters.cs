using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [System.Serializable]
    public class Parameters : IDisposable
    {
        public int currentHP;
        public int currentStamina;
        public int speed;
        public int attackPower;

        private Player _player;


        public Parameters(Player player, int maxHP, int maxStamina, int speed)
        {
            _player = player;
            _player.takeDamage += GetDamage;
            _player.swapHP += SwapHP;

            currentHP = maxHP;
            currentStamina = maxStamina;
            this.speed = speed;
        }

        public void GetDamage(int damage)
        {
            currentHP -= damage;
        }

        public void SwapHP(int hp)
        {
            currentHP = hp;
        }

        public void Dispose()
        {
            _player.takeDamage -= GetDamage;
            _player.swapHP -= SwapHP;
        }
    }
}
