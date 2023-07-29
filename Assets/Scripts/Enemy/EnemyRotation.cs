using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    
    private void Update()
    {
        if(PlayerMovement.Instance != null)
        {
            Vector3 fromEnemyToPlayer = (PlayerMovement.Instance.transform.position - transform.position).normalized;
            fromEnemyToPlayer.y = 0;
            transform.forward = fromEnemyToPlayer;
        }
    }
}
