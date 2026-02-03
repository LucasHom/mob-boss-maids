using UnityEngine;

public class BodyBagging : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MakeSplurt.splurtCount += 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) 
    {
        GameObject bag = GameObject.Find("Trashbag");
        if (collision.gameObject == bag) {
            SFXManager.Instance.PlaySFX("trash");
            MakeSplurt.splurtCount -= 500;
            Destroy(this.gameObject);
        }
    }
}
