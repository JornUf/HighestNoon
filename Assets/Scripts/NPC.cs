using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    
    [SerializeField] private AudioSource deathsound;
    
    [SerializeField] private Transform forwardTarget;

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

    [SerializeField] private bool tutorialnpc = false;

    public NPCManager manager;

    private float walktimer = 0;


    private bool walking = true;

    private float attackTimer = 0;

    private bool alive = true;

    private float spawnInDelay = 0.1f;

    public bool seekTarget = false;

    private int bonusRange;

    void Start()
    {
        bonusRange = Random.Range(0, 30);
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
            if (transform.position.z + bonusRange > -95 && transform.position.z + bonusRange < 10)
            {
                seekTarget = true;
            }
            attackTimer += Time.deltaTime;
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
                        walktimer = walkdelay / 3;
                    else
                        walktimer = 0;
                }

                // Move our position a step closer to the target.
                var step = speed * Time.deltaTime; // calculate distance to move
                if (seekTarget)
                {            
                    transform.LookAt(target);
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, forwardTarget.position, step);
                }

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, target.position) <
                    10 + (towerScript.sizeIncrease/10 * towerScript.amountOfPieces))
                {
                    walking = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) <
                    10 + (towerScript.sizeIncrease/10 * towerScript.amountOfPieces))
                {
                    if (attackTimer >= attackcd)
                    {
                        animator.SetTrigger("Attack");
                        towerScript.EnemyAttack(2);
                        attackTimer = 0;
                    }
                }
                else
                {
                    walking = true;
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (alive)
        {
            if(!tutorialnpc)
                manager.RemoveNPC(this);
            collider.isTrigger = true;
            collider.enabled = false;
            animator.Play("death");
            if(deathsound != null)
                deathsound.Play();
            int rngcards = Random.Range(1, 4);
            if (tutorialnpc)
                rngcards = 2;
            for (int i = 0; i < rngcards; i++)
            {
                int rngX = Random.Range(1, 2);
                if (rngX == 2)
                {
                    rngX = -1;
                }
                int rngY = Random.Range(1, 2);
                if (rngY == 2)
                {
                    rngY = -1;
                }
                Instantiate(cardPickup, transform.position + new Vector3(rngX * i, 0, rngY * i), transform.rotation);
            }
            alive = false;
            StartCoroutine(despawn());
        }
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(10);
        GameObject.Destroy(this.gameObject);
    }
}
