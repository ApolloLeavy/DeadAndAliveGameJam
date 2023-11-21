using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumCrystal : Item
{
    public new void pickup()
    {
        player.maxDecoherence += 2;
        player.decoherence += 2;
    }
}
