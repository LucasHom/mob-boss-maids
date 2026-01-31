using UnityEngine;
using UnityEngine.InputSystem;

public class splurtMovement : MonoBehaviour
{
    public GameObject camera;
    public Vector2 sizeRange = new Vector2(.05f, .3f);
    public Vector3 launchMod = new Vector3(3, 7, 3);

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

        this.transform.localScale = Mathf.Pow(Random.Range(sizeRange[0], sizeRange[1]), 2) * new Vector3(1, 1, 1);
        rb.linearVelocity = Random.insideUnitSphere;
        rb.linearVelocity = new Vector3(launchMod[0]*rb.linearVelocity.x, launchMod[1]*Mathf.Abs(rb.linearVelocity.y), launchMod[2]*rb.linearVelocity.z);
    }

    void onCollisionEnter(Collision collisionInfo) 
    {
        state = "LANDED";
        print("landed!");
        rb.linearVelocity = new Vector3(0, 0, 0);
    }
}
