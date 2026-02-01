using UnityEngine;
using UnityEngine.InputSystem;

public class splurtMovement : MonoBehaviour
{
    public GameObject camera;
    public Vector2 sizeRange = new Vector2(.05f, .3f);
    public Vector3 launchMod = new Vector3(3, 7, 3);

    private string state = "INAIR"; 
    private Rigidbody rb; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        launchSplurt();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((state == "WAIT") || (state == "INAIR")) transform.LookAt(camera.transform.position, -Vector3.up);


        // if ((Keyboard.current.spaceKey.isPressed) && (state == "WAIT")) {
        //     

        // }

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

    void OnCollisionEnter(Collision collision) 
    {
        state = "LANDED";
        //print("landed!");


        transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);

        //if (collision.gameObject.tag == "interactable")
        //{
        //    // Instead of parenting:
        //    FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        //    joint.connectedBody = collision.rigidbody; // attach to interactable
        //    joint.breakForce = Mathf.Infinity;
        //    joint.breakTorque = Mathf.Infinity;
        //}

        if (collision.gameObject.tag == "interactable")
        {
            transform.SetParent(collision.transform, true);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.useGravity = false;
        }

        




    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Clean") {
            print("cleaned!");
            Destroy(this.gameObject);
        }
    }
}
