using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : EnemyProjectile
{
    public GameObject pool;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        myRig = this.GetComponent<Rigidbody>();

        ttl = 5;
        speed = 5;
        myRig.velocity += this.transform.up * speed;
        StartCoroutine(TTL());
    }

    // Update is called once per frame
    new void Update()
    {
        
    }
    new public void OnCollisionEnter(Collision other)
    {
        GameObject tmp1 = GameObject.Instantiate(pool, this.transform.position + (new Vector3(0,-.25f,0)), Quaternion.identity, null);
        base.OnCollisionEnter(other);
    }
}
