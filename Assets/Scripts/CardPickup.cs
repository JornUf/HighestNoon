using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    [SerializeField] private float highesthigh = 1f;

    [SerializeField] private float lowestlow = 0;

    [SerializeField] private float hoverspeed = 0.1f;

    [SerializeField] private float rotatespeed = 1f;

    [SerializeField] private int resourcegiven = 1;

    private ResourceManager resourceManager;

    // Start is called before the first frame update
    private void Awake()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotatespeed, 0));
        if (transform.position.y <= highesthigh && transform.position.y >= lowestlow)
        {
            transform.position += new Vector3(0, hoverspeed, 0);
        }
        else
        {
            hoverspeed *= -1;
            transform.position += new Vector3(0, hoverspeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            resourceManager.AddResources(resourcegiven);
            Destroy(this.gameObject);
        }
    }
}
