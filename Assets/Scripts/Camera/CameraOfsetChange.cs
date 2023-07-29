using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOfsetChange : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCameraFollowPlayer;
    [SerializeField] private Rigidbody _playerRigidbody;
    private CinemachineTransposer _trans;
    [SerializeField] private float _changeOffsetSpeed = 1f;

    void Start()
    {
        _trans = _virtualCameraFollowPlayer.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Update()
    {
        if(_playerRigidbody.velocity.x > 0.2 && _trans.m_FollowOffset.z > -4)
        {
            _trans.m_FollowOffset.z -= _changeOffsetSpeed * Time.deltaTime * _playerRigidbody.velocity.x;
        }

        if(_playerRigidbody.velocity.x < -0.2 && _trans.m_FollowOffset.z < 4)
        {
            _trans.m_FollowOffset.z -= _changeOffsetSpeed * Time.deltaTime * _playerRigidbody.velocity.x;
        }
    }
}
