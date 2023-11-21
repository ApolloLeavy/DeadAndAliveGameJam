using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlReliable : Item
{
    public new void pickup()
    {
        player.hp++;
    }
}
