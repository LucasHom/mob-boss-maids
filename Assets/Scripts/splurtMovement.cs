using UnityEngine;
using UnityEngine.InputSystem;

public class splurtMovement : MonoBehaviour
{
    public GameObject camera;

    private string state = "WAIT"; 
    private Rigidbody rb; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((state == "WAIT") || (state == "INAIR")) transform.LookAt(camera.transform.position, -Vector3.up);

        if ((Keyboard.current.spaceKey.isPressed) && (state == "WAIT")) {
            launchSplurt();
        }
        
    }

    public void launchSplurt()
    {
        state = "INAIR";
        rb.useGravity = true;
        print("launch!");

        this.transform.localScale = Mathf.Pow(Random.Range(.05f, .5f), 2) * new Vector3(1, 1, 1);
        // rb.linearVelocity = .05f * Random.insideUnitSphere;
        // rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Abs(rb.linearVelocity.y), rb.linearVelocity.z);
    }
}
