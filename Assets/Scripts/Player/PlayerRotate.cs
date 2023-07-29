using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    static public PlayerRotate Instance;

    private float _xEuler;

    private PlayerInputActions _playerInputActions;

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    float _xAxisPrev = 0f;
    float _timeFromRotStarted = 0f;
    Quaternion _startRotation;
    private void Update()
    {
        float _xAxis = _playerInputActions.Player.Movement.ReadValue<Vector2>().x;

        if(_xAxis > 0)
        {
            if(_xAxisPrev <= 0)
            {
                _startRotation = transform.localRotation;
                _timeFromRotStarted = 0;
            }
            _xEuler = 0f;
        }else if(_xAxis < 0)
        {
            if (_xAxisPrev >= 0)
            {
                _startRotation = transform.localRotation;
                _timeFromRotStarted = 0;
            }
            _xEuler = 180f;
        }

        if(_xAxis != 0)
        {
            transform.localRotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(0, _xEuler, 0), _timeFromRotStarted);
            _timeFromRotStarted += Time.deltaTime;
            if (_timeFromRotStarted > 1f)
            {
                _timeFromRotStarted = 1f;
            }
            _xAxisPrev = _xAxis;
        }
    }
}
