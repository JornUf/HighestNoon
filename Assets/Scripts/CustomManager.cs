using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomManager : MonoBehaviour
{
    private bool foundsettings = false;
    private CustomSettings settings;
    [SerializeField] private Transform Star;
    [SerializeField] private NPCManager npcs;

    void Start()
    {
        settings = FindObjectOfType<CustomSettings>();
        if (settings != null)
        {
            foundsettings = true;
            LoadItAll();
        }
    }

    private void Update()
    {
        if (!foundsettings)
        {
            print("No Settings?");
            settings = FindObjectOfType<CustomSettings>();
            if (settings != null)
            {
                foundsettings = true;
                LoadItAll();
            }
        }
    }

    void LoadItAll()
    {
        print("Loading Settings...");
        Star.transform.position += new Vector3(0, settings.BadgeHeight, 0);
        npcs.EnemiesPerMinute = settings.EnemiesPerMinute;
        GameObject.Destroy(settings);
    }
}
