using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    [SerializeField] private LayerMask _enemyLayerMask;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != _enemyLayerMask)
        {
            Debug.Log(other);
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
