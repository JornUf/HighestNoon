using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float range = 100f;

    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Animator _animator;

    private float shootct = 0;

    void Update()
    {
        shootct += Time.deltaTime;
        if (Input.GetButton("Fire1") && shootct > shootDelay)
        {
            Shoot();
            shootct = 0;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        _animator.SetTrigger("Shoot");
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
