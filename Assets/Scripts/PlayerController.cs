using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private Collider2D playerAttackTrigger;
    private PoollerObject poollerObjectScr;
    public GameObject pointThrow;
    private PlayerHealth playerHealthScr;
    public AudioClip attackSound;
    private AudioSource soundSource;
    public AudioClip fireSound;
    public AudioClip jumpSound;

    public bool isOnGround = true, faceRight = true, glide = false;
    public bool attack = false;
    public bool playerThrow = false;
    public float horizontal, speed = 1, jumpForce, velocityYFall;
    public float timeDownToAttack = 0, timeToAttack = 0.2f;
    public float timeDownToThrow = 0, timeToThrow = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHealthScr = GetComponent<PlayerHealth>();
        playerAttackTrigger = GameObject.Find("AttackTrigger").GetComponent<Collider2D>();
        playerAttackTrigger.enabled = false;
        poollerObjectScr = GameObject.Find("PoollerObject").GetComponent<PoollerObject>();
        timeDownToAttack = timeToAttack;
        timeDownToThrow = timeToThrow;
        soundSource = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        
        
        if(playerThrow)
        {
            TimeDownToThrow();
        }

        if(attack)
        { 
            TimeDownToAttack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerAnimator.SetBool("IsOnGround", isOnGround);
        playerAnimator.SetBool("Attack", attack);
        playerAnimator.SetFloat("SpeedX", Mathf.Abs(playerRigidbody.velocity.x));
        playerAnimator.SetBool("Glide",glide);
        playerAnimator.SetBool("Throw",playerThrow);
       
        Move();

        Flip();

        Jump();

        Glide();

        Attack();

        Throw();
        
        
    }

    void Flip()
    {
        if(playerRigidbody.velocity.x >0 && !faceRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *=-1;
            transform.localScale = theScale;
            faceRight = !faceRight;
        }else if (playerRigidbody.velocity.x <0 && faceRight)
        {
           Vector3 theScale = transform.localScale;
            theScale.x *=-1;
            transform.localScale = theScale;
            faceRight = !faceRight;
        }
    }

    void Move()
    {
        if(!attack)
        {
            horizontal = Input.GetAxis("Horizontal");
            playerRigidbody.AddForce(Vector2.right * speed * horizontal, ForceMode2D.Impulse);

            Vector2 friction = playerRigidbody.velocity;
            friction.x *= 0.7f;
            playerRigidbody.velocity = friction;
        }
        
    }

    void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRigidbody.AddForce (Vector2.up*jumpForce, ForceMode2D.Impulse);
            soundSource.PlayOneShot(jumpSound);

        }

        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y*1.05f);
        }
    }

    void Glide()
    {
        
        if(Input.GetKey(KeyCode.Space) && playerRigidbody.velocity.y < velocityYFall)
        {
            glide = true;
        }

        if(glide)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y*0.7f);
        }

    }

    void Attack()
    {
        if(!attack)
        {
            if(Input.GetKey(KeyCode.Z) && !playerThrow && !glide)
            {
                attack = true;
                playerAttackTrigger.enabled = true;
                soundSource.PlayOneShot(attackSound);
                
            }
        }
       

    }

    void Throw()
    {
        if(playerHealthScr.curKunai >0 && !playerThrow)
        {
            if(Input.GetKey(KeyCode.X) && !attack  && !glide)
            {
                playerThrow = true;
                StartCoroutine(DelayFire());
                playerHealthScr.curKunai --;
                
            }
        }
    }


    void Fire()
    {
        
        if(faceRight)
        {
            GameObject kunai = PoollerObject.poollerObjectInstance.PoollerKunaiLeft();
            if(kunai != null)
            {
                kunai.SetActive(true);
            }
            kunai.transform.position = pointThrow.transform.position;
        }else if(!faceRight)
        {
            GameObject kunai = PoollerObject.poollerObjectInstance.PoollerKunaiRight();
            if(kunai != null)
            {
                kunai.SetActive(true);
            }
            kunai.transform.position = pointThrow.transform.position;
        } 
    }

    IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(0.6f);
        soundSource.PlayOneShot(fireSound);
        Fire();
    }

    void TimeDownToAttack()
    {
        timeDownToAttack -=Time.deltaTime;
        if(timeDownToAttack <=0)
        {
            attack = false;
            playerAttackTrigger.enabled = false;
            timeDownToAttack = timeToAttack;
        }
        
    }

    void TimeDownToThrow()
    {
        timeDownToThrow -=Time.deltaTime;
        if(timeDownToThrow <=0)
        {
            playerThrow = false;
            timeDownToThrow = timeToThrow;
        }
        
    }

}
