using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownOfWebs : Item
{
    public new void pickup()
    {
        
        player.qecd *= .666f;
        if (player.qeCost > 0)
            player.qeCost--;
        
    }
}
