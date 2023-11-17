using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public bool isWander;
    public float sightRange;
    public float closeRange;
    public GameObject poison;
    public GameObject Eyebeam;
    public GameObject player;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    public float DistanceFromPlayer()
    {
        return 100.0f;
    }
    public void EyeBeamFire()
    {

    }
    public void GetEntangled()
    {
        this.isEntangled = true;
    }
}
