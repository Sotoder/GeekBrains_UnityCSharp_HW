using UnityEngine;
using System.Collections;
using Model.ShootingGame;
using System;

public class PlayerCamera : MonoBehaviour, IDisposable
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private float _speedX = 360f;
    [SerializeField] private LayerMask _noPlayer;
    [SerializeField] private LayerMask _environtment;

    private Player _player;
    private float _camDistance;
    private Vector3 _localPosition;
    private float _mouseLookX;
    private LayerMask _camBaseMask;
    private float _stayTime;
    private bool _isCameraRotated;

    private const float HIDE_DISTANSE = 1f;
    private const float CAMERA_FOLLOW_SPEED = 0.05f;
    private const float OFFSET = 0.1f;
    private const float MOTION_WAITING_TIME = 1f;
    private const float CAMERA_LERP_SPEED = 5f;

    private void Awake()
    {
        _localPosition = _target.InverseTransformPoint(transform.position);
        _camDistance = Vector3.Distance(transform.position, _target.position);
        _camBaseMask = _camera.cullingMask;
    }

    private void Start()
    {
        _player = _playerObject.GetComponent<Player>();
        _player.takeDamage += CameraShake;
    }

    private void LateUpdate()
    {
        if (_player.IsStay && _player.CurrentInput.IsCameraRotate)
        {
            CameraLook();
        } else
        {
            CameraFolow();
        }

    }

    private void CameraFolow()
    {
        if (_player.IsStay && _isCameraRotated)
        {
            CameraLerp();
            EnvirontmentReact();
            PlayerReact();

        }
        else
        {
            transform.position = _target.TransformPoint(_localPosition);
            transform.LookAt(_target);
            EnvirontmentReact();
            PlayerReact();
            _localPosition = _target.InverseTransformPoint(transform.position);
        }
    }

    private void CameraLook()
    {
        if(_player.CurrentInput.MouseLookX != 0)
        {
            _stayTime = 0f;
            _mouseLookX = _player.CurrentInput.MouseLookX * _speedX * Time.deltaTime;
            transform.RotateAround(_target.position, transform.up, _mouseLookX);
            transform.LookAt(_target);
            _isCameraRotated = true;
        } else
        {
            _stayTime += Time.deltaTime;

            if (_stayTime >= MOTION_WAITING_TIME)
            {
                CameraLerp();
            }
        }

    }

    private void CameraLerp()
    {
        transform.position = Vector3.Lerp(transform.position, _target.TransformPoint(_localPosition), CAMERA_LERP_SPEED * Time.deltaTime);
        transform.LookAt(_target);
        StartCoroutine(ChangeCameraRotatedStatus());
    }

    private void CameraShake(int damage)
    {
        if (damage > _player.Parameters.currentHP * 0.5f) 
        {
            Debug.Log("Shaking Camera Hard");
        } else
        {
            Debug.Log("Shaking Camera Easy");
        }
        
    }

    private IEnumerator ChangeCameraRotatedStatus()
    {
        float CoroutineWaitTime = 1.5f;
        float timeOut = 0;

        while (timeOut != CoroutineWaitTime)
        {
            timeOut+=0.5f;
            if (timeOut == CoroutineWaitTime)
            {
                _stayTime = 0f;
                _isCameraRotated = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void EnvirontmentReact()
    {
        var distance = Vector3.Distance(transform.position, _target.position);
        RaycastHit hit;

        if (Physics.Raycast(_target.position, transform.position - _target.position, out hit, _camDistance, _environtment))
        {
            transform.position = hit.point;
        }
        else if (distance < _camDistance && !Physics.Raycast(transform.position, -transform.forward, 0.1f, _environtment))
        {
            transform.position -= transform.forward * CAMERA_FOLLOW_SPEED;
        }
        else if (distance > _camDistance + OFFSET)
        {
            transform.position += transform.forward * CAMERA_FOLLOW_SPEED;
        }
    }

    void PlayerReact()
    {
        var distance = Vector3.Distance(transform.position, _target.position);
        if (distance < HIDE_DISTANSE)
            _camera.cullingMask = _noPlayer;
        else
            _camera.cullingMask = _camBaseMask;
    }

    public void Dispose()
    {
        _player.takeDamage -= CameraShake;
    }
}
