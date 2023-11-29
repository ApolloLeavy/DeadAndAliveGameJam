using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Follow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Vector3 wallOffset;
    public GameObject cam;
    public Vector2 camDelta;
    public Vector2 camRot;
    public Rigidbody myRig;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 0.1f;
        wallOffset = Vector3.zero;
        myRig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        camRot.y -= camDelta.y * sensitivity;
        camRot.x += camDelta.x * sensitivity;
        if (camRot.y > 100)
            camRot.y = 100;
        if (camRot.y < -60)
            camRot.y = -60;
        transform.position = player.position + offset + wallOffset;
        transform.rotation = Quaternion.Euler(camRot.y,camRot.x,0);
         
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
}
