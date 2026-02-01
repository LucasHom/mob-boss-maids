using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f; // seconds before bullet is destroyed

    [Header("Audio")]
    public AudioClip clip;
    private AudioSource source;

    [Header("Flash")]
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Recoil")]
    [SerializeField] private Transform visualsObject;
    [SerializeField] private float recoilForce = 2f;

    private Vector3 originalLocalPos;

    private void Start()
    {
        originalLocalPos = visualsObject.localPosition;

        source = GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        visualsObject.localPosition = Vector3.Lerp(
            visualsObject.localPosition,
            originalLocalPos,
            10f * Time.deltaTime
        );
    }

    public void FireBullet()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            muzzleFlash.Play();
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        if (rbBullet != null)
        {
            rbBullet.linearVelocity = firePoint.forward * bulletSpeed;
        }

        visualsObject.position -= visualsObject.right * recoilForce;

        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }

        //Destroy(bullet, bulletLifetime);
    }

}