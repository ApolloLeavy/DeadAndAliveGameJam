using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        sightRange = 25;
        closeRange = 20;
        jumpSpeed = new Vector2(20, 10);
        jumpTimer = 10;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        this.transform.LookAt(player.transform.position);
        Physics.Raycast(this.transform.position, this.transform.forward, out check);
        if (check.collider.tag == "Player" && check.distance <= sightRange)
        {
            myNav.SetDestination(player.transform.position);
            if (check.distance <= closeRange && canJumpDelay)
            {
                lastJump = true;
                StartCoroutine(JumpDelay());

            }
        }
        else
        {
            if (goal == 0)
                myNav.SetDestination(goal1);
            else
                myNav.SetDestination(goal2);
            if (myNav.remainingDistance == 0 && goal == 0)
            {
                goal += 1;
                myNav.SetDestination(goal2);
                myNav.isStopped = false;
            }
            else if (myNav.remainingDistance == 0 && goal == 1)
            {
                goal -= 1;
                myNav.SetDestination(goal1);
                myNav.isStopped = false;
            }
        }
    }
}
