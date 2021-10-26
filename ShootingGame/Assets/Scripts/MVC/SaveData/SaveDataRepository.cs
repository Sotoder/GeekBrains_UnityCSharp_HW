using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class SaveDataRepository : ISaveDataRepository
    {
        private List<BuffObject> _buffObjectsCollection;
        private PickUpObjectsController _pickUpObjectsController;
        private RadarController _radarController;

        private readonly IData<SavedData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveDataRepository(List<BuffObject> buffObjectCollection, PickUpObjectsController pickUpObjectsController, RadarController radarController)
        {
            _radarController = radarController;
            _pickUpObjectsController = pickUpObjectsController;
            _buffObjectsCollection = buffObjectCollection;
            _data = new JsonData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var saveBuffObjects = new SavedData
            {
                buffSaveData = new List<BuffDataForSave>()
            };

            foreach (var element in _buffObjectsCollection)
            {
                saveBuffObjects.buffSaveData.Add(new BuffDataForSave
                {
                    isActive = element.Object.activeSelf,
                    InstanseID = element.Object.GetInstanceID()
                });
            }

            _data.Save(saveBuffObjects, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            //Признаюсь сразу, тут я читанул. Изначально баффы при поднятии у меня уничтожались, теперь просто отключаются
            //По хорошему тут нужен нормальный загрузчик, уничтожающий все объекты на карте и загружающий их согласно сохраненному списку
            //По сути выполняющий новую инициализацию BuffController и PickUpObjectsController, но уже сверяясь со списком сохраненных данных

            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }

            var savedBuffs = _data.Load(file).buffSaveData;
            var objectsToActivate = new Dictionary<GameObject, float>();

            for (int i = 0; i < savedBuffs.Count; i++)
            {
                foreach(var element in _buffObjectsCollection)
                {
                    if (element.Object.GetInstanceID() == savedBuffs[i].InstanseID && element.Object.activeSelf != savedBuffs[i].isActive)
                    {
                        if (savedBuffs[i].isActive == true)
                        {
                            element.Object.SetActive(savedBuffs[i].isActive);
                            objectsToActivate.Add(element.Object.transform.GetChild(0).gameObject, element.Object.transform.position.y);
                            _radarController.RegisterRadarObject(element.Object, element.IcoForRadarObject);
                        } else
                        {
                            element.Object.SetActive(savedBuffs[i].isActive);
                            _pickUpObjectsController.DestroyBuffObject(element.Object);
                        } 
                    }
                }
            }
            if (objectsToActivate.Count > 0)
            {
                _pickUpObjectsController.AddObjectsList(objectsToActivate);
            }

            Debug.Log("Load");

        }
    }
}