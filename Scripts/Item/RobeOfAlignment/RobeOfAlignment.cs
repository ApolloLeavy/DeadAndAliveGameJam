using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeOfAlignment : Item
{
    public new void pickup()
    {
        player.GetComponent<Player>().dodgeChance+=10;
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
