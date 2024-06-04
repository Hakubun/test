using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Drag In Required")]
    public NavMeshAgent navMeshAgent; // EnemySelf NavMesh Reference
    public Transform AttackPoint;
    public GameObject AttackPrefab;
    [SerializeField] protected Animator Anim; // Animator component for animations
    public GameObject[] exps;

    [Header("Auto Setup, Debug purpose")]
    [SerializeField]
    private GameObject player; // Reference to Player
    [SerializeField]
    private Enemyspawner spawner; // Reference to Enemyspawner
    [SerializeField]
    private GameObject ExpContainer;
    public GameManager gm;

    [Header("Status")]
    [SerializeField]
    private float HP; // Inital HP of Enemy
    [SerializeField]
    private float currentHP; // Current Enemy HP
    [SerializeField]
    private float attackPower; // Enemy attack power
    [SerializeField]
    private float speed; // Speed of the enemy
    public int expWorth;

    private bool attack;
    public float attackCooldown = 2.0f;
    public float currentCooldown = 0.0f;
    public bool Dropped = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        navMeshAgent.speed = speed;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<Enemyspawner>();
        currentHP = HP;
        ExpContainer = GameObject.Find("ExpContainer");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (currentHP > 0)
        {
            //Set Enemy destination to player position
            navMeshAgent.SetDestination(player.transform.position);

            // Make the enemy face the player
            Vector3 lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
        }
        else
        {
            Anim.SetBool("death", true);
            navMeshAgent.isStopped = true;
        }
        
    }

    public virtual void FixedUpdate()
    {
        if (attack && currentCooldown <= 0.0f)
        {
            //Debug.Log("Attack!");

            Anim.SetTrigger("attack");

            currentCooldown = attackCooldown;
        }

        if (currentCooldown > 0.0f)
        {
            currentCooldown -= Time.fixedDeltaTime;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().takeDamage(1f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().takeDamage(1f);
        }
    }

    public void stopMovement()
    {
        //Debug.Log("stopMovement called");
        navMeshAgent.isStopped = true;
    }

    public void resumeMovment()
    {
        //Debug.Log("resumeMovment called");
        navMeshAgent.isStopped = false;
        
    }

    public void attackSetter(bool _states)
    {
        attack = _states;
    }

    public void Attack()
    {
        GameObject AttackGO = Instantiate(AttackPrefab, AttackPoint.position, Quaternion.identity);
        AttackGO.GetComponent<EnemyMeleeAttackLogic>().SetupDMG(attackPower);
    }

    public virtual void takeDamage(float dmg)
    {
        currentHP -= dmg;
        
        //Leave for death (play aniamtion)/drop xp/add count for kills/death pop up(play animaiton)/damage num(play animaiton)
        if (currentHP <= 0)
        {
            //Play Death Animation here
            //    Anim.SetBool("death", true);
            //    navMeshAgent.isStopped = true;
            // dropxp
            if (!Dropped)
            {
                DropXP();
                gm.addKill();
                Debug.Log("addkill called");
            }
            
        }

    }

    public virtual void DropXP()
    {
        GameObject exp = exps[Random.Range(0, exps.Length)];
        exp.GetComponent<XpDrop>().SetUpXp(expWorth);
        GameObject xp = Instantiate(exp, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        xp.transform.parent = ExpContainer.transform;
        Dropped = true;
    }

    public void DestroyObject ()
    {
        Destroy(gameObject);
        Debug.Log("destroy called");

    }

    public float getHP()
    {
        return HP;
    }
    
    public float getCurrentHP()
    {
        return currentHP;
    }

    public float getAttackPower()
    {
        return attackPower;
    }

}
