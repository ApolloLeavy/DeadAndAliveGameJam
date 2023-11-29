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
    public GameObject Goal1;
    public GameObject Goal2;
    public Vector3 goal1;
    public Vector3 goal2;
    public bool canJumpDelay;
    public float jumpTimer;
    public float shootTimer;
    public int goal = 1;
    public Transform eyebeamLoc;

    public NavMeshAgent myNav = null;
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
        myNav = this.gameObject.GetComponent<NavMeshAgent>();
        goal1 = Goal1.transform.position;
        goal2 = Goal2.transform.position;
        myNav.SetDestination(goal2);
        myNav.isStopped = false;
    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();

        if (lastJump && canJumpDelay)
        {
            myRig.velocity = new Vector3(this.transform.forward.x * jumpSpeed.x, this.transform.forward.y * jumpSpeed.y, this.transform.forward.z * jumpSpeed.z);
            lastJump = false;
            canJumpDelay = false;
            StartCoroutine(JumpDelay());
        }
        this.transform.LookAt(player.transform.position);
        Physics.Raycast(this.transform.position + this.transform.forward, this.transform.forward, out check);
        if (check.distance <= sightRange && check.collider)
        {

            myNav.SetDestination(player.transform.position);
            
        }
        else
        {
            
            if (goal == 0)
                myNav.SetDestination(goal1);
            else
                myNav.SetDestination(goal2);
            if (myNav.remainingDistance == 0 && goal == 0)
            {
                goal += 1;
                myNav.SetDestination(goal2);
                myNav.isStopped = false;
            }
            else if (myNav.remainingDistance == 0 && goal == 1)
            {
                goal -= 1;
                myNav.SetDestination(goal1);
                myNav.isStopped = false;
            }
        }

    }

    public IEnumerator JumpDelay()
    {
       
        yield return new WaitForSecondsRealtime(jumpTimer);
        canJumpDelay = true;
        myRig.velocity = Vector3.zero;
    }
    public IEnumerator ShootDelay()
    {
        
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
            spin += other.GetComponent<Electron>().player.GetComponent<Player>().spinAmount;
            hp -= 1;
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= 1;
        }
        if (other.gameObject.tag == "Wave" || other.gameObject.tag == "The Wave")
        {
            hp -= (1 + spin);
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= (1 + spin);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("World"))
            myRig.velocity = Vector3.zero;
    }
}
