using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth, maxKunai;
    public int curHealth, curKunai, score;

    public GameObject playerHealth;
    public GameObject playerKunai;
    public GameObject scoreText;
    public PauseManager pauseManager;
    public AudioClip itemSound;
    public AudioClip playerDead;
    private AudioSource playerSource;
    public float xMin, xMax;
    public Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth.GetComponent<Slider>().maxValue = maxHealth;
        playerSource = GetComponent<AudioSource>();
        transform.position = startPosition;
        if(PlayerPrefs.HasKey("CurHealth"))
        {
            curHealth = PlayerPrefs.GetInt("CurHealth");
        } else{
            curHealth = maxHealth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.GetComponent<Slider>().value = curHealth;  
        playerKunai.GetComponent<Text>().text = "x " + curKunai;
        scoreText.GetComponent<Text>().text = "Score: " + score;

        Limit();

        Dead();
    }

    void Limit()
    {
        if(transform.position.x <= xMin)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        if(transform.position.x >= xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }
    }

    public void Damge(int enemyDamge)
    {
        curHealth -= enemyDamge;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("KunaiItem"))
        {
            playerSource.PlayOneShot(itemSound);
            curKunai++;
            if(curKunai >= maxKunai)
            {
                curKunai = maxKunai;
            }
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("Kunai", curKunai);
        }

        if(col.CompareTag("HealthItem"))
        {
            playerSource.PlayOneShot(itemSound);
            curHealth ++;
            if(curHealth >=maxHealth)
            {
                curHealth = maxHealth;
            }
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("Health", curHealth);
        }

        if(col.CompareTag("Coin"))
        {
            playerSource.PlayOneShot(itemSound);
            score++;
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("Score",score);
        }

        if(col.CompareTag("Sign"))
        {
            score = PlayerPrefs.GetInt("Score");
            curKunai = PlayerPrefs.GetInt("CurKunai");
            
           
        }

        if(col.CompareTag("SignArrow"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("CurKunai", curKunai);
            PlayerPrefs.SetInt("CurHealth", curHealth);
        }
        
    }

    void Dead()
    {
        if(curHealth <= 0 || gameObject.transform.position.y <= -7)
        {
            curHealth = 0;
            playerSource.PlayOneShot(playerDead);
            pauseManager.Dead();
        }
    }
}
