using UnityEngine;
using UnityEngine.InputSystem;

public class BodyChoppin : MonoBehaviour
{
    public GameObject sliced;
    public GameObject torso;
    public GameObject legs;

    public MakeSplurt splurter;
    

    private string state = "HEALTHY";
    private bool choppable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MakeSplurt.splurtCount += 100;
        splurter = GameObject.Find("GameManager").GetComponent<MakeSplurt>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame) {
        //     chopBody();
        // }
    }

    public void chopBody()
    {
        if (state == "HEALTHY")
        {
            GameObject newSliced = Instantiate(sliced, this.transform.position, this.transform.rotation);
            newSliced.GetComponent<BodyChoppin>().choppable = false;
            newSliced.GetComponent<BodyChoppin>().state = "CHOPPED";

            splurter.beginSplurt(default(GameObject), 10, this.transform.position + new Vector3(0, 1f, 0), new Vector2(.02f, .2f), new Vector3(2, 3, 2));
            print(this.transform.position + new Vector3(0, 1f, 0));

            MakeSplurt.splurtCount -= 100;

            Destroy(this.gameObject);
        }   
        
        else if (state == "CHOPPED")
        {

            Instantiate(torso, this.transform.position, this.transform.rotation);
            Instantiate(legs, this.transform.position, this.transform.rotation);
            
            splurter.beginSplurt(default(GameObject), 15, this.transform.position + new Vector3(0, 1f, 0), new Vector2(.02f, .2f), new Vector3(3, 5, 3));
            print(this.transform.position + new Vector3(0, 1f, 0));

            MakeSplurt.splurtCount -= 100;

            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter(Collider collision) 
    {
        GameObject axe = GameObject.Find("Axe");
        if (collision.gameObject == axe && choppable) {
            chopBody();
            choppable = false;
        }
    }

    void OnTriggerExit(Collider collision) 
    {
        GameObject axe = GameObject.Find("Axe");
        if (collision.gameObject == axe) {
            choppable = true;
        }
    }
}
