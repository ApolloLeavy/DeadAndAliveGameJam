using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeOfAlignment : Item
{
    public new void pickup()
    {
        player.dodgeChance+=10;
    }
}
