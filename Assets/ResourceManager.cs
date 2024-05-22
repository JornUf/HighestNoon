using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int AmountOfResources = 5;
    // Start is called before the first frame update

    private void Start()
    {
        updateUI();
    }

    public int reload(int maxBullets)
    {
        if (maxBullets <= AmountOfResources)
        {
            AmountOfResources -= maxBullets;
            updateUI();
            return maxBullets;
        }
        else
        {
            int temp = AmountOfResources;
            AmountOfResources = 0;
            updateUI();
            return temp;
        }
    }

    void updateUI()
    {
        text.text = AmountOfResources.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AmountOfResources++;
            updateUI();
        }
    }
}
