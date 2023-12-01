using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
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
    new public void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Enemy>() && other.GetComponent<Enemy>().hp <= 0)
        {
            other.gameObject.GetComponent<Enemy>().dieSound.Play();
            other.gameObject.GetComponent<Enemy>().myAnim.SetInteger("Anim", 1);
            Destroy(other.gameObject);

        }
        base.OnTriggerEnter(other);
    }
}
