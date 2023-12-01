using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumHands : Item
{
    public new void pickup()
    {
        player.GetComponent<Player>().dcd = 0;
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
