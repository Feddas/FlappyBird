using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeIncreaseScore : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var playerHealth = collider.gameObject.GetComponentInChildren<PlayerHealth>();
            if (playerHealth != null && playerHealth.IsAlive)
            {
                Score.instance.UpdateScore();
            }
        }
    }
}
