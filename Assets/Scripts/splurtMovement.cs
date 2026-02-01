using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class splurtMovement : MonoBehaviour
{

    public GameObject camera;
    public Vector2 sizeRange = new Vector2(.05f, .3f);
    public Vector3 launchMod = new Vector3(3, 7, 3);

    private string state = "INAIR"; 
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider sphereCol;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.useGravity = false;
        MakeSplurt.splurtCount++;
        launchSplurt();

        //StartCoroutine(SafeGaurdDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        if ((state == "WAIT") || (state == "INAIR")) transform.LookAt(camera.transform.position, -Vector3.up);

        if (transform.position.y < -10f)
        {
            Debug.Log("Destroyed safeguard splurt at y<0");
            Destroy(gameObject);
        }
    }

    public void launchSplurt()
    {
        state = "INAIR";
        rb.useGravity = true;
        //print("launch!");

        this.transform.localScale = Mathf.Pow(Random.Range(sizeRange[0], sizeRange[1]), 2) * new Vector3(1, 1, 1);
        rb.linearVelocity = Random.insideUnitSphere;
        rb.linearVelocity = new Vector3(launchMod[0] * rb.linearVelocity.x, launchMod[1] * Mathf.Abs(rb.linearVelocity.y), launchMod[2] * rb.linearVelocity.z);



    }


    void OnCollisionEnter(Collision collision) 
    {
        if (state != "INAIR") return;
        state = "LANDED";
        //print("landed!");
        rb.linearVelocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.useGravity = false;

        transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
        rb.isKinematic = true;
        sphereCol.enabled = false;

        transform.SetParent(collision.transform, true);
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Clean") {
            //print("cleaned!");
            MakeSplurt.splurtCount--;
            Destroy(gameObject);
    
        }
    }
}
