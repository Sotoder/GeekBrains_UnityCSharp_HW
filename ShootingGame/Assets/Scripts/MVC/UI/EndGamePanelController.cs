using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    public class EndGamePanelController : UIPanelController
    {
        private GameObject _endGamePanel;

        public EndGamePanelController(IEndPanelData EndGamePanelData, IPlayer player, InputController inputController)
        {
            _buttonRestart = EndGamePanelData.ButtonRestartonEndGamePanel;
            _buttonExit = EndGamePanelData.ButtonExitonEndGamePanel;
            _endGamePanel = EndGamePanelData.EndGamePanel;

            _player = player;
            _inputController = inputController;

            SignOnEvents();
        }

        protected override void SignOnEvents()
        {
            _player.PlayerData.Keystorege.allKeysCollected += ShowEndGameMenu;
            _buttonRestart.onClick.AddListener(RestartGame);
            _buttonExit.onClick.AddListener(ExitGame);
        }

        protected void ShowEndGameMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            _endGamePanel.SetActive(true);
        }

        public override void Dispose()
        {
            _buttonRestart.onClick.RemoveListener(RestartGame);
            _buttonExit.onClick.RemoveListener(ExitGame);
            _player.PlayerData.Keystorege.allKeysCollected -= ShowEndGameMenu;
        }
    }
}
