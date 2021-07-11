using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private int damge = 2;
    private float speedKunai = 3f;
    private Rigidbody2D kunaiRigidbody;
    public AudioClip kunai;
    private AudioSource enemySource;
    // Start is called before the first frame update
    void Start()
    {
        kunaiRigidbody = GetComponent<Rigidbody2D>();
        enemySource = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*speedKunai*Time.deltaTime);
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine(DestroyKunai());
        }
        
    }


    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            enemySource.PlayOneShot(kunai);
            col.SendMessage("EnemyDamge", damge);
        }
        
        gameObject.SetActive(false);
    }

    IEnumerator DestroyKunai()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
