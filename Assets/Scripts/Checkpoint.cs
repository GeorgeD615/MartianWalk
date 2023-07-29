using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Material _bloomLampMaterial;
    [SerializeField] private CapsuleCollider _lamp;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.LastCheckpoint = this;
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _lamp.GetComponent<MeshRenderer>().material = _bloomLampMaterial;
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}
