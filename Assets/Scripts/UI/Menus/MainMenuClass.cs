using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuClass : MonoBehaviour
{
    public GameObject settingsMenu, creditsMenu;


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsMenu()
    {
        Instantiate(settingsMenu, gameObject.transform);
    }

    public void CreditsMenu()
    {
        Instantiate(creditsMenu, gameObject.transform);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
