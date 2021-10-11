using System;
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    [Serializable]
    public struct UIInitializationData: IEndPanelData, IMenuPanelData
    {
        [SerializeField] private Button _buttonRestartOnMenuPanel;
        [SerializeField] private Button _buttonExitOnMenuPanel;
        [SerializeField] private Button _buttonResumeOnMenuPanel;
        [SerializeField] private Button _buttonRestartOnEndGamePanel;
        [SerializeField] private Button _buttonExitOnEndGamePanel;
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _endGamePanel;

        public Button ButtonRestartonMenuPanel { get => _buttonRestartOnMenuPanel; }
        public Button ButtonExitonMenuPanel { get => _buttonExitOnMenuPanel; }
        public Button ButtonResumeonMenuPanel { get => _buttonResumeOnMenuPanel; }
        public Button ButtonRestartonEndGamePanel { get => _buttonRestartOnEndGamePanel; }
        public Button ButtonExitonEndGamePanel { get => _buttonExitOnEndGamePanel; }
        public GameObject MenuPanel { get => _menuPanel; }
        public GameObject EndGamePanel { get => _endGamePanel; }
    }
}
