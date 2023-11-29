using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    public float speed;
    public float hp;
    public float maxHp;
    public bool canJump;
    public bool lastJump;
    public Vector3 jumpSpeed;
    public bool canAttack;
    public bool lastAttack;
    
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
        canAttack = true;
        lastAttack = false;
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
        
        
    }

}
