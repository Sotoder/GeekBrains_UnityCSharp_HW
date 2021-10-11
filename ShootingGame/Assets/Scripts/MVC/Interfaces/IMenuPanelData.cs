using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    public interface IMenuPanelData
    {
        public Button ButtonRestartonMenuPanel { get; }
        public Button ButtonExitonMenuPanel { get; }
        public Button ButtonResumeonMenuPanel { get; }
        public GameObject MenuPanel { get; }
    }
}