using UnityEngine;

namespace Model.ShootingGame
{
    public class GameStarter : MonoBehaviour
    {

        [SerializeField] private GameInitializationData _dataForInitialization;

        private GameController _mainController;

        public GameInitializationData DataForInitialization { get => _dataForInitialization; }

        private void Start()
        {

            _mainController = new GameController();
            new MainInitializator(_dataForInitialization, _mainController);
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
            var fixedTime = Time.fixedTime;
            var fixedDeltaTime = Time.fixedDeltaTime;
            _mainController.FixedExecute(fixedTime, fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _mainController.TriggerEnter(other);
        }
    }
}
