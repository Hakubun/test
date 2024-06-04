using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Status")]
    public Transform target;
    public float speed = 20f;
    public float dmg;

    public void setUp(Transform _target, float _dmg)
    {
        target = _target;
        dmg = _dmg;
    }

    // Update is called once per frame
    public void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget ()
    {
        if (target.CompareTag("Enemy"))
        {
            Enemy _en = target.GetComponent<Enemy>();
            _en.takeDamage(dmg);
        }

        Destroy (gameObject);

    }

}
