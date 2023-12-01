using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity 
{
    public float playerHeight;
    public float decoherence;
    public float maxDecoherence;
    public float decoherenceGain;

    public bool canTunnel;

    public float qtRange;
    public float spRange;
    public float qeRange;

    public bool canSuper;

    public bool canAlignment;
    public bool isAlignment;

    public bool canEntangle;


    public bool canDuality;
    public bool isDuality; //false is particle, true is wave

    public float qtcd;
    public float spcd;
    public float aacd;
    public float qecd;
    public float dcd;
    public float attackcd;
    public float qeCost;

    public float spinAmount;


    public float dodgeChance;

    public float iframes;
    public bool isInvincible;

    public float electronRange;

    public GameObject electron;
    public GameObject wave;
    public Transform look;
    public Transform cam;
    public GameObject staff;
    public GameObject sword;
    public GameObject reticle;
    public GameManager gm;
    public GameObject bone;
    public AudioSource particleSound;
    public AudioSource waveSound;
    public AudioSource tunnelSound;
    public AudioSource entangleSound;
    public AudioSource alignmentSound;
    public AudioSource superPositionSound;
    public AudioSource dualitySound;
    public bool onDelay;


    // Start is called before the first frame update
    new void Start()
    {
        canJump = true;
        lastJump = false;
        canAttack = true;
        lastAttack = false;
        myRig = this.GetComponent<Rigidbody>();
        myAnim = this.GetComponentInChildren<Animator>();
        playerHeight = 4.75f;
        speed = 10.0f;
        hp = 20.0f;
        maxHp = 20;
        jumpSpeed = new Vector3(0, 10, 0);
        decoherence = 0;
        maxDecoherence = 6;
        decoherenceGain = 1;
        canTunnel = true;
        qtRange = 10;
        spRange = 20;
        qeRange = 20;
        canSuper = true;
        canAlignment = true;
        isAlignment = false;
        canEntangle = true;
        canDuality = true;
        isDuality = false;
        canAttack = true;
        lastAttack = false;
        qtcd = 3;
        spcd = 7;
        aacd = 10;
        qecd = 18;
        attackcd = .5f;
        dcd = 8;
        spinAmount = 1;
        qeCost = 3;
        dodgeChance = 0;
        iframes = .67f;
        isInvincible = false;
        electronRange = 30;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Decoherence());
        
    }

    // Update is called once per frame
    new void Update()
    {
        if (lastDirection == Vector2.zero && !onDelay)
           StartCoroutine(DelayIdle());
        myRig.velocity =  new Vector3(look.forward.x,0, look.forward.z).normalized * speed * lastDirection.y + new Vector3(0, myRig.velocity.y, 0);
        myRig.velocity += new Vector3(look.right.x, 0, look.right.z).normalized * speed * lastDirection.x;


            if (lastJump && canJump)
            {
            myAnim.SetInteger("Anim", 2);
            lastJump = false;
                canJump = false;
                StartCoroutine(Jump());
                
            }
            else if (!canJump && myRig.velocity.y <= 0)
            {

                RaycastHit[] checks = Physics.RaycastAll(this.myRig.position, this.transform.up * -1);
                if (checks != null)
                {
                    foreach (RaycastHit ready in checks)
                    {
                        if (ready.distance < 1f && (ready.collider.gameObject.CompareTag("World")))
                        {
                        canJump = true;
                        }

                    }
                }

            }

        
            Physics.Raycast(cam.transform.position + cam.transform.forward * .5f,cam.transform.forward, out RaycastHit check);

            if (check.distance <= qeRange && check.collider)
            {
                if (check.collider.gameObject.CompareTag("Enemy"))
                {
                    reticle.transform.localScale = new Vector3(2,2,2);
                }
                else
                    reticle.transform.localScale = new Vector3(1, 1, 1);

            }
            else
                reticle.transform.localScale = new Vector3(1, 1, 1);

            if(lastAttack && canAttack)
            {
            myAnim.SetInteger("Anim", 4);
            StartCoroutine(Attack());
            
            }

        bone.transform.localRotation.eulerAngles.Set(look.transform.rotation.eulerAngles.x+ transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, look.transform.rotation.eulerAngles.z+ transform.rotation.eulerAngles.z);
        

    }

    IEnumerator Jump()
    {
        yield return new WaitForSecondsRealtime(.667f);
        myAnim.SetInteger("Anim", 0);


        myRig.velocity += new Vector3(jumpSpeed.x, jumpSpeed.y, 0);
    }
    IEnumerator DelayIdle()
    {
        onDelay = true;

        yield return new WaitForSecondsRealtime(2);
        myAnim.SetInteger("Anim", 0);
        onDelay = false;

    }
    public void onMove(InputAction.CallbackContext ev)
    {
        if (ev.started)
        {
            myAnim.SetInteger("Anim", 3);
        }

        if (ev.performed)
            {
                lastDirection = ev.ReadValue<Vector2>();
            }
        if (ev.canceled)
        {
            lastDirection = Vector2.zero;

        }
    }
    public void Look(InputAction.CallbackContext ev)
    {
        if (ev.performed)
        {
            look.GetComponent<Follow>().camDelta = ev.ReadValue<Vector2>();
        }
        if(ev.canceled)
        {
            look.GetComponent<Follow>().camDelta = Vector2.zero;
        }
    }
    public void Fire(InputAction.CallbackContext ev)
    {
        if(ev.started)
        {
            lastAttack = true;

        }
        if(ev.performed)
        {
            

        }
        if (ev.canceled)
            lastAttack = false;
        
        
    }
    IEnumerator Attack()
    {
        
        canAttack = false;

        yield return new WaitForSecondsRealtime(.25f);
        myAnim.SetInteger("Anim", 0);
        if (isDuality == false)
        {
            GameObject tmp = GameObject.Instantiate(electron, staff.transform.position, cam.transform.rotation, null);
            tmp.transform.LookAt(this.cam.position + cam.transform.forward * electronRange);
            canAttack = false;
            particleSound.Play();

        }
        else if (isDuality == true)
        {
            
            GameObject tmp = GameObject.Instantiate(wave, sword.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)), null);
            canAttack = false;
            waveSound.Play();
           

        }
        yield return new WaitForSecondsRealtime(attackcd);
        
        canAttack = true;
    }

    public void Pause()
    {
        if (gm.pauseMenu.activeSelf == false)
        {
            gm.pauseMenu.SetActive(true);
            gm.HUD.SetActive(false);
            gm.reticle.SetActive(false);


            Time.timeScale = 0;
        }
        else
        {
            gm.pauseMenu.SetActive(false);
            Time.timeScale = 1;
            gm.HUD.SetActive(true);
            gm.reticle.SetActive(true);


        }

    }
    
    IEnumerator Decoherence()
    {
        yield return new WaitForSecondsRealtime(2);
        if (decoherence < maxDecoherence - decoherenceGain + 1)
            decoherence += decoherenceGain;
        else
            decoherence = maxDecoherence;
        StartCoroutine(Decoherence());
    }
    public void QuantumTunnel(InputAction.CallbackContext ev)
    {
        if(ev.started && canTunnel && decoherence >= 1)
        {
            Physics.Raycast(this.transform.position + new Vector3(0, playerHeight, 0) + look.transform.forward, look.transform.forward, out RaycastHit tunnelCheck, qtRange);
            if (tunnelCheck.collider)
            {
                tunnelSound.Play();
                canTunnel = false;
                decoherence -= 1;
                this.transform.position += new Vector3(look.transform.forward.x, 0, look.transform.forward.z) * tunnelCheck.distance * lastDirection.y;
                this.transform.position += new Vector3(look.transform.forward.x, 0, look.transform.forward.z) * tunnelCheck.distance * lastDirection.x;
                StartCoroutine(QTCD());
            }
            
        }   
    }
    IEnumerator QTCD()
    {

        yield return new WaitForSecondsRealtime(qtcd);
        canTunnel = true;

    }
    public void SuperPosition(InputAction.CallbackContext ev)
    {
        if (ev.started && canSuper && decoherence >= 2)
        {
            superPositionSound.Play();
            decoherence -= 2;
            canSuper = false;
            if (canJump)
            {
                Physics.Raycast(this.transform.position + new Vector3(0, playerHeight, 0), this.transform.up, out RaycastHit superCheck);

                if (superCheck.distance <= spRange)
                {
                    if (superCheck.collider.gameObject.CompareTag("Enemy") || superCheck.collider.gameObject.CompareTag("World"))

                    {
                        this.transform.position += new Vector3(0, superCheck.distance - playerHeight, 0);
                        
                    }
                }
                else
                    this.transform.position += new Vector3(0, spRange, 0);
            }
            else
            {
                Physics.Raycast(this.transform.position - new Vector3(0, -.25f, 0), this.transform.up * -1, out RaycastHit superCheck);

                if (superCheck.distance <= spRange)
                {
                    if (superCheck.collider.gameObject.CompareTag("Enemy") || superCheck.collider.gameObject.CompareTag("World"))

                    {
                        this.transform.position -= new Vector3(0, superCheck.distance, 0);
                        myRig.velocity = new Vector3(myRig.velocity.x, 0, myRig.velocity.z);
                        canJump = true;
                    }
                }
                else
                    this.transform.position -= new Vector3(0, spRange, 0);
            }
            StartCoroutine(SPCD());
        }
    }
    IEnumerator SPCD()
    {

        yield return new WaitForSecondsRealtime(spcd);
        canSuper = true;

    }
    public void AtomicAlignment(InputAction.CallbackContext ev)
    {
        if (ev.started && canAlignment && decoherence >= 3)
        {
            alignmentSound.Play();
            decoherence -= 3;
            isAlignment = true;
            canAlignment = false;
            StartCoroutine(AACD());
            
        }
    }
    IEnumerator AACD()
    {

        yield return new WaitForSecondsRealtime(aacd);
        isAlignment = false;
        canAlignment = true;
    }
    public void QuantumEntangle(InputAction.CallbackContext ev)
    {
        if (ev.started && canEntangle && decoherence >= qeCost)
        {
            entangleSound.Play();
            decoherence -= 3;
            canEntangle = false;
            Physics.Raycast(cam.transform.position + cam.transform.forward, cam.transform.forward, out RaycastHit check);
            
                if (check.distance <= qeRange)
                {
                    if (check.collider.gameObject.CompareTag("Enemy"))

                    {
                        check.collider.gameObject.GetComponentInParent<Enemy>().getEntangled();


                    }
                }
            StartCoroutine(QECD());
            
        }
    }
    IEnumerator QECD()
    {
        yield return new WaitForSecondsRealtime(qecd);
        canEntangle = true;
    }
    public void Duality(InputAction.CallbackContext ev)
    {
        if (ev.started && canDuality)
        {
            dualitySound.Play();
            canDuality = false;
            if (isDuality == true)
                isDuality = false;
            else
                isDuality = true;
            StartCoroutine(DCD());
            
        }
    }
    IEnumerator DCD()
    {
        if (dcd > 0)
            yield return new WaitForSecondsRealtime(dcd);
        canDuality = true;
    }
    public void Jump(InputAction.CallbackContext ev)
    {
       
        if (ev.started)
        {
            
            lastJump = true;
        }
    }
    IEnumerator Invincibility()
    {
        yield return new WaitForSecondsRealtime(iframes);
        isInvincible = false;
    }
   
    private void OnCollisionStay(Collision other)
    {
        if (!isAlignment && !isInvincible)
        {
            float rand = 100;
            if (dodgeChance > 0)
                rand = Time.realtimeSinceStartup % 100 + 1;
            if (rand > dodgeChance)
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hitSound.Play();
                    hp--;
                    isInvincible = true;
                    StartCoroutine(Invincibility());
                    if (hp <= 0)
                    {
                        StartCoroutine(Die());
                    }
                        
                        
                }
                if (other.gameObject.CompareTag("Eyebeam"))
                {
                    hitSound.Play();

                    if (decoherence > 0)
                        decoherence--;
                    hp--;
                    isInvincible = true;
                    StartCoroutine(Invincibility());
                    if (hp <= 0)
                    {
                        StartCoroutine(Die());
                    }
                }
                else if (other.gameObject.CompareTag("Poison"))
                {
                    hitSound.Play();

                    hp--;
                    isInvincible = true;
                    StartCoroutine(Invincibility());
                    if (hp <= 0)
                    {
                        StartCoroutine(Die());
                    }
                }
            }

        }
    }
    IEnumerator Die()
    {
        myAnim.SetInteger("Anim", 2);
        yield return new WaitForSecondsRealtime(1);
        gm.Lose();
        Time.timeScale = 0;
    }
}
