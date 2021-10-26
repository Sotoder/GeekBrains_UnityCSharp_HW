using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public class KeyInitializator
    {
        private KeyObjectsInitializationData _keyObjectsInitializationData;
        private Dictionary<GameObject, float> _keyObjectsList = new Dictionary<GameObject, float>();
        private RadarController _radarController;

        private const int BUFF_LAYER = 8;

        public KeyInitializator(KeyObjectsInitializationData keyObjectsInitializationData, PickUpObjectsController pikUpObjectController, RadarController radarController)
        {
            _radarController = radarController;
            _keyObjectsInitializationData = keyObjectsInitializationData;

            _keyObjectsInitializationData.KeyObjectsCollection.CheckOnRepeats();

            InstantiateKeyObjects();
            pikUpObjectController.AddObjectsList(_keyObjectsList);
        }

        private void InstantiateKeyObjects()
        {
            foreach (var element in _keyObjectsInitializationData.KeyObjectsCollection)
            {
                var buffObject = Object.Instantiate(element.KeyPrefab, element.Object.transform.position, new Quaternion(x: -0.7f, element.Object.transform.rotation.y, element.Object.transform.rotation.z, w: 0.7f));
                buffObject.transform.SetParent(element.Object.transform);

                element.Object.layer = BUFF_LAYER;

                _keyObjectsList.Add(buffObject, buffObject.transform.position.y);
                _radarController.RegisterRadarObject(element.Object, _keyObjectsInitializationData.IcoForRadarObject);
            }
        }
    }
}