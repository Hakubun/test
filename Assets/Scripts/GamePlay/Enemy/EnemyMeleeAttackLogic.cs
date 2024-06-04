using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMeleeAttackLogic : MonoBehaviour
{
    private float _dmg;
    public float delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", delay);
    }

    public void SetupDMG(float dmg)
    {
        
        _dmg = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<Player>().takeDamage(_dmg);
            Destroy(this.gameObject);

        }
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
