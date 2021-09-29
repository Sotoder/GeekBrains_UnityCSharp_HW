using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{

    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void Start()
        {
            var buffObjects = Object.FindObjectsOfType<BuffBehaviour>();
            new ControllersInitializator(_player, buffObjects);
        }
    }
}
