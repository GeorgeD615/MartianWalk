using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    static public PlayerMovement Instance;

    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _maxMoveSpeed = 5;
    [SerializeField] private float _jumpForce = 14;
    [SerializeField] private float _friction = 0.3f;

    //[SerializeField] private AudioSource jumpAudio;
    //[SerializeField] private AudioSource attackAudio;
    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private AudioClip _walkSoundClip;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerCamera;

    private PlayerInputActions _playerInputActions;

    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    private float _jumpFramesTimer;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerCamera.SetActive(true);
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Jump.performed += JumpButtonPressed;
    }





    private bool _grounded;
    private float _xAxisInput;
    private bool _jumpButtonPressed;

    private void Update()
    {
        _jumpFramesTimer += Time.deltaTime;

        _xAxisInput = _playerInputActions.Player.Movement.ReadValue<Vector2>().x;

        if (_grounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (_jumpButtonPressed)
        {
            _jumpBufferCounter = _jumpBufferTime;
            _jumpButtonPressed = false;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if(_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _jumpFramesTimer > 0.4f)
        {
            _jumpFramesTimer = 0f;
            _jumpBufferCounter = 0f;
            Jump();
        }
    }


    private void JumpButtonPressed(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
            _jumpButtonPressed = true;
    }
    private void Jump()
    {
        //jumpAudio.Play();
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(0, _jumpForce, 0, ForceMode.VelocityChange);
    }






    private bool _stepSoundPlay = false;
    private void FixedUpdate()
    {
        float speedMultiplierInAir = 1f;
        float input = _playerInputActions.Player.Movement.ReadValue<Vector2>().x;

        if (_grounded)
        {
            _rigidbody.AddForce(input * _moveSpeed, 0, 0, ForceMode.VelocityChange);
            if (input != 0 && !_stepSoundPlay)
            {
                _walkSound.Play();
                Debug.Log("play");
                _stepSoundPlay = true;
            }
            else if (input == 0)
            {
                _walkSound.Pause();
                _stepSoundPlay = false;
            }
        }
        else
        {
            _walkSound.Pause();
            _stepSoundPlay = false;
            speedMultiplierInAir = 0.5f;
            if (_rigidbody.velocity.x > _maxMoveSpeed && input > 0)
            {
                speedMultiplierInAir = 0;
            }
            if (_rigidbody.velocity.x < -_maxMoveSpeed && input < 0)
            {
                speedMultiplierInAir = 0;
            }
            _rigidbody.AddForce(input * _moveSpeed * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
        }

        _rigidbody.AddForce(-_rigidbody.velocity.x * _friction * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
        _animator.SetFloat("XSpeed", Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat("YSpeed", _rigidbody.velocity.y);
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; ++i)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);

            if (angle < 45f)
            {
                _grounded = true;
                _animator.SetBool("Grounded", true);
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
        _animator.SetBool("Grounded", false);
    }

}
