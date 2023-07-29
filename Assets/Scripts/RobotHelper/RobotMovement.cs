using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public static RobotMovement Instanse;

    [SerializeField] private Transform _leftTargetPosition;
    [SerializeField] private Transform _rightTargetPosition;
    [SerializeField] private float _velocity;
    [SerializeField] private Transform _targetPosition;

    private Vector3 _currentTargetPosition;

    void Start()
    {
        if(Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _currentTargetPosition = _leftTargetPosition.position;
    }

    private Vector3 _prevRobotPosition;
    void Update()
    {

        if (PlayerRotate.Instance.transform.rotation.eulerAngles.y > 260)
        {
            _currentTargetPosition = _rightTargetPosition.position;
        }

        if(PlayerRotate.Instance.transform.rotation.eulerAngles.y < 100)
        {
            _currentTargetPosition = _leftTargetPosition.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTargetPosition, _velocity * Time.deltaTime);

        transform.LookAt(_targetPosition.position);
    }
}
