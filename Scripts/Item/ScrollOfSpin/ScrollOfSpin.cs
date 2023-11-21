using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollOfSpin : Item
{
    public new void pickup()
    {
        player.spinAmount += 1;
    }
}
