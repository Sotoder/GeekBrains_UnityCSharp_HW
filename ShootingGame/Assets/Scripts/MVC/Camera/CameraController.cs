using System;
using UnityEngine;

namespace Model.ShootingGame
{


    public class CameraController : IController, ILateExecute, IDisposable
    {

        private CameraControllerData _controllerData;

        public CameraController (IPlayer player, CameraInitializationData cameraInitializationData, InputController inputController)
        {
            var camera = Camera.main;

            _controllerData = new CameraControllerData(camera, player, cameraInitializationData, inputController);

            Initiation();
        }

        private void Initiation()
        {
            var camera = _controllerData.Camera;
            var target = _controllerData.Target;

            _controllerData.localPosition = target.InverseTransformPoint(camera.transform.position);
            _controllerData.camDistance = (camera.transform.position - target.position).magnitude;
            _controllerData.camBaseMask = camera.cullingMask;

            _controllerData.Player.TakeDamage += CameraShake;
            _controllerData.InputController.isCameraRotateButtonDown += CameraRotateButtonDown;
            _controllerData.InputController.isCameraRotateButtonUp += CameraRotateButtonUp;
            _controllerData.InputController.mouseXAxisOnChange += MouseAxisXOnChange;
        }

        private void MouseAxisXOnChange(float value)
        {
            _controllerData.mouseAxisX = value;
        }

        private void CameraRotateButtonDown(bool isCameraRotateButtonDown)
        {
            if (isCameraRotateButtonDown)
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

        public void LateExecute(float deltaTime)
        {
            var camera = _controllerData.Camera;
            var target = _controllerData.Target;

            if (_controllerData.Player.PlayerData.isStay && _controllerData.isCameraRotate)
            {
                CameraLook(deltaTime, camera, target);
            }
            else
            {
                CameraFolow(deltaTime, camera, target);
            }


        }

        private void CameraFolow(float deltaTime, Camera camera, Transform target)
        {

            if (_controllerData.Player.PlayerData.isStay && _controllerData.isCameraRotated)
            {
                CameraLerp(deltaTime, camera, target);
                EnvirontmentReact(camera, target);
                PlayerReact(camera, target);
            }
            else
            {
                camera.transform.position = target.TransformPoint(_controllerData.localPosition);
                camera.transform.LookAt(target);
                EnvirontmentReact(camera, target);
                PlayerReact(camera, target);
                _controllerData.localPosition = target.InverseTransformPoint(camera.transform.position);
            }
        }

        private void CameraLook(float deltaTime, Camera camera, Transform target)
        {

            if (_controllerData.mouseAxisX != 0)
            {
                _controllerData.stayTime = 0f;
                var lookSpeed = _controllerData.mouseAxisX * CameraControllerData.SPEED_X * deltaTime;
                camera.transform.RotateAround(target.position, camera.transform.up, lookSpeed);
                camera.transform.LookAt(target);
                _controllerData.isCameraRotated = true;
            }
            else
            {
                _controllerData.stayTime += deltaTime;

                if (_controllerData.stayTime >= CameraControllerData.MOTION_WAITING_TIME)
                {
                    CameraLerp(deltaTime, camera, target);
                }
            }
            EnvirontmentReact(camera, target);
            PlayerReact(camera, target);

        }

        private void CameraLerp(float deltaTime, Camera camera, Transform target)
        {
            
            camera.transform.position = Vector3.Lerp(camera.transform.position, target.TransformPoint(_controllerData.localPosition), 
                CameraControllerData.CAMERA_LERP_SPEED * deltaTime);
            camera.transform.LookAt(target);

            if (!_controllerData.isTimerWorking)
            {
                _controllerData.timer = new CountdownTimer(CameraControllerData.CAMERA_LERP_TIME);
                _controllerData.isTimerWorking = true;

                _controllerData.timer.timeIsOver += () =>
                {
                    _controllerData.stayTime = 0f;
                    _controllerData.isCameraRotated = false;
                    _controllerData.isTimerWorking = false;
                    _controllerData.timer.Dispose();
                };
            }

        }

        void EnvirontmentReact(Camera camera, Transform target)
        {
            var distance = Vector3.Distance(camera.transform.position, target.position);
            RaycastHit hit;

            if (Physics.Raycast(target.position, camera.transform.position - target.position, out hit, _controllerData.camDistance, _controllerData.Environtment))
            {
                camera.transform.position = hit.point;
            }
            else if (distance < _controllerData.camDistance && !Physics.Raycast(camera.transform.position, -camera.transform.forward, 0.1f, _controllerData.Environtment))
            {
                camera.transform.position -= camera.transform.forward * CameraControllerData.CAMERA_FOLLOW_SPEED;
            }
            else if (distance > _controllerData.camDistance + CameraControllerData.OFFSET)
            {
                camera.transform.position += camera.transform.forward * CameraControllerData.CAMERA_FOLLOW_SPEED;
            }
        }

        void PlayerReact(Camera camera, Transform target)
        {
            var distance = (camera.transform.position - target.position).magnitude;
            if (distance < CameraControllerData.HIDE_DISTANSE)
            {
                camera.cullingMask = _controllerData.NoPlayer;
            }
            else
            {
                camera.cullingMask = _controllerData.camBaseMask;
            }
        }

        private void CameraShake(int damage)
        {
            if (damage > _controllerData.Player.PlayerData.Parameters.currentHP * 0.5f)
            {
                Debug.Log("Shaking Camera Hard");
            }
            else
            {
                Debug.Log("Shaking Camera Easy");
            }

        }
        public void Dispose()
        {
            _controllerData.Player.TakeDamage -= CameraShake;
            _controllerData.InputController.isCameraRotateButtonDown -= CameraRotateButtonDown;
            _controllerData.InputController.isCameraRotateButtonUp -= CameraRotateButtonUp;
            _controllerData.InputController.mouseXAxisOnChange -= MouseAxisXOnChange;
        }
    }
}