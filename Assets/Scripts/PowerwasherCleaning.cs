using UnityEngine;

public class PowerwasherCleaning : MonoBehaviour
{  

    public float maxDistance = 20f;
    public float beamThickness = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 origin = this.transform.position;
        Vector3 direction = this.transform.TransformDirection(Vector3.up);

        powerwasherOn(origin, direction);
    }

    public void powerwasherOn(Vector3 origin, Vector3 direction) 
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance, LayerMask.GetMask("Default", "Slurt"))) {
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // sphere.transform.position = hit.point;
            // print("Did Hit");
            GameObject target = hit.transform.gameObject;

            CapsuleCollider[] beams = this.GetComponentsInChildren<CapsuleCollider>();


            foreach (CapsuleCollider beam in beams) 
            {
                if (beam.gameObject == this.gameObject) continue;
                beam.height = hit.distance*2;
                beam.transform.localPosition = new Vector3(0, hit.distance - 1.25f, 0);
                beam.radius = beamThickness;
                //beam.transform.position = new Vector3(0, hit.distance/2, 0);
            }
        }
        else {
            Debug.DrawRay(origin, direction * maxDistance, Color.blue);
            // print("Did not Hit");
        }
    }

}

