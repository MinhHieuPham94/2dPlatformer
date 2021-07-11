using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().isOnGround)
        {
            GetComponent<Collider2D>().isTrigger = false;
        } else if(player.GetComponent<PlayerController>().isOnGround == false)
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        
    }
}
