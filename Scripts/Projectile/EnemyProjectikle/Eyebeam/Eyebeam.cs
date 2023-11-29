using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyebeam : EnemyProjectile
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        myRig = this.GetComponent<Rigidbody>();

        ttl = 5;
        speed = 8;
        myRig.velocity += this.transform.forward * speed;
        StartCoroutine(TTL());

    }

    // Update is called once per frame
    new void Update()
    {
        
    }
   
}
