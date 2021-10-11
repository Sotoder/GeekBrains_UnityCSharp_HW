namespace Model.ShootingGame
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {

            new MiniMapInitializator(data.Player, data.MiniMapInitializationData);
            var miniMapController = new MiniMapController(data.MiniMapInitializationData, data.Player);
            mainController.Add(miniMapController);

            var radarController = new RadarController(data.Player, data.RadarInitializationData.Radar);
            mainController.Add(radarController);

            var inputController = new InputController(data.InputData);
            mainController.Add(inputController);

            var pikUpObjectController = new PickUpObjectsController(data.Player, radarController);
            mainController.Add(pikUpObjectController);

            new SaveDataController(inputController, data.BuffObjectsInitializationData.BuffObjectCollection, pikUpObjectController, radarController);
            new MenuPanelController(data.UIInitializationData, data.Player, inputController);
            new EndGamePanelController(data.UIInitializationData, data.Player, inputController);
            new BuffInitializator(data.Player, data.BuffObjectsInitializationData, pikUpObjectController, radarController);
            new KeyInitializator(data.KeyObjectsInitializationData, pikUpObjectController, radarController);
        }
    }
}