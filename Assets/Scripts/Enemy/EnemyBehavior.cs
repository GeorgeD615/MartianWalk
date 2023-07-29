using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform _rightTriggerBound;
    [SerializeField] private Transform _leftTriggerBound;
    [SerializeField] private GameObject _hpSlider;

    [SerializeField] private Animator _animator;

    enum State
    {
        IDLE,
        ANGRY
    }

    private State _currentState;


    private void Start()
    {
        _rightTriggerBound.transform.parent = null;
        _leftTriggerBound.transform.parent = null;
        _currentState = State.IDLE;
        _hpSlider.SetActive(false);
    }

    private void Update()
    {
        
        if(_currentState == State.IDLE)
        {
            if(PlayerMovement.Instance != null 
                && PlayerMovement.Instance.transform.position.x >= _leftTriggerBound.position.x
                && PlayerMovement.Instance.transform.position.x <= _rightTriggerBound.position.x)
            {
                _animator.SetTrigger("PlayerIN");
                _currentState = State.ANGRY;
                _hpSlider.SetActive(true);
            }

        }
        else
        {
            if (PlayerMovement.Instance == null
                || PlayerMovement.Instance.transform.position.x < _leftTriggerBound.position.x
                || PlayerMovement.Instance.transform.position.x > _rightTriggerBound.position.x)
            {
                _animator.SetTrigger("PlayerOUT");
                _currentState = State.IDLE;
            }
            
        }
    }







}
