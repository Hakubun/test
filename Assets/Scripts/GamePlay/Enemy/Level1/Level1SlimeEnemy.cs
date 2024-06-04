using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1SlimeEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //Debug.Log(navMeshAgent.velocity.magnitude);
        Anim.SetFloat("move", navMeshAgent.velocity.magnitude);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
