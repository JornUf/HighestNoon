using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisualBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private TrailRenderer trail;
    
    private Vector3 target;

    private bool going = false;


    public void MakeShootLine(Vector3 trgt, Vector3 strtpos)
    {
        transform.position = strtpos;
        target = trgt;
        going = true;
        trail.enabled = true;
    }
    
    void Update()
    {
        if (going)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                going = false;
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
