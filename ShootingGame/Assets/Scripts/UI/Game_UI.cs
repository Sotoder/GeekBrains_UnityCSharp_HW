using UnityEngine;
using PlayerInput.ShootingGame;
using Model.ShootingGame;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI.ShootingGame {

    public class Game_UI : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] GameObject _pauseMenuPanel;
        [SerializeField] Button _pauseMenuButtonResume;
        [SerializeField] Button _pauseMenuButtonRestart;
        [SerializeField] Button _pauseMenuButtonExit;

        private BaseInput _baseInput;

        private void Awake()
        {
            _pauseMenuButtonResume.onClick.AddListener(ResumeGame);
            _pauseMenuButtonRestart.onClick.AddListener(RestartGame);
            _pauseMenuButtonExit.onClick.AddListener(ExitGame);
        }

        private void Start()
        {
            _baseInput = _player.CurrentInput;
            _baseInput.pressGameMenuKey += ShowPauseMenu;
        }

        private void ShowPauseMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
        }

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            _pauseMenuPanel.SetActive(false);
        }

        private void RestartGame()
        {
            _player.Keystorege.Dispose();
            _player.Parameters.Dispose();
            Debug.Log("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

        private void ExitGame()
        {
            _player.Keystorege.Dispose();
            _player.Parameters.Dispose();
            Application.Quit();
        }

        private void OnDisable()
        {
            _pauseMenuButtonResume.onClick.RemoveListener(ResumeGame);
            _pauseMenuButtonRestart.onClick.RemoveListener(RestartGame);
            _pauseMenuButtonExit.onClick.RemoveListener(ExitGame);
        }
    }
}