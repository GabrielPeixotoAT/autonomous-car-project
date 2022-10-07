using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    public void Continue()
    {
        GameManagerClass.isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(gameObject);
    }

    public void Settings()
    {
        Instantiate(settingsMenu, gameObject.transform.parent.transform);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
