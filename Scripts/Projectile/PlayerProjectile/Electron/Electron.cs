using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : PlayerProjectile
{
    public GameObject player;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = 20;
        ttl = 2;
        StartCoroutine(TTL());
        myRig.velocity = this.transform.forward * speed;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    new void Update()
    {
        
    }
    new public void OnTriggerEnter(Collider other)
    {
        
        base.OnTriggerEnter(other);
        
    }
}
