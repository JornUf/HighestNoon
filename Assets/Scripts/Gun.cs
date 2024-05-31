using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float shootDelay = 0.1f;
    private float range = 1000f;
    [SerializeField] private int bulletAmount = 6;

    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Animator _animator;

    [SerializeField] private List<Image> cards = new List<Image>();
    [SerializeField] private Sprite guncard;
    [SerializeField] private Sprite cardback;

    [SerializeField] private AudioSource shootsound;

    [SerializeField] private ResourceManager resourceManager;

    [SerializeField] private VisualBullet bullet;

    [SerializeField] private Transform barrel;

    [SerializeField] private bool tutorial = false;

    private float shootct = 0;
    private int currentBulletAmount = 6;

    private bool reloading = false;

    private void Start()
    {
        currentBulletAmount = bulletAmount;
        if (tutorial)
        {
            currentBulletAmount = 0;
            updateUI();
        }
    }

    void Update()
    {
        shootct += Time.deltaTime;
        if (Input.GetButton("Fire1") && shootct > shootDelay && currentBulletAmount > 0)
        {
            _animator.Play("Nothing");
            shootsound.Play();
            reloading = false;
            currentBulletAmount--;
            updateUI();
            Shoot();
            shootct = 0;
        }

        if ((Input.GetButton("Reload") && !reloading || (Input.GetButton("Fire1") && currentBulletAmount <= 0 && !reloading)) && resourceManager.AmountOfResources > 0)
        {
            _animator.SetTrigger("Reload");
            reloading = true;
            Invoke("bulletInsert", 1.6f);
        }
    }

    void bulletInsert()
    {
        if (reloading)
        {
            currentBulletAmount += resourceManager.reload(bulletAmount - currentBulletAmount);
            updateUI();
            reloading = false;
        }
    }

    void updateUI()
    {
        for (int i = 0; i < currentBulletAmount; i++)
        {
            cards[i].sprite = cardback;
        }

        for (int j = currentBulletAmount; j < bulletAmount; j++)
        {
            cards[j].sprite = guncard;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        _animator.SetTrigger("Shoot");
        RaycastHit hit;
        VisualBullet vb = Instantiate(bullet);
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            vb.MakeShootLine(hit.point, barrel.position);
            if (hit.transform.parent != null)
            {
                if (hit.transform.parent.gameObject.GetComponent<TowerScript>())
                {
                    hit.transform.parent.gameObject.GetComponent<TowerScript>().PlayerShotAt(damage);
                }
            }

            if (hit.transform.gameObject.GetComponent<NPC>())
            {
                hit.transform.gameObject.GetComponent<NPC>().TakeDamage();
            }
        }
        else
        {
            Ray r = new Ray(_camera.transform.position, _camera.transform.forward);
            vb.MakeShootLine(r.GetPoint(range), barrel.position);
        }
    }
}
