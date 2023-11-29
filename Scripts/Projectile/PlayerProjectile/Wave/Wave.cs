using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : PlayerProjectile
{
    public Vector3 waveScale;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ttl = .67f;
        waveScale = new Vector3(1, 1, 0);
        StartCoroutine(TTL());
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        this.transform.localScale += waveScale;
    }
    public new void OnTriggerEnter(Collider other)
    {
        
        
    }
    
}
