using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : Enemy
{
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sightRange = 30;
        closeRange = 5;
        hp = 10;
        maxHp = 10;
        jumpSpeed = new Vector3(0, 0, 10);
        jumpTimer = 5;
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        Physics.Raycast(this.transform.position + (3*this.transform.forward), this.transform.forward, out check);
        if (check.distance <= closeRange && check.collider && canJumpDelay)
            {
                lastJump = true;
            }
        else
            lastJump = false;


    }
    
}
