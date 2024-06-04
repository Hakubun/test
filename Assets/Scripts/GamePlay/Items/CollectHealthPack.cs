using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealthPack : MonoBehaviour
{
    [SerializeField] private float HealAmount;
    [SerializeField] private Vector3 _rotation = new Vector3(0f, 100f, 0f);
    [SerializeField] AudioSource ItemCollectSound;
    // Start is called before the first frame update
    private void Start()
    {
        HealAmount = 10;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Play Audio here
            other.GetComponent<Player>().getHeal(HealAmount);
            Destroy(gameObject);
        }
    }
}
