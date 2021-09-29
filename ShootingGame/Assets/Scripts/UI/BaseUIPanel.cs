using UnityEngine;
using PlayerInput.ShootingGame;
using Model.ShootingGame;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace UI.ShootingGame {

    public abstract class BaseUIPanel : MonoBehaviour, IDisposable
    {
        [SerializeField] protected Player _player;
        [SerializeField] protected Button _buttonRestart;
        [SerializeField] protected Button _buttonExit;

        protected BaseInput _baseInput;

        protected abstract void Awake();

        protected abstract void Start();

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