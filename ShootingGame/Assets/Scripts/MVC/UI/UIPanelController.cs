using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

namespace Model.ShootingGame {

    public abstract class UIPanelController : IDisposable
    {
        protected InputController _inputController;
        protected Player _player;
        protected Button _buttonRestart;
        protected Button _buttonExit;

        protected abstract void SignOnEvents();

        protected void RestartGame()
        {
            _player.Keystorege.Dispose();
            _player.Parameters.Dispose();
            Dispose();
            Debug.Log("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

        protected void ExitGame()
        {
            _player.Keystorege.Dispose();
            _player.Parameters.Dispose();
            Dispose();
            Application.Quit();
        }

        public abstract void Dispose();
    }
}