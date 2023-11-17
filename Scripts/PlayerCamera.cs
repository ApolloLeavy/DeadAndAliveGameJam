using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector2 orientation;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = player.gameObject.GetComponent<Rigidbody>().velocity;
        
    }
   
    public void Look(InputAction.CallbackContext ev)
    {
        if(ev.performed)
        {
            orientation = ev.ReadValue<Vector2>();
        }
    }
}
