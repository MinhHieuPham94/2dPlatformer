using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnGroud : MonoBehaviour
{
    private PlayerController playerControllerScr;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScr = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if(col.isTrigger == false)
        {
            playerControllerScr.isOnGround = true;
            playerControllerScr.glide = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerControllerScr.isOnGround = false;
    }
}
