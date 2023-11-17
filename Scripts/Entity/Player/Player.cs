using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity 
{
    public float decoherence;
    public float maxDecoherence;
    public Item[] items;
    public bool canTunnel;
    public float rangeQT;
    public bool canSuper;
    public bool canAlignment;
    public bool isAlignment;
    public bool canEntangle;
    public GameObject electron;
    public GameObject wave;
    public GameObject cam;
    public GameObject staff;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = 10.0f;
        hp = 20.0f;
        jumpSpeed = new Vector2(0, 10);
        decoherence = 6;
        canTunnel = true;

        canSuper = true;
        canAlignment = true;
        isAlignment = false;
        canEntangle = true;
    }

    // Update is called once per frame
    new void Update()
    {
        myRig.velocity +=  cam.transform.forward * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0);
        myRig.velocity += cam.transform.right * speed * lastDirection.x;
        base.Update();
    }
  
    public void onMove(InputAction.CallbackContext ev)
    {
        if (ev.started)
        {
            //myAnim.SetInteger("Action", 3);
        }

        if (ev.performed)
            {
                lastDirection = ev.ReadValue<Vector2>();
                //myAnim.SetInteger("Action", 3);
            }
        if (ev.canceled)
        {
            lastDirection = Vector2.zero;
            //myAnim.SetInteger("Action", 0);
        }
    }
    public void QuantumTunnel(InputAction.CallbackContext ev)
    {
        if(ev.started && canTunnel)
        {
            canTunnel = false;
            if (lastDirection != Vector2.zero)
                myRig.position += new Vector3(lastDirection.x, 0, lastDirection.y) * rangeQT;
            else
                myRig.position += new Vector3(lastDirection.x, 0, lastDirection.y) * rangeQT;
            StartCoroutine(QTCD(5));
   
        }
       
    }
    IEnumerator QTCD(float s)
    {

        yield return new WaitForSecondsRealtime(s);
        canTunnel = true;
    }
    public void SuperPosition(InputAction.CallbackContext ev)
    {
        if (ev.started && canSuper)
        {
            canSuper = false;

        }
    }
    IEnumerator SPCD(float s)
    {

        yield return new WaitForSecondsRealtime(s);
        canSuper = true;
    }
    public void AtomicAlignment(InputAction.CallbackContext ev)
    {
        if (ev.started && canAlignment)
        {
            isAlignment = true;
            canAlignment = false;
        }
    }
    IEnumerator AACD(float s)
    {

        yield return new WaitForSecondsRealtime(s);
        isAlignment = false;
        canAlignment = true;
    }
    public void QuantumEntangle(InputAction.CallbackContext ev)
    {
        if (ev.started && canEntangle)
        {
            canEntangle = false;
            RaycastHit check;
            Physics.Raycast(staff.transform.position, cam.transform.forward, out check);
            if (check.distance < 10f )
            {
                if (check.collider.gameObject.tag == "Enemy")
                    check.collider.gameObject.GetComponent<Enemy>().GetEntangled();
            }
             
            
        }
    }
    IEnumerator QECD(float s)
    {

        yield return new WaitForSecondsRealtime(s);
        canEntangle = true;
    }
}
