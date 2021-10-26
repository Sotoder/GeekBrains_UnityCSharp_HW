using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    public interface IEndPanelData
    {
        public Button ButtonRestartonEndGamePanel { get; }
        public Button ButtonExitonEndGamePanel { get; }
        public GameObject EndGamePanel { get; }
    }
}