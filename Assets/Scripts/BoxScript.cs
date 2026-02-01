using UnityEngine;

public class BoxScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        string objectName = other.gameObject.name;
        // Check the tag, name, or component of the other object to verify it's the right one
        if (objectName == "Page")
        {
            GameManager.Instance.SceneLoadCondition = true;
        }
    }
}
