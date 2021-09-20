using UnityEngine;
using PlayerInput.ShootingGame;
using Model.ShootingGame;
using System;

public class PlayerCamera : MonoBehaviour
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
    private bool _isPositionSaved;
    public bool IsPositionSaved { get => _isPositionSaved; set => _isPositionSaved = value; }

    private const float _hideDistance = 1f;
    private const float _camFolowSpeed = 0.05f;
    private const float _offset = 0.1f;

    private void Awake()
    {
        _localPosition = _target.InverseTransformPoint(transform.position);
        _camDistance = Vector3.Distance(transform.position, _target.position);
        _camBaseMask = _camera.cullingMask;
    }

    private void Start()
    {
        _player = _playerObject.GetComponent<Player>();
    }

    private void LateUpdate()
    {
        if (_player.IsStay)
        {
            CameraLook();
        } else
        {
            CameraFolow();
        }

    }

    private void CameraFolow()
    {
        transform.position = _target.TransformPoint(_localPosition);
        transform.LookAt(_target);
        EnvirontmentReact();
        PlayerReact();
        _localPosition = _target.InverseTransformPoint(transform.position);
    }

    private void CameraLook()
    {
        _mouseLookX = _player.CurrentInput.MouseLookX * _speedX * Time.deltaTime;
        transform.RotateAround(_target.position, transform.up, _mouseLookX);
        transform.LookAt(_target);
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
            transform.position -= transform.forward * _camFolowSpeed;
        }
        else if (distance > _camDistance + _offset)
        {
            transform.position += transform.forward * _camFolowSpeed;
        }
    }

    void PlayerReact()
    {
        var distance = Vector3.Distance(transform.position, _target.position);
        if (distance < _hideDistance)
            _camera.cullingMask = _noPlayer;
        else
            _camera.cullingMask = _camBaseMask;
    }
}
