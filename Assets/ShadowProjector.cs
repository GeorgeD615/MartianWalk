using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowProjector : MonoBehaviour
{
    [SerializeField] private Transform _shadowTransform;
    [SerializeField] private LayerMask _groundLayer;
    private float _rayDistance = 100;


    private void Update()
    {
        Ray downRay = new Ray(transform.position, -transform.up);

        if(Physics.Raycast(downRay, out RaycastHit raycastHit, _rayDistance, _groundLayer.value))
        {
            _shadowTransform.position = new Vector3
                (transform.position.x, raycastHit.point.y + 0.05f, transform.position.z);
        }
    }
}
