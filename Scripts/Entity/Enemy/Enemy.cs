using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Entity
{
    public bool isWander;
    public float sightRange;
    public float closeRange;
    public GameObject poison;
    public GameObject Eyebeam;
    public GameObject player;
    public GameObject tangled;
    public RaycastHit check;
    public float spin;
    public bool isEntangled;
    public float entangleBounce;
    public float entangleDuration;

    public bool canJumpDelay;
    public float jumpTimer;
    public float shootTimer;
    public Transform eyebeamLoc;

    public NavMeshAgent myNav = null;
    public AudioSource dashSound;


    // Start is called before the first frame update
    new public void Start()
    {
        base.Start();
        spin = 0;
        isEntangled = false;
        canJumpDelay = true;
        entangleBounce = 10f;
        entangleDuration = 6f;
        
        player = GameObject.Find("Player");
        if(this.gameObject.GetComponent<NavMeshAgent>())
            myNav = this.gameObject.GetComponent<NavMeshAgent>();
        myNav.isStopped = false;
        
    }

    // Update is called once per frame
    new public void Update()
    {
       

        if (lastJump && canJumpDelay)
        {
           
            StartCoroutine(JumpDelay());
        }
        this.transform.LookAt(player.transform.position);
        Physics.Raycast(this.transform.position + this.transform.forward, this.transform.forward, out check);
        if ((this.transform.position - player.transform.position).magnitude <= sightRange)
        {

            myNav.SetDestination(player.transform.position);
            
        }

    }

    public IEnumerator JumpDelay()
    {
        myNav.speed *= 2;

        lastJump = false;
        canJumpDelay = false;
        myAnim.SetInteger("Anim", 2);
        dashSound.Play();
        yield return new WaitForSecondsRealtime(2);
        myNav.speed /= 2;
        myAnim.SetInteger("Anim", -1);
        yield return new WaitForSecondsRealtime(jumpTimer);
        canJumpDelay = true;
        
        myRig.velocity = Vector3.zero;
    }
    public IEnumerator ShootDelay()
    {
        attackSound.Play();
        
        yield return new WaitForSecondsRealtime(shootTimer);
        canAttack = true;
    }
    public void EyeBeamFire()
    {
        canAttack = false;
        GameObject tmp1 = GameObject.Instantiate(Eyebeam, eyebeamLoc.transform.position + this.transform.forward, this.transform.rotation, null);
        GameObject tmp2 = GameObject.Instantiate(Eyebeam, eyebeamLoc.transform.position + this.transform.forward + this.transform.forward, this.transform.rotation, null);
        GameObject tmp3 = GameObject.Instantiate(Eyebeam, eyebeamLoc.transform.position + this.transform.forward - this.transform.forward, this.transform.rotation, null);
        tmp1.transform.LookAt(worldPosition: player.transform.position + new Vector3(0, player.GetComponent<Player>().playerHeight,0));
        tmp2.transform.LookAt(worldPosition: player.transform.position + new Vector3(0, player.GetComponent<Player>().playerHeight, 0) + this.transform.forward * player.GetComponent<Player>().playerHeight);
        tmp3.transform.LookAt(worldPosition: player.transform.position + new Vector3(0, player.GetComponent<Player>().playerHeight, 0) - this.transform.forward * player.GetComponent<Player>().playerHeight);
 

        StartCoroutine(ShootDelay());
    }
    public void PoisonFire()
    {
        canAttack = false;
        GameObject tmp1 = GameObject.Instantiate(poison, this.transform.position + this.transform.up * 4, this.transform.rotation, null);
        tmp1.transform.LookAt(player.transform.position);
        tmp1.transform.Rotate(new Vector3(90, 0, 0));

        StartCoroutine(ShootDelay());
    }
    public void getEntangled()
    {
        RaycastHit[] checks = Physics.SphereCastAll(this.transform.position, entangleBounce, this.transform.up * -1);
        if (checks.Length != 0)
        {

            foreach (RaycastHit check in checks)
            {
                Debug.Log(check.collider.gameObject.name);
                if (check.collider.gameObject.CompareTag("Enemy") && check.collider.transform != this.transform)
                {
                    isEntangled = true;
                    tangled = check.collider.gameObject;
                    check.collider.gameObject.GetComponent<Enemy>().isEntangled = true;
                    check.collider.gameObject.GetComponent<Enemy>().tangled = this.gameObject;
                    
                    break;
                }

            }
        }
        StartCoroutine(GetEntangled());
    }
    IEnumerator GetEntangled()
    {
        if(isEntangled)
        {
            yield return new WaitForSecondsRealtime(entangleDuration);
            isEntangled = false;
            tangled.GetComponent<Enemy>().isEntangled = false;
            tangled.GetComponent<Enemy>().tangled = null;
            tangled = null;
        }
        
            
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Electron")
        {
            myAnim.SetInteger("Anim", 0);
            hitSound.Play();   
            spin += other.GetComponent<Electron>().player.GetComponent<Player>().spinAmount;
            hp -= 1;
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= 1;
        }
        if (other.gameObject.tag == "Wave" || other.gameObject.tag == "The Wave")
        {
            hitSound.Play();
            myAnim.SetInteger("Anim", 0);

            hp -= (1 + spin);
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= (1 + spin);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("World"))
        {
            myAnim.SetInteger("Anim", -1);
            myRig.velocity = Vector3.zero;

        }
    }
}
