using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private int CoinAmount;
    [SerializeField] private Vector3 _rotation = new Vector3(0f, 100f, 0f);
    [SerializeField] AudioSource ItemCollectSound;

    private void FixedUpdate()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //playaudio here
            GameManager.Instance.addCoin(CoinAmount);
            Destroy(gameObject);
        }
    }


}
