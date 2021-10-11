using UnityEngine;

namespace Model.ShootingGame
{
    internal class MiniMapController: IController, ILateExecute
    {
        private Camera _miniMapCamera;
        private Player _player;

        public MiniMapController(MiniMapInitializationData miniMapInitializationData, Player player)
        {
            _miniMapCamera = miniMapInitializationData.MiniMapCamera;
            _player = player;
        }

        public void LateExecute(float deltaTime)
        {
            var newPosition = _player.transform.position;
            newPosition.y = _miniMapCamera.transform.position.y;
            _miniMapCamera.transform.position = newPosition;
            _miniMapCamera.transform.rotation = Quaternion.Euler(90, _player.transform.eulerAngles.y, 0);
        }
    }
}