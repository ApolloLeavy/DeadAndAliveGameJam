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
            Destroy(other.gameObject);
        base.OnCollisionEnter(other);
    }
}
