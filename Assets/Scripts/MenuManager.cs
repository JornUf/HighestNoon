using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject CustomScreen;
    public GameObject MenuScreen;

    [SerializeField] private CustomSettings settings;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ToCustomScreen()
    {
        CustomScreen.SetActive(true);
        MenuScreen.SetActive(false);
    }

    public void BackFromCustom()
    {
        CustomScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void LoadGame()
    {
        GameObject.Destroy(settings.gameObject);
        SceneManager.LoadScene("BasicScene");
    }

    public void LoadTutorial()
    {
        GameObject.Destroy(settings.gameObject);
        SceneManager.LoadScene("Tutorial Scene");
    }

    public void LoadCustomGame()
    {
        SceneManager.LoadScene("CustomScene");
    }
}
