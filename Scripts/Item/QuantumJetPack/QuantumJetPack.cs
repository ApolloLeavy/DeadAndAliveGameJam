using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumJetPack : Item
{
    public new void pickup()
    {
        player.qtRange += 5;
    }
}
