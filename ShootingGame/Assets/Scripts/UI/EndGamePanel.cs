using UnityEngine;

namespace UI.ShootingGame
{
    public class EndGamePanel : BaseUIPanel
    {

        protected override void Start()
        {
            _baseInput = _player.CurrentInput;
            _player.Keystorege.allKeysCollected += ShowEndGameMenu;
            gameObject.SetActive(false);
        }

        protected override void Awake()
        {
            _buttonRestart.onClick.AddListener(RestartGame);
            _buttonExit.onClick.AddListener(ExitGame);
        }

        protected void ShowEndGameMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            _buttonRestart.onClick.RemoveListener(RestartGame);
            _buttonExit.onClick.RemoveListener(ExitGame);
            _player.Keystorege.allKeysCollected -= ShowEndGameMenu;
        }
    }
}
