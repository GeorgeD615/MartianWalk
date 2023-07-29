using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    [SerializeField] private Transform _stoneSpawnPoint;
    [SerializeField] private float _stoneSpeed;
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private Transform _playerHead;

    private Rock _currentRock;
    private bool _picked = false;


    public void PickUpRock()
    {
        _currentRock = Instantiate(_rockPrefab, _stoneSpawnPoint.position, Quaternion.identity);
        _currentRock.GetComponent<SphereCollider>().enabled = false;
        _picked = true;

    }

    public void Update()
    {
        if (_picked)
        {
            _currentRock.transform.position = _stoneSpawnPoint.position;
        }
    }

    public void ThrowStone()
    {
        _picked = false;
        _currentRock.GetComponent<SphereCollider>().enabled = true;
        _currentRock.GetComponent<Rigidbody>().velocity = (_playerHead.position - _stoneSpawnPoint.position).normalized * _stoneSpeed;
    }
}
