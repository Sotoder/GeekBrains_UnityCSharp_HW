using UnityEngine;
using PlayerInput.ShootingGame;
using Model.ShootingGame;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace UI.ShootingGame {

    public class Game_UI : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] GameObject _pauseMenuPanel;
        [SerializeField] GameObject _endMenuPanel;
        [SerializeField] Button _pauseMenuButtonResume;
        [SerializeField] Button _pauseMenuButtonRestart;
        [SerializeField] Button _pauseMenuButtonExit;
        [SerializeField] Button _endMenuButtonRestart;
        [SerializeField] Button _endMenuButtonExit;

        private BaseInput _baseInput;

        private void Awake()
        {
            _pauseMenuButtonResume.onClick.AddListener(ResumeGame);
            _pauseMenuButtonRestart.onClick.AddListener(RestartGame);
            _pauseMenuButtonExit.onClick.AddListener(ExitGame);
            _endMenuButtonRestart.onClick.AddListener(RestartGame);
            _endMenuButtonExit.onClick.AddListener(ExitGame);
        }

        private void Start()
        {
            _baseInput = _player.CurrentInput;
            _baseInput.pressGameMenuKey += ShowPauseMenu;
            _player.Keystorege.allKeysCollected += ShowEndGameMenu;
        }

        private void ShowEndGameMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            _endMenuPanel.SetActive(true);
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
            _endMenuButtonRestart.onClick.RemoveListener(RestartGame);
            _endMenuButtonExit.onClick.RemoveListener(ExitGame);
        }
    }
}