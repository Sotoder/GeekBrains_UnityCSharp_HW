using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    public class MenuPanelController : UIPanelController
    {
        private Button _buttonResume;
        private GameObject _menuPanel;
        private bool _isMenuPanelActive;

        public MenuPanelController(IMenuPanelData MenuData, Player player, InputController inputController)
        {
            _buttonRestart = MenuData.ButtonRestartonMenuPanel;
            _buttonExit = MenuData.ButtonExitonMenuPanel;
            _buttonResume = MenuData.ButtonResumeonMenuPanel;
            _menuPanel = MenuData.MenuPanel;

            _player = player;
            _inputController = inputController;

            SignOnEvents();
        }

        protected override void SignOnEvents()
        {
            _buttonRestart.onClick.AddListener(RestartGame);
            _buttonExit.onClick.AddListener(ExitGame);
            _buttonResume.onClick.AddListener(ResumeGame);
            _inputController.isMenuButtonPressed += PauseMenuButtonPress;
        }

        protected void PauseMenuButtonPress(bool isMenuButtonPressed)
        {
            if (isMenuButtonPressed && !_isMenuPanelActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                _menuPanel.SetActive(true);
                _isMenuPanelActive = true;
            } else if (isMenuButtonPressed && _isMenuPanelActive)
            {
                ResumeGame();
            }
        }

        protected void ResumeGame()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            _menuPanel.SetActive(false);
            _isMenuPanelActive = false;
        }

        public override void Dispose()
        {
            _buttonRestart.onClick.RemoveListener(RestartGame);
            _buttonExit.onClick.RemoveListener(ExitGame);
            _buttonResume.onClick.RemoveListener(ResumeGame);
            _inputController.isMenuButtonPressed -= PauseMenuButtonPress;
        }
    }
}