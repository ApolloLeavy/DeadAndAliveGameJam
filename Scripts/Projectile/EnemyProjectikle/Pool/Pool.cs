using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : EnemyProjectile
{
    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        myRig = this.GetComponent<Rigidbody>();

        ttl = 10;
        speed = 0.01f;
        transform.Rotate(new Vector3(-90, 0, 0));

        myRig.velocity += this.transform.up * speed;
        StartCoroutine(TTL());
    }

    // Update is called once per frame
    new public void Update()
    {
        
    }
    new public void OnCollisionEnter(Collision other)
    {
        return;
    }

}
