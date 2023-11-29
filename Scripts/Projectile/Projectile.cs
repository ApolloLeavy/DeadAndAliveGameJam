using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody myRig;
    public float ttl;
    // Start is called before the first frame update
    public void Start()
    {

        myRig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {

    }
    public void OnCollisionEnter(Collision other)
    {   
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
    public IEnumerator TTL()
    {
        yield return new WaitForSecondsRealtime(ttl);
        Destroy(this.gameObject);
    }
}
