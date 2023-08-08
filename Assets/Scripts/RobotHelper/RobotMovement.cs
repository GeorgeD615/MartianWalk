using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public static RobotMovement Instanse;

    [SerializeField] private Transform _leftTargetPosition;
    [SerializeField] private float _velocity;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;


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
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            targetPosition = raycastHit.point;
        }
        else
        {
            targetPosition = ray.direction.normalized * 10000;
        }
        transform.LookAt(targetPosition);
    }


}
