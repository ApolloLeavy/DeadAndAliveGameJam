using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        myAnim = this.GetComponentInChildren<Animator>();
        sightRange = 30;
        closeRange = 30;
        jumpSpeed = new Vector3(0, 0, 20);
        jumpTimer = 10;
        shootTimer = 10;
        hp = 30;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        Physics.Raycast(this.transform.position + this.transform.forward, this.transform.forward, out check);
        if ((transform.position - player.transform.position).magnitude <= closeRange && canJumpDelay)
        {
            lastJump = true;
        }
        else
            lastJump = false;
        if((transform.position - player.transform.position).magnitude <= sightRange && canAttack)
        {
            PoisonFire();
        }
    }
}
