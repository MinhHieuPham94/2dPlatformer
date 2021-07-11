using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    private Rigidbody2D platFallRb;
    // Start is called before the first frame update
    void Start()
    {
        platFallRb = GetComponent<Rigidbody2D>();
        platFallRb.bodyType = RigidbodyType2D.Static;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine (Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(1);
        platFallRb.bodyType = RigidbodyType2D.Dynamic;
    }

}
