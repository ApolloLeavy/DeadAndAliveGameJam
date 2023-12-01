using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball : Enemy
{
    

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
        sightRange = 60;
        closeRange = 10;
        shootTimer = 5;
        hp = 5;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        this.transform.Rotate(new Vector3(0, 90, 0));

        if (check.distance <= sightRange && canAttack)
        {
            EyeBeamFire();
        }
        if (check.distance <= closeRange)
        {
            myNav.isStopped = true;
            myNav.SetDestination(this.transform.position - new Vector3(-1, 0, 0));
            myNav.isStopped = false;
        }
        
        
    }
}
