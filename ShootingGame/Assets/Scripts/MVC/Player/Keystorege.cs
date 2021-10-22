using UnityEngine;
using System;
using UnityEngine.Events;

namespace Model.ShootingGame
{

    [System.Serializable]
    public class Keystorege: IDisposable
    {
        public UnityAction allKeysCollected;

        [SerializeField] private int _key;
        private Player _player;

        public Keystorege(Player player)
        {
            _player = player;
            _key = 0;
            _player.GetKey += AddKey;
        }

        public int Key => _key;

        public void AddKey()
        {
            if (_key < 4)
            {
                _key++;
            } else
            {
                allKeysCollected?.Invoke();
            }

        }

        public void Dispose()
        {
            _player.GetKey -= AddKey;
        }
    }
}
