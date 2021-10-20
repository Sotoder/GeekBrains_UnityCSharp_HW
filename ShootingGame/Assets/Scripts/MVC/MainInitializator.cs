using UnityEngine;

namespace Model.ShootingGame
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var inputController = new InputController(data.InputData);
            mainController.Add(inputController);

            var player = new Player(data.PlayerInitializationData, inputController);
            mainController.Add(player);

            var traps = Resources.FindObjectsOfTypeAll<Mine>(); // костыль для проверки работы ловушек, до их перевода на МВЦ
            foreach (var element in traps)
            {
                element.player = player;
            }

            new MiniMapInitializator(player, data.MiniMapInitializationData);
            var miniMapController = new MiniMapController(data.MiniMapInitializationData, player);
            mainController.Add(miniMapController);

            var radarController = new RadarController(player, data.RadarInitializationData.Radar);
            mainController.Add(radarController);

            var pikUpObjectController = new PickUpObjectsController(player, radarController);
            mainController.Add(pikUpObjectController);

            var moveController = new MoveController(player, inputController);
            mainController.Add(moveController);

            var cameraController = new CameraController(player, data.CameraInitializationData, inputController);
            mainController.Add(cameraController);

            new SaveDataController(inputController, data.BuffObjectsInitializationData.BuffObjectCollection, pikUpObjectController, radarController);
            new MenuPanelController(data.UIInitializationData, player, inputController);
            new EndGamePanelController(data.UIInitializationData, player, inputController);
            new BuffInitializator(player, data.BuffObjectsInitializationData, pikUpObjectController, radarController);
            new KeyInitializator(data.KeyObjectsInitializationData, pikUpObjectController, radarController);
        }
    }
}