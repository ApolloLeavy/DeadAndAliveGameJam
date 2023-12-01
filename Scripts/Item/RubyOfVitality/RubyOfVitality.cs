using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyOfVitality : Item
{
    public new void pickup()
    {
        player.GetComponent<Player>().maxHp+=5;
        player.GetComponent<Player>().hp += 5;
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
