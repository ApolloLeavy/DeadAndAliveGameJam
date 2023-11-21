using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : Item
{
    public new void pickup()
    {
        player.hp++;
    }
}
