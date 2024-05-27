using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float attackcd = 2f;
    
    private Transform target;

    private TowerScript towerScript;

    [SerializeField] private GameObject cardPickup;
    [SerializeField] private Collider collider;
    
    [SerializeField] private List<AudioClip> walkclips = new List<AudioClip>();
    [SerializeField] private AudioSource walkingAudioSource;
    [SerializeField] private float walkdelay = 0.5f;

    public NPCManager manager;
    
    private float walktimer = 0;


    private bool walking = true;

    private float attackTimer = 0;

    private bool alive = true;

    private float spawnInDelay = 0.1f;


    void Start()
    {

    }
    
    void Update()
    {
        if (target == null || towerScript == null)
        {
            if (manager != null)
            {
                target = manager.target;
                towerScript = manager.towerScript;
            }

            return;
        }
        else if (alive)
        {
            attackTimer += Time.deltaTime;
            transform.LookAt(target);
            animator.SetBool("Walking", walking);
            if (walking)
            {
                walktimer += Time.deltaTime;
                if (walktimer >= walkdelay)
                {
                    int rng = Random.Range(0, walkclips.Count);
                    walkingAudioSource.clip = walkclips[rng];
                    walkingAudioSource.Play();
                    if (Input.GetKey(KeyCode.LeftShift))
                        walktimer = walkdelay /3;
                    else
                        walktimer = 0;
                }
                
                // Move our position a step closer to the target.
                var step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, target.position) < 10 + (towerScript.sizeIncrease * towerScript.amountOfPieces))
                {
                    walking = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) < 10 + (towerScript.sizeIncrease * towerScript.amountOfPieces))
                {
                    if (attackTimer >= attackcd)
                    {
                        animator.SetTrigger("Attack");
                        attackTimer = 0;
                    }
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (alive)
        {
            manager.RemoveNPC(this);
            collider.isTrigger = true;
            collider.enabled = false;
            animator.Play("death");
            Instantiate(cardPickup, transform.position, transform.rotation);
            alive = false;
        }
    }
}
