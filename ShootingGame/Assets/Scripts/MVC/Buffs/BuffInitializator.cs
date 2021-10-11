using Model.ShootingGame;
using System.Collections.Generic;
using UnityEngine;

public class BuffInitializator
{
    private Player _player;
    private BuffInitializationData _buffInitializationData;
    private RadarController _radarController;
    private Dictionary<GameObject, float> _buffObjectList = new Dictionary<GameObject, float>();

    private const int BUFF_LAYER = 7;

    public BuffInitializator(Player player, BuffInitializationData buffInitializationData, PickUpObjectsController pikUpObjectController, RadarController radarController)
    {
        _player = player;
        _buffInitializationData = buffInitializationData;
        _radarController = radarController;

        _buffInitializationData.BuffObjectCollection.CheckOnRepeats();

        InstantiateBuffObjects();
        pikUpObjectController.AddObjectsList(_buffObjectList);

        new BuffController(_player, _buffInitializationData.BuffObjectCollection);
    }

    private void InstantiateBuffObjects()
    {
        var pathsCollection = new BuffPrefabPath();

        foreach (var element in _buffInitializationData.BuffObjectCollection)
        {
            var prefabPath = pathsCollection.prefabsPaths[element.BuffData.BuffStruct.BuffType];
            var buffPrefab = Resources.Load(prefabPath) as GameObject;

            var buffObject = Object.Instantiate(buffPrefab, element.Object.transform.position, new Quaternion(x: -0.7f, element.Object.transform.rotation.y, element.Object.transform.rotation.z, w: 0.7f));
            buffObject.transform.SetParent(element.Object.transform);

            element.Object.layer = BUFF_LAYER;

            _buffObjectList.Add(buffObject, buffObject.transform.position.y);
            _radarController.RegisterRadarObject(element.Object, element.IcoForRadarObject);
        }
    }
}
