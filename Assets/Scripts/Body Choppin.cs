using UnityEngine;
using UnityEngine.InputSystem;

public class BodyChoppin : MonoBehaviour
{
    public GameObject sliced;
    public GameObject torso;
    public GameObject legs;

    public MakeSplurt splurter;

    [SerializeField] private int splurtPerBody = 1000;
    

    private string state = "HEALTHY";
    private bool choppable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (state == "HEALTHY")
        {
            MakeSplurt.splurtCount += splurtPerBody;
            Debug.Log(MakeSplurt.splurtCount);
        }

        splurter = GameObject.Find("GameManager").GetComponent<MakeSplurt>();
    
        splurter.beginSplurt(default(GameObject), 50, this.transform.position + new Vector3(0, 1f, 0), new Vector2(.02f, .2f), new Vector3(2, 3, 2));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void chopBody()
    {
        if (state == "HEALTHY")
        {
            GameObject newSliced = Instantiate(sliced, this.transform.position, this.transform.rotation);
            newSliced.GetComponent<BodyChoppin>().choppable = false;
            newSliced.GetComponent<BodyChoppin>().state = "CHOPPED";

            splurter.beginSplurt(default(GameObject), 15, this.transform.position + new Vector3(0, 1f, 0), new Vector2(.02f, .2f), new Vector3(2, 3, 2));

            Destroy(this.gameObject);
        }   
        
        else if (state == "CHOPPED")
        {

            Instantiate(torso, this.transform.position, this.transform.rotation);
            Instantiate(legs, this.transform.position, this.transform.rotation);
            
            splurter.beginSplurt(default(GameObject), 20, this.transform.position + new Vector3(0, 1f, 0), new Vector2(.02f, .2f), new Vector3(3, 5, 3));

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
