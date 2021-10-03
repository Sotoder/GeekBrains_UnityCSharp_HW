using UnityEngine;

namespace Model.ShootingGame
{

    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void Start()
        {
            new HomeWorkClass(); //Запускаем ДЗ 7 урока

            var iBuffObjects = Object.FindObjectsOfType<BuffBehaviour>() as IBuff[];
            new ControllersInitializator(_player, iBuffObjects);
        }
    }
}
