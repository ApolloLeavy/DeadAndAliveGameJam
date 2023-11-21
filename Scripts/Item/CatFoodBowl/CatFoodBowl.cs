using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodBowl : Item
{

    public new void pickup()
    {
        player.hp = player.maxHp;
    }
}
