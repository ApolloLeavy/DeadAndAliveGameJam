using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    public float speed;
    public float hp;
    public bool canJump;
    public bool lastJump;
    public Vector2 jumpSpeed;
    public bool canFire;
    public bool lastFire;
    public bool isEntangled;
    public Vector2 lastDirection;
    public Rigidbody myRig;
    public Animator myAnim;

    // Start is called before the first frame update
    public void Start()
    {
        //speed
        //hp
        canJump = true;
        lastJump = false;
        //jumpSpeed
        canFire = true;
        lastFire = false;
        isEntangled = false;
        myRig = this.GetComponent<Rigidbody>();
        myAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
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
                }
            }

        }
    }
   
    public void Fire(InputAction.CallbackContext ev)
    {
        if (ev.started)
            lastFire = true;
    }

    public void Jump(InputAction.CallbackContext ev)
    {
        if(ev.started && canJump == true)
        lastJump = true;
    }
}
