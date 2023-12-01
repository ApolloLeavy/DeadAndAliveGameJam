using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    new public void OnCollisionEnter(Collision other)
    {

        if (other.collider.GetComponent<Player>() && other.collider.GetComponent<Player>().hp <= 0)
        {
            other.gameObject.GetComponent<Player>().dieSound.Play();
            other.gameObject.GetComponent<Player>().myAnim.SetInteger("Anim", 1);

            Destroy(other.gameObject);

        }
        base.OnCollisionEnter(other);
    }
}
