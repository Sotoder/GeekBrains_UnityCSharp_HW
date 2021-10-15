using UnityEngine;

namespace Model.ShootingGame
{
    public class GameStarter : MonoBehaviour
    {

        [SerializeField] private GameInitializationData _dataForInitialization;

        private GameController _mainController;
        [Path]
        public string path;

        public GameInitializationData DataForInitialization { get => _dataForInitialization; }

        private void Start()
        {
            new HomeWorkClass(); //��������� �� 7 �����

            _mainController = new GameController();
            new MainInitializator(_dataForInitialization, _mainController);
            Debug.Log(path);
        }

        private void Update()
        {
            var time = Time.deltaTime;
            _mainController.Execute(time);
        }

        private void LateUpdate()
        {
            var time = Time.deltaTime;
            _mainController.LateExecute(time);
        }

        private void FixedUpdate()
        {
            var time = Time.fixedTime;
            _mainController.FixedExecute(time);
        }
    }
}
