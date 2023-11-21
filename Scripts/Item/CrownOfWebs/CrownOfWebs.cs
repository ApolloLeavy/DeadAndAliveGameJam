using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownOfWebs : Item
{
    public new void pickup()
    {
        if(player.qecd >= 3)
        player.qecd *= .666f;
        if (player.qeCost > 0)
            player.qeCost--;
        
    }
}
