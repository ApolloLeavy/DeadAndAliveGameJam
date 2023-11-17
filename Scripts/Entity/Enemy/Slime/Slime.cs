using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sightRange = 15;
        closeRange = 5;
        speed = 7;
        jumpSpeed = new Vector2(15, 10);
    }

    // Update is called once per frame
   new  void Update()
    {
        base.Update();
    }
}
