using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float range = 100f;
    [SerializeField] private int bulletAmount = 6;

    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Animator _animator;

    [SerializeField] private List<Image> cards = new List<Image>();
    [SerializeField] private Sprite guncard;
    [SerializeField] private Sprite cardback;

    private float shootct = 0;
    private int currentBulletAmount = 6;

    private bool reloading = false;

    private void Start()
    {
        currentBulletAmount = bulletAmount;
    }

    void Update()
    {
        shootct += Time.deltaTime;
        if (Input.GetButton("Fire1") && shootct > shootDelay && currentBulletAmount > 0)
        {
            _animator.Play("Nothing");
            reloading = false;
            currentBulletAmount--;
            updateUI();
            Shoot();
            shootct = 0;
        }

        if (Input.GetKeyDown(KeyCode.R) || (Input.GetButton("Fire1") && currentBulletAmount <= 0 && !reloading))
        {
            _animator.SetTrigger("Reload");
            reloading = true;
            Invoke("bulletInsert", 1.8f);
        }
    }

    void bulletInsert()
    {
        if (reloading)
        {
            currentBulletAmount = bulletAmount;
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
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
