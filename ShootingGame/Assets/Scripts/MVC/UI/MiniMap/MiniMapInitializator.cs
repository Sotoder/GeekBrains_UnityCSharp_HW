using System;
using UnityEngine;

namespace Model.ShootingGame
{
    internal class MiniMapInitializator
    {
        private Transform _player;
        private Camera _camera;

        private const float CAMERA_HIGHT = 6.0f;

        public MiniMapInitializator(IPlayer player, MiniMapInitializationData miniMapInitializationData)
        {
            _player = player.PlayerData.GameObject.transform;
            _camera = miniMapInitializationData.MiniMapCamera;

            InitializeCamera();
        }

        private void InitializeCamera()
        {
            var mainCamera = Camera.main;
            _camera.transform.parent = null;
            _camera.transform.rotation = Quaternion.Euler(90.0f, 0, 0);
            _camera.transform.position = _player.position + new Vector3(0, CAMERA_HIGHT, 0);

            var rt = Resources.Load<RenderTexture>("MiniMap/MiniMapTexture");

            _camera.targetTexture = rt;
            mainCamera.depth = --mainCamera.depth;
        }
    }
}