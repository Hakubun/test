using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player in the attack zone");
            
            this.transform.parent.gameObject.GetComponent<Enemy>().attackSetter(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player out of the attack zone");
            
            this.transform.parent.gameObject.GetComponent<Enemy>().attackSetter(false);
        }
    }
}
