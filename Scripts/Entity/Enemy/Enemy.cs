using System.Collections;
using System.Collections.Generic;
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
    public int spin;
    public bool isEntangled;
    public float entangleBounce;
    public float entangleDuration;
    public GameObject Goal1;
    public GameObject Goal2;
    public Vector3 goal1;
    public Vector3 goal2;
    public bool canJumpDelay;
    public float jumpTimer;
    public int goal = 1;

    public NavMeshAgent myNav = null;
    // Start is called before the first frame update
    new void Start()
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
    new void Update()
    {
        base.Update();
        this.transform.LookAt(player.transform.position);
        Physics.Raycast(this.transform.position, this.transform.forward, out check);
        if (check.collider.CompareTag("Player") && check.distance <= sightRange)
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
        canJumpDelay = false;
        yield return new WaitForSecondsRealtime(jumpTimer);
        canJumpDelay = true;
    }
    public float DistanceFromPlayer()
    {
        return 100.0f;
    }
    public void EyeBeamFire()
    {

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
        if(other.gameObject.tag == "electron")
        {
            spin += other.GetComponent<Electron>().spin;
            hp -= 1;
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= 1;
        }
        if (other.gameObject.tag == "Wave" || other.gameObject.tag == "Sword")
        {
            hp -= (1 + spin);
            if (tangled)
                tangled.GetComponent<Enemy>().hp -= (1 + spin);
        }
        
    }
}
