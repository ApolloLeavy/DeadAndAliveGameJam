using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfJumposition : Item
{
    public new void pickup()
    {
        player.doubleJump = true;
        player.doubleJumps++;
    }
}
