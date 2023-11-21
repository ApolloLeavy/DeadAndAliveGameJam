using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity 
{
    public float decoherence;
    public float maxDecoherence;
    public float decoherenceGain;
    public Item[] items;
    public bool canTunnel;
    public float qtRange;
    public bool canSuper;
    public bool canAlignment;
    public bool isAlignment;
    public bool canEntangle;
    public bool canDuality;
    public bool isDuality; //false is particle, true is wave
    public GameObject electron;
    public GameObject wave;
    public GameObject cam;
    public GameObject staff;
    public float qtcd;
    public float spcd;
    public float aacd;
    public float qecd;
    public float qeCost;
    public float dcd;
    public float spinAmount;
    public bool doubleJump;
    public int doubleJumps;
    public int canDoubleJump;
    public float dodgeChance;
    
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = 10.0f;
        hp = 20.0f;
        jumpSpeed = new Vector2(0, 10);
        decoherence = 6;
        maxDecoherence = 6;
        decoherenceGain = 1;
        canTunnel = true;
        qtRange = 10;
        canSuper = true;
        canAlignment = true;
        isAlignment = false;
        canEntangle = true;
        canDuality = true;
        isDuality = false;
        qtcd = 3;
        spcd = 7;
        aacd = 10;
        qecd = 18;
        dcd = 8;
        spinAmount = 1;
        doubleJump = false;
        doubleJumps = 0;
        canDoubleJump = 0;
        qeCost = 3;
        dodgeChance = 0;
        Decoherence();
    }

    // Update is called once per frame
    new void Update()
    {
        
        myRig.velocity =  cam.transform.up * -1 * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0);
        myRig.velocity += cam.transform.right * speed * lastDirection.x;

            if (lastDirection != Vector2.zero)
            {
                //myAnim.SetInteger("Action", 3);
            }
            if (lastJump && canJump)
            {
                myRig.velocity += new Vector3(jumpSpeed.x, jumpSpeed.y, 0);
                lastJump = false;
                canJump = false;
            }
            else if (!canJump && myRig.velocity.y <= 0)
            {

                RaycastHit[] checks = Physics.RaycastAll(this.myRig.position, this.transform.up * -1);
                if (checks != null)
                {
                    foreach (RaycastHit check in checks)
                    {
                        if (check.distance < .5f && (check.collider.gameObject.tag == "World"))
                            canJump = true;
                    if (doubleJump == true)
                        canDoubleJump = doubleJumps;
                    }
                }

            }
        
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
    public void Look(InputAction.CallbackContext ev)
    {
        if (ev.performed)
        {
            cam.GetComponent<Follow>().camDelta = ev.ReadValue<Vector2>();
        }
        if(ev.canceled)
        {
            cam.GetComponent<Follow>().camDelta = Vector2.zero;
        }
    }
    public new void Fire(InputAction.CallbackContext ev)
    {
        if(ev.started)
        {

        }
    }
    IEnumerator Decoherence()
    {

        yield return new WaitForSecondsRealtime(2);
        
        if (decoherence < maxDecoherence)
            decoherence+=decoherenceGain;
        Decoherence();
    }
    public void QuantumTunnel(InputAction.CallbackContext ev)
    {
        if(ev.started && canTunnel && decoherence >= 1)
        {
            decoherence -= 1;
            canTunnel = false;
            if (lastDirection != Vector2.zero)
                myRig.position += new Vector3(lastDirection.x, 0, lastDirection.y) * qtRange;
            else
                myRig.position += new Vector3(lastDirection.x, 0, lastDirection.y) * qtRange;
            StartCoroutine(QTCD());
            canTunnel = true;

        }
       
    }
    IEnumerator QTCD()
    {

        yield return new WaitForSecondsRealtime(qtcd);
        
    }
    public void SuperPosition(InputAction.CallbackContext ev)
    {
        if (ev.started && canSuper && decoherence >= 2)
        {
            decoherence -= 2;
            canSuper = false;
            StartCoroutine(SPCD());
            canSuper = true;
        }
    }
    IEnumerator SPCD()
    {

        yield return new WaitForSecondsRealtime(spcd);
        
    }
    public void AtomicAlignment(InputAction.CallbackContext ev)
    {
        if (ev.started && canAlignment && decoherence >= 3)
        {
            decoherence -= 3;
            isAlignment = true;
            canAlignment = false;
            StartCoroutine(AACD());
            isAlignment = false;
            canAlignment = true;
        }
    }
    IEnumerator AACD()
    {

        yield return new WaitForSecondsRealtime(aacd);
        
    }
    public void QuantumEntangle(InputAction.CallbackContext ev)
    {
        if (ev.started && canEntangle && decoherence >= qeCost)
        {
            decoherence -= 3;
            canEntangle = false;
            RaycastHit check;
            Physics.Raycast(staff.transform.position, cam.transform.forward, out check);
            if (check.distance < 10f )
            {
                if (check.collider.gameObject.tag == "Enemy")
                    check.collider.gameObject.GetComponentInParent<Enemy>().GetEntangled();
            }
            StartCoroutine(QECD());
            canEntangle = true;
        }
    }
    IEnumerator QECD()
    {
        if(qecd > 0)
        yield return new WaitForSecondsRealtime(qecd);
    }
    public void Duality(InputAction.CallbackContext ev)
    {
        if (ev.started && canDuality)
        {
            if (isDuality == true)
                isDuality = false;
            else
                isDuality = true;
            DCD();
            canDuality = true;
        }
    }
    IEnumerator DCD()
    {
        if (dcd > 0)
        yield return new WaitForSecondsRealtime(dcd);  
    }
    public new void Jump(InputAction.CallbackContext ev)
    {
        base.Jump(ev);
        if(ev.started && canDoubleJump > 0)
        {
            lastJump = true;
            canDoubleJump--;
        }
    }
    protected void OnTriggerEnter(Collider other)
    {
        float rand = 100;
        if (dodgeChance > 0)
            rand = Time.realtimeSinceStartup % 100 + 1;
        if (!isAlignment || rand < dodgeChance)
        {
            if (other.gameObject.tag == "Eyebeam")
            {
                    if (decoherence > 0)
                        decoherence--;
                    hp--;
            }
            else if (other.gameObject.tag == "Pool" || other.gameObject.tag == "Poison")
            {
                hp--;
            }
        }
            
            
    }
}
