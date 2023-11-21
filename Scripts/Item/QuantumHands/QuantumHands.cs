using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumHands : Item
{
    public new void pickup()
    {
        player.dcd = 0;
    }
}
