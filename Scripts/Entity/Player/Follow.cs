using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Follow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public GameObject cam;
    public Vector2 camDelta;
    public Vector2 camRot;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camRot.y += camDelta.y;
        camRot.x += camDelta.x;
        if (camRot.y > 100)
            camRot.y = 100;
        if (camRot.y < -60)
            camRot.y = -60;
        this.transform.position = player.position + offset;
        this.transform.rotation = Quaternion.Euler(camRot.y,camRot.x,0);
         
    }
    
}
