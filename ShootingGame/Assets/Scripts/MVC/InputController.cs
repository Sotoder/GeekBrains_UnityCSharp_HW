using UnityEngine;
using UnityEngine.Events;

namespace Model.ShootingGame
{
    public class InputController : IController, IExecute
    {
        public UnityAction<bool> isMenuButtonPressed = delegate (bool b) { };
        public UnityAction<bool> isFireButtonPressed = delegate (bool b) { };
        public UnityAction<bool> isCameraRotateButtonPressed = delegate (bool b) { };
        public UnityAction<bool> isSaveButtonPressed = delegate (bool b) { };
        public UnityAction<bool> isLoadButtonPressed = delegate (bool b) { };
        public UnityAction<float> horizontalAxisOnChange = delegate (float f) { };
        public UnityAction<float> verticalAxisOnChange = delegate (float f) { };
        public UnityAction<float> mouseXAxisOnChange = delegate (float f) { };
        
        private InputData _inputData;

        public InputController(InputData inputData)
        {
            _inputData = inputData;
        }

        public void Execute(float deltaTime)
        {
            horizontalAxisOnChange.Invoke(Input.GetAxis(InputData.HORIZONTAL));
            verticalAxisOnChange.Invoke(Input.GetAxis(InputData.VERTICAL));
            mouseXAxisOnChange.Invoke(Input.GetAxis(InputData.MOUSE_X));
            isMenuButtonPressed.Invoke(Input.GetKeyDown(_inputData.GameMenu));
            isFireButtonPressed.Invoke(Input.GetKeyDown(_inputData.Fire));
            isCameraRotateButtonPressed.Invoke(Input.GetKeyDown(_inputData.CameraRotate));
            isSaveButtonPressed.Invoke(Input.GetKeyDown(_inputData.SavePlayer));
            isLoadButtonPressed.Invoke(Input.GetKeyDown(_inputData.LoadPlayer));
        }
    }
}

