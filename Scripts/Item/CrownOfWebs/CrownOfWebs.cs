using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownOfWebs : Item
{
    public new void pickup()
    {
        
        player.GetComponent<Player>().qecd *= .666f;
        if (player.GetComponent<Player>().qeCost > 0)
            player.GetComponent<Player>().qeCost--;
        
    }
    public new void OnCollisionEnter(Collision collision)
    {
        if (collision.collider && collision.collider.CompareTag("Player"))
        {
            this.pickup();
            Destroy(this.gameObject);
        }
    }
}
