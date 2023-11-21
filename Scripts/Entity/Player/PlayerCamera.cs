using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Transform look;
    public Vector2 orientation;

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = player.position + new Vector3(0,7.5f,-7.5f);
        //this.transform.LookAt(look);
        transform.Rotate(player.position, orientation.x);
    }

    public void Look(InputAction.CallbackContext ev)
    {
        if (ev.performed)
        {
            orientation = ev.ReadValue<Vector2>();

        }
    }
}
