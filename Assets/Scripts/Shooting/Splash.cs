using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Transform visualTransform;
    [SerializeField] private MeshRenderer visualRenderer;

    private void Awake()
    {
        transform.localScale = Vector3.one * 0.3f;

        DoSplashHit();
        StartCoroutine(Grow());
    }

    private void DoSplashHit()
    {
        float radius = 0.3f;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            radius
        );

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Splurt"))
            {
                MakeSplurt.splurtCount--;
                if (MakeSplurt.splurtCount < 5)
                {
                    GameManager.Instance.Win();
                }
                Destroy(hit.gameObject);
            }
        }
    }

    private void Update()
    {
        if (ps == null) return;

        // Destroy once particles finish
        if (!ps.IsAlive(true))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Grow()
    {
        Vector3 start = visualTransform.localScale * 1.2f;
        Vector3 target = Vector3.one;

        float duration = 0.12f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            visualTransform.localScale = Vector3.Lerp(start, target, normalized);
            yield return null;
        }

        visualTransform.localScale = target;
        visualRenderer.enabled = false;
    }
}
