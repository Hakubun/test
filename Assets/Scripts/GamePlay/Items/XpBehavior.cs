using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation = new Vector3 (0f, 100f, 0f);
    [SerializeField] private bool collected = false;
    [SerializeField] private float _speed;
    [SerializeField] AudioSource ItemCollectSound;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    private void Update()
    {
        if (collected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //play audio here
            collect();
        }
    }

    public void collect()
    {
        collected = true;
    }

}
