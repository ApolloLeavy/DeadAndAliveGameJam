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
    public AudioSource dieSound;
    public AudioSource attackSound;


    // Start is called before the first frame update
    public void Start()
    {
        canJump = true;
        lastJump = false;
        canAttack = true;
        lastAttack = false;
        myRig = this.GetComponent<Rigidbody>();
        myAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(lastDirection == Vector2.zero)
        {
            myAnim.SetInteger("Anim", 0);
        }


    }

}
