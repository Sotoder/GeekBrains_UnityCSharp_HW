using UnityEngine;
using UnityEngine.Events;

namespace Model.ShootingGame
{
    public interface IPlayer
    {
        public PlayerData PlayerData { get; }
        public UnityAction<GameObject> BuffObjectCollected { get; set; }
        public UnityAction<GameObject> KeyObjectCollected { get; set; }
        public UnityAction<int> TakeDamage { get; set; }
        public UnityAction<int> SwapHP { get; set; }
        public UnityAction GetKey { get; set; }
    }
}