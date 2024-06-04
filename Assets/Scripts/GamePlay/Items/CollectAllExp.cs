using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAllExp : MonoBehaviour
{
    [SerializeField] private GameObject expContainer;
    [SerializeField] private Vector3 _rotation = new Vector3(0f, 100f, 0f);
    [SerializeField] AudioSource ItemCollectSound;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //play sound here
            expContainer = GameObject.FindWithTag("ExpContainer");
            foreach (Transform exp in expContainer.transform)
            {
                exp.GetComponent<XpBehavior>().collect();
            }
            Destroy(gameObject);
        }
    }
}
