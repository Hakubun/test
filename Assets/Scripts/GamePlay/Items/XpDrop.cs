using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpDrop : MonoBehaviour
{
    [SerializeField] private int xpAmount;
    [SerializeField] private int multiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().xpUp(xpAmount);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void SetUpXp(int _xpAmount)
    {
        xpAmount = _xpAmount;
        xpAmount *= multiplier;
    }
}
