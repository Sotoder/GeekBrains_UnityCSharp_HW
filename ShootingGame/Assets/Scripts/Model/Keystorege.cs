using UnityEngine;
using System;

namespace Model.ShootingGame
{

    [System.Serializable]
    public class Keystorege: IDisposable
    {
        [SerializeField] private int _key;
        private Player _player;

        public Keystorege(Player player)
        {
            _player = player;
            _key = 0;
            _player.getKey += AddKey;
        }

        public int Key => _key;

        public void AddKey()
        {
            _key++;
        }

        public void Dispose()
        {
            _player.getKey -= AddKey;
        }
    }
}
