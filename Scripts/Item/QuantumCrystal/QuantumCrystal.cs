using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumCrystal : Item
{
    public new void pickup()
    {
        player.GetComponent<Player>().maxDecoherence += 2;
        player.GetComponent<Player>().decoherence += 2;
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
