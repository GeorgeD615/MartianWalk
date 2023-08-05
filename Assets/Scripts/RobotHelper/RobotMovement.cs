using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public static RobotMovement Instanse;

    [SerializeField] private Transform _leftTargetPosition;
    [SerializeField] private float _velocity;
    [SerializeField] private Transform _targetPosition;


    void Awake()
    {
        if(Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.LookAt(_targetPosition.position);
    }


}
