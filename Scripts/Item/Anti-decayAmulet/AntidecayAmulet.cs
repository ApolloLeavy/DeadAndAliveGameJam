using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidecayAmulet : Item
{
    // Start is called before the first frame update
    public new void pickup()
    {
        player.GetComponent<Player>().decoherenceGain++;
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
