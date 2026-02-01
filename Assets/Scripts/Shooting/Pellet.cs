using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private GameObject splashPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Pellet collided with " + collision.gameObject.name);
        Instantiate(splashPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
