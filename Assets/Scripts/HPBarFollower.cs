using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarFollower : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.forward);
    }
}
