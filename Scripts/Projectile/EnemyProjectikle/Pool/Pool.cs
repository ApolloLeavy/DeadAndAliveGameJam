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
        if (other.collider.GetComponent<Player>() && other.collider.GetComponent<Player>().hp <= 0)
        {
            other.gameObject.GetComponent<Player>().dieSound.Play();
            other.gameObject.GetComponent<Player>().myAnim.SetInteger("Anim", 1);
            Destroy(other.gameObject);

        }
   }

}
