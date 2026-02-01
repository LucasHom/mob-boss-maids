using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private void Awake()
    {
    }

    private void Update()
    {
        if (ps == null) return;

        // Check if the system has finished playing
        if (!ps.IsAlive(true)) // true = include child particles
        {
            Destroy(gameObject);
        }
    }
}
