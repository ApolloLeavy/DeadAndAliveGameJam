using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodBowl : Item
{

    public new void pickup()
    {
        player.GetComponent<Player>().hp = player.GetComponent<Player>().maxHp;
        player.GetComponent<Player>().decoherence = player.GetComponent<Player>().maxDecoherence  ;
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
