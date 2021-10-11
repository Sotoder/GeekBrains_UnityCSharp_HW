using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame 
{
    public class SaveDataController : MonoBehaviour
    {
        private InputController _inputController;
        private SaveDataRepository _saveDataRepository;
        public SaveDataController(InputController inputController, List<BuffObject> buffObjectCollection, PickUpObjectsController pikUpObjectController, RadarController radarController)
        {
            _inputController = inputController;
            _saveDataRepository = new SaveDataRepository(buffObjectCollection, pikUpObjectController, radarController);

            SignOnEvents();
        }

        private void SignOnEvents()
        {
            _inputController.isSaveButtonPressed += SaveButtonPress;
            _inputController.isLoadButtonPressed += LoadButtonPress;
        }

        protected void SaveButtonPress(bool isSaveButtonPressed)
        {
            if (isSaveButtonPressed)
            {
                _saveDataRepository.Save();
            }
        }

        protected void LoadButtonPress(bool isSaveButtonPressed)
        {
            if (isSaveButtonPressed)
            {
                _saveDataRepository.Load();
            }
        }
    }
}