using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidbody;
    private GameObject player;
    private GameObject EnemyAttackTrigger;
    public Slider enemySliderHealth;

    public float enemySpeed = 0.07f;
    public float enemyWakeUp, enemySleep;
    private float distance;
    private bool enemyFaceRight = true;
    public bool enemyAttack;
    public float xMax, xMin;
    public int maxEnemyHealth, curEnemyHealth;
    public bool dead = false;
    public float timeDown, timeToAttack;
    public int enemyDamge;
    public AudioClip deadSound;
    private AudioSource audioZombie;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAttack = false;
        curEnemyHealth = maxEnemyHealth;
        enemySliderHealth.maxValue = maxEnemyHealth;
        timeDown = timeToAttack;
        audioZombie = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        enemySliderHealth.value = curEnemyHealth;
        enemyAnimator.SetFloat("EnemySpeed",Mathf.Abs(enemyRigidbody.velocity.x));
        enemyAnimator.SetBool("EnemyAttack", enemyAttack);
        enemyAnimator.SetBool("EnemyDead", dead);
        distance = Vector3.Distance(transform.position, player.transform.position); 

        if(dead)
        {
            StartCoroutine(DelayDead());
        }

        TimeDown();

        Flip();
        Dead();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(distance <= enemyWakeUp  && distance >= enemySleep)
        {
            enemyRigidbody.velocity = new Vector2(player.transform.position.x - transform.position.x, 0).normalized * enemySpeed;
             if(Mathf.Abs(player.transform.position.x - transform.position.x) < enemySleep)
            {
                enemyRigidbody.velocity = new Vector2(0, 0);
            }
        }


        if(transform.position.x >= xMax)
        {
            transform.position = new Vector3 (xMax,transform.position.y, transform.position.z);
            enemyRigidbody.velocity = new Vector2(0, 0) * enemySpeed;
        } else if(transform.position.x <= xMin)
        {
            transform.position = new Vector3 (xMin,transform.position.y, transform.position.z);
            enemyRigidbody.velocity = new Vector2(0, 0) * enemySpeed;
        }

    }

    void Flip()
    {
        if(enemyRigidbody.velocity.x < -0.1f && enemyFaceRight)
        {
            enemyFaceRight = !enemyFaceRight;
            transform.localScale = new Vector3(-1, 1, 1);
        } else if(enemyRigidbody.velocity.x > 0 && !enemyFaceRight)
        {
            enemyFaceRight = !enemyFaceRight;
            transform.localScale = new Vector3(1,1,1);
        }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            DelayAttack();
            enemyRigidbody.velocity = new Vector2(0, 0);
            
        } 
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemyRigidbody.velocity = new Vector2(player.transform.position.x - transform.position.x, 0).normalized * enemySpeed;

    }

    void OnTriggerExit2D(Collider2D col)
    {
       
        if(col.CompareTag("Player"))
        {
            enemyAttack = false;
        } 
    }

    void Dead()
    {
        if(curEnemyHealth <=0 )
        {
            dead = true;
            audioZombie.PlayOneShot(deadSound);
            
        }
    }

    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void TimeDown()
    {
        if(timeDown > 0)
        {
            if(timeDown <= 1)
            {
                enemyAttack = false;
            }
            
            timeDown -= Time.deltaTime;
        }
    }

    void DelayAttack()
    {
        if(timeDown <= 0)
        {
            enemyAttack = true;
            player.GetComponent<PlayerHealth>().Damge(enemyDamge);
            timeDown = timeToAttack;
            
        }
    }

    public void EnemyDamge(int damge)
    {
        curEnemyHealth -= damge;
    }

    

    
}
