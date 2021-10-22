using System;

namespace Model.ShootingGame
{
    public class MoveController : IController, IFixedExecute, IDisposable
    {
        private MoveControllerData _controllerData;

        public MoveController(Player player, InputController inputController)
        {
            _controllerData = new MoveControllerData(player, inputController);

            SignOnEvents();
        }

        private void SignOnEvents()
        {
            _controllerData.InputController.horizontalAxisOnChange += HorizontalAxisOnChange;
            _controllerData.InputController.verticalAxisOnChange += VerticalAxisOnChange;
            _controllerData.InputController.mouseXAxisOnChange += MouseAxisXOnChange;
            _controllerData.InputController.isCameraRotateButtonDown += CameraRotateButtonDown;
            _controllerData.InputController.isCameraRotateButtonUp += CameraRotateButtonUp;
        }

        private void VerticalAxisOnChange(float value)
        {
            _controllerData.vertical = value;
        }

        private void HorizontalAxisOnChange(float value)
        {
            _controllerData.horizontal = value;
        }

        private void MouseAxisXOnChange (float value)
        {
            _controllerData.mouseAxisX = value;
        }

        private void CameraRotateButtonDown(bool isCameraRotateButtonDown)
        {
            if(isCameraRotateButtonDown)
            {
                _controllerData.isCameraRotate = true;
            }
        }

        private void CameraRotateButtonUp(bool isCameraRotateButtonUp)
        {
            if (isCameraRotateButtonUp)
            {
                _controllerData.isCameraRotate = false;
            }
        }

        public void FixedExecute(float fixedTime, float fixedDeltaTime)
        {
            if (_controllerData.horizontal != 0 || _controllerData.vertical != 0)
            {
                var moveForvard = _controllerData.vertical * _controllerData.Player.Parameters.speed * fixedDeltaTime * _controllerData.PlayerObject.transform.forward;
                var moveRight = _controllerData.horizontal * _controllerData.Player.Parameters.speed * fixedDeltaTime * _controllerData.PlayerObject.transform.right;
                _controllerData.direction = moveForvard + moveRight;
                _controllerData.Player.unitRigidBody.velocity = _controllerData.direction;
                _controllerData.Player.isStay = false;
            }
            else _controllerData.Player.isStay = true;

            if (!_controllerData.isCameraRotate)
            {
                var sensetivity = _controllerData.Player.Sensetivity;
                _controllerData.PlayerObject.transform.Rotate(0, _controllerData.mouseAxisX * sensetivity * fixedDeltaTime, 0);
            }
        }

        public void Dispose()
        {
            _controllerData.InputController.horizontalAxisOnChange -= HorizontalAxisOnChange;
            _controllerData.InputController.verticalAxisOnChange -= VerticalAxisOnChange;
            _controllerData.InputController.mouseXAxisOnChange -= MouseAxisXOnChange;
            _controllerData.InputController.isCameraRotateButtonDown -= CameraRotateButtonDown;
            _controllerData.InputController.isCameraRotateButtonUp -= CameraRotateButtonUp;
        }
    }
}
