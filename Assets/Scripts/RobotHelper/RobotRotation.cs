using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotation : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    
    void Update()
    {
        transform.LookAt(_targetPosition.position);
    }
}
