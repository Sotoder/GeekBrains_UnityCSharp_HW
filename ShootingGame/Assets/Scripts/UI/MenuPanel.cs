using UnityEngine;
using UnityEngine.UI;

namespace UI.ShootingGame
{
    public class MenuPanel : BaseUIPanel
    {
        [SerializeField] Button _buttonResume;

        protected override void Start()
        {
            _baseInput = _player.CurrentInput;
            _baseInput.pressGameMenuKey += ShowPauseMenu;
            gameObject.SetActive(false);
        }

        protected override void Awake()
        {
            _buttonRestart.onClick.AddListener(RestartGame);
            _buttonExit.onClick.AddListener(ExitGame);
            _buttonResume.onClick.AddListener(ResumeGame);
        }

        protected void ShowPauseMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        protected void ResumeGame()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            _buttonRestart.onClick.RemoveListener(RestartGame);
            _buttonExit.onClick.RemoveListener(ExitGame);
            _buttonResume.onClick.RemoveListener(ResumeGame);
            _baseInput.pressGameMenuKey -= ShowPauseMenu;
        }
    }
}