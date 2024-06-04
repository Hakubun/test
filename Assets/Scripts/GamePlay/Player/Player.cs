using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;
    public FloatingJoystick joystick;
    [SerializeField] private PlayerStatus PlayerStatus;

    [Header("Status")]
    [SerializeField] public float currentHP; // Current player HP
    [SerializeField] public float maxHP; // Inital max HP of player
    [SerializeField] public float speed;
    [SerializeField] public float dmg;
    [SerializeField] private float armor;

    [Header("XP Related")]
    [SerializeField] private float currentExp = 0;
    [SerializeField] private float maxExp = 100;
    [SerializeField] private int lvl = 1;

    [Header("Attack")]
    public Transform target;
    public Transform firePoint;
    [SerializeField] private GameObject Bulletprefab;
    private GameObject nearestEnemy;
    public float range = 15f;
    public float fireCD = 3f;
    public float fireCount = 0f;

    [Header("Dash")]
    [SerializeField] public bool DashReady;
    [SerializeField] public float DashCD = 2f;
    [SerializeField] public float DashCDCurrent = 0.0f;
    [SerializeField] private float DashDistance;

    // Start is called before the first frame update
    public void Start()
    {
        //Setup player Status from save
        //HP dmg Armor
        //maxHP = await SaveSystem.LoadPlayerHP();
        maxHP = 100;
        //HP = maxHP;
        currentHP = maxHP;
        //dmg = await SaveSystem.LoadPlayerDmg();
        dmg = 5f;
        //Armor = await SaveSystem.LoadPlayerArmor();
        armor = 1f;

        speed = 10;

        //Attack Stepup
        InvokeRepeating("UpdateTarget", 0f, 1f);

        //Setup UI
        PlayerStatus.UpdateHP(currentHP, maxHP);
        PlayerStatus.UpdateEXP(currentExp, maxExp);

    }

    // Update is called once per frame
    public virtual void Update()
    {
        rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }

        //Attack logic
        if (fireCount >= fireCD && target != null)
        {
            launchBullet();
            fireCount = 0;
        }
        else
        {
            fireCount += 1 * Time.deltaTime;
        }


    }

    public void takeDamage(float _dmg)
    {
        float actualDamage = _dmg * (100f / (100f + armor));
        currentHP -= actualDamage;
        PlayerStatus.UpdateHP(currentHP, maxHP);

        VibrationManager.Vibrate();

        if (currentHP <= 0)
        {
            //gameover function called here
        }
    }

    public void getHeal(float _healamount)
    {
        currentHP += _healamount;
    }

    public void xpUp(int xps)
    {
        currentExp += xps;
        if (currentExp >= maxExp)
        {
            lvl += 1;
            maxExp += currentExp * 0.5f;
            currentExp = 0;
            //setup lvlup effect here
            if (fireCD > 0.5f)
            {
                fireCD -= 0.1f;
            }
        }
        PlayerStatus.UpdateEXP(currentExp, maxExp);
    }

    public int getLvl()
    {
        return lvl;
    }

    public void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance && enemy.GetComponent<Enemy>().getHP() > 0)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
                Debug.Log("Eenemy found");
            }
        }

        if (nearestEnemy != null && minDistance <= range)
        {
                target = nearestEnemy.transform;
            
        }else
        {
            target = null;
        }
    }

    public void launchBullet()
    {
        GameObject bulletGO = (GameObject)Instantiate(Bulletprefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.setUp(target, dmg);
        }

    }

    public void Dash()
    {
        Vector3 dashDirection;

        // Check if the joystick is pointing in a direction
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            // Joystick is pointing in a direction
            dashDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        }
        else
        {
            // Joystick is not pointing anywhere, dash forward
            Debug.Log("dash forward called");
            dashDirection = transform.forward;
        }

        // Calculate the dash destination point
        Vector3 dashDestination = transform.position + dashDirection * DashDistance;

        this.transform.position = dashDestination;

    }

    public void IncreaseRange()
    {
        range += 5;
    }

}
