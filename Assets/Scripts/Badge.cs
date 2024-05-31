using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Badge : MonoBehaviour
{
    [SerializeField] private float highesthigh = 1f;

    [SerializeField] private float lowestlow = 0;

    [SerializeField] private float hoverspeed = 0.1f;

    [SerializeField] private float rotatespeed = 1f;
    
    // Start is called before the first frame update
    private void Awake()
    {
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
            SceneManager.LoadScene("Win");
        }
    }

}
