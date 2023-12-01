using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumJetPack : Item
{
    public new void pickup()
    {
        player.GetComponent<Player>().qtRange += 5;
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
