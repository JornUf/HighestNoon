using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] private Animator gunAnimator;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private List<AudioClip> walkclips = new List<AudioClip>();
    [SerializeField] private AudioClip jumpsound;
    [SerializeField] private AudioClip landsound;
    [SerializeField] private AudioSource walking;
    [SerializeField] private float walkdelay = 0.5f;

    private Vector3 velocity;
    private bool isGrounded;
    private float walktimer = 0;
    private bool wasgrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && !wasgrounded)
        {
            walking.clip = landsound;
            walking.Play();
        }

        wasgrounded = isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if ((Mathf.Abs(x) > 0.5f || Mathf.Abs(z) > 0.5f) && isGrounded)
        {
            gunAnimator.SetBool("Walking", true);
            walktimer += Time.deltaTime;
            if (walktimer >= walkdelay)
            {
                int rng = Random.Range(0, walkclips.Count);
                walking.clip = walkclips[rng];
                walking.Play();
                if (Input.GetKey(KeyCode.LeftShift))
                    walktimer = walkdelay / 3;
                else
                    walktimer = 0;
            }
        }
        else
        {
            if (!isGrounded)
            {
                gunAnimator.SetBool("Walking", true);

            }
            else
            {
                gunAnimator.SetBool("Walking", false);
            }
        }

        float bonus = 1;

        if (Input.GetKey(KeyCode.LeftShift))
            bonus = 1.5f;

        gunAnimator.SetFloat("Speed", bonus);

        _controller.Move(move * speed * bonus * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            walking.clip = jumpsound;
            walking.Play();
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime);
    }
}
