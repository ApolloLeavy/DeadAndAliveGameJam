using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyOfVitality : Item
{
    public new void pickup()
    {
        player.maxHp+=5;
    }
}
