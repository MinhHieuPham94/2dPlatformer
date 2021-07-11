using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    private int damge = 2;
    private PlayerController playerControllerScr;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScr = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            col.SendMessage("EnemyDamge", damge);

              
        }
    }
}
