using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Transform visualTransform;
    [SerializeField] private MeshRenderer visualRenderer;

    [Header("Audio")]
    public AudioClip clip;
    private AudioSource source;
    private bool playingSound = false;


    private void Awake()
    {
        transform.localScale = Vector3.one * 0.3f;

        DoSplashHit();
        StartCoroutine(Grow());

        source = GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {

    }

    private void DoSplashHit()
    {
        float radius = 0.3f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Splurt"))
            {
                hit.gameObject.GetComponent<splurtMovement>().DestroySelf();

                
            }
        }

        if (MakeSplurt.splurtCount < 1)
        {
            GameManager.Instance.Win();
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

        if (clip != null && source != null && !playingSound)
        {
            source.PlayOneShot(clip);
            playingSound = true;
        }

        if (source == null)
        {
            playingSound = false;
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
