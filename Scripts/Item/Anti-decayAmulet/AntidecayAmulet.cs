using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidecayAmulet : Item
{
    // Start is called before the first frame update
    public new void pickup()
    {
        player.decoherenceGain++;
    }
}
